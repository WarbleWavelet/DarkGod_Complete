using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol;
using PENet;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            PESocket<ServerSession, NetMsg> server = new PESocket<ServerSession, NetMsg>();
            server.StartAsServer(IPCfg.srvIP,IPCfg.srvPort);

            Console.ReadLine();
            //防止立即断开
            while (true)
            {

            }
        }
    }
}
