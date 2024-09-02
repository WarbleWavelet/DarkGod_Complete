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
    #region 单例
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
    #endregion

    CacheSvc cacheSvc = null;
    TimerSvc timerSvc = null;
    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        timerSvc = TimerSvc.Instance;
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
        };

        ReqLogin data = pack.msg.reqLogin;
        if (cacheSvc.IsAcctOnLine(data.acct))
        {
            msg.err = (int)ErrorCode.AcctIsOnLine;
        }
        else
        {
            PlayerData _pd = cacheSvc.GetPlayerData(data.acct, data.pass);
            if (_pd == null)
            {
                msg.err = (int)ErrorCode.WrongPass;
            }
            else
            {
                UpdatePlayerDataByOfflineAddPower(_pd);
                //
                msg.rspLogin = new RspLogin
                {
                    pd = _pd
                };
                cacheSvc.AcctOnline(data.acct, pack.session, _pd);
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


    public void ClearOfflineData(ServerSession session)
    {
        PlayerData pd = cacheSvc.GetPlayerDataBySession(session);
        if (pd != null)
        {
            pd.time = timerSvc.GetNowTime();
            if (cacheSvc.UpdatePlayerData(pd.id, pd)==false)
            {
                PECommon.Log("更新下线时间失败", LogType.Error);
            }
            else
            {
                PECommon.Log("更新下线时间成功");
            }
            cacheSvc.AcctOffline(session);
        }
        
    }



    #region 辅助
    /// <summary>
    /// 上次离线到现在要增加的体力
    /// </summary>
    /// <param name="preTime">上次登录的时间</param>
    /// <returns></returns>
     void UpdatePlayerDataByOfflineAddPower(PlayerData pd)
    {
        int nowPower = pd.power;
        long millSec = (timerSvc.GetNowTime() - pd.time);
        int addPower =  (int)(millSec / (PECommon.PowerAddSpace*1000)) * PECommon.PowerAddCount;
       int maxPower = PECommon.GetPowerLimit(pd.lv);
        if (addPower > 0)
        {
            
            if (nowPower<maxPower)
            {
                nowPower += addPower;
                if (nowPower > maxPower)
                {
                    nowPower = maxPower;
                }
               
            }
        }
        if (pd.power != nowPower)
        {
   PECommon.Log("离线增加了" + (nowPower - pd.power) + "体力");
            pd.power = nowPower;
            pd.time = TimerSvc.Instance.GetNowTime();
            cacheSvc.UpdatePlayerData(pd.id, pd);
            //
         
        }

    }
    #endregion

}

