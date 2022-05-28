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
    }


    #region 增删查改

    public int InsertPlayerData(string acct, string pass, PlayerData pd)
    {
        int id = -1;
        try
        {
            string sql = "insert into account set " +
            "acct=@acct,pass=@pass,name = @name,lv = @lv,exp = @exp,power = @power," +
            "coin = @coin,diamond = @diamond,crystal=@crystal," +
            "ad = @ad,ap = @ap,addef = @addef,apdef = @apdef,dodge = @dodge,critical = @critical,pierce = @pierce," +
            "guideid=@guideid,strong=@strong,time=@time,taskreward=@taskreward,instance=@instance";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("acct", acct);
            cmd.Parameters.AddWithValue("pass", pass);
            cmd.Parameters.AddWithValue("name", pd.name);
            cmd.Parameters.AddWithValue("lv", pd.lv);
            cmd.Parameters.AddWithValue("exp", pd.exp);
            cmd.Parameters.AddWithValue("power", pd.power);
            cmd.Parameters.AddWithValue("coin", pd.coin);
            cmd.Parameters.AddWithValue("diamond", pd.diamond);
            cmd.Parameters.AddWithValue("crystal", pd.crystal);
            cmd.Parameters.AddWithValue("hp", pd.hp);
            cmd.Parameters.AddWithValue("ad", pd.ad);
            cmd.Parameters.AddWithValue("ap", pd.ap);
            cmd.Parameters.AddWithValue("addef", pd.addef);
            cmd.Parameters.AddWithValue("apdef", pd.apdef);
            cmd.Parameters.AddWithValue("dodge", pd.dodge);
            cmd.Parameters.AddWithValue("critical", pd.critical);
            cmd.Parameters.AddWithValue("pierce", pd.pierce);
            cmd.Parameters.AddWithValue("guideid", pd.guideid);
            cmd.Parameters.AddWithValue("strong", Strong_ArrToString(pd.strongArr));
            cmd.Parameters.AddWithValue("time", pd.time);
            cmd.Parameters.AddWithValue("taskreward", TaskReward_ArrToString(pd.taskRewardArr)  );
            cmd.Parameters.AddWithValue("instance", pd.instance);
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
        return id;
    }


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
                        diamond = reader.GetInt32("diamond"),
                        crystal=reader.GetInt32("crystal"),
                        hp = reader.GetInt32("hp"),
                        ad = reader.GetInt32("ad"),
                        ap = reader.GetInt32("ap"),
                        addef = reader.GetInt32("addef"),
                        apdef = reader.GetInt32("apdef"),
                        critical = reader.GetInt32("critical"),
                        pierce = reader.GetInt32("pierce"),
                        dodge = reader.GetInt32("dodge"),
                        guideid = reader.GetInt32("guideid"),
                        strongArr=Strong_StringToArr( reader.GetString("strong") ),
                        time= reader.GetInt64("time"),
                        taskRewardArr = TaskReward_StringToArr(reader.GetString("taskreward")),
                        instance = reader.GetInt32("instance")

                    };
                    PECommon.Log("已查到acct:" + acct);
                }

            }

            reader.Close();
        }
        catch (Exception e)
        {
            PECommon.Log("Query PlayerData By Acct&Pass Error:" + e, LogType.Error);
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
                    power = 50,
                    coin = 5000,
                    diamond = 500,
                    crystal = 100,
                    hp = 2000,
                    ad = 275,
                    ap = 265,
                    addef = 67,
                    apdef = 43,
                    critical = 2,
                    pierce = 5,
                    dodge = 7,
                    guideid = 1001,
                    strongArr = new int[] { 0, 0, 0, 0, 0, 0 },
                    time = TimerSvc.Instance.GetNowTime(),
                    taskRewardArr = new string[] {
                        "1|0|2",
                        "2|0|2",
                        "3|0|2",
                        "4|0|2",
                        "5|0|2",
                        "6|0|2"
                    },
                    instance=10001
                };
                playerData.id = InsertPlayerData(acct, pass, playerData);
            }

        }


        return playerData;
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

    public bool UpdatePlayerData(int id, PlayerData pd)
    {
        try
        {
            string sql = "update account set " +
                " name = @name,lv = @lv,exp = @exp,power = @power," +
                "coin = @coin,diamond = @diamond,crystal=@crystal," +
                " ad = @ad,ap = @ap,addef = @addef,apdef = @apdef,dodge = @dodge,critical = @critical,pierce = @pierce," +
                " guideid=@guideid,strong=@strong,time=@time,taskreward=@taskreward, instance=@instance" +
                " where id=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("name", pd.name);
            cmd.Parameters.AddWithValue("lv", pd.lv);
            cmd.Parameters.AddWithValue("exp", pd.exp);
            cmd.Parameters.AddWithValue("power", pd.power);
            cmd.Parameters.AddWithValue("coin", pd.coin);
            cmd.Parameters.AddWithValue("diamond", pd.diamond);
            cmd.Parameters.AddWithValue("crystal", pd.crystal);
            cmd.Parameters.AddWithValue("hp", pd.hp);
            cmd.Parameters.AddWithValue("ad", pd.ad);
            cmd.Parameters.AddWithValue("ap", pd.ap);
            cmd.Parameters.AddWithValue("addef", pd.addef);
            cmd.Parameters.AddWithValue("apdef", pd.apdef);
            cmd.Parameters.AddWithValue("critical", pd.critical);
            cmd.Parameters.AddWithValue("dodge", pd.dodge);
            cmd.Parameters.AddWithValue("pierce", pd.pierce);
            cmd.Parameters.AddWithValue("guideid", pd.guideid);
            cmd.Parameters.AddWithValue("strong", Strong_ArrToString(pd.strongArr));
            cmd.Parameters.AddWithValue("time", pd.time);
            cmd.Parameters.AddWithValue("taskreward", TaskReward_ArrToString(pd.taskRewardArr));
            cmd.Parameters.AddWithValue("instance", pd.instance);

            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
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

    #region   强化

    string Strong_ArrToString(int[] strongArr)
    {

        string strong = "";
        if (strongArr == null) return strong;
        for (int i = 0; i < strongArr.Length; i++)
        {
            strong += "#" + strongArr[i].ToString();
        }

        return strong;
    }

    int[] Strong_StringToArr(string strong)
    {

        if (strong == null)
            return null;


        string[]  _strongArr =strong.Split('#');//解析后第一个是""
        int len = _strongArr.Length;
        for (int i = 0; i < _strongArr.Length; i++)
        {
            if (_strongArr[i] == "")
            {
                len--;
            }
        }
        //
        int[] strongArr =new int[len];
        int j = 0;
        for (int i = 0; i < _strongArr.Length; i++)
        {
            if (_strongArr[i] == "")
            {
                j--;
                continue;
            }
            j++;
            strongArr[j] = int.Parse(_strongArr[i]) ;
        }

        return strongArr;
    }

    #endregion

    #region 任务奖励
    string TaskReward_ArrToString(string[] taskRewardArr)
    {

        string taskReward = "";
        if (taskRewardArr == null)
            return "";

        for (int i = 0; i < taskRewardArr.Length; i++)
        {
            taskReward += "#" + taskRewardArr[i];
        }

        return taskReward;
    }

    string[] TaskReward_StringToArr(string taskReward)
    {

        if (taskReward == null)
            return null;


        string[] _taskRewardArr = taskReward.Split('#');//解析后第一个是""
        int len = _taskRewardArr.Length;
        for (int i = 0; i < _taskRewardArr.Length; i++)
        {
            if (_taskRewardArr[i] == "")
            {
                len--;
            }
        }
        //
        string[] taskRewardArr = new string[len];
        int j = 0;
        for (int i = 0; i < _taskRewardArr.Length; i++)
        {
            if (_taskRewardArr[i] == "")
            {
                j--;
                continue;
            }
            j++;
            taskRewardArr[j] = _taskRewardArr[i];
        }

        return taskRewardArr;
    }
    #endregion

    #endregion

}

