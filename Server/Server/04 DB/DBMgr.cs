using System;
using System.Data;
using MySql.Data.MySqlClient;
using PEProtocol;

/// <summary>数据库管理类</summary>
class DBMgr
{
    #region 单例
    private static DBMgr _instance;
    public static DBMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DBMgr();
            }
            return _instance;
        }

    }
    #endregion

    MySqlConnection conn;
    
    public void Init()
    {
        PECommon.Log("Init DBMgr");
        conn = new MySqlConnection("server=localhost;User Id=root;passwrod=;Database=studymysql;Charset=utf8");
        if (conn.State != ConnectionState.Open)
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("连不上" + ex);
            }
        }
    }


    /// <summary>
    /// 查账号
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="pass"></param>
    /// <returns></returns>

    public PlayerData QueryPlayerData(string acct,string pass)
    {
        PlayerData playerData = null;
        return playerData;
    }
}

