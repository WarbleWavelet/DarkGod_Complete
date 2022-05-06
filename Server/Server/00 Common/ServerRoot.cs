

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
        CacheSvc.Instance.Init();
        NetSvc.Instance.Init();
        LoginSys.Instance.Init();
       
    }

    public void Update()
    {
        NetSvc.Instance.Update();
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
}

