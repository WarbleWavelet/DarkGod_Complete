/****************************************************
	文件：InstanceSys.cs
	作者：WWS
	邮箱: 
	日期：2022/05/28 22:36   	
	功能：副本系统
*****************************************************/

using PEProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class InstanceSys
{

    #region 单例
    private static InstanceSys _instance;      

    public static InstanceSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InstanceSys();
            }
            return _instance;
        }
    }
    #endregion


    public CacheSvc cacheSvc;
    public CfgSvc cfgSvc;
    public NetSvc netSvc;

    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        cfgSvc = CfgSvc.Instance;
        netSvc = NetSvc.Instance;
        PECommon.Log("InstanceSys Init");
    }

    internal void ReqInstanceFight(MsgPack pack)
    {
        PlayerData pd = cacheSvc.GetPlayerDataBySession(pack.session);
        ReqInstanceFight data = pack.msg.reqInstanceFight;
       MapCfg cfg= cfgSvc.GetMapCfg(data.instance);
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspInstanceFight
        };

        if (pd.power >= cfg.power)
        {
            pd.power -= cfg.power;
            if (cacheSvc.UpdatePlayerData(pd.id, pd))
            {
                msg.rspInstanceFight = new RspInstanceFight
                {
                    power = pd.power,
                    instance = data.instance
                };
            }
        }
        else
        {
            msg.err = (int)ErrorCode.LackPower;
        }

        pack.session.SendMsg(msg);
    }

    internal void ReqInstanceFightEnd(MsgPack pack)
    {
        ReqInstanceFightEnd data = pack.msg.reqInstanceFightEnd;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspInstanceFightEnd
        };

        if (data.isWin)
        {
            
            if (data.costTime > PECommon.EndBattleMinTime && data.remainHP > 0)//检验
            {
                PlayerData pd = cacheSvc.GetPlayerDataBySession(pack.session);                
                MapCfg cfg = cfgSvc.GetMapCfg(data.instance);
                //
                TaskSys.Instance.CalcTaskPrgs(pd, TaskID.InstanceZones);
                pd.coin += cfg.coin;
                pd.crystal += cfg.crystal;
                PECommon.CalcExp(pd, cfg.exp);
                //        
                
                if (data.instance == pd.instance)
                {
                    int after= pd.instance +1;
                    if (after <= cfgSvc.Get_MaxID_MapCfg())
                    {
                         pd.instance=after;
                    }
                  
                    
                }
                
                //
                if (cacheSvc.UpdatePlayerData(pd.id, pd))
                {
                    msg.rspInstanceFightEnd = new RspInstanceFightEnd
                    {
                        instance = pd.instance,
                        remainHP=data.remainHP,
                        costTime=data.costTime,
                        isWin=data.isWin,
                        coin = pd.coin,
                        crystal = pd.crystal,
                        exp = pd.exp,
                        lv=pd.lv
                    };
            
                }  
                //
              
                else
                {
                    msg.err = (int)ErrorCode.UpdateDBError;
                }
            }
            else
            {
                msg.err = (int)ErrorCode.ClientDataError;
            }
        }
        else
        {
            msg.err = (int)ErrorCode.ClientDataError;
        }

        pack.session.SendMsg(msg);
    }
}

