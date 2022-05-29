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
       InstanceCfg cfg= cfgSvc.GetInstanceCfg(data.instanceID);
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
                    instanceID = data.instanceID
                };
            }
        }
        else
        {
            msg.err = (int)ErrorCode.LackPower;
        }

        pack.session.SendMsg(msg);
    }
}

