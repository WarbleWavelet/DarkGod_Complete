using PENet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



/// <summary>
/// 日志类型
/// </summary>
public enum LogType
{
    Log=0,
    Warn=1,
    Error=2,
    Info=4
}

/// <summary>
/// CS共用工具
/// </summary>
public class PECommon
{
    public static void Log(string msg = "", LogType tp = LogType.Log)
    {
        LogLevel lv = (LogLevel)tp;
        PETool.LogMsg(msg,lv);
    }
}

