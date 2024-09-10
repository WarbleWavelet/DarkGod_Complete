/****************************************************
    文件：ExtendAlgorithm.cs
	作者：lenovo
    邮箱: 
    日期：2023/9/10 16:10:36
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
 

public static partial class ExtendLayoutElement
{
    /// <summary>首选宽度</summary>
    public static void SetPreferredWidth(this LayoutElement layoutElement,float width)
    { 
        layoutElement.preferredWidth = width;
    }
    /// <summary>首选高度</summary>
    public static void SetPreferredHieight(this LayoutElement layoutElement, float height)
    {
        layoutElement.preferredHeight = height;
    }

}




