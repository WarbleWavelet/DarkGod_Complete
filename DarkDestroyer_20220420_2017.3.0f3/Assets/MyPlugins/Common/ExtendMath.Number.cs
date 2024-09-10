/****************************************************
    文件：ExtendMath.Number.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/21 15:44:15
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static partial class ExtendMathNumber   // 接近0归0
{

    /// <summary>比如0.00001f,小于offset0.001f,就直接等于to0</summary>
    public static float TendTo(this float n,float to=0f, float offset=0.001f)
    {
        float realOffset = (n - to).Abs();
        if (realOffset <= offset)
        {
            return to;
        }
        else
        {
            return n;
        }
    }
}
public static partial class ExtendMathNumber
{
      #if NET_4_7_OR_NEWER
    /// <summary>float 包括max</summary>
    public static float RR(this (float, float) os)
    {
        return UnityEngine.Random.Range(os.Item1, os.Item2);
    }

    /// <summary>int 不包括max</summary>
    public static int RR(this (int,int) os)
    {
        return UnityEngine.Random.Range(os.Item1, os.Item2);
    }
#endif
}

public static partial class ExtendMathNumber //取反
{
    public static float Inverse( this float num)
    {
        float res = -num;
        return res;
    }

}
public static partial class ExtendMathNumber //倒数
{


    /// <summary>倒数</summary>
    public static float Reciprocal(this int num)
    {
        return 1.0f / num;
    }

    /// <summary>倒数</summary>
    public static float Reciprocal(this float num)
    {
        return 1.0f / num;
    }

    /// <summary>倒数</summary>
    public static double Reciprocal(this double num)
    {
        return 1.0d / num;
    }

    public static float 倒数(this float num)
    {
        return 1.0f / num;
    }
}
public static partial class ExtendMathNumber //一轮
{
    /// <summary>一轮后重新计算
    /// 比如12生肖,13年后还是1</summary>
    public static int Round(this int num, int round)
    { 
           return num%round; 
    }
}
public static partial class ExtendMathNumber //最小最小倍数
{
    /// <summary>补足,返回factor的倍数,大于等于num的factor的最小倍数</summary>
    public static int MultipleMore(this int num, int factor)
    {
        if (num < factor)
        {
            return factor;
        }
        else
        {
            if (num % factor == 0)
            {
                return num;
            }
            else
            {
                return (num / factor) * factor + factor; //7=>8,9=>12
            }
        }               
    }

    /// <summary>砍掉,返回factor的倍数,小于等于num的factor的最大倍数</summary>
    public static int MultipleLess(this int num, int factor)
    {
        if (num < factor)   //比如(3,4)=>1, (4,5)=> 1
        {

            return 1;
        }
        else
        {
            if (num % factor == 0)
            {
                return num;
            }
            else
            {
                return (num / factor) * factor ; //(7,4)=>4,(9,4)=>8
            }
        }
    }

}
public  static partial class ExtendMathNumber 
{
    /// <summary>数学中数的类型</summary>
    public enum ENumberType
    {
        /// <summary>非负整数,包括0</summary>
        自然数,
        /// <summary> 紧接某个自然数后面的一个数，如2的后继数是3</summary>
        后继数
    }
   
}



