/****************************************************
    文件：SBug.cs
	作者：lenovo
    邮箱: 
    日期：2024/6/19 20:30:59
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
public static class SBug
{

    public static void IndexOutside(string detail)
    {
        throw new System.Exception("异常索引超出长度:"+detail);
    }
}



