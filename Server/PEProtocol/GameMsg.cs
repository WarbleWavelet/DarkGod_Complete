
using System;
using PENet;
/// <summary>
/// 网络通信协议（CS共用）
/// </summary>

namespace PEProtocol
{
    [Serializable]
    public class GameMsg:PEMsg
    {
        public string text;

    }

   public class SrvCfg
    {
        public const string srvIp = "127.0.0.1";
        public const int srvPort = 17666;
    }
}
