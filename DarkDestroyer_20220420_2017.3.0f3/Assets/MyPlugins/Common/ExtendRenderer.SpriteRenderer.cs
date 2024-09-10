/****************************************************
    文件：ExtendRenderer.SpriteRenderer.cs
	作者：lenovo
    邮箱: 
    日期：2024/3/31 16:39:9
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static partial class ExtendSpriteRenderer
{
    /// <summary>这种必须用中间变量，因为sr.color.a不允许被引用</summary>
    public static SpriteRenderer SetAlpha(this SpriteRenderer sr,float a)
    {
        Color tmp=sr.color;
        tmp.a = a;
        sr.color = tmp;

        return sr;
    }

    public static SpriteRenderer SetR(this SpriteRenderer sr, float r)
    {
        Color tmp = sr.color;
        tmp.r = r;
        sr.color = tmp;

        return sr;
    }
    public static SpriteRenderer SetG(this SpriteRenderer sr, float g)
    {
        Color tmp = sr.color;
        tmp.g = g;
        sr.color = tmp;

        return sr;
    }
    public static SpriteRenderer SetB(this SpriteRenderer sr, float b)
    {
        Color tmp = sr.color;
        tmp.b = b;
        sr.color = tmp;

        return sr;
    }

}



