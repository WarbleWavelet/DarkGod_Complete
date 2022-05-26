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
    private TimerSvc timerSvc;

    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        timerSvc = TimerSvc.Instance;
        //
        TimerSvc.Instance.AddTimerTask(CalcPowerAdd,PECommon.PowerAddSpace,PETimeUnit.Second,0);
        PECommon.Log("PowerSys Init");
    }


    /// <summary>
    /// 在线时体力增加
    /// </summary>
    /// <param name="tid"></param>
    private void CalcPowerAdd(int tid)
    {
      
        GameMsg msg = new GameMsg()
        {
            cmd = (int)CMD.PshPower,
        };

        Dictionary<ServerSession,PlayerData> onlineDic = cacheSvc.onLineSessionDic;
        //PECommon.Log("onlineDic.Count"+ onlineDic.Count.ToString());
        foreach (var item in onlineDic)
        {
            PlayerData pd = item.Value;
            int maxPower = PECommon.GetPowerLimit(pd.lv);
            ServerSession session = item.Key;
            if (pd.power >= maxPower)
            {
                continue;
            }
            else 
            {
                pd.time = timerSvc.GetNowTime();
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

