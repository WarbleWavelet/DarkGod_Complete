

/// <summary>
/// 网络服务
/// </summary>
class NetSvc
{


    private static NetSvc _instance;      

    public static NetSvc Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NetSvc(); 
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
       
    }


}

