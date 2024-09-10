/****************************************************
    文件：ExtendLinearAlgebraByNumerics.cs
	作者：lenovo
    邮箱: 
    日期：2024年7月11日
	功能： 尝试用 ExtendLinearAlgebra.IMartix来封装System.Numerics的相关Martix,行不通
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
#if !NET_4_6
using System.Numerics;
using Vector3 = System.Numerics.Vector3;


public static partial class ExtendLinearAlgebraByNumerics 
{
     interface IMartix4x4
    {
        /// <summary>转置</summary>
        Vector3 Translation();
    }


     class Martix : IMartix4x4
    {
       //缺点,只能4*4
        System.Numerics.Matrix4x4 M44;
        System.Numerics.Matrix3x2 M32;

        public Vector3 Translation()
        {
            throw new System.NotImplementedException();
        }
    }


}

#endif

