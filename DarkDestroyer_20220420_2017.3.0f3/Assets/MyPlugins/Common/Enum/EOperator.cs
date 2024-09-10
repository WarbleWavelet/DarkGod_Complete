/****************************************************
    文件：EOperator.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/7 14:54:0
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary></summary>

/// <summary>运算符与优先级</summary>
public enum EOperator
{
    /// <summary>()</summary>
    括号运算符 = 1,
    /// <summary>~</summary>
    位逻辑运算符 = 2,


    /// <summary>
    /// &lt;&lt;
    /// 乘以2的n次方
    /// </summary>
    左移运算符 = 5,
    /// <summary>
    /// &gt;&gt;
    /// 除以2的n次方
    /// </summary>
    右移运算符 = 5,


    /// <summary> &amp; </summary>
    AND = 8,
    /// <summary>^</summary>
    XOR = 9,
    /// <summary>|</summary>
    OR = 10,


}
                        




