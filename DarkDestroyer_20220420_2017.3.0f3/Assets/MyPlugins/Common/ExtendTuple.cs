/****************************************************
    文件：ExtendTuple.cs
	作者：lenovo
    邮箱: 
    日期：2023/8/28 14:39:5
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static partial class ExtendTuple 
{

    static bool _unity = true;
    public static void Example()
    {
        _unity = true;
        一行代码交换两个值();
    }



    static void 一行代码交换两个值()
    {

        string str1 = "余化龙";
        string str2 = "何元庆";
        Log($"{str1},{str2}");
  #if NET_4_7_OR_NEWER
        (str1, str2) = (str2, str1);
#endif
        Log($"{str1},{str2}");

    }
    #region 辅助
    static void Log(string str)
    {
        if (_unity)
        {

            Debug.LogFormat(str);//支持$
        }
        else
        {
            Console.WriteLine(str);
        }
    }
    #endregion

}




