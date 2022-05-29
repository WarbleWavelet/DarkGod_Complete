

/// <summary>
/// 服务器初始化
/// </summary>
class ServerRoot
{

    public int SessionID = 0;
    private static ServerRoot _instance;      

    public static ServerRoot Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ServerRoot();
            }
            return _instance;
        }

    set
        {
            _instance = value;
        }
    }

    public void Init()
    {
        DBMgr.Instance.Init();
        //
        CacheSvc.Instance.Init();
        NetSvc.Instance.Init();
        CfgSvc.Instance.Init();
        //
        LoginSys.Instance.Init();
        GuideSys.Instance.Init();
        StrongSys.Instance.Init();
        BuySys.Instance.Init();
        ChatSys.Instance.Init();
        TimerSvc.Instance.Init();
        //TestTimerSvc();
        PowerSys.Instance.Init();
        TaskSys.Instance.Init();
        InstanceSys.Instance.Init();
    }

    public void Update()
    {
        NetSvc.Instance.Update();
        TimerSvc.Instance.Update();
        
    }


    /// <summary>
    /// 生成sessionID
    /// </summary>
    /// <returns></returns>
    public int GetSessionID()
    {
        if (SessionID == int.MaxValue)
        {
            SessionID = 0;
        }
        return ++SessionID;
    }

    void TestTimerSvc()
    {
        int ms = 1000;
        TimerSvc.Instance.AddTimerTask((int tid)=> {
            PECommon.Log("1秒");

       },ms,PETimeUnit.Millisecond,0);
    }
}

