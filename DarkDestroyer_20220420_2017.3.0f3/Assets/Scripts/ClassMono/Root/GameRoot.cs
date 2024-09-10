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


    /// <summary>不进行联网,方便单元测试</summary>
    public bool TestWithoutNetSvc;
    public static GameRoot Instance;
    public LoadingWnd loadingWnd;
    public DynamicWnd dynamicWnd;
    public PlayerCtrlWnd playerCtrlWnd;
   //
    /// <summary>保存数据信息</summary>
    public PlayerData playerData=null;
    public PlayerData PlayerData
    {
        get
        {
            return playerData;
        }
    }

    LoginSys _login;//单元测试放这里
    #endregion


    #region Life

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

    
    #region internal

  /// <summary>
    /// 桌面消息
    /// </summary>
    /// <param name="tips"></param>
    internal static void AddTips(string tips)
    {
       Instance.dynamicWnd.AddTips(tips);
    }



    /// <summary>
    /// 从注册到登录
    /// </summary>
    internal  void ClearUIRoot()
    {
        Transform canvas = transform.Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).Hide();
        }
        if (!TestWithoutNetSvc && dynamicWnd != null) //有时战斗场景单独拎出来整理,不需要dynamicWnd
        { 
            dynamicWnd.SetWndState();
        }
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="rspLogin"></param>
    internal void SetPlayerData(RspLogin rspLogin)
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


    #region pri


    /// <summary>
    /// 控制初始化模块的顺序(乱了就空指针错误)
    /// </summary>
    void Init()
    {
        if (!TestWithoutNetSvc) 
        {
            NetSvc netSvc = GetComponent<NetSvc>();
            netSvc.InitSvc();        
        }

        ResSvc res =gameObject.GetOrAddComponent<ResSvc>();
        res.InitSvc();
        AudioSvc audio = gameObject.GetOrAddComponent<AudioSvc>();
        audio.InitSvc();
        if (!TestWithoutNetSvc)
        {
            _login = gameObject.GetOrAddComponent<LoginSys>();
            _login.InitSys();
        }
        //
       // if (!TestWithoutNetSvc)
        if (true)//单例初始化强需要
        {
            MainCitySys maincitySys = gameObject.GetOrAddComponent<MainCitySys>();
            maincitySys.InitSys();
        }
        InstanceSys instanceSys= gameObject.GetOrAddComponent<InstanceSys>();
        instanceSys.InitSys();
        BattleSys battleSys = gameObject.GetOrAddComponent<BattleSys>();
        battleSys.InitSys();
        //
        TimerSvc timerSvc = gameObject.GetOrAddComponent<TimerSvc>();
        timerSvc.InitSys();
        //TestTimerSvc(); 
        //
        if (!TestWithoutNetSvc)//伤害飘数字,不能扔掉
        {
            dynamicWnd.Init();
            _login.EnterLogin();
        }
        else //模拟Net,逻辑在Server
        {
            dynamicWnd.Init();
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.RspInstanceFight,
                rspInstanceFight = new RspInstanceFight
                {
                    power = 100,
                    instance = 10001//副本1
                }
            };
            InstanceSys.Instance.RspInstanceFight(msg);
        }
    }


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
    #endregion

}