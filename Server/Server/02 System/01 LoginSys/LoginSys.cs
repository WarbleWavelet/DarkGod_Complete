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
    CacheSvc cacheSvc = null;

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
        cacheSvc = CacheSvc.Instance;
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

        ReqLogin data = pack.msg.reqLogin;
        if (cacheSvc.IsAcctOnLine(data.acct))
        {
            msg.err = (int)ErrorCode.AcctIsOnLine;
        }
        else
        {
           PlayerData _playerData= cacheSvc.GetPlayerData(data.acct, data.pass);
            if (_playerData == null)
            {
                msg.err = (int)ErrorCode.WrongPass;
            }
            else
            {
                msg.rspLogin = new RspLogin { playerData = _playerData };

                cacheSvc.AcctOnline(data.acct,pack.session,_playerData);
            }
        }


        pack.session.SendMsg(msg);

    }
}

