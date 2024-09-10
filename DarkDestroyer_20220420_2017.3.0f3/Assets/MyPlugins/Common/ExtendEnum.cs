/****************************************************
    文件：ExtendEnum.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/24 18:0:35
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;


public static partial class ExtendEnum  //转来转去
{

#if NET_4_7_3_OR_NEWER
    /// <summary>判断一个枚举是否过时加了Obsolete标签</summary>
    public static bool IsObsolete<T>(this T t) where T:System.Enum 
    {
        string name = t.Enum2String();
        FieldInfo fi = typeof(T).GetField(name);
        bool isObsolete = true;
        if (fi != null)
        { 
             isObsolete = Attribute.IsDefined(fi, typeof(ObsoleteAttribute));
        }
         
        return isObsolete;
    }
#endif

}
public static partial class ExtendEnum  //转来转去
{

    /// <summary></summary>
     enum ExtendEnum_Dir
    {
        //[InspectorName("上")] Up = 0,
        //[InspectorName("下")] Don = 1,
        //[InspectorName("左")] Left = 2,
        //[InspectorName("右")] Right = 3,
        /// <summary></summary>
        Up = 0,
        /// <summary></summary>
        Don = 1,
        /// <summary></summary>
        Left = 2,
        /// <summary></summary>
        Right = 3,
    }

     enum ExtendEnum_XXX
    {
        A=0,
        B=1,
        C=2,                                               

    }
  
    public static void Example()
    {
        ExtendEnum_XXX para = ExtendEnum_XXX.A;
        string str = para.ToString();
        int idx = (int)para;
        Debug.LogFormat("String2Enum：({0}){1}" 
            , str.String2Enum<ExtendEnum_XXX>().GetType()
            , str.String2Enum<ExtendEnum_XXX>()  );
        Debug.LogFormat("Index2Enum：({0}){1}"
            , idx.Int2Enum<ExtendEnum_XXX>().GetType()
            , idx.Int2Enum<ExtendEnum_XXX>()  );
        //      
        Debug.LogFormat("String2Index：({0}){1}"
            , str.String2Int<ExtendEnum_XXX>().GetType()
            , str.String2Int<ExtendEnum_XXX>()  );
        Debug.LogFormat("Enum2Index：({0}){1}" 
            , para.Enum2Int().GetType()
            , para.Enum2Int()  );
        //      
        Debug.LogFormat("Index2String：({0}){1}" 
            , idx.Int2String<ExtendEnum_XXX>().GetType()
            , idx.Int2String<ExtendEnum_XXX>()  );
        Debug.LogFormat("Enum2String：({0}){1}" 
            , para.Enum2String().GetType()
            , para.Enum2String()  );
    }


    /// <summary>字符串转枚举</summary>
    public static T String2Enum<T>(this string str) //where T:Enum
    {
        T t;
        try
        {
            t = (T)Enum.Parse(typeof(T), str);
        }
        catch (Exception)
        {
            throw new System.Exception($"String2Enum异常:{str}");
        }

          return t;
    }
    /// <summary.值转枚举</summary>
    public static T Int2Enum<T>(this int idx)// where T : Enum
    {
       string str= Enum.GetName(typeof(T), idx);
        return str.String2Enum <T>();
    }


    public static int String2Int<T>(this string str)// where T : Enum
    {
        T t= (T)Enum.Parse(typeof(T), str);
        return (int)Enum.Parse(typeof(T), str);
    }

    public static int Enum2Int<T>(this T t) //where T : Enum
    {
        string str= t.ToString();
        return (int)Enum.Parse(typeof(T), str);
    }

    public static string Int2String<T>(this int idx) //where T : Enum
    {
        return Enum.GetName(typeof(T), idx);
    }

    public static string Enum2String<T>(this T t)// where T : Enum
    {
        return t.ToString();
    }


}




