using System;
using System.ComponentModel;
using System.Linq;
using PENet;
using System.Collections.Generic;
/// <summary>
/// 网络通信协议（CS共用）
/// </summary>

namespace PEProtocol
{

    #region IP
    public class SrvCfg
    {
        public const string srvIp = "127.0.0.1";
        public const int srvPort = 17666;
    }
    #endregion

    #region PlayerData 来自数据库的表
    [Serializable]

    public class PlayerData
    {
        //含义看数据库
        //当前副本进度 
        // ID | 已经完成次数 | 是否已经领取奖励   
        public int id;
        public string name;
        public int exp;
        public int lv;
        public int power;
        public int coin;
        public int diamond;
        public int crystal;
        public int hp;
        public int ad;
        public int ap;
        public int addef;
        public int apdef;
        public int dodge;
        public int critical;
        public int pierce;
        public int guideid;
        public int[] strongArr;
        public long time;
        public string[] taskRewardArr;    
        public int instance; 
    }
    #endregion

    #region Serializable

    #region GameMsg族
    [Serializable]
    public class GameMsg_Text : PEMsg
    {
        public string text;

    }

    [Serializable]
    public class GameMsg : PEMsg
    {
        public ReqLogin reqLogin;
        public RspLogin rspLogin;
        public ReqRename reqRename;
        public RspRename rspRename;
        //主城
        public ReqGuide reqGuide;
        public RspGuide rspGuide;
        public ReqStrong reqStrong;
        public RspStrong rspStrong;
        public SndChat sndChat;
        public PshChat pshChat;
        public ReqBuy reqBuy;
        public RspBuy rspBuy;
        public PshPower pshPower;
        public ReqTakeTaskReward reqTakeTaskReward;
        public RspTakeTaskReward rspTakeTaskReward;
        public PshTaskPrgs pshTaskPrgs;
        //副本
        public ReqInstanceFight reqInstanceFight;
        public RspInstanceFight rspInstanceFight;
        public ReqInstanceFightEnd reqInstanceFightEnd;
        public RspInstanceFightEnd rspInstanceFightEnd;



    }

    /// <summary>
    /// 命令集
    /// </summary>
    public enum CMD
    {
        None = 0,
        ReqLogin = 101,
        RspLogin = 102,
        ReqRename = 103,
        RspRename = 104,
        //主城
        ReqGuide = 201,
        RspGuide = 202,
        ReqStrong = 203,
        RspStrong = 204,
        SndChat = 205,
        PshChat = 206,
        ReqBuy = 207,
        RspBuy = 208,
        [Description("体力计时增加")]
        PshPower =209,
        ReqTakeTaskReward = 210,
        RspTakeTaskReward = 211,
        [Description("任务进度")]
        PshTaskPrgs = 212,
        //副本
        ReqInstanceFight=301,
        RspInstanceFight=302,
        ReqInstanceFightEnd = 303,
        RspInstanceFightEnd = 304
    }

    /// <summary>
    /// 错误码
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>没错误</summary>
        None = 0,

        /// <summary>账号已被登录</summary>
        AcctIsOnLine,

        /// <summary>密码错误</summary>
        WrongPass,

        /// <summary>账号名已经存在</summary>
        NameIsExist,

        /// <summary>更新数据出错</summary>
        UpdateDBError,

        /// <summary>服务器数据异常</summary>
        ServerDataError,
        #region Lack 不足，缺少
        /// <summary>等级不足</summary>
        LackLv,
        /// <summary>金币不足</summary>
        LackCoin,
        /// <summary>钻石不足</summary>
        LackDiamond,
        /// <summary>水晶不足</summary>
        LackCrystal,
        [Description("体力不足")]
        LackPower,
        #endregion 

