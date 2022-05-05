
using System;
using PENet;
/// <summary>
/// 网络通信协议（CS共用）
/// </summary>

namespace PEProtocol
{

    #region 序列化
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



    }

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
        public int hp;
        public int ad ;
        public int ap;
        public int addef;
        public int apdef;
        public int dodge;
        public int critical;
        public int pierce;

    }


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


    public class SrvCfg
    {
        public const string srvIp = "127.0.0.1";
        public const int srvPort = 17666;
    }


   


    /// <summary>
    /// 命令集
    /// </summary>
    public enum CMD
    {
        None=0,
        ReqLogin=101,
        RspLogin =102,
        ReqRename=103,
        RspRename=104
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
        /// <summary>密码精度不足</summary>
    }
}
