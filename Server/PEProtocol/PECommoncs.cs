using PENet;
using PEProtocol;
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

    /// <summary>
    /// 战力 
    /// </summary>
    /// <param name="pd"></param>
    /// <returns></returns>
    public static int GetFightByProps( PlayerData pd)
    {
        return pd.lv * 100 + pd.ap + pd.ad + pd.addef + pd.apdef;

    }

    /// <summary>
    /// 每10级体力上限+150
    /// </summary>
    /// <param name="lv"></param>
    /// <returns></returns>

    public static int GetPowerLimit(int lv)
    {
        return ((lv - 1) / 10) * 150 + 150;
    }
}

