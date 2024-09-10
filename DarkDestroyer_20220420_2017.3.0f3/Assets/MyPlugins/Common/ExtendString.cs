/****************************************************
    文件：ExtendString.cs
	作者：lenovo
    邮箱: 
    日期：2023/8/4 22:4:12
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendString
{ 
    //string.IsNullOrEmpty个判断字符串是否为空引用或者值为空的方法

}
public static partial class ExtendString
{
    /// <summary>头一个字母大写</summary>
    public static string UpperFirstLetter(this string str)
    {
        string first = str.ToString().Substring(0, 1).ToUpper();
        string others = str.ToString().Substring(1);
        return first + others;
    }

    /// <summary>头一个字母小写</summary>
    public static string LowerFirstLetter(this string str)
    {
        string first = str.ToString().Substring(0, 1).ToLower();
        string others = str.ToString().Substring(1);
        return first + others;
    }

    /// <summary>以tagStr为开头截取后面的字符串（包括tarString）</summary>
    public static string SubStringStartWith( this string str,string  tagStr= StringMark.Assets)
    {
        List<string> strs=  str.Split('/').ToList();
        int startIdx = strs.IndexOf(tagStr);
        string res = "";
        for (int i = startIdx; i < strs.Count; i++)
        {
            res += strs[i]+"/"; 
        }
       res= res.Remove( res.LastIndexOf('/'));
        return res;
    }
    public static bool ToBool(this string str)
    {
        if (str == "true")
        { 
            return true;
        }
        else if (str == "false")
        {
            return false;
        }


        throw new System.Exception($"ToBool()异常:{str}");
    }
}




