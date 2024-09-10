/****************************************************
    文件：IOPaths.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/20 14:52:18
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class IOPaths
{
    const string saveFolder = "StreamingFile";
    static string pre = Application.dataPath + "/StreamingFile";

    const string saveByXmlFile = "ByXml.txt";
    const string saveByJsonFile = "ByJson.txt";
    const string saveByBinFile = "ByBin.txt";
    //
    public static string saveByXmlPath = $"{pre}/{saveByXmlFile}";
    public static string saveByJsonPath = $"{pre}/{saveByJsonFile}";
    public static string saveByBinPath = $"{pre}/{saveByBinFile}";
}






