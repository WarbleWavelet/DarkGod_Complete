using MySql.Data.MySqlClient;
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
    /// <summary>acct与session账号在线</summary>
    Dictionary<string, ServerSession> onLineAcctDic = new Dictionary<string, ServerSession>();
    /// <summary>session与pd</summary>

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

    #region Login的情况
    public bool IsAcctOnLine(string acct)
    {
        return onLineAcctDic.ContainsKey(acct);
    }


    /// <summary>
    /// name已存在
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsNameExist(string name)
    {
        return dBMgr.QueryNameData(name);
    }
    #endregion

    #region PlayerData
    public PlayerData GetPlayerDataBySession(ServerSession session)
    {
        if (onLineSessionDic.TryGetValue(session, out PlayerData pd))
        {
            return pd;
        }
        else
        {
            return null;
        }
    }


 /// <summary>
    /// 没拿到就到DB取
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    public PlayerData GetPlayerData(string acct, string pass)
    {
        return dBMgr.QueryPlayerData(acct, pass);
    }

    /// <summary>
    /// 根据Id更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pd"></param>
    public bool UpdatePlayerData(int id, PlayerData pd)
    {
        return dBMgr.UpdatePlayerData(id, pd);
    }
    #endregion

    #region Acct
    /// <summary>
    /// 缓存起来
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="session"></param>
    /// <param name="pd"></param>
    public void AcctOnline(string acct, ServerSession session, PlayerData pd)
    {
        onLineAcctDic.Add(acct, session);
        onLineSessionDic.Add(session, pd);
    }

    /// <summary>
    /// 下线对两个字典的处理
    /// </summary>
    /// <param name="session"></param>
    public void AcctOffline(ServerSession session)
    {
        foreach (var item in onLineAcctDic)
        {
            if (session == onLineAcctDic[item.Key])
            {
                onLineAcctDic.Remove(item.Key);
                break;
            }
        }

        bool suc = onLineSessionDic.Remove(session);

        PECommon.Log("SessionID Offline："+session.sessionID);
         
    }
    #endregion


}

