/****************************************************
	文件：PowerSys.cs
	作者：WWS
	邮箱: 
	日期：2022/05/20 17:56   	
	功能：体力恢复系统
*****************************************************/
using PEProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PEProtocol;


class PowerSys
{


    #region 单例
    private static PowerSys _instance;      

    public static PowerSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PowerSys();
            }
            return _instance;
        }
    }
    #endregion

    public CacheSvc cacheSvc;
    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        //
        TimerSvc.Instance.AddTimerTask(CalcPowerAdd,PECommon.PowerAddSpace,PETimeUnit.Second,0);
        PECommon.Log("PowerSys Inited");
    }

    private void CalcPowerAdd(int tid)
    {
        PECommon.Log("Add Power");
        GameMsg msg = new GameMsg()
        {
            cmd = (int)CMD.PshPower,
        };

        Dictionary<ServerSession,PlayerData> onlineDic = cacheSvc.onLineSessionDic;

       

        foreach (var item in onlineDic)
        {
            PlayerData pd = item.Value;
            int maxPower = PECommon.GetPowerLimit(pd.lv);
            ServerSession session = item.Key;
            if (pd.power > maxPower)
            {
                continue;
            }
            else 
            {
                pd.power += PECommon.PowerAddCount;
                if (pd.power >= maxPower)
                {
                    pd.power = maxPower;
                }
            }
            //
            if (!cacheSvc.UpdatePlayerData(pd.id, pd))
            {
                msg.err = (int)ErrorCode.UpdateDBError;
            }
            else
            {
                msg.pshPower = new PshPower
                {
                    power=pd.power
                };
            }
            session.SendMsg(msg);
            
        }
    }
}

