/****************************************************
    文件：EditorUtil.Exporter.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/4 22:19:20
	功能：
*****************************************************/

using System.IO;
using System;
using UnityEditor;
using UnityEngine;

public static partial class EditorUtil
{
#if UNITY_EDITOR
    public static void ExportPackage(string assetPathName, string fileName)
    {
        AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);
    }

    public static void CallMenuItem(string menuName)
    {
        EditorApplication.ExecuteMenuItem(menuName);
    }

    //[MenuItem("QFramework/Framework【CommonClass】/Editor/导出 UnityPackage（自定义为Plugins） %e", false, 1)]
    static void MenuClicked()
    {
        ExportPackage("Assets/Plugins", GeneratePackageName() + ".unitypackage");
        EditorUtil.OpenInFolder(Path.Combine(Application.dataPath, "../"));
    }
#endif


    private static string GeneratePackageName()
    {
        return "Common_" + DateTime.Now.ToString("yyyyMMddHHmm");///注意年小写的yyyy
    }

    public static void OpenInFolder(string folderPath)
    {
        Application.OpenURL("file:///" + folderPath);
    }
}