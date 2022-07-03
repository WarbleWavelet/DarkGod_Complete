/****************************************************
    文件：GameRoot.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/23 17:24:44
	功能：游戏启动入口
*****************************************************/

using PEProtocol;
using System;
using UnityEngine;

public class GameRoot : MonoBehaviour 
{

    #region 属性 字段

    public static GameRoot Instance;



    public LoadingWnd loadingWnd;
    public DynamicWnd dynamicWnd;

    /// <summary>保存数据信息</summary>
   public PlayerData playerData=null;
    public PlayerData PlayerData
    {
        get
        {
            return playerData;
        }
    }
    #endregion


    #region 生命

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PECommon.Log("GameStart",LogType.Log);
        ClearUIRoot();
        Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            NetSvc.Instance.client.ShutDown();
        }
    }

    #endregion


    /// <summary>
    /// 控制初始化模块的顺序(乱了就空指针错误)
    /// </summary>
    void Init()
    {
        NetSvc netSvc = GetComponent<NetSvc>();
        netSvc.InitSvc();
        ResSvc res = GetComponent<ResSvc>();
        res.InitSvc();
        AudioSvc audio = GetComponent<AudioSvc>();
        audio.InitSvc();
        LoginSys login = GetComponent<LoginSys>();
        login.InitSys();
        //
        MainCitySys maincitySys = GetComponent<MainCitySys>();
        maincitySys.InitSys();
        InstanceSys instanceSys= GetComponent<InstanceSys>();
        instanceSys.InitSys();
        BattleSys battleSys = GetComponent<BattleSys>();
        battleSys.InitSys();
        //
        TimerSvc timerSvc = GetComponent<TimerSvc>();
        timerSvc.InitSys();
        //TestTimerSvc(); 
        //
        dynamicWnd.Init();
        login.EnterLogin();     


    }


    /// <summary>
    /// 桌面消息
    /// </summary>
    /// <param name="tips"></param>
    public static void AddTips(string tips)
    {
       Instance.dynamicWnd.AddTips(tips);
    }



    /// <summary>
    /// 从注册到登录
    /// </summary>
    public  void ClearUIRoot()
    {
        Transform canvas = transform.Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }

        dynamicWnd.SetWndState();
    }



    #region SetPlayerData
 /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="rspLogin"></param>
    public void SetPlayerData(RspLogin rspLogin)
    {
        this.playerData = rspLogin.pd;

     
    }

    internal void SetPlayerName(string name)
    {
        playerData.name = name;
    }

    public void SetPlayerDataByGuide(RspGuide data)
    {
        PlayerData.guideid = data.guideid;
        PlayerData.coin = data.coin;
        PlayerData.exp = data.exp;
        PlayerData.lv = data.lv;
    }

    internal void SetPlayerDataByStrong(GameMsg msg)
    {
        RspStrong data = msg.rspStrong;
        PlayerData.coin = data.coin;
        PlayerData.crystal = data.crystal;
        PlayerData.hp = data.hp;
        PlayerData.ad = data.ad;
        PlayerData.ap = data.ap;
        PlayerData.addef = data.addef;
        PlayerData.apdef = data.apdef;
        PlayerData.strongArr = data.strongArr;

    }

    internal void SetPlayerDataByBuy(GameMsg msg)
    {
        RspBuy   data = msg.rspBuy;

        //cost
        switch ( data.buyType )
        {
            case  BuyType.COIN:
                {
                    PlayerData.coin -= data.buyCnt;
                }
                break;
            case BuyType.DIAMOND:
                {
                    PlayerData.diamond -= data.buyCnt;
                }
                break;
            case BuyType.CRYSTAL:
                {
                    PlayerData.crystal -= data.buyCnt;
                }
                break;
            default:
                {

                }
                break;
        }
        //get

        switch (  data.goodType)
        {
            case GoodType.POWER :
                {
                    PlayerData.power += data.goodCnt;
                }
                break;
            case GoodType.COIN:
                {
                    PlayerData.coin += data.goodCnt;
                }
                break;
            default:
                {

                }
                break;
        }
    }



    internal void SetPlayerDataByPower(PshPower data)
    {
        PlayerData.power = data.power;
        //PECommon.Log("玩家体力："+data.power);
    }

    internal void SetPlayerDataByTaskReward(RspTakeTaskReward data)
    {
        PlayerData.coin = data.coin;
        PlayerData.lv = data.lv;
        PlayerData.exp = data.exp;
        PlayerData.taskRewardArr = data.taskArr;
    }

    internal void SetPlayerDataByTaskPrgs(PshTaskPrgs data)
    {
        PlayerData.taskRewardArr = data.taskArr;
    }
    internal void SetPlayerDataByInstance(RspInstanceFight data)
    {
        PlayerData.power = data.power;
    }

    internal void SetPlayerDataByInstanceEnd(RspInstanceFightEnd data)
    {
        PlayerData.coin=data.coin;
        PlayerData.crystal=data.crystal;
        PlayerData.instance=data.instance;
        PlayerData.exp=data.exp;
        PlayerData.lv=data.lv;

    }
    #endregion


    #region 测试代码
    void TestTimerSvc()
    {
        TimerSvc timerSvc = GetComponent<TimerSvc>();
        timerSvc.AddTimerTask(
            (int tid) => {
                PECommon.Log("5秒后");
            }, 5000
        );
    }




    #endregion

}