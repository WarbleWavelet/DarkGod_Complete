using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PEProtocol;
using PENet;


public class ServerSession : PESession<GameMsg>
{
    /// <summary>
    /// 让玩家下线，结束session
    /// </summary>
    public int sessionID = 0;
    protected override void OnConnected()
    {
        PECommon.Log("A Client Connected");
        sessionID = ServerRoot.Instance.GetSessionID();
        PECommon.Log("SessionID:"+sessionID +" client connected");
    }

    protected override void OnReciveMsg(GameMsg msg)
    {
        PECommon.Log("RcvPack CMD：" +((CMD)msg.cmd).ToString());

        NetSvc.Instance.AddMsgQue(this,msg);

    }

    protected override void OnDisConnected()
    {
        //PECommon.Log("A Client Disconnected");
        PECommon.Log("SessionID:" + sessionID + " client disconnected");
        LoginSys.Instance.ClearOfflineData(this);

    }
}


