/****************************************************
	文件：ExtendIO.cs
	作者：lenovo
	邮箱: 
	日期：2024/7/8 16:47:13
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Networking;
using UnityEditor;
using System.Text;





public static partial class ExtendIO
{
	//Profile文件中配置资源构建路径和资源首次加载路径，资源如何设置了缓存，在首次加载之后会将再用缓存在缓存目录，后面将直接从缓存目录中读取，方便项目发包时候进行使用


}





public static partial class ExtendIO //FildeStream
{


    #region EncodingFactory
    public static class EncodingFactory
	{
		public static string Bytes2String(byte[] bs, EEncoding encoding = EEncoding.UTF8)
		{
            string str = "";
            switch (encoding)
			{
				case EEncoding.UTF7: str = Encoding.UTF7.GetString(bs); break;
				case EEncoding.UTF8: str = Encoding.UTF8.GetString(bs); break;
				case EEncoding.UTF32: str = Encoding.UTF32.GetString(bs); break;
				default: throw new System.Exception("异常");
			}

			return str;
		}
        public static byte[] String2Bytes(string str, EEncoding encoding = EEncoding.UTF8)
        {
			byte[] bs;
            switch (encoding)
            {                      
                case EEncoding.UTF7: bs = Encoding.UTF7.GetBytes(str); break;
                case EEncoding.UTF8: bs = Encoding.UTF8.GetBytes(str); break;
                case EEncoding.UTF32: bs = Encoding.UTF32.GetBytes(str); break;
                default: throw new System.Exception("异常");
            }

            return bs;
        }


    }
	public enum EEncoding
	{
		UTF7,
		UTF8,
		UTF32,
		Unicode

	}
	#endregion


	



	#region Done

	public static string Read(this FileStream fs, ref byte[] bs, EEncoding encoding = EEncoding.UTF8)
	{
		fs.Read(bs, 0, bs.Length);
		string str = EncodingFactory.Bytes2String(bs, encoding);

		return str;
	}

public static void Write(this FileStream fs, byte[] bs)
	{
		fs.Write(bs, 0, bs.Length);

	}


    public static void Read(this FileStream fs, byte[] bs, int start, long end)
    {
        fs.Read(bs, start, Convert.ToInt32(end));

    }



    /// <summary>
    /// 从开始索引start读取到结束索引end
    /// </summary>
    /// <param name="fs"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static byte[] Read(this FileStream fs, long start, long end)
	{
		byte[] bs = new byte[end - start];//内容
		fs.Read(bs, 0, Convert.ToInt32(end - start));
		fs.Seek(0, SeekOrigin.Begin);
		fs.SetLength(0);

		return bs;
	}


	/// <summary>
	/// 读取前length，byte[]转string
	/// </summary>
	/// <param name="fs"></param>
	/// <param name="length"></param>
	/// <returns></returns>
	public static string Read(this FileStream fs, int length, EEncoding encoding= EEncoding.UTF8)
	{
		byte[] bs = new byte[length];
		fs.Read(bs, 0, bs.Length);
		string str = EncodingFactory.Bytes2String(bs, encoding);

		

		return str;
	}  	
	
	public static byte[] Read(this FileStream fs)
	{
		fs.Seek(0, SeekOrigin.Begin);       //读完 seek回去，保持原始状态
		byte[] bs = new byte[fs.Length];
		fs.Read(bs, 0, Convert.ToInt32(fs.Length));     //获取所有
		fs.Seek(0, SeekOrigin.Begin);                       //移动到开头
		fs.SetLength(0);                                    //清空

		return bs;


	}

	/// <summary>
	/// fs写入 str  .fs会返回出去（）但因为是using，所以不能用返回值，ref out
	/// </summary>
	public static void Write(this FileStream fs, string str)
	{
		byte[] bs = EncodingFactory.String2Bytes(str);
		fs.Write(bs, 0, bs.Length);
	}
	#endregion










}
public static partial class ExtendIO //Config
{
	public static void File2Dic(string fileName, out Dictionary<string, string> dic)
	{
		string fileContent = File2String(fileName,EFilePathType.StreamingAssetsPath);
		NamePathDic(fileContent,out dic);
	}

	 static void NamePathDic(string fileContent,out Dictionary<string,string> dic)
	{
		dic = new Dictionary<string, string>();
		using (StringReader stringReader = new StringReader(fileContent))
		{
			//当程序退出using代码块，将自动释放内存
			string line;
			while ((line = stringReader.ReadLine()) != null)
			{
				string[] keyValue = line.Split(CharMark.Equal);
				dic.Add(keyValue[0], keyValue[1]);
			}
		}

	}


#if UNITY_EDITOR

	/// <summary>Resources文件夹下的所有预制体的name8path,生成txt</summary>
	public static void ResourcesFolder2Txt(string filter=TColone.Texture, string connector= StringMark.Equal,string txtName="ConfigMap.txt")
	{
			string affies = filter.Replace("t:", ".");

			//生成资源配置文件
			//(过滤器（t:后缀名），文件夹)
			string[] resFiles = AssetDatabase.FindAssets(filter, new string[] { ConstAssetsPath.Resources });
			//找到的是资源id
			for (int i = 0; i < resFiles.Length; i++)
			{
				//将找到的资源id改为路径
				resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);

				//根据一定的规定生成记录
				string fileName = Path.GetFileNameWithoutExtension(resFiles[i]);
				string filePath = resFiles[i].Replace(ConstAssetsPath.ResourcesFolder, string.Empty).Replace(affies, string.Empty);
				resFiles[i] = fileName + connector + filePath;
			}

			//将记录写入文档
			File.WriteAllLines( ConstAssetsPath.StreamingAssetsFolder+txtName, resFiles);
			//刷新，释放内存
			AssetDatabase.Refresh();
	}


	public static void StreamingAssetsFolder2Txt(string filter=TColone.Prefab, string connector = StringMark.Equal, string txtName = "ConfigMap.txt")
	{


		//生成资源配置文件
		//(过滤器（t:后缀名），文件夹)
		string[] resFiles = AssetDatabase.FindAssets(filter, new string[] { ConstAssetsPath.Resources });
		//找到的是资源id
		for (int i = 0; i < resFiles.Length; i++)
		{
			//将找到的资源id改为路径
			resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);

			//根据一定的规定生成记录
			string fileName = Path.GetFileNameWithoutExtension(resFiles[i]);
			string filePath = resFiles[i].Replace(ConstAssetsPath.ResourcesFolder, string.Empty).Replace(Affixes.PointPrefab, string.Empty);
			resFiles[i] = fileName + connector + filePath;
		}

		//将记录写入文档
		File.WriteAllLines(ConstAssetsPath.StreamingAssetsFolder + txtName, resFiles);
		//刷新，释放内存
		AssetDatabase.Refresh();
	}
#endif

}
public static partial class ExtendIO //Dic<name,path>
{

}
public static partial class ExtendIO //StreamWriter  File 
{
	/// <summary>
	/// Application.persistentDataPath + "Test.txt"
	/// </summary>
	public static void String2File(this string str, string path)
	{
		StreamWriter sw = File.CreateText(path);
		sw.Write(str);
		sw.Close();
	}


	public enum EFilePathType
	{
		PersistentDataPath,
		StreamingAssetsPath
	}
	public static string File2String(this string file, EFilePathType filePathType)
	{

		switch (filePathType)
		{
			case EFilePathType.StreamingAssetsPath:  return File2String_StreamingAssetsPath(file);
			case EFilePathType.PersistentDataPath:  return File2String_PersistentDataPath(file);
			default: throw new System.Exception("异常:未定义");
		}

	}

		/// <summary>
		/// ConfigMap.txt
		/// Application.streamingAssetsPath
		/// UnityWebRequest
		///  static string 
		/// </summary>
	static string File2String_StreamingAssetsPath(string fileName)
	{
		fileName = fileName.TrimName(TrimNameType.SlashAfter);
		//通过文档的名字生成路径并寻找
		string url = new System.Uri(Path.Combine(Application.streamingAssetsPath, fileName)).ToString();
		UnityWebRequest request = UnityWebRequest.Get(url);
		request.SendWebRequest();
		if (request.error == null)
		{
			while (true)
			{
				//返回文档中的记录
				if (request.downloadHandler.isDone)
				{
					return request.downloadHandler.text;
				}
			}
		}
		else
		{
			Debug.LogError("读取Config文件失败");
			return string.Empty;
		}
	}

	/// <summary>
	/// Application.persistentDataPath + "Test.txt"
	/// </summary>
	public static string File2String_PersistentDataPath(this string path)
	{
		string str = File.ReadAllText(path);
		return str;
	}
}



