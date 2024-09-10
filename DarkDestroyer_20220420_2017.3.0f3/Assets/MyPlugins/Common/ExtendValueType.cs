/****************************************************
    文件：ExtendValueType.cs
	作者：lenovo
    邮箱: 
    日期：2024/3/31 17:16:9
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendValueType  //对称
{
    /// <summary>对称</summary>
    public static float Symmetry(this float from, float middle)
    {
        float offset=(from-middle).Abs();
        if (from > middle)
        {
            return middle - offset;
        }
        else if (from < middle)
        {
            return middle + offset;
        }

        return middle;
    }

    public static double Symmetry(this double from, double middle)
    {
        return (double)from.Symmetry(middle);
    }

    public static int Symmetry(this int from, int middle)
    {
        return (int)from.Symmetry(middle);
    }

}
public static partial class ExtendValueType  //Between
{
    public static bool Between(this float  para, float min, float max)
    {
        if (min == max)
        {
            return false;
        }
        if (min > max)
        {
            float tmp = min;
            min = max;
            max = tmp;
        }

        if (para >= min && para <= max)
        { 
            return true;
        }
        return false;

    }

    public static bool Between(this int para, int min, int max)
    {
        return ((float)para).Between(min, max);
    }
}



public static partial class ExtendValueType  //TO
{
    public static T To<T>(this object o) where T : IConvertible 
    {
        T after;
        try
        {
             after = (T)o;
        }
        catch (Exception)
        {
            Debug.LogErrorFormat($"To<{typeof(T)}>失败。处理对象为：" + o);
        }
        return (T)(typeof(T).DefaultForType()); 
    }
}
public static partial class ExtendValueType  //Get
{
    /// <summary>
    /// </summary>
    public static float[] ToFloatArray(this double[] ds)
    {
        float[] fs = new float[ds.Length];
        for (int i = 0; i < ds.Length; i++)
        {
            fs[i] = (float)ds[i];
        }
        return fs;
    }

    public static double[] ToDoubleArray(this float[] fs)
    {
        double[] ds = new double[fs.Length];
        for (int i = 0; i < fs.Length; i++)
        {
            ds[i] = (double)fs[i];
        }
        return ds;
    }

    /// <summary>是Int就返回,不是就报错</summary>
    public static int GetInt(this object o)
    {
        if (o.GetType() == typeof(int))
        {
            return (int)o;
        }
        else
        {
            Debug.LogError("GetInt失败。当前值不是int类型，值为：" + o);
            return 0;
        }
    }


    /// <summary>尝试的
    /// <para/> float 必须加 f，double必须阿加d。
    /// <para/> 不能用枚举的TryParse的情况:
    /// <br/> string l = os[0].Get &lt; string &gt;();
    /// <br/> Debug.Log("XXXXXX"+l);//打印只有XXXXXX
    /// <br/> if (Enum.TryParse(os[0].Get&lt; string &gt;((), out type))  // 这种不行
    /// </summary>
    public static T Get<T>(this object o) where T : IConvertible
    {
        if (o.GetType() == typeof(T))
        {
            return (T)o;
        }
        else
        {
            Debug.LogErrorFormat($"Get<{typeof(T)}>失败。处理对象为：" + o);
            return (T)(typeof(T).DefaultForType());
        }
    }
}



