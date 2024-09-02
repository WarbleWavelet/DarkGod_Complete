
/****************************************************
	文件：BuySys.cs
	作者：WWS
	邮箱: 
	日期：2022/05/19 14:37   	
	功能：物品交易系统
*****************************************************/

using PEProtocol;
using System;

class BuySys
{
    #region 单例
    private static BuySys _instance;

    public static BuySys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BuySys();
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
        PECommon.Log("BuySys Init");
    }


    internal void ReqBuy(MsgPack pack)
    {
        PlayerData pd = cacheSvc.GetPlayerDataBySession(pack.session);
        ReqBuy data = pack.msg.reqBuy;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspBuy
        };
        //cost
        switch ( data.buyType )
        {
            case  BuyType.COIN:
                {
                    if (pd.coin < data.buyCnt)
                    {
                        msg.err = (int)ErrorCode.LackCoin;
                    }
                    else
                    {
                        pd.coin -= data.buyCnt;

                    }
                } break;
            case BuyType.DIAMOND:
                {
                    if (pd.diamond < data.buyCnt)
                    {
                        msg.err = (int)ErrorCode.LackDiamond;
                    }
                    else
                    {
                        pd.diamond -= data.buyCnt;
                    }
                }
                break;
            case BuyType.CRYSTAL:
                {
                    if (pd.crystal < data.buyCnt)
                    {
                        msg.err = (int)ErrorCode.LackCrystal;
                    }
                    else
                    {
                        pd.crystal -= data.buyCnt;
                    }
                }
                break;
            default: break;
        }

        //get
        switch (data.goodType)
        {
            case GoodType.POWER:
                {
                    pd.power += data.goodCnt;
                }
                break;
            case GoodType.COIN:
                {
                    pd.coin += data.goodCnt;
                }
                break;
            default: break;
        }
        //sendMsg
        if ( !cacheSvc.UpdatePlayerData(pd.id, pd) )
        {
            msg.err = (int)ErrorCode.UpdateDBError;
        }
        else
        {
            switch (data.goodType)
            {
                case GoodType.POWER:
                    {
                        msg.pshTaskPrgs= TaskSys.Instance.GetTaskPrgs(pd, TaskID.BuyPower);
                        msg.rspBuy = new RspBuy
                        {
                            goodType = GoodType.POWER,
                            goodCnt = data.goodCnt
                        };
                    }
                    break;
                case GoodType.COIN:
                    {
                        msg.pshTaskPrgs = TaskSys.Instance.GetTaskPrgs(pd, TaskID.MKCoin);
                        msg.rspBuy = new RspBuy
                        {
                            goodType = GoodType.COIN,
                            goodCnt = data.goodCnt
                        };
                    }
                    break;
                default: break;
            }


        }
        pack.session.SendMsg(msg);

    }
}

