/****************************************************
    文件：ScenePath.cs
	作者：lenovo
    邮箱: 
    日期：2024/6/20 20:4:22
	功能： 必要.ScenePath 也可能是枚举名,
            St=Static,防止跟sealed混了.
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
public static class StScenePath 
{

    public static string Scene_Main = "Scene/Main";
    public static string Scene_Game = "Scene/Game";
}

public static class StSceneName
{
    public static string Main = "Main";
    public static string Game = "Game";
}

public static class StSceneIndex
{
    public static int Main = 0;
    public static int Game = 1;
}



