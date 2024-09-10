/****************************************************
    文件：ExtendTime.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/21 22:32:18
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

    public static partial class ExtendTime
    {
    public static float TotalTime()
    {
        return Time.realtimeSinceStartup;
    }
    public static double TotalTimeAsDouble()
    {
#if NET_4_6
        return 0;
#else
return Time.realtimeSinceStartupAsDouble;
#endif
    }

    public static int TotalFrame()
    {
        return Time.renderedFrameCount;
    }
}




