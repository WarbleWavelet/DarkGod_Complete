/****************************************************
    文件：ExtendCharacter.cs
	作者：lenovo
    邮箱: 
    日期：2024/1/10 17:16:18
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>人物常用</summary>
public static class ExtendCharacter
{

    #region Injure    

#if NET_7_3_OR_NEWER
    /// <summary></summary>
    public static float Injure(ref this float cur, float change, Action injureAction,Action deadAction)
    {
        if (cur <= 0)
        {
            cur = 0;
            return cur;
        }



        var after = cur - change;
        if (after <= 0)
        {
            after = 0;
            deadAction();
        }
        else
        {
            injureAction();
        }
        cur = after;
        return cur;
    }
    public static int Injure(ref this int cur, int change, Action injureAction, Action deadAction)
    {
        if (cur <= 0)
        {
            cur = 0;
            return cur;
        }



        var after = cur - change;
        if (after <= 0)
        {
            after = 0;
            deadAction();
        }
        else
        {
            injureAction();
        }
        cur = after;
        return cur;
    }
#endif

#endregion


}




