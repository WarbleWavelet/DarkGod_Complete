/****************************************************
    文件：ExtendRigidbody.cs
	作者：lenovo
    邮箱: 
    日期：2024/4/22 15:13:30
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
public static partial class ExtendRigidbody
{

    /// <summary>
    /// 运动学的
    /// <br/>
    /// true 刚体就不受重力,其它物体作用力影响,只受代码
    /// </summary>
    public static Rigidbody Kinematic(this Rigidbody rgb,bool _bool)
    { 
         rgb.isKinematic = _bool;//
        return rgb;
    }


    #region Velocity 
        /// <summary>给一个线性速度</summary>
    public static Rigidbody2D Velocity(this Rigidbody2D rgb, Vector2 v) 
    {
        rgb.velocity =v;
        return rgb;
    }

    /// <summary>给一个线性速度</summary>
    public static Rigidbody Velocity(this Rigidbody rgb, Vector2 v)
    {
        rgb.velocity = v;
        return rgb;
    }
    #endregion


}



