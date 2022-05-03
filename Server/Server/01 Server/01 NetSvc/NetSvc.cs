
using PENet;
using PEProtocol;
using System.Collections.Generic;
/// <summary>
/// 网络服务
/// </summary>
class NetSvc
{


    private static NetSvc _instance;
    Queue<MsgPack> msgPackQue = new Queue<MsgPack>();
    /// <summary>消息队列异步多线程，锁一下</summary>
    public static readonly string obj = "lock";

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
        PECommon.Log("NetSvc Inited");

    }

    public void AddMsgQue(ServerSession session,GameMsg msg)
    {
        lock (obj)
        {
            msgPackQue.Enqueue(new MsgPack( session,  msg));
        }

    }

    public void Update()
    {
        if (msgPackQue.Count > 0)
        {
            PECommon.Log("PackCount：" + msgPackQue.Count);
            lock (obj)
            {
                MsgPack pack = msgPackQue.Dequeue();
                HandleOutMessage(pack);
            }
        }
    }


    void HandleOutMessage(MsgPack pack)
    {

        switch ((CMD)pack.msg.cmd)
        {
            case CMD.ReqLogin:
                {
                    LoginSys.Instance.ReqLogin(pack);
                }
                break;
            default: break;
        }

    }

}

/// <summary>
/// 消息包
/// </summary>

public class MsgPack
{
    public ServerSession session;
    public GameMsg msg;

    public MsgPack(ServerSession session, GameMsg msg)
    {
        this.session = session;
        this.msg = msg;
    }
}