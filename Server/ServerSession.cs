
using PENet;
using Protocal;

namespace Server
{
    /// <summary>
    /// 与客户端连接
    /// </summary>
    class ServerSession:PESession<NetMsg>
    {
        protected override void OnConnected()
        {
            PETool.LogMsg("Client开始连接");
        }

        protected override void OnReciveMsg(NetMsg msg)
        {
            PETool.LogMsg("收到Client连接：" + msg.text);
        }

        protected override void OnDisConnected()
        {
            PETool.LogMsg("Client断开连接");
        }
    }
}
