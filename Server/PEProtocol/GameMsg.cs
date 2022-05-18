
using System;
using PENet;
/// <summary>
/// 网络通信协议（CS共用）
/// </summary>

namespace PEProtocol
{

    #region Serializable
    #region Root

    #endregion
    [Serializable]
    public class GameMsg_Text:PEMsg
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
        ReqGuide = 200,
        RspGuide = 201,
        ReqStrong = 202,
        RspStrong = 203,
        SndChat=204,
        PshChat=205
    }

    #region     GameMsg里的表
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
        public PlayerData playerData;
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
    #endregion

    #endregion




    #region PlayerData 来自数据库的表
    [Serializable]

    public class PlayerData
    {
        //含义看数据库
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

    #endregion


    public class SrvCfg
    {
        public const string srvIp = "127.0.0.1";
        public const int srvPort = 17666;
    }


   
    /// <summary>
    /// 错误码
    /// </summary>
    public enum ErrorCode
    {
         /// <summary>没错误</summary>
        None=0,

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
        #endregion 

        /// <summary>密码精度不足</summary>
    }
}
