/****************************************************
	文件：StrongSys.cs
	作者：WWS
	邮箱: 
	日期：2022/05/17 15:47   	
	功能：装备强化系统
*****************************************************/

using PEProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StrongSys
{
    #region 单例
    private static StrongSys _instance;

    public static StrongSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StrongSys();
            }
            return _instance;
        }

    }
    #endregion

    public CacheSvc cacheSvc;
    public CfgSvc cfgSvc;

    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        cfgSvc = CfgSvc.Instance;
        PECommon.Log("StrongSys Init");
    }


    /// <summary>
    /// （真正计算）
    /// </summary>
    /// <param name="pack"></param>
    public void ReqStrong(MsgPack pack)
    {
        int posIdx= pack.msg.reqStrong.pos;
        PlayerData pd = cacheSvc.GetPlayerDataBySession(pack.session);

        int curStarLv = pd.strongArr[posIdx];
        ReqStrong data = pack.msg.reqStrong;
        StrongCfg nextCfg = cfgSvc.GetStrongCfg(posIdx,curStarLv+1);


        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspStrong,

        };
        if (pd.lv < nextCfg.minlv )
        {
            msg.err = (int)ErrorCode.LackLv;
            PECommon.Log("等级不足");
        }
        else if (pd.crystal < nextCfg.crystal)
        {
            msg.err = (int)ErrorCode.LackCoin;
            PECommon.Log("金币不足"); return;
        }
        else if (pd.coin < nextCfg.coin)
        {
            msg.err = (int)ErrorCode.LackCrystal;
            PECommon.Log("水晶不足"); return;
        }
        else
        {
           msg.pshTaskPrgs= TaskSys.Instance.GetTaskPrgs(pd, TaskID.Strong);
            //
            pd.coin -= nextCfg.coin;
            pd.crystal -= nextCfg.crystal;
            pd.strongArr[posIdx] +=1;
            pd.hp += nextCfg.addhp;
            pd.addef += nextCfg.adddef;
            pd.apdef += nextCfg.adddef;
            pd.ad += nextCfg.addhurt;
            pd.ap += nextCfg.addhurt;
            if (!cacheSvc.UpdatePlayerData(pd.id, pd))
            {
                msg.err = (int)ErrorCode.UpdateDBError;
            }
            else
            {
                msg.rspStrong = new RspStrong
                {
                    coin = pd.coin,
                    crystal = pd.crystal,
                    hp=pd.hp,
                    ad=pd.ad,
                    ap=pd.ap,
                    addef=pd.addef,
                    apdef=pd.apdef,
                    strongArr=pd.strongArr

                };
            }
        }
        pack.session.SendMsg(msg);
    }
}