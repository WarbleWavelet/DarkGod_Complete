/****************************************************
    文件：IOExtension.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/30 16:55:33
	功能：
*****************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using LitJson;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;



public static partial class ExtendIO
{
    public interface IValue { string value { get; set; } }
    /// <summary>如果对拓展参数有分类需求</summary>
    public struct tPath :IValue
    {
        public string value { get;set;}
        public tPath(string value)
        {
           this.value=value;
        }
    }

    public static FileInfo[] FindFileInfos(this tPath path)
    {
        DirectoryInfo di = new DirectoryInfo(path.value);
        FileInfo[] fis = di.GetFiles("*", SearchOption.AllDirectories);
         
        return fis;

    }

    public static FileInfo[] FindFileInfos(this string path)
    {
        DirectoryInfo di = new DirectoryInfo(path);
        FileInfo[] fis = di.GetFiles("*", SearchOption.AllDirectories);

        return fis;

    }

    public static bool FindIfExist(this string path)
    {
        return File.Exists(path);

    }
}



/// <summary>
/// 各种文件的读写复制操作,主要是对System.IO的一些封装
/// <para/>QF的
/// </summary>
public static partial class ExtendIO

{
    public static void Example()
    {
        var testDir = "Assets/TestFolder";
        testDir.CreateDirIfNotExists();

        Directory.Exists(testDir).LogInfo();
        testDir.DeleteDirIfExists();
        Directory.Exists(testDir).LogInfo();

        var testFile = testDir.CombinePath("test.txt");
        testDir.CreateDirIfNotExists();
        File.Create(testFile);
        testFile.DeleteFileIfExists();
    }

    /// <summary>
    /// 创建新的文件夹,如果存在则不创建
    /// </summary>
    public static string CreateDirIfNotExists(this string dirFullPath)
    {
        if (!Directory.Exists(dirFullPath))
        {
            Directory.CreateDirectory(dirFullPath);
        }

        return dirFullPath;
    }

    /// <summary>
    /// 删除文件夹，如果存在
    /// </summary>
    public static void DeleteDirIfExists(this string dirFullPath)
    {
        if (Directory.Exists(dirFullPath))
        {
            Directory.Delete(dirFullPath, true);
        }
    }

    /// <summary>
    /// 清空 EDir,如果存在。
    /// </summary>
    public static void EmptyDirIfExists(this string dirFullPath)
    {
        if (Directory.Exists(dirFullPath))
        {
            Directory.Delete(dirFullPath, true);
        }

        Directory.CreateDirectory(dirFullPath);
    }

    /// <summary>
    /// 删除文件 如果存在
    /// </summary>
    /// <param name="fileFullPath"></param>
    /// <returns> True if exists</returns>
    public static bool DeleteFileIfExists(this string fileFullPath)
    {
        if (File.Exists(fileFullPath))
        {
            File.Delete(fileFullPath);
            return true;
        }

        return false;
    }

    public static string CombinePath(this string selfPath, string toCombinePath)
    {
        return Path.Combine(selfPath, toCombinePath);
    }

    #region 未经过测试

    /// <summary>
    /// 保存文本
    /// </summary>
    /// <param name="text"></param>
    /// <param name="path"></param>
    public static void SaveText(this string text, string path)
    {
        path.DeleteFileIfExists();

        using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            using (var sr = new StreamWriter(fs))
            {
                sr.Write(text); //开始写入值
            }
        }
    }


    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string ReadText(this FileInfo file)
    {
        return ReadText(file.FullName);
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string ReadText(this string fileFullPath)
    {
        var result = string.Empty;

        using (var fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read))
        {
            using (var sr = new StreamReader(fs))
            {
                result = sr.ReadToEnd();
            }
        }

        return result;
    }

#if UNITY_EDITOR
    /// <summary>
    /// 打开文件夹
    /// </summary>
    /// <param name="path"></param>
    public static void OpenFolder(string path)
    {
#if UNITY_STANDALONE_OSX
        System.Diagnostics.Process.Start("open", path);
#elif UNITY_STANDALONE_WIN
       // System.Diagnostics.Process.Start("explorer.exe", path);
        System.Diagnostics.Process.Start(path);
#endif
    }
#endif

    /// <summary>
    /// 获取文件夹名
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetDirectoryName(string fileName)
    {
        fileName = ExtendIO.MakePathStandard(fileName);
        return fileName.Substring(0, fileName.LastIndexOf('/'));
    }

    /// <summary>
    /// 获取文件名
    /// </summary>
    /// <param name="path"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string GetFileName(string path, char separator = '/')
    {
        path = ExtendIO.MakePathStandard(path);
        return path.Substring(path.LastIndexOf(separator) + 1);
    }

    /// <summary>
    /// 获取不带后缀的文件名
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string GetFileNameWithoutExtention(string fileName, char separator = '/')
    {
        return GetFilePathWithoutExtention(GetFileName(fileName, separator));
    }

    /// <summary>
    /// 获取不带后缀的文件路径
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetFilePathWithoutExtention(string fileName)
    {
        if (fileName.Contains("."))
            return fileName.Substring(0, fileName.LastIndexOf('.'));
        return fileName;
    }

    /// <summary>
    /// 使目录存在,Path可以是目录名必须是文件名
    /// </summary>
    /// <param name="path"></param>
    public static void MakeFileDirectoryExist(string path)
    {
        string root = Path.GetDirectoryName(path);
        if (!Directory.Exists(root))
        {
            Directory.CreateDirectory(root);
        }
    }

    /// <summary>
    /// 使目录存在
    /// </summary>
    /// <param name="path"></param>
    public static void MakeDirectoryExist(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    /// <summary>
    /// 结合目录
    /// </summary>
    /// <param name="paths"></param>
    /// <returns></returns>
    public static string Combine(params string[] paths)
    {
        string result = "";
        foreach (string path in paths)
        {
            result = Path.Combine(result, path);
        }

        result = MakePathStandard(result);
        return result;
    }

    /// <summary>
    /// 获取父文件夹
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetPathParentFolder(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return string.Empty;
        }

        return Path.GetDirectoryName(path);
    }


    /// <summary>
    /// 使路径标准化，去除空格并将所有'\'转换为'/'
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string MakePathStandard(string path)
    {
        return path.Trim().Replace("\\", "/");
    }

    public static List<string> GetDirSubFilePathList(this string dirABSPath, bool isRecursive = true, string suffix = "")
    {
        var pathList = new List<string>();
        var di = new DirectoryInfo(dirABSPath);

        if (!di.Exists)
        {
            return pathList;
        }

        var files = di.GetFiles();
        foreach (var fi in files)
        {
            if (!string.IsNullOrEmpty(suffix))
            {
                if (!fi.FullName.EndsWith(suffix, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
            }

            pathList.Add(fi.FullName);
        }

        if (isRecursive)
        {
            var dirs = di.GetDirectories();
            foreach (var d in dirs)
            {
                pathList.AddRange(GetDirSubFilePathList(d.FullName, isRecursive, suffix));
            }
        }

        return pathList;
    }

    public static List<string> GetDirSubDirNameList(this string dirABSPath)
    {
        var di = new DirectoryInfo(dirABSPath);

        var dirs = di.GetDirectories();

        return dirs.Select(d => d.Name).ToList();
    }

    public static string GetFileName(this string absOrAssetsPath)
    {
        var name = absOrAssetsPath.Replace("\\", "/");
        var lastIndex = name.LastIndexOf("/");

        return lastIndex >= 0 ? name.Substring(lastIndex + 1) : name;
    }

    public static string GetFileNameWithoutExtend(this string absOrAssetsPath)
    {
        var fileName = GetFileName(absOrAssetsPath);
        var lastIndex = fileName.LastIndexOf(".");

        return lastIndex >= 0 ? fileName.Substring(0, lastIndex) : fileName;
    }

    public static string GetFileExtendName(this string absOrAssetsPath)
    {
        var lastIndex = absOrAssetsPath.LastIndexOf(".");

        if (lastIndex >= 0)
        {
            return absOrAssetsPath.Substring(lastIndex);
        }

        return string.Empty;
    }

    public static string GetDirPath(this string absOrAssetsPath)
    {
        var name = absOrAssetsPath.Replace("\\", "/");
        var lastIndex = name.LastIndexOf("/");
        return name.Substring(0, lastIndex + 1);
    }

    public static string GetLastDirName(this string absOrAssetsPath)
    {
        var name = absOrAssetsPath.Replace("\\", "/");
        var dirs = name.Split('/');

        return absOrAssetsPath.EndsWith("/") ? dirs[dirs.Length - 2] : dirs[dirs.Length - 1];
    }


    #endregion
}


/// <summary>
/// 我总结的
/// <para/>RealFrame
/// </summary>
public static partial class ExtendIO
{

    #region FileStream
    /// <summary>
    /// 读取前length，byte[]转string
    /// </summary>
    /// <param name="fs"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string FileStream_Read(FileStream fs, int length)
    {
        byte[] buffer = new byte[length];
        fs.Read(buffer, 0, buffer.Length);
        string str = Encoding.UTF8.GetString(buffer);

        return str;
    }


    public static string FileStream_Read(FileStream fs, ref byte[] buffer)
    {
        fs.Read(buffer, 0, buffer.Length);
        string str = Encoding.UTF8.GetString(buffer);

        return str;
    }


    /// <summary>
    /// 从开始索引start读取到结束索引end
    /// </summary>
    /// <param name="fs"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static byte[] FileStream_Read(FileStream fs, long start, long end)
    {
        byte[] buffer = new byte[end - start];//内容
        fs.Read(buffer, 0, Convert.ToInt32(end - start));
        fs.Seek(0, SeekOrigin.Begin);
        fs.SetLength(0);

        return buffer;



    }





    public static byte[] FileStream_Read(FileStream fs)
    {
        fs.Seek(0, SeekOrigin.Begin);       //读完 seek回去，保持原始状态
        byte[] buffer = new byte[fs.Length];
        fs.Read(buffer, 0, Convert.ToInt32(fs.Length));     //获取所有
        fs.Seek(0, SeekOrigin.Begin);                       //移动到开头
        fs.SetLength(0);                                    //清空

        return buffer;


    }
    /// <summary>
    /// fs写入 str  .fs会返回出去（）但因为是using，所以不能用返回值，ref out
    /// </summary>
    public static void FileStream_Write(FileStream fs, string str)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(str);
        fs.Write(buffer, 0, buffer.Length);

    }


    public static void FileStream_Write(FileStream fs, byte[] bs)
    {
        fs.Write(bs, 0, bs.Length);

    }




    public static void FileStream_Write(FileStream fs, byte[] buffer, int start, int end)
    {
        fs.Write(buffer, start, end);

    }

    public static void FileStream_Read(FileStream fs, byte[] buffer, int start, long end)
    {
        fs.Read(buffer, start, Convert.ToInt32(end));

    }

    #endregion




    #region File FileInfo StreamWriter     




    /// <summary>
    /// 新建并且向filePath写入fileContent（StreamWriter）
    /// </summary>
    /// <param name="filePath">全写，包括文件名和后缀</param>
    /// <param name="fileContent"></param>
    public static void File_Create_Write(string filePath, string fileContent)
    {
        FileInfo fi = new FileInfo(filePath);
        StreamWriter sw = fi.CreateText();
        sw.WriteLine(fileContent);

        sw.Close();
        sw.Dispose();
    }

    /// <summary>
    /// 新建并且向filePath写入bytes（Stream）
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="bytes"></param>
    public static void File_Create_Write(string filePath, byte[] bytes)
    {
        File_Delete(filePath);
        //
        FileInfo fi = new FileInfo(filePath);
        Stream stream = fi.Create();

        stream.Write(bytes, 0, bytes.Length);
        stream.Close();

        stream.Dispose();
    }



    public static void AB_Clear(string path)
    {
        if (Folder_Exits(path) == false)
        {

            Debug.LogErrorFormat("该路径不存在：{0}", path);
            return;
        }

        DirectoryInfo di = new DirectoryInfo(path);  //搜索该文件夹
        FileInfo[] fis = di.GetFiles("*", SearchOption.AllDirectories);

        for (int i = 0; i < fis.Length; i++)//全删除
        {
            string fileFullName = fis[i].FullName;    // IgnoreLayerCollision/B/n.xxx

            if (File.Exists(fileFullName))
            {
                File.Delete(fileFullName);//删除本身
            }
            if (File.Exists(fileFullName + ".manifest"))
            {
                File.Delete(fileFullName + ".manifest");//删除他的manifest
            }

        }

    }

    public static void File_Move(string from, string to)
    {
        File.Move(from, to);
    }

    /// <summary>
    /// 移动+加后缀
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="suffix"></param>
    public static void File_Move_Suffix(string from, string suffix)
    {
        File.Move(from, from + suffix);
    }




    /// <summary>
    /// 文件夹拷贝
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public static void File_Copy(string from, string to)
    {


        try //递归拷贝
        {
            //取路径
            Folder_New(to);
            string toPath = Path.Combine(to, Path.GetFileName(from));// IgnoreLayerCollision/   B/m  =>  IgnoreLayerCollision/m
            toPath = CommonClass.TrimName(toPath, TrimNameType.SlashPre);//去掉StreamingAssets
            if (File.Exists(from) == true)
            {
                toPath += Path.DirectorySeparatorChar;// Path.DirectorySeparatorChar: '\'
            }

            //取文件
            Folder_New(toPath);

            string[] fileArr = Directory.GetFileSystemEntries(from);
            //赋值
            foreach (string file in fileArr)
            {
                if (file.EndsWith(".meta") == true)
                {
                    continue;
                }
                if (Directory.Exists(file) == true)
                {
                    File_Copy(file, toPath);  //文件夹拷贝
                }
                else
                {

                    File.Copy(file, toPath + "/" + Path.GetFileName(file), true);//文件拷贝  ,文件夹和文件名
                }
            }

        }
        catch (Exception)
        {

            Debug.LogErrorFormat("无法复制：{0} => {1}", from, to);
        }
    }


    /// <summary>
    /// 删除文件夹下的所有文件,递归删除
    /// </summary>
    public static void Folder_Clear_Recursive(string path)
    {
        try
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileSystemInfo[] fsiArr = di.GetFileSystemInfos();

            foreach (var fsi in fsiArr)
            {
                if (fsi is DirectoryInfo)
                {
                    DirectoryInfo _di = new DirectoryInfo(fsi.FullName);
                    _di.Delete(true);
                }
                else
                {
                    File.Delete(fsi.FullName);
                }
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    /// <summary>
    /// 删除文件夹下的所有文件,除了后缀是lst的任一个 ,不递归删除
    /// </summary>
    public static void Folder_ClearWithout_NotRecursive(string path, params string[] lst)
    {
        try
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileSystemInfo[] fsiArr = di.GetFiles("*", SearchOption.AllDirectories);

            foreach (var fsi in fsiArr)
            {
                if (CommonClass.EndsWith(path, lst))
                {
                    continue;
                }

                File.Delete(fsi.FullName);
            }

        }
        catch (Exception)
        {

            throw;
        }
    }



    /// <summary>
    /// 到path下删除后缀名为suffix
    /// </summary>
    /// <param name="abPath"></param>
    /// <param name="sufffix"></param>
    public static void Folder_Delete(string path, string sufffix)//m_AB_OutterPath
    {
        FileInfo[] fis = Folder_GetAllFileInfo(path);
        foreach (FileInfo item in fis)
        {
            if (item.FullName.EndsWith(sufffix))
            {
                File_Delete(item.FullName);
            }
        }

    }

    public static FileInfo[] Folder_GetAllFileInfo(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);
        FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories);

        return files;

    }


    /// <summary>
    /// 存在文件夹（带/也可以）
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool Folder_Exits(string path)
    {
        return Directory.Exists(path);
    }


    /// <summary>文件夹有就好，没有就创建</summary>
    public static void Folder_New(string path)
    {
        if (Directory.Exists(path) == false) //输出path
        {
            Directory.CreateDirectory(path);
        }
    }


    /// <summary>链式编程的方式来写</summary>
    public static void CreateFolder(this string path)
    {
        if (Directory.Exists(path) == false) //输出path
        {
            Directory.CreateDirectory(path);
        }
    }
    #endregion



    #region File


    public static void File_Rename(string sourceName, string destName, string path)
    {
        string destPath = path.Replace(sourceName, destName);
        if (File_Exits(destPath))
        {
            Debug.LogError("当前文件名称已存在");
        }
        else
        {
            File_Move(path, destPath);
        }
    }

    /// <summary>
    /// 存在文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool File_Exits(string path)
    {
        return File.Exists(path);
    }

    /// <summary>
    /// 删除该文件
    /// </summary>
    /// <param name="path"></param>
    public static void File_Delete(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }



    public static string File_Name_Suffix(string path)
    {
        return Path.GetExtension(path);
    }



    public static string File_Name_WithoutSuffix(string path)
    {
        return Path.GetFileNameWithoutExtension(path);
    }
    #endregion
}




