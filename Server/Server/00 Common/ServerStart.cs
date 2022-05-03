using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 服务器入口
/// </summary>
class ServerStart
{
    static void Main(string[] args)
    {
        GameRoot.Instance.Init();


        while (true)
        {
            GameRoot.Instance.Update();
        }
    }
}

