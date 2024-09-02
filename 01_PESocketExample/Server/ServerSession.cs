using PENet;
using Protocol;

namespace Server
{
    /// <summary>
    /// 与客户端连接
    /// </summary>
    class ServerSession:PESession<NetMsg>
    {
        protected override void OnConnected()
        {
            PETool.LogMsg("A Client Connected");
            SendMsg(new NetMsg
            { 
                text= "Welcome To Connect" 
            });
        }

        protected override void OnReciveMsg(NetMsg msg)
        {
            PETool.LogMsg("Client Request：" + msg.text);
            SendMsg(new NetMsg 
            { 
                text = "Server Response: " + msg.text 
            });
        }

        protected override void OnDisConnected()
        {
            PETool.LogMsg("A Client Disconnect");
            SendMsg(new NetMsg
            { 
                text = "Yd Disconnect" 
            });
        }
    }
}
