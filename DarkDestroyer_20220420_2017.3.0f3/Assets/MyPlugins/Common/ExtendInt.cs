/****************************************************
    文件：ExtendInt.cs
	作者：lenovo
    邮箱: 
    日期：2024/4/22 20:35:17
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static partial class ExtendInt 
{

    /// <summary>偶数</summary>
    public static bool IsEven(this int value)
    {
        return !value.IsOdd();
    }

    /// <summary>奇数</summary>
    public static bool IsOdd(this int value)
    {
        return value % 2 == 1;
    }

}



