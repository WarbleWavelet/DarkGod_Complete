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


    #region Req
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
            PlayerData _playerData = cacheSvc.GetPlayerData(data.acct, data.pass);
            if (_playerData == null)
            {
                msg.err = (int)ErrorCode.WrongPass;
            }
            else
            {
                msg.rspLogin = new RspLogin { playerData = _playerData };

                cacheSvc.AcctOnline(data.acct, pack.session, _playerData);
            }
        }


        pack.session.SendMsg(msg);

    }


    /// <summary>
    /// 重命名<para/>
    /// 01 mtd1：Update数据库，完成后回Client（所选）<para/>
    /// 02 mtd2：苛刻点，回Client，用消息队列等机制，一直Update数据库到成功<para/>
    /// </summary>
    /// <param name="pack"></param>
    internal void ReqRename(MsgPack pack)
    {
        ReqRename data = pack.msg.reqRename;
        GameMsg msg = new GameMsg { cmd=(int)CMD.RspRename};

        if (cacheSvc.IsNameExist(data.name))
        {
            //错误码
            msg.err = (int)ErrorCode.NameIsExist;
        }
        else
        {
            //更新缓存
            PlayerData pd = cacheSvc.GetPlayerDataBySession(pack.session );
            pd.name = data.name;
            if (cacheSvc.UpdatePlayerData(pd.id, pd) == false)
            {
                msg.err = (int)ErrorCode.UpdateDBError;
            }
            else
            {
                msg.rspRename = new RspRename
                {
                    name = data.name
                };
                
            }

      
        }
          pack.session.SendMsg(msg);
    
    }
    #endregion

}

