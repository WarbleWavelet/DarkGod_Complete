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
        PETool.LogMsg(msg, lv);
    }


    /// <summary>
    /// 战力 
    /// </summary>
    /// <param name="pd"></param>
    /// <returns></returns>
    public static int GetFightByProps(PlayerData pd)
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

    /// <summary>
    /// 升级所需经验
    /// </summary>
    /// <param name="lv"></param>
    /// <returns></returns>

    public static int GetExpUpValByLV(int lv)
    {
        return lv * lv * 100;
    }

    public static void CalcExp(PlayerData pd, int exp)
    {
        int remainExp = pd.exp + exp;
        while (remainExp >= PECommon.GetExpUpValByLV(pd.lv))
        {
            remainExp -= PECommon.GetExpUpValByLV(pd.lv);
            pd.lv += 1;
        }
        pd.exp = remainExp;
    }

    /// <summary>每5单位（看是分钟还是其它）加一次</summary> 
    public const int PowerAddSpace = 5;
    /// <summary>每次加2点体力</summary> 
    public const int PowerAddCount = 2;

    /// <summary>结束异常战斗的最小时间，数据测试，用作检验合法</summary>
    public const double EndBattleMinTime = 0d;


}

