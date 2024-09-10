/****************************************************
    文件：ExtendColor.cs
	作者：lenovo
    邮箱: 
    日期：2023/8/12 19:31:50
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static partial class ExtendColor //16互转Color
{
    public static Color Hex2Color(this string hex)
    {
        if (hex.Length != 7 && !hex.Contains("#"))  //照理说末尾的/n应该是8
        {
            throw new System.Exception("异常:Hex2Color");
        }
        try
        {
            Color color;
            ColorUtility.TryParseHtmlString(hex, out color);
            //int len=hex.Length;
            return color;
        }
        catch (Exception)
        {
            throw new System.Exception("异常:Hex2Color");
        }
    }

    /// <summary></summary>
    public static string Color2Hex(this Color color)
    {
        try
        {
            string hex = ColorUtility.ToHtmlStringRGB(color);
            return hex;
        }
        catch (Exception)
        {
            throw new System.Exception("异常:Color2Hex");
        }
    }
}
public static partial class ExtendColor //透明
{

    public enum ERGBA
    {
        R, B, G, A
    }
}
public static partial  class ExtendColor
{
#if NET_4_7_2_OR_NEWER

    static void Example()
    {
        Color color = Color.blue;
        Console.WriteLine(color);
        color.SetAlpha(1);
        Console.WriteLine(color);
    }



    /// <summary>必须加ref，不然 01用中间变量Color或者 02直接赋值，都不能成功，跟Vector3有点像
    /// <para/> 加了ref，01用中间变量Color或者 02直接赋值都能成功。所以用直接复制最简单的</summary>
    public static Color SetAlpha(ref this Color color, float alpha)
    {
        color .a = alpha;

        return color;
    }

    public static Color SetR(ref this Color color, float r)
    {
        color.r = r;

        return color;
    }

    public static Color SetG(ref this Color color, float g)
    {
        color.g = g;

        return color;
    }

    public static Color SetB(ref this Color color, float b)
    {
        color.b = b;

        return color;
    }
#endif

}
public static partial  class ExtendColor
{
#if NET_4_7_2_OR_NEWER

    public static Color ToColor(this string htmlString )
    {

        #region 抄QF的
        /**
        public static Color HtmlStringToColor(this string htmlString)
        {
            var parseSucceed = ColorUtility.TryParseHtmlString(htmlString, out var retColor);
            return parseSucceed ? retColor : Color.black;
        }
        **/

        #endregion
        var parseSucceed = ColorUtility.TryParseHtmlString(htmlString, out var retColor);
        return parseSucceed ? retColor : Color.black;
    }
#endif


}



