/****************************************************
    文件：LogExtension.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/30 16:59:40
	功能：QF的
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public enum LogLevel
{
    None,
    Exception,
    Error,
    Warning,
    Normal,
    Max
}

public static class Log
{
    private static LogLevel mLogLevel = LogLevel.Normal;

    public static LogLevel Level
    {
        get
        {
            return mLogLevel;
        }
        set
        {
            mLogLevel = value;
        }
    }

    public static void LogInfo(this object selfMsg)
    {
        I(selfMsg);
    }

    public static void LogWarning(this object selfMsg)
    {
        W(selfMsg);
    }

    public static void LogError(this object selfMsg)
    {
        E(selfMsg);
    }

    public static void LogException(this Exception selfExp)
    {
        E(selfExp);
    }

    public static void I(object msg, params object[] args)
    {
        if (mLogLevel >= LogLevel.Normal)
        {
            if (args == null || args.Length == 0)
            {
                Debug.Log(msg);
            }
            else
            {
                Debug.LogFormat(msg.ToString(), args);
            }
        }
    }

    public static void E(Exception e)
    {
        if (mLogLevel >= LogLevel.Exception)
        {
            Debug.LogException(e);
        }
    }

    public static void E(object msg, params object[] args)
    {
        if (mLogLevel >= LogLevel.Error)
        {
            if (args == null || args.Length == 0)
            {
                Debug.LogError(msg);
            }
            else
            {
                Debug.LogError(string.Format(msg.ToString(), args));
            }
        }
    }

    public static void W(object msg)
    {
        if (mLogLevel >= LogLevel.Warning)
        {
            Debug.LogWarning(msg);
        }
    }

    public static void W(string msg, params object[] args)
    {
        if (mLogLevel >= LogLevel.Warning)
        {
            Debug.LogWarning(string.Format(msg, args));
        }
    }
}




