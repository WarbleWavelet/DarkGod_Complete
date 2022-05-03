using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PEProtocol;
using PENet;


public class ServerSession : PESession<GameMsg>
{
    protected override void OnConnected()
    {
        PECommon.Log("A Client Connected");

    }

    protected override void OnReciveMsg(GameMsg msg)
    {
        PECommon.Log("RcvPack CMD：" +((CMD)msg.cmd).ToString()+"accct:"+msg.reqLogin.acct.ToString()+ "pass:"+msg.reqLogin.pass.ToString());

        NetSvc.Instance.AddMsgQue(this,msg);

    }

    protected override void OnDisConnected()
    {
        PECommon.Log("A Client Disconnected");

    }
}


