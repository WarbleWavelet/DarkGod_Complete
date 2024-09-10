/****************************************************
    文件：ExtendUI.IMGUI.cs
	作者：lenovo
    邮箱: 
    日期：2023/9/11 17:37:45
	功能：IMGUI
        偏向例子，不想static
        挂在细节器上的
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public   class ExtendGUILayout
{
    public static void Example()
    {
        GUILayoutOption();
        Area();
        Horizontal();
        DefaultVertical();
    }

    /// <summary>放在Button等的最后面参数</summary>

   public static void GUILayoutOption()
    {
        GUILayout.Height(100)  ;
        GUILayout.Width(100);
        GUILayout.MinHeight(50);
        GUILayout.MinWidth(50);
        GUILayout.MaxHeight(200);
        GUILayout.MaxWidth(200);
        GUILayout.ExpandHeight(false);
        GUILayout.ExpandWidth(false);
    }


    public static void Area()
    {
        GUILayout.BeginArea(new Rect(100,100,100,100));
        GUILayout.BeginHorizontal();
        GUILayout.Button("123");
        GUILayout.Button("123");
        GUILayout.Button("123");
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    public static void Horizontal()
    { 
        GUILayout.BeginHorizontal();
        GUILayout.Button("123");
        GUILayout.Button("123");
        GUILayout.Button("123");
        GUILayout.EndHorizontal();
    }
    public static void DefaultVertical()
    {
        GUILayout.Button("123");
        GUILayout.Button("123");
        GUILayout.Button("123");
    }


}



