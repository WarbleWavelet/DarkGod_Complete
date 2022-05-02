using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PEProtocol;
using PENet;


class ServerSession : PESession<GameMsg>
{
    protected override void OnConnected()
    {
        PETool.LogMsg("A Client Connected");
        SendMsg(
            new GameMsg { text = "Welcome To Connect" }
        );
    }

    protected override void OnReciveMsg(GameMsg msg)
    {
        PETool.LogMsg("Client Request：" + msg.text);
        SendMsg(
            new GameMsg { text = "Server Response: " + msg.text }
        );
    }

    protected override void OnDisConnected()
    {
        PETool.LogMsg("A Client Disconnected");
        SendMsg(
            new GameMsg { text = "Yd Disconnected" }
        );
    }
}