        /// <summary>密码精度不足</summary>
        /// 
        ClientDataError
    }
    #endregion


    #region GameMsg内部包括的类
    #region Login
    /// <summary>
    /// 请求登录
    /// </summary>
    [Serializable]
    public class ReqLogin
    {
        public string acct;
        public string pass;
    }

    /// <summary>
    /// 对请求登录的回复
    /// </summary>
    [Serializable]
    public class RspLogin
    {
        public PlayerData pd;
    }
    #endregion

    #region Rename
    [Serializable]
    public class ReqRename
    {
        public string name;
    }

    [Serializable]
    public class RspRename
    {
        public string name;
    }
    #endregion

    #region 任务引导
    [Serializable]
    public class ReqGuide
    {
        public int guideid;
    }

    [Serializable]
    public class RspGuide
    {
        public int guideid;
        public int exp;
        public int coin;
        public int lv;
    }
    #endregion

    #region 强化

    [Serializable]
    public class ReqStrong
    {
        public int pos;
    }

    [Serializable]
    public class RspStrong
    {
        public int coin;
        public int crystal;
        public int hp;
        public int ad;
        public int ap;
        public int addef;
        public int apdef;
        /// <summary>强化后变化</summary>
        public int[] strongArr;
    }
    [Serializable]
    public class ReqTakeTaskReward
    {
        public int id;
    }
    [Serializable]
    public class RspTakeTaskReward
    {
        public int id;
        public int lv;
        public int exp;
        public int coin;
        public string[] taskArr;
    }

    [Serializable]
    public class PshTaskPrgs
    {
        public string[] taskArr;
    }
    #endregion

    #region 聊天

    [Serializable]
    public class SndChat
    {
        public string chat;
    }


    [Serializable]
    public class PshChat
    {
        public string name;
        public string chat;
    }
    #endregion

    #region 副本
    [Serializable]
    public class ReqInstanceFight
    {
        public int instance;
    }

    [Serializable]
    public class RspInstanceFight
    {
        public int instance;
        public int power;
    }

    [Serializable]
    public class ReqInstanceFightEnd
    {
        public int instance;
        public bool isWin;
        /// <summary>耗HP</summary>
        public int remainHP;
        /// <summary>耗时</summary>
        public int costTime;
    }

    [Serializable]
    public class RspInstanceFightEnd
    {
        public int instance;
        public bool isWin;
        /// <summary>耗HP</summary>
        public int remainHP;
        /// <summary>耗时</summary>
        public int costTime;

        public int coin;
        public int crystal;
        public int lv;
        public int exp;
    }
    #endregion
    #endregion

    #region 交易

    [Serializable]
    public class ReqBuy
    {
        public BuyType buyType;
        public int buyCnt;
        public GoodType goodType;
        public int goodCnt;
    }

    [Serializable]
    public class RspBuy
    {
        public BuyType buyType;
        public int buyCnt;
        public GoodType goodType;
        public int goodCnt;
    }

    [Serializable]
    public class RspBuyLst
    {
        public List<RspBuy> list;
    }

    public enum BuyType
    {
        [Description("钻石")]
        DIAMOND,
        [Description("金币")]
        COIN,
        [Description("水晶")]
        CRYSTAL
    }

    public enum GoodType
    {
        [Description("体力")]
        POWER,
        [Description("金币")]
        COIN
    }

    #endregion

    #region     体力恢复
    [Serializable]
    public class PshPower
    {
        public int power;
    }
    #endregion

    #endregion


    public enum TaskState
    {
        [Description("未定义")]
        None,
        [Description("未接受")]
        UnAccept,
        [Description("接受")]
        Accept,
        [Description("完成")]
        Done,
        [Description("完成领完奖励")]
        Got
    }
}

#region      EnumExtension
/****************************************************
    文件：EnumExtension.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/19 12:42:6
	功能：给枚举以描述(https://blog.csdn.net/sd7o95o/article/details/87707384)
*****************************************************/


public static class EnumExtension
{
    public static string ToDes(this Enum val)
    {
        var type = val.GetType();
        var memberInfo = type.GetMember(val.ToString());
        var attr = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attr == null || attr.Length != 1)
        {
            return val.ToString();
        }

        return (attr.Single() as DescriptionAttribute).Description;
    }
}

/* 用法

public enum BuyType
{
    [Description("钻石")]
    DIAMOND,
    [Description("金币")]
    COIN
}

EnumExtension.ToDes(BuyType.DIAMOND)
*/
#endregion



