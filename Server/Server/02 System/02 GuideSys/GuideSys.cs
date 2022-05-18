using PEProtocol;
using System;
using System.Collections.Generic;
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
            pd.guideid += 1;
            pd.coin += cfg.coin;
            CalcExp(pd, cfg.exp);

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



    void CalcExp(PlayerData pd,int exp)
    {
        int remainExp= pd.exp + exp;
        while (remainExp >= PECommon.GetExpUpValByLV(pd.lv))
        {
            remainExp -= PECommon.GetExpUpValByLV(pd.lv);
            pd.lv += 1;
        }
        pd.exp = remainExp;
    }
}

