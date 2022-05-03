

/// <summary>
/// 服务器初始化
/// </summary>
class GameRoot
{


    private static GameRoot _instance;      

    public static GameRoot Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameRoot();
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
        CacheSvc.Instance.Init();
        NetSvc.Instance.Init();
        LoginSys.Instance.Init();
       
    }

    public void Update()
    {
        NetSvc.Instance.Update();
    }


}

