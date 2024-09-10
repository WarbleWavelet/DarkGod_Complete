/****************************************************
    文件：ExtendEncoding.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/21 20:58:22
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static partial class ExtendEncoding 
{
    public static void Test()
    {

        Debug.Log("".ByteCountShow());         
        Debug.Log("'".ByteCountShow());         
        Debug.Log("1".ByteCountShow());         
        Debug.Log("aa".ByteCountShow());         
        Debug.Log("AA".ByteCountShow());         
        Debug.Log("汉汉".ByteCountShow()); //B/C,3
        //
        Debug.Log("".CharCountShow());   //报错
        Debug.Log("'".CharCountShow());
        Debug.Log("1".CharCountShow());
        Debug.Log("aa".CharCountShow());
        Debug.Log("AA".CharCountShow());
        Debug.Log("汉汉".CharCountShow());

    }


    #region GetByteCount


    public static int ByteCount(this string str)
    {
        return Encoding.UTF8.GetByteCount(str);
            
    }

    public static string ByteCountShow(this string str)
    {
        return str+"\t"+Encoding.UTF8.GetByteCount(str);

    }
    #endregion


    #region GetCharCount


    public static int CharCount(this string str)
    {
        byte[] cs = Encoding.UTF8.GetBytes(str);
        return  Encoding.UTF8.GetCharCount(cs, 0, cs.Length);

    }

    /// <summary>
    /// bytes
    ///Byte[],包含要解码的字节序列的字节数组。
    ///index,第一个要解码的字节的索引。
    ///count,要解码的字节数。
    /// </summary>
    public static string CharCountShow(this string str)
    {
        byte[] cs = Encoding.UTF8.GetBytes(str);
        return str + "\t" + Encoding.UTF8.GetCharCount(cs,0,cs.Length);

    }
    #endregion

}



