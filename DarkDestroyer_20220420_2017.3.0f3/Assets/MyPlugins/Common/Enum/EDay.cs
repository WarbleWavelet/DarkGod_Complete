/****************************************************
    文件：EDay.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/7 14:14:39
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//多选枚举的格式
//01[Flags]
//02None=0,
//03枚举值为2的次方(位运算)
/// <summary></summary>
[System.Flags]    
public enum EDay
{
    /// <summary>0</summary>
    None = 0,
    /// <summary>1</summary>
    Mon = 1<<0,
    /// <summary>2</summary>
    Tue = 1<<1,
    /// <summary>4</summary>
    Wed = 1<<2,
    /// <summary>6</summary>
    Thu = 1 << 3,
    /// <summary>16</summary>
    Fri = 1 << 4,
    /// <summary>32</summary>
    Sat = 1 << 5,
    /// <summary>64</summary>
    Sun = 1 << 6
}



