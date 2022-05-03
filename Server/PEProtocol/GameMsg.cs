
using System;
using PENet;
/// <summary>
/// 网络通信协议（CS共用）
/// </summary>

namespace PEProtocol
{
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

    public class SrvCfg
    {
        public const string srvIp = "127.0.0.1";
        public const int srvPort = 17666;
    }

    public class PlayerData
    {
        public int id;
        public string name;
        public int exp;
        public int lv;
        public int power;
        public int coin;
        public int diamond;

    }


    /// <summary>
    /// 命令集
    /// </summary>
    public enum CMD
    {
        None=0,
        ReqLogin=101,
        RspLogin =102,
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
        /// <summary>密码精度不足</summary>
    }
}
