using PENet;
using PEProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 登录业务系统
/// </summary>
    class LoginSys
    {
    private static LoginSys _instance;

    public static LoginSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LoginSys();
            }
            return _instance;
        }
    }

    public void Init()
    {
        PECommon.Log("LoginSys Inited");
    }

    /// <summary>
    /// 处理登录
    /// </summary>
    /// <param name="msg"></param>
    public void ReqLogin(MsgPack pack)
    {
        //OnLine();
        //OffLine();
        //    Login();
        //    Register();
        //RspClient();

        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspLogin,
            rspLogin = new RspLogin
            {
            }
        };
        pack.session.SendMsg(msg);

    }
}

