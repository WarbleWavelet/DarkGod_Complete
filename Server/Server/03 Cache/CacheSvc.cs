using PEProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 缓存层
/// </summary>

class CacheSvc
{

    #region 属性 字段
    /// <summary>账号在线</summary>
    Dictionary<string, ServerSession> onLineAcctDic = new Dictionary<string, ServerSession>();

    Dictionary<ServerSession, PlayerData> onLineSessionDic = new Dictionary<ServerSession, PlayerData>();

    private static CacheSvc _instance;
    public static CacheSvc Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CacheSvc();
            }
            return _instance;
        }

    }

    DBMgr dBMgr;
    #endregion


    public void Init()
    {
        PECommon.Log("Init CacheSvc");
        dBMgr = DBMgr.Instance;

    }

    public bool IsAcctOnLine(string acct)
    {
        return onLineAcctDic.ContainsKey(acct);
    }


    /// <summary>
    /// 没拿到就到DB取
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    public PlayerData GetPlayerData(string acct, string pass)
    {
        dBMgr.QueryPlayerData(acct,pass);
        return null;
    }

    /// <summary>
    /// 缓存起来
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="session"></param>
    /// <param name="pd"></param>
    public void AcctOnline(string  acct,ServerSession session, PlayerData pd)
    {
        onLineAcctDic.Add(acct,session);
        onLineSessionDic.Add(session,pd);
    }
}

