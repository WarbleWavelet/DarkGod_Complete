
using PENet;
using PEProtocol;
using System.Collections.Generic;
/// <summary>
/// 网络服务
/// </summary>
class NetSvc
{



    Queue<MsgPack> msgPackQue = new Queue<MsgPack>();
    /// <summary>消息队列异步多线程，锁一下</summary>
    public static readonly string obj = "lock";
    #region 单例
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
    #endregion


    public void Init()
    {
        PESocket<ServerSession, GameMsg> server = new PESocket<ServerSession, GameMsg>();
        server.StartAsServer(SrvCfg.srvIp, SrvCfg.srvPort);
        PECommon.Log("NetSvc Init");

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
            case CMD.ReqRename:
                {
                    LoginSys.Instance.ReqRename(pack);
                }
                break;
            case CMD.ReqGuide:
                {
                    GuideSys.Instance.ReqGuide(pack);
                }
                break;
            case CMD.ReqStrong:
                {
                    StrongSys.Instance.ReqStrong(pack);
                }
                break;
            case CMD.SndChat:
                {
                    ChatSys.Instance.SndChat(pack);
                }
                break;
            case CMD.ReqBuy:
                {
                    BuySys.Instance.ReqBuy(pack);
                }
                break;
            case CMD.ReqTakeTaskReward:
                {
                    TaskSys.Instance.ReqTakeTaskReward(pack);
                }
                break;
            case CMD.ReqInstanceFight:
                {
                    InstanceSys.Instance.ReqInstanceFight(pack);
                }
                break;
            case CMD.ReqInstanceFightEnd:
                {
                    InstanceSys.Instance.ReqInstanceFightEnd(pack);
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