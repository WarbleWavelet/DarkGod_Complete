/****************************************************
    文件：ExtendMath.Quaternion.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/26 0:5:0
	功能：四元数
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
public static partial class ExtendMath_Quaternion //EQ互转,V转EQ
{
    public static void Example(Transform t)
    {
        //t.eulerAngles;
        //t.localRotation;
    }
    public static Quaternion XYZWToQuaternion(this float[] arr)
    {
        return new Quaternion(arr[0], arr[1], arr[2], arr[3]);
    }
    public static Quaternion V4ToQuaternion(this Vector4 e)
    {
        return new Quaternion(e.x,e.y,e.z,e.w);
    }
    public static Vector3 QuaternionToV3(this Quaternion q)
    { 
          return q.eulerAngles;
        //t.eulerAngles;
        //t.localRotation;
        // Quaternion.ToEulerAngles(q);

    }

    public static Quaternion XYZToQuaternion(this float[] arr)
    {
        return Quaternion.Euler(arr[0], arr[1], arr[2]);
        //Quaternion.EulerAngles(arr[0], arr[1], arr[2]);//过时,用Quaternion.Euler(v3);
        //Quaternion.EulerRotation(arr[0], arr[1], arr[2]); //过时,用Quaternion.Euler(x,y,z);
    }
   public static Quaternion V3ToQuaternion (this Vector3 v)
    {
        return  Quaternion.Euler(v);
        //Quaternion.EulerAngles(v3);//过时,用Quaternion.Euler(v3);
        //Quaternion.EulerRotation(v3); //过时,用Quaternion.Euler(v3);
    }
}



