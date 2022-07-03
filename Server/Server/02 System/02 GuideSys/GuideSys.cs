using PEProtocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class GuideSys
    {
    #region 单例
    private static GuideSys _instance;

    public static GuideSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GuideSys();
            }
            return _instance;
        }

    }
    #endregion

    public CacheSvc cacheSvc;
    private CfgSvc cfgSvc;

    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        cfgSvc = CfgSvc.Instance;
        PECommon.Log("GuideSys Init");
    }



    public void ReqGuide(MsgPack pack)
    {

        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspGuide,

        };

        ReqGuide data = pack.msg.reqGuide;
        GuideCfg cfg = cfgSvc.GetGuideCfg(data.guideid);
        PlayerData pd = cacheSvc.GetPlayerDataBySession(pack.session);

        //
        if (pd.guideid!=data.guideid)
        {
            msg.err = (int)ErrorCode.ServerDataError;
        }
        else
        {
            msg.pshTaskPrgs =TaskSys.Instance.GetTaskPrgs(pd, TaskID.WiseMan);
            //
            pd.guideid += 1;
            pd.coin += cfg.coin;
            PECommon.CalcExp(pd, cfg.exp);

            if (!DBMgr.Instance.UpdatePlayerData(pd.id, pd))
            {
                msg.err = (int)ErrorCode.UpdateDBError;
            }
            else
            {
                msg.rspGuide = new RspGuide
                {
                    guideid=pd.guideid,
                    exp=pd.exp,
                    coin=pd.coin,
                    lv=pd.lv

                };
            }
        }

        pack.session.SendMsg(msg);

    }



}

#region 方便阅读，希望以后学到自动生成代码
public enum GuideID
{
    [Description("智者点拨")]
    WiseMan = 1001,
    [Description("副本战斗")]
    InstanceZones = 1002,
    [Description("强化升级")]
    Strong = 1003,
    [Description("购买体力")]
    Power = 1004,
    [Description("铸造金币")]
    MKCoin = 1005,
    [Description("能言善辩")]
    Speak = 1006
}


public enum TaskID
{
    [Description("智者点拨")]
    WiseMan = 1,
    [Description("副本战斗")]
    InstanceZones = 2,
    [Description("强化升级")]
    Strong = 3,
    [Description("购买体力")]
    BuyPower = 4,
    [Description("铸造金币")]
    MKCoin = 5,
    [Description("能言善辩")]
    Speak = 6
}
#endregion

