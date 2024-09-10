/****************************************************
    文件：ExtendDateTime.cs
	作者：lenovo
    邮箱: 
    日期：2024/1/29 19:20:14
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using Random = UnityEngine.Random;


public static partial class ExtendDateTime //时间戳与DateTime   //https://www.jb51.net/article/232705.htm
{
    public static int DateTime2TimeStamp(this DateTime now)
    {
        int ts = Convert.ToInt32((now - TheFirstYearOfAnEra()).TotalSeconds);
        return ts;
    }


    public static DateTime TimeStamp2DateTime(this long timeStamp)
    { 
         DateTime preDateTime =  TimeZone.CurrentTimeZone.ToLocalTime(TheFirstYearOfAnEra());
        long nowTimeStamp = timeStamp * 10000000;
        TimeSpan nowTimeSpan=new TimeSpan(nowTimeStamp);
        DateTime newDateTime = preDateTime.Add(nowTimeSpan);
        return newDateTime;


    }

    public static DateTime TimeStamp2DateTime(this int timeStamp)
    {
        long after = (long)timeStamp;
        return timeStamp.TimeStamp2DateTime();
    }

    public static DateTime TimeStamp2DateTime(this string timeStamp)
    {
        long after = long.Parse(timeStamp);
        return timeStamp.TimeStamp2DateTime();
    }

    static DateTime TheFirstYearOfAnEra()
    {
        return new DateTime(1970, 1, 1, 8, 0, 0);
    }

}
public static partial class ExtendDateTime 
{
    static void A()
    {

        Console.WriteLine(DateTime.Now);        //2024/1/29 19:21:15
        Console.WriteLine(DateTime.MinValue);   //0001/1/1 0:00:00
        Console.WriteLine(DateTime.MaxValue);   //9999/12/31 23:59:59
        Console.WriteLine(DateTime.UtcNow);     //2024/1/29 11:22:22
    }

}



