/****************************************************
    文件：EDir.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/23 21:20:53
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>丰富但不规则,所以别想用数字</summary>
public enum EDir 
{
    None,
   NONE,
   NULL,
   LEFT, RIGHT,TOP,BOTTOM,LEFTTOP,LEFTBOTTOM,RIGHTTOP,RIGHTBOTTOM,CENTER,//锚定常用词语
   Left, Right,Top,Bottom,LeftTop,LeftBottom,RightTop,RightBottom,Center,//锚定常用词语
   UP,DOWN, Up, Down,
    //
    FORWARD,BACK,
    /// <summary>在前面</summary>
    FRONT,
    /// <summary>在后面</summary>
    BEHIND,
   //
   EAST, WEST, SOUTH,NORTH,EASTSOUTH,EASTNORTH,WESTSOUTH,WESTNORTH,MIDDLE,

    //
    /// <summary>顺时针</summary>
    CLOCKWISE,
    /// <summary>逆时针</summary>
    CONTRACLOCKWISE,
    /// <summary>逆时针</summary>
    COUNTERCLOCKWISE,
    /// <summary>逆时针</summary>
    ANTICLOCKWISE,
    /// <summary>正时针,自己造的词,表示还在原方向</summary>
    MIDDLECLOCKWISE ,
    //
    //点积叉积时用到
    /// <summary>相同</summary>
    SAME,
    /// <summary>相反</summary>
    OPPOSITE,
    /// <summary>垂直</summary>
    VERTICAL, 
}




