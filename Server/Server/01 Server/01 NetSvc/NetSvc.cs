
using PENet;
using PEProtocol;
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
    }

    public void Init()
    {
        PESocket<ServerSession, GameMsg> server = new PESocket<ServerSession, GameMsg>();
        server.StartAsServer(SrvCfg.srvIp, SrvCfg.srvPort);
        PETool.LogMsg("NetSvc Inited");

    }


}

