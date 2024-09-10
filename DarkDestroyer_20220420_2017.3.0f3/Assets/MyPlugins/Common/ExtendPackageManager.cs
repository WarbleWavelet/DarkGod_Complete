/****************************************************
	文件：ExtendAssetsManager.cs
	作者：lenovo
	邮箱: 
	日期：2024/8/4 16:53:39
	功能： 包与unity版本
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static System.Net.WebRequestMethods;
using Random = UnityEngine.Random;






/// <summary></summary>
public enum EPackage
{
	Entities, 
	Burst ,
	Jobs ,
	Mathematics ,
	Collections ,
	/// <summary>把物体渲染到屏幕上</summary>
	Hybrid_Renderer
}


public static partial class ExtendPackageManager
{
	//包版本,unity版本,下载url,介绍url,子包列表	,选项卡
	//
   static List<EPackage> entitiesPackageLst = new List<EPackage> 
   { 
	   EPackage.Entities, 
	   EPackage.Burst, 
	   EPackage.Jobs, 
	   EPackage.Mathematics, 
	   EPackage.Collections
   };
    static List<string> entitiesUrlLst = new List<string>
    {
		"com.unity.entities",
		"https://docs.unity3d.com/Packages/com.unity.entities@1.1/license/LICENSE.html"  ,
		"https://docs.unity.cn/Packages/com.unity.entities@1.0/manual/index.html" ,
        "https://github.com/Unity-Technologies/EntityComponentSystemSamples/tree/master"
    };
	static List<string> entitiesTabLst = new List<string>
	{ 
		"DOTS",
		"Jobs"
	};
	  #if NET_4_7_OR_NEWER
    public static (string, string,List<EPackage>, List<string>) Entities = ("1.0.11","2022.3.0f1", entitiesPackageLst, entitiesUrlLst);
	//
	public static (string, string,string) Job = ("","2018.1", "https://docs.unity.cn/2018.1/Documentation/Manual/JobSystem.html");
	public static (string, string,string) AssetBundles_Browser = ("","", "https://github.com/Unity-Technologies/AssetBundles-Browser.git");
	public static (string, string,string) XLua = ("","", "https://github.com/Tencent/xLua");
	public static (string, string,string) huatuo = ("","", "https://github.com/tuyoogame/huatuo");
	public static (string, string,string) Framing_ET = ("","", "https://github.com/wqaetly/NKGMobaBasedOnET");
	public static (string, string,string) MVVM_Avalonia = ("","", "https://github.com/AvaloniaUI/Avalonia");
	public static (string, string,string) 游戏编程模式 = ("","", "https://gpp.tkchu.me/");
	//
	public static (string, string,string) GDTM = ("","", "https://github.com/gonglei007/GameDevMind");
	public static (string, string,string) 加强版的Blog = ("","", "https://github.com/QianMo/Game-Programmer-Study-Notes");
	public static (string, string,string) Unity面试题总结 = ("","", "https://github.com/Lafree317/Unity-InterviewQuestion");

	//
	public static (string, string,string) Ds8A = ("","", "https://github.com/krahets/hello-algo");
	public static (string, string,string) MVP = ("","", "https://github.com/agre1981/MVP");
	public static (string, string,  List<string>) PureMVC = ("", "", new List<string>
	{
		"https://puremvc.org/",	//官网
		"puremvc-csharp-standard-frameworkPublic单线程的" ,
         "puremvc-csharp-multicore-frameworkPublic多线程的",
        "https://github.com/spr1ngd/Unity.PureMVC/tree/master",//别人的一个点菜例子
		"https://blog.csdn.net/qq_29579137/article/details/73692842",//点菜例子的博文
        "https://blog.csdn.net/drgfd345/article/details/121918695" ,// UML画得好
		"https://cloud.tencent.com/developer/article/1337400" ,// UML画得好
	});


	public static (string, string,string) blog = ("","", "https://github.com/ldqk/Masuit.MyBlogs");
	public static (string, string,string) shaderlab = ("","", "https://github.com/FaithTong/UnityShaderLabTutorial");
	public static (string, string,string) WebSocket = ("","", "https://github.com/endel/NativeWebSocket");
	public static (string, string,string) UnityWebSocket = ("","", "https://github.com/psygames/UnityWebSocket");
	public static (string, string,string) SocketIO = ("","", "https://github.com/endel/NativeWebSockethttps://github.com/NetEase/UnitySocketIO");
	public static (string, string,string) 捏脸 = ("","", "https://github.com/huailiang/face-nn");
	public static (string, string,string) 七日杀 = ("","", "https://github.com/atom-chen/War");
	public static (string, string,string) UnityShader = ("","", "https://github.com/candycat1992/Unity_Shaders_Book");
	public static (string, string,string) UGUI = ("","", "https://github.com/Unity-Technologies/uGUI");
	public static (string, string,string) UniTask = ("","", "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask");
	public static (string, string,string) SiYuanYaHei = ("SiYuanYaHei.unitypackage", "报过版本错误.VS安装包", "");
#endif
}




