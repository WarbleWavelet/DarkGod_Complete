/****************************************************
    文件：ExtendQuaternion.cs
	作者：lenovo
    邮箱: 
    日期：2024/4/22 16:6:5
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
public static partial class ExtendQuaternion 
{

    /// <summary>基于axis偏移offset的四元数
    /// <br/>射击角度偏移,弹壳掉落偏移</summary>
    public static Quaternion AngleBase(this Vector3 axis,float offset)
    {
        return  Quaternion.AngleAxis(offset,axis);
    }

}



