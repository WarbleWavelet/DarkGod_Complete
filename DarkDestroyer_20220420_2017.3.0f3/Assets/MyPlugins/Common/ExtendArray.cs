/****************************************************
    文件：ExtendArray.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/18 22:12:42
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendArray
{
    /// <summary>每一项执行相同操作</summary>
    public static float[] ForeachAdd(this float[] arr,float para)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] += para;
        }
        return arr;
    }

    /// <summary>每一项都作为参数传进去执行相同操作</summary>
    public static float[] ForeachAction(this float[] arr, Action<float> cb)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            cb(arr[i]);
        }
        return arr;
    }
}


public static partial class ExtendArray //带长度
{




    #region 生命

    public static void Example()
    {
        Example01();
        Example02();
        Example03();

    }

    public static void Example01()
    {
        int[] arr0 = new int[]{ };
        int[] arr1 = new int[5];
    }
    public static void Example02()
    { 
        int[,] arr0 = new int[2, 3]
        {
            {1,2,3}, //第0行的数据
            {4,5,6} //第1行的数据
        };


        int[,] arr1 = 
        {
            {1,2,3},
            {4,5,6}
        };    
    }
    public static void Example03()
    {
        float[][] arr1 = new float[3][] { new float[2] { 1, 1 }, new float[2] { 2, 2 }, new float[2] { 3, 3 } };
    }


    #endregion



    #region 辅助

    /// <summary>
    /// 缺点：
    /// <para />01 新建了一份List数据到内存
    /// <para />注意：
    /// <para />01 得先判断
    /// </summary> 
    static void ToList_Example(this object[] objArr)
    {
        objArr.ToList();
    }

    public static int[,] Foreach(this int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Debug.Log(array[i, j]);
            }
        }

        return array;
    }
    #endregion

}

public static partial class ExtendArray
{
    //public static Dictionary<T1, T> Arr2Dic<T1>(this T[] arr) where T:new(),T1
    //{
    //   Dictionary<T1, T> Dic = new Dictionary<T1, T>();
    //    foreach (T item in arr)
    //    {
    //        Dic.Add(T1,item);
    //    }
    //    return Dic;
    //}
}




