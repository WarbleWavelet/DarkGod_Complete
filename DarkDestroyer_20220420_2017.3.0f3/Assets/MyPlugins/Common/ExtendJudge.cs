/****************************************************
    文件：ExtendLitJson.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/24 22:32:11
	功能：
*****************************************************/

using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;
using System.IO;

public static partial class ExtendJudge
{



    public static void Example()
    {
    }


    /// <summary>三者相等</summary>
    internal static bool IsAllEquals(int a, int b, int c)
    {
        return a == b && b == c;
    }

    /// <summary>


    /// </summary>

    private static int CompareTo(this string strA,string strB)
    {
        /**
            string strA = "l";
            string strB = "B";
            int re = strA.CompareTo(strB);
            若strA大于strB返回1
            若strA小于strB返回-1
            若strA等于strB返回0
        **/
        return strA.CompareTo(strB);
    }

}


    public static partial class ExtendJudge
    {
        public static bool IsAllNull(params object[] paras)
        {
            bool res = paras[0].IsNull();

            for (int i = 0; i < paras.Length; i++)
            {
                res = res && paras[i].IsNull();
            }
            return res;
        }
        public static bool IsAllFull(params object[] paras)
        {
            bool res = paras[0].IsNotNull();

            for (int i = 0; i < paras.Length; i++)
            {
                res = res && paras[i].IsNotNull();
            }
            return res;
        }

        /// <summary>只要有一个为null</summary>
        public static bool IsOnceNull(params object[] paras)
        {
            bool res = paras[0].IsNull();

            for (int i = 0; i < paras.Length; i++)
            {
                res = res || paras[i].IsNull();
            }
            return res;
        }

    /// <summary>只要有一个为null</summary>
    public static bool IsOnceNullObject(params UnityEngine.Object[] paras)
    {
        bool res = paras[0].IsNullObject();

        for (int i = 0; i < paras.Length; i++)
        {
            res = res || paras[i].IsNullObject();
        }
        return res;
    }
    /// <summary>只要有一个不为null</summary>
    public static bool IsOnceFull(params object[] paras)
        {
            bool res = paras[0].IsNotNull();

            for (int i = 0; i < paras.Length; i++)
            {
                res = res || paras[i].IsNotNull();
            }
            return res;
        }

    }



