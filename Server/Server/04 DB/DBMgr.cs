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
        conn = new MySqlConnection("server=localhost;User Id=root;passwrod=;Database=darkgod;Charset=utf8");
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

        QueryPlayerData("12","12132");
    }


    #region 增删查改

    /// <summary>
    /// 查账号
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="pass"></param>
    /// <returns></returns>

    public PlayerData QueryPlayerData(string acct, string pass)
    {
        bool isNew = true;//新账号
        PlayerData playerData = null;
        MySqlDataReader reader = null;

        try
        {
            MySqlCommand cmd = new MySqlCommand("select * from account where acct=@acct", conn);
            cmd.Parameters.AddWithValue("acct", acct);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string _acct = reader.GetString("acct");
                if (_acct.Equals(acct))
                {
                    isNew = false;
                    playerData = new PlayerData
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        lv = reader.GetInt32("lv"),
                        exp = reader.GetInt32("exp"),
                        power = reader.GetInt32("power"),
                        coin = reader.GetInt32("coin"),
                        diamond = reader.GetInt32("diamond")
                    };
                    PECommon.Log("已查到acct:"+acct );
                }

            }

            reader.Close();
        }
        catch (Exception e)
        {
            PECommon.Log("Query PlayerData By Acct&Pass Error:" + e,LogType.Error);
        }
        finally
        {
            //写在这里而不是try，防止catch的内容太多
            if (isNew)
            {
                playerData = new PlayerData
                {
                    id = -1,
                    name = "",
                    lv = 1,
                    exp = 0,
                    power = 150,
                    coin = 5000,
                    diamond = 500

                };
                playerData.id = InsertPlayerData(acct, pass, playerData);
            }

        }


        return playerData;
    }



    public int InsertPlayerData(string acct, string pass, PlayerData pd)
    {
        int id = -1;
        try
        {
            MySqlCommand cmd = new MySqlCommand("insert into account set acct=@acct,pass=@pass,name=@name,lv=@lv,exp=@exp,power=@power,coin=@coin,diamond=@diamond", conn);
            cmd.Parameters.AddWithValue("acct", acct);
            cmd.Parameters.AddWithValue("pass", pass);
            cmd.Parameters.AddWithValue("name", pd.name);
            cmd.Parameters.AddWithValue("lv", pd.lv);
            cmd.Parameters.AddWithValue("exp", pd.exp);
            cmd.Parameters.AddWithValue("power", pd.power);
            cmd.Parameters.AddWithValue("coin", pd.coin);
            cmd.Parameters.AddWithValue("diamond", pd.diamond);
            cmd.ExecuteNonQuery();
            id = (int)cmd.LastInsertedId;
            PECommon.Log("已增id:" + id);

        }
        catch (Exception e)
        {

            PECommon.Log("Insert PlayerData失败，原因：" + e, LogType.Error);
        }
        finally
        {

        }
        return 0;
    }

    public bool QueryNameData(string name)
    {
        bool exist = false;
        MySqlDataReader reader = null;
        try
        {
            MySqlCommand cmd = new MySqlCommand("select * from account where name=@name", conn);
            cmd.Parameters.AddWithValue("name",name);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                exist = true;

            }
            if (reader != null)
            {
                reader.Close();
            }
        }
        catch (Exception e)
        {
            PECommon.Log("查找name错误："+e,LogType.Error);
        }
        finally
        {

        }
        return exist;
    }

    public bool UpdatePlayerData(int id, PlayerData playerData)
    {
        MySqlDataReader reader = null;
        try
        {
            MySqlCommand cmd = new MySqlCommand("update account set name=@name,lv=@lv,exp=@exp,power=@power,coin=@coin,diamond=@diamond where id=@id", conn);
            cmd.Parameters.AddWithValue("name", playerData.name);
            cmd.Parameters.AddWithValue("lv", playerData.lv);
            cmd.Parameters.AddWithValue("exp", playerData.exp);
            cmd.Parameters.AddWithValue("power", playerData.power);
            cmd.Parameters.AddWithValue("coin", playerData.coin);
            cmd.Parameters.AddWithValue("diamond", playerData.diamond);
            cmd.Parameters.AddWithValue("id", id);

            reader = cmd.ExecuteReader();
            if (reader != null)
            {
                reader.Close();
            }
        }
        catch (Exception e)
        {
            PECommon.Log("查找id错误：" + e, LogType.Error);
            return false;
        }
        finally
        {

        }
        return true;
    }
    #endregion

}

