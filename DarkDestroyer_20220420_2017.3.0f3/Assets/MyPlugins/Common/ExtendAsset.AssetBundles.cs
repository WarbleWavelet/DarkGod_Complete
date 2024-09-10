/****************************************************
    文件：ExtendAssets.AssetBundles.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/15 16:57:6
	功能：
*****************************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using static ExtendIO;
using static System.Net.WebRequestMethods;
using Random = UnityEngine.Random;





public static partial class ExtendAssetBundles  //加密
{

    #region Mgr
    public class EncryptorMgr
    {
        IAESEncryptor encryptor;
        public EncryptorMgr(IAESEncryptor encryptor)
        {
             this.encryptor = encryptor;

        }
        public  void EncryptAB(string path, string privateKey)
        {
            path.GetDirPath();
            FileInfo[] fis = new tPath(path).FindFileInfos();
            for (int i = 0; i < fis.Length; i++)
            {
                if (!fis[i].Name.EndsWith(Affixes.Meta) && !fis[i].Name.EndsWith(Affixes.PointManifest))
                {
                    encryptor.EncryptConfig(fis[i].FullName, privateKey);
                }
            }
            Debug.LogFormat("加密完成！\n{0}", path);
        }
    }

    #endregion


    #region 接口
    /// <summary>加密器</summary>
    public interface IEncryptor
    {

        /// <summary>加密头</summary>
        string EncryptHead { get; set; }
    }


    static string EncryptConfig= Application.dataPath + "/xxx.xml";


    public interface IAESEncryptor : IEncryptor
    {
        /// <summary><see cref=EncryptConfig"></summary>
        void EncryptConfig(string path, string EncrptyKey);
        /// <summary><see cref=EncryptConfig"></summary>
        void DecryptConfig(string path, string EncrptyKey);
        /// <summary><see cref=EncryptConfig"></summary>
        byte[] DecryptConfigBytes(string path, string EncrptyKey);
        //
        string Encrypt(string EncryptString, string EncryptKey);
        byte[] Encrypt(byte[] EncryptByte, string EncryptKey);
        string Decrypt(string DecryptString, string DecryptKey);
        byte[] Decrypt(byte[] DecryptByte, string DecryptKey);

    }
    #endregion


    #region 实现类
    public class AESEncryptor : IAESEncryptor
    {
        private static string m_Head = "AESEncrypt";

     public   string EncryptHead { get { return m_Head; } set { } }

        #region 加密



        /// <summary>
        /// 文件加密，传入文件路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="EncrptyKey"></param>
        public  void EncryptConfig(string path, string EncrptyKey)
        {
            if (!path.FindIfExist())
            {
                return;
            }



            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (fs != null)
                    {
                        string headTag = fs.Read(10);   //读取字节头，判断是否已经加密过了

                        if (headTag == m_Head)
                        {
#if UNITY_EDITOR
                            Debug.LogFormat("已经加密过了！{0}", path);
#endif
                            return;
                        }

                        byte[] buffer = fs.Read();
                        fs.Write(m_Head);
                        byte[] EncBuffer = Encrypt(buffer, EncrptyKey);      //内容+密钥
                        fs.Write(EncBuffer);               //写入密钥+内容
                        Debug.LogFormat("加密成功！{0}", path);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("加密失败！{0}\n{1}", path, e);
            }
        }

        #endregion


        #region 解密


        /// <summary>
        /// 文件解密，传入文件路径（会改动加密文件，不适合运行时）
        /// </summary>
        /// <param name="path"></param>
        /// <param name="EncrptyKey"></param>
        public  void DecryptConfig(string path, string EncrptyKey)
        {
            if (!path.FindIfExist())
            {
                return;
            }

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (fs != null)
                    {
                        byte[] headBuff = new byte[10];
                        string headTag = fs.Read( headBuff.Length);
                        if (headTag == m_Head)
                        {
                            byte[] buffer = fs.Read((long)headBuff.Length, fs.Length);
                            byte[] DecBuffer = Decrypt(buffer, EncrptyKey);
                            fs.Write(DecBuffer);
                            Debug.LogFormat("解密成功！{0}", path);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("解密失败！{0}\n{1}", path, e);
            }
        }

        /// <summary>
        /// 文件界面，传入文件路径，返回字节（AB包主用）
        /// </summary>
        /// <returns></returns>
        public  byte[] DecryptConfigBytes(string path, string EncrptyKey)
        {
            if (!path.FindIfExist())
            {

                Debug.LogErrorFormat("assetbundleconfig完整路径不存在{0}", path);
                return null;
            }
            byte[] DecBuffer = null;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    if (fs != null)
                    {
                        byte[] headBuff = new byte[10];
                        string headTag = fs.Read( ref headBuff);
                        if (headTag == m_Head)
                        {
                            byte[] buffer = new byte[fs.Length - headBuff.Length];
                            fs.Read( buffer, 0, (fs.Length - headBuff.Length));
                            DecBuffer = Decrypt(buffer, EncrptyKey);
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Debug.LogErrorFormat("Stream中不允许使用FileStream：{0}", e);
            }

            return DecBuffer;
        }

        #endregion




        #region 一开始就有的



        /// <summary>
        /// AES 加密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
        /// <param name="EncryptKey">加密密钥</param>
        public  string Encrypt(string EncryptString, string EncryptKey)
        {
            return Convert.ToBase64String(Encrypt(Encoding.Default.GetBytes(EncryptString), EncryptKey));
        }

        /// <summary>
        /// AES 加密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
        /// <param name="EncryptKey">加密密钥</param>
        public  byte[] Encrypt(byte[] EncryptByte, string EncryptKey)
        {
            if (EncryptByte.Length == 0) { throw (new Exception("明文不得为空")); }
            if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }
            byte[] m_strEncrypt;
            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            byte[] m_salt = Convert.FromBase64String("gsf4jvkyhye5/d7k8OrLgM==");
            Rijndael m_AESProvider = Rijndael.Create();
            try
            {
                MemoryStream m_stream = new MemoryStream();
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(EncryptKey, m_salt);
                ICryptoTransform transform = m_AESProvider.CreateEncryptor(pdb.GetBytes(32), m_btIV);
                CryptoStream m_csstream = new CryptoStream(m_stream, transform, CryptoStreamMode.Write);
                m_csstream.Write(EncryptByte, 0, EncryptByte.Length);
                m_csstream.FlushFinalBlock();
                m_strEncrypt = m_stream.ToArray();
                m_stream.Close(); m_stream.Dispose();
                m_csstream.Close(); m_csstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_AESProvider.Clear(); }
            return m_strEncrypt;
        }


        /// <summary>
        /// AES 解密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey">解密密钥</param>
        public  string Decrypt(string DecryptString, string DecryptKey)
        {
            return Convert.ToBase64String(Decrypt(Encoding.Default.GetBytes(DecryptString), DecryptKey));
        }

        /// <summary>
        /// AES 解密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey">解密密钥</param>
        public  byte[] Decrypt(byte[] DecryptByte, string DecryptKey)
        {
            if (DecryptByte.Length == 0) { throw (new Exception("密文不得为空")); }
            if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("密钥不得为空")); }
            byte[] m_strDecrypt;
            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            byte[] m_salt = Convert.FromBase64String("gsf4jvkyhye5/d7k8OrLgM==");
            Rijndael m_AESProvider = Rijndael.Create();
            try
            {
                MemoryStream m_stream = new MemoryStream();
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(DecryptKey, m_salt);
                ICryptoTransform transform = m_AESProvider.CreateDecryptor(pdb.GetBytes(32), m_btIV);
                CryptoStream m_csstream = new CryptoStream(m_stream, transform, CryptoStreamMode.Write);
                m_csstream.Write(DecryptByte, 0, DecryptByte.Length);
                m_csstream.FlushFinalBlock();
                m_strDecrypt = m_stream.ToArray();
                m_stream.Close(); m_stream.Dispose();
                m_csstream.Close(); m_csstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_AESProvider.Clear(); }
            return m_strDecrypt;
        }
        #endregion




    }

    #endregion

}
public static partial class ExtendAssetBundles  //ABLoader
{

    public interface IABLoader : ILoader
    {

        #region 属性
        /// <summary>Assets/AssetBundles(文件夹)/AssetBundles(ab包名)/AssetBundleManifest(asset)</summary>
        string ManifestPath { get; set; }
        AssetBundleManifest Manifest { get; }

        #endregion


        #region LoadFromAsset
        T LoadAsset<T>(Func<string, AssetBundle> cb, string abPath, string assetName) where T : UnityEngine.Object;

         T LoadAsset<T>(AssetBundle ab, string assetName) where T : UnityEngine.Object;

         AssetBundleRequest LoadAssetAsync<T>(AssetBundle ab, string assetName) where T : UnityEngine.Object;

         T[] LoadAssets<T>(AssetBundle ab) where T : UnityEngine.Object;

         AssetBundleRequest LoadAssetsAsync<T>(AssetBundle ab) where T : UnityEngine.Object;
        #endregion



        #region LoadFromFile
        /// <summary>
        /// 从本地加载
        /// 有后缀就必须加后缀</summary>
        AssetBundle LoadABFromFile(string path);

        T[] LoadAssetsFromFile<T>(string abPath) where T : UnityEngine.Object;


        T LoadAssetFromFile<T>(string abPath, string assetName) where T : UnityEngine.Object;

        T LoadAssetFromFile<T>(string path) where T : UnityEngine.Object;

        /// <summary>
        /// "Assets/AssetBundles/asset"
        /// "Cube"
        /// <cre=>
        /// </summary>
        IEnumerator LoadAssetFromFileAsync<T>(string abPath, string assetName, Action<T> cb) where T : UnityEngine.Object;
        #endregion


        #region LoadFromMemory
        /// <summary> "Assets/AssetBundles/asset"</summary>
        AssetBundle LoadABFromMemory(string abPath);

        IEnumerator LoadAssetFromMemoryAsync<T>(string abPath, string assetName, Action<T> cb) where T : UnityEngine.Object;

        T LoadAssetFromMemory<T>(string abPath, string assetName) where T : UnityEngine.Object;
        #endregion


        #region LoadFromStream
        /// <summary>从流中加载</summary>
        AssetBundle LoadABFromStream(Stream stream);

         T LoadAssetFromStream<T>(string abPath, string assetName, Action<T> cb = null) where T : UnityEngine.Object;


         IEnumerator LoadAssetFromStreamAsync<T>(string abPath, string assetName, Action<T> cb) where T : UnityEngine.Object;



        #endregion


        #region LoadFromWRAB
        /// <summary>从远程服务器加载（也可以从本地加载）</summary>
        IEnumerator LoadABFromUnityWebRequestAssetBundle<T>(string uri, Action<T> cb) where T : UnityEngine.Object;
        #endregion



        void UnloadAB(AssetBundle ab, bool unloadAllLoadedObjects = true);


        void BuildAB();
        /// <summary>
        /// AssetBundle压缩 
        /// <para/>压缩算法：
        /// <br/>BuildAssetBundleOptions.None 这是默认采用的压缩策略。使用LZMA压缩算法，压缩的包会比较小，但是加载时间会比较长，在使用前需要进行整体解压。一旦被解压，这些包就会重新采用LZ4算法重新进行压缩。LZ4是基于块的压缩算法，当Unity需要从LZ4压缩的包中访问资源时，只需要解压缩并读取包含所请求资源的字节块即可。优点是加载时间短，但相应的包的体积也会比较大。
        /// <br/>BuildAssetBundleOptions.UncompressedAssetBundle不进行压缩。包占用空间很大，但加载速度会很快。
        /// <br/>BuildAssetBundleOptions.ChunkBasedCompression使用LZ4进行压缩，包比较大，但加载速度也会比较快。
        /// </summary>
        AssetBundleManifest CompressAB(string path,
           BuildAssetBundleOptions buildAssetBundleOptions = BuildAssetBundleOptions.None,
           BuildTarget buildTarget = BuildTarget.StandaloneWindows64);
        void EncryptAB();
        void BuildABGroupStrategy();
        /// <summary> 获取指定AssetBundle的所有依赖</summary>
        string[] GetAllDependencies(string abName);
        /// <summary>获取指定AssetBundle的直接依赖</summary>
        string[] GetDirectDependencies(string abName);
        // 
        /// <summary>获取所有AssetBundle</summary>
        string[] GetAllAssetBundles();

    }
    #region ABLoader


    public class ABLoader    : IABLoader
    {


        #region 字属构造
        AssetBundleManifest _manifest;

        string _manifestPath;
        public string ManifestPath { get { return _manifestPath; } set { _manifestPath = value; } }
        public AssetBundleManifest Manifest
        {
            get
            {
                if (_manifest == null)
                {
                    string abPath = _manifestPath.TrimName(TrimNameType.SlashPre);
                    string assetName = _manifestPath.TrimName(TrimNameType.SlashAfter);
                    _manifest = LoadAssetFromFile<AssetBundleManifest>(assetName);

                }
                return _manifest;
            }
        }


        public ABLoader(string manifestPath)
        {
            _manifestPath = manifestPath;
        }
        #endregion









        #region LoadAsset/LoadAssets
        public T LoadAsset<T>(Func<string,AssetBundle> cb,string abPath, string assetName) where T : UnityEngine.Object
        {
            AssetBundle ab = cb(abPath);
            return ab.LoadAsset<T>(assetName);
        }

        public T LoadAsset<T>(AssetBundle ab, string assetName) where T : UnityEngine.Object
        {
            return ab.LoadAsset<T>(assetName);
        }

        public AssetBundleRequest LoadAssetAsync<T>(AssetBundle ab, string assetName) where T : UnityEngine.Object
        {
            return ab.LoadAssetAsync<T>(assetName);
        }

        public T[] LoadAssets<T>(AssetBundle ab) where T : UnityEngine.Object
        {
            return ab.LoadAllAssets<T>();
        }

        public AssetBundleRequest LoadAssetsAsync<T>(AssetBundle ab) where T : UnityEngine.Object
        {
            return ab.LoadAllAssetsAsync<T>();
        }
        #endregion  


        #region LoadFromFile

        public AssetBundle LoadABFromFile(string path)
        {
            return AssetBundle.LoadFromFile(path);
        }
        public T[] LoadAssetsFromFile<T>(string abPath) where T : UnityEngine.Object
        {
            AssetBundle ab = AssetBundle.LoadFromFile(abPath);//有后缀就必须加后缀
            T[] assets = ab.LoadAllAssets<T>();
            return assets;
        }
        public T LoadAssetFromFile<T>(string abPath,string assetName) where T:UnityEngine.Object
        {
            AssetBundle ab = LoadABFromFile(abPath);
            T asset = ab.LoadAsset<T>(assetName);
            return asset;
        }

        public T LoadAssetFromFile<T>(string path) where T : UnityEngine.Object
        {
            string abPath = path.TrimName(TrimNameType.SlashPre);
            string assetName = path.TrimName(TrimNameType.SlashAfter);

            AssetBundle ab = LoadABFromFile(abPath);
            T asset = ab.LoadAsset<T>(assetName);

            return asset;
        }


        public IEnumerator LoadAssetFromFileAsync<T>(string abPath, string assetName, Action<T> cb) where T : UnityEngine.Object
        {
            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(abPath);
            yield return request;


            var asset = request.assetBundle.LoadAsset<T>(assetName);
            cb(asset);
        }
        #endregion


        #region LoadFromMemory

        /// <summary>"Assets/AssetBundles/asset"</summary>
        public IEnumerator LoadAssetFromMemoryAsync<T>(string abPath, string assetName, Action<T> cb) where T : UnityEngine.Object
        {
            AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(System.IO.File.ReadAllBytes(abPath));
            yield return request;


            var asset = request.assetBundle.LoadAsset<T>(assetName);
            cb(asset);
        }



        public T LoadAssetFromMemory<T>(string abPath, string assetName) where T : UnityEngine.Object
        {
            AssetBundle ab = LoadABFromMemory(abPath);
            T asset = ab.LoadAsset<T>(assetName);
            return asset;
        }       
        
        public AssetBundle LoadABFromMemory(string abPath)
        {
            byte[] bs = System.IO.File.ReadAllBytes(abPath);
            return AssetBundle.LoadFromMemory(bs);
        }
        #endregion


        #region LoadFromStream
        public AssetBundle LoadABFromStream(Stream stream)
        {
            return AssetBundle.LoadFromStream(stream);
        }

        public T  LoadAssetFromStream<T> (string abPath, string assetName, Action<T> cb=null) where T : UnityEngine.Object
        {
            AssetBundle.UnloadAllAssetBundles(true);
            FileStream stream = new FileStream(abPath, FileMode.Open, FileAccess.Read);
            AssetBundle assetBundle = AssetBundle.LoadFromStream(stream);


            T asset = assetBundle.LoadAsset<T>(assetName);
            cb.DoIfNotNull(asset);


            assetBundle.Unload(false);
            stream.Close();
            return asset;  //可能有错,返回就用就用回调
        }



       public IEnumerator LoadAssetFromStreamAsync<T>(string abPath, string assetName, Action<T> cb) where T : UnityEngine.Object
        {
            FileStream stream = new FileStream(abPath, FileMode.Open, FileAccess.Read);
            AssetBundleCreateRequest request = AssetBundle.LoadFromStreamAsync(stream);
            yield return request;


            var asset = request.assetBundle.LoadAsset<T>(assetName);
            cb(asset);


            request.assetBundle.Unload(false);
            stream.Close();
        }

        #endregion


        #region LoadFromUnityWebRequestAssetBundle
        public IEnumerator LoadABFromUnityWebRequestAssetBundle<T>(string uri,Action<T> cb) where T : UnityEngine.Object
        {
            string path = @"uri";
            string abPath = path.TrimName(TrimNameType.SlashPre);
            string assetName = path.TrimName(TrimNameType.SlashAfter);
            UnityWebRequest req;
#if UNITY_2018_3_OR_NEWER
            req = UnityWebRequestAssetBundle.GetAssetBundle(abPath);
#else
            req = UnityWebRequest.GetAssetBundle(abPath);
#endif
            yield return req.SendWebRequest();

            AssetBundle ab = DownloadHandlerAssetBundle.GetContent(req);
            T asset = ab.LoadAsset<T>(assetName);
            cb(asset);


        }

        #endregion


        #region Other




      



        /// <summary>
        /// true，则会卸载AssetBundle本身及从AssetBundle加载的全部资源
        /// <br/> 01 在应用程序生命周期中具有明确定义的卸载AssetBundle的时间点，例如在关卡之间或在加载期间。
        /// <br/> 02 维护单个对象的引用计数，仅当未使用所有组成对象时才卸载 AssetBundle。这允许应用程序卸载和重新加载单个对象，而无需复制内存。
        /// <para/>false，则会保留已经加载的资源.
        /// <br/>当参数为false时，会中断材质资源M与当前AssetBundle的联系
        /// <br/>即便再次加载 AB 并且调用 AB.LoadAsset()，Unity也不会将现有 M 副本重新链接到新加载的材质
        /// <br/>如果现在创建了一个预制体的实例并引用了材质M，也不会使用现有的M，而是会加载一个新的M副本
        /// <br/>这样就导致了M在内存中存在两份，
        /// <br/>如果不得不使用AssetBundle.Unload(false)，则只能用以下两种方式卸载单个对象：
        /// <br/>01 在场景和代码中消除对不需要的对象的所有引用。完成此操作后，调用Resources.UnloadUnusedAssets。
        /// <br/> 02 以非附加方式加载场景。这样会销毁当前场景中的所有对象并自动调用Resources.UnloadUnusedAssets。
        /// </summary>
        public void UnloadAB(AssetBundle ab,bool unloadAllLoadedObjects =true)
        {
            ab.Unload(unloadAllLoadedObjects);
        }


        public void UnloadUnusedAssets()
        {
            Resources.UnloadUnusedAssets();
        }


    public void BuildABGroupStrategy()
        {
            throw new NotImplementedException();
        }

        public void BuildAB()
        {
            throw new NotImplementedException();
        }

        public AssetBundleManifest CompressAB(string path,
            BuildAssetBundleOptions buildAssetBundleOptions = BuildAssetBundleOptions.None,
            BuildTarget buildTarget = BuildTarget.StandaloneWindows64)
        {
            return BuildPipeline.BuildAssetBundles(path, buildAssetBundleOptions, buildTarget);
        }

        public void EncryptAB()
        {
            throw new NotImplementedException();
        }



        public string[] GetAllDependencies(string abName)
        {
            return Manifest.GetAllDependencies(abName); ;
        }

        /// <summary>获取指定AssetBundle的直接依赖</summary>
        public string[] GetDirectDependencies(string abName)
        {
            return Manifest.GetDirectDependencies(abName);
        }

        /// <summary>获取所有AssetBundle</summary>
        public string[] GetAllAssetBundles()
        {
            return Manifest.GetAllAssetBundles();
        }

        #endregion



    }
    #endregion


}



