/****************************************************
    文件：MainCitySys.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/4 23:23:1
	功能：主城业务系统
*****************************************************/

using PEProtocol;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainCitySys : SystemRoot
{

    #region 字段

    [Header("MainCitySys")]
    public static MainCitySys Instance;
    public MainCityWnd maincityWnd;
    public PlayerController playerCtrl;

    [Header("人物信息")]
    public InfoWnd infoWnd;
    public Transform charCamTrans;


    //
    public float imgPlayerRotate;
    public float playerStartRotate;
    //



    [Header("自动任务")]
    public Transform[] npcPosTrans = new Transform[4];
    public AutoGuideCfg agc;
    public NavMeshAgent nav;
    public Transform navTarget;
    public bool isNavGuide = false;
    public float navStoppedDis = 0.5f;
    public GuideWnd guideWnd;


    [Header("强化铸造")]
    public StrongWnd strongWnd;
    public BuyWnd buyWnd;


    [Header("聊天")]
    public ChatWnd chatWnd;


    [Header("任务")]
    public TaskWnd taskWnd;
    List<TaskRewardData> taskDataLst = new List<TaskRewardData>();



    [Header("副本")]
    public InstanceWnd instanceWnd;


    MapCfg cfg;

    #endregion


    void Update()
    {
        if (isNavGuide)
        {
            playerCtrl.SetMainCamera();

            if (IsNavArrived())
            { 
                StopNavTask();
                OpenGuideWnd();
            }
        }

    }

    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        PECommon.Log("MainCitySys Init");
    }





    #region Scene
    public void EnterMainCity(EnterMainCityType _type = EnterMainCityType.None)
    {
        Action action = EventHandler_EnterMainCity;

        switch (_type)
        {
            case  EnterMainCityType.None:break;
            case EnterMainCityType.InstanceSys:
                {
                    action+= ()=>{ InstanceSys.Instance.SetInstanceState(); };
                } 
                break;
            default:break;
        }
            
        cfg = ResSvc.Instance.GetMapCfg(Constants.MainCityMapID);

        resSvc.AsyncLoadScene(cfg.sceneName, () => {

            if (action != null)
            {
                action();
            }
        });
    }

    void EventHandler_EnterMainCity()
    {
        PECommon.Log("进入主城");
        //
        if (resSvc != null)
        {
            LoadPlayer(cfg);
        }
        else
        {
            throw new Exception("异常");
        }
        GameRoot.Instance.ClearUIRoot();
        maincityWnd.SetWndState();
        audioSvc.PlayBgMusic(Constants.BGMainCity);
        //
        charCamTrans = GameObject.FindGameObjectWithTag(Tags.CharShowCam).transform;
        //
        GetNpcPosTrans();
        if (playerCtrl.transform.GetComponent<NavMeshAgent>() == null)
        {
            nav = playerCtrl.transform.gameObject.AddComponent<NavMeshAgent>();
        }
        else
        {
            nav = playerCtrl.transform.GetComponent<NavMeshAgent>();
        }
        strongWnd.RefreshItem(0);
        //
        GameRoot.Instance.GetComponent<AudioListener>().enabled = true;
    }

           




    #endregion


    #region 玩家
    /// <summary>
    /// 加载游戏主角
    /// </summary>
    /// <param name="mapData"></param>
    void LoadPlayer(MapCfg mapData)
    {
        GameObject player = resSvc.LoadPrefab(PathDefine.AssassinCityPlayerPrefab, true);
        //
        player.transform.position = mapData.playerBornPos;
        player.transform.localEulerAngles = mapData.playerBornRote;
        player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Camera.main.transform.position = mapData.mainCamPos;
        Camera.main.transform.localEulerAngles = mapData.mainCamRote;
        //
        playerCtrl = player.GetComponent<PlayerController>();
        playerCtrl.Init();
    }


    /// <summary>
    /// 通过摇杆控制移动方向
    /// </summary>
    /// <param name="dir"></param>
    public void SetMoveDir(Vector2 dir)
    {
        StopNavTask();
        //
        if (dir == Vector2.zero)
        {
            playerCtrl.SetAniBlend(Constants.BlendIdle);
        }
        else
        {
            playerCtrl.SetAniBlend(Constants.BlendWalk);
        }
        playerCtrl.Dir = dir;

    }



    public void InitPlayerRotate()
    {
        playerStartRotate = playerCtrl.transform.localEulerAngles.y;
    }

    public void SetPlayerRotate(float rotate)
    {
        playerCtrl.transform.localEulerAngles = new Vector3(0f, playerStartRotate + rotate, 0f);
    }


    #endregion


    #region 人物信息页
    /// <summary>
    /// 角色信息面板的展示
    /// </summary>
    /// <param name="state"></param>
    public void SetCharShowCamState(bool state = true)
    {

        if (charCamTrans == null)
        {
            charCamTrans = GameObject.FindGameObjectWithTag(Tags.CharShowCam).transform;
        }

        //playerCtrl = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
        Transform player = playerCtrl.transform;

        if (charCamTrans != null)
        {
            charCamTrans.localPosition = player.position + player.forward * 2.8f + new Vector3(0f, 1.2f, 0f);//人物前上方
            charCamTrans.localEulerAngles = new Vector3(0f, 180f + player.localEulerAngles.y, 0f);//相机对着人物
            charCamTrans.localScale = Vector3.one;


            charCamTrans.gameObject.SetActive(state);
        }



    }



    public void OpenInfoWnd()
    {
        StopNavTask();
        StopInoutADWS();

        SetCharShowCamState(true);
        infoWnd.SetWndState();


    }

    public void CloseInfoWnd()
    {
        StartInoutADWS();
        SetCharShowCamState(false);
        infoWnd.SetWndState(false);
    }
    #endregion


    #region 引导
    internal void RunNavTask(AutoGuideCfg agc)
    {
        if (agc != null)
        {
            this.agc = agc;
        }

        if (this.agc.npcID != -1)
        {
            isNavGuide = true;
            GetNpcPosTrans();
            navTarget = npcPosTrans[this.agc.npcID];
            RunNavTask();
        }
    }


    /// <summary>
    /// 已经到达？
    /// </summary>
    /// <returns></returns>
    bool IsNavArrived()
    {
        return Vector3.Distance(playerCtrl.transform.position, navTarget.position) < navStoppedDis;
    }
    void RunNavTask()
    {
        if (isNavGuide)
        { 
            nav.enabled = true;
        //
            nav.speed = Constants.PlayerMoveSpeed;
            nav.SetDestination(navTarget.position);
            playerCtrl.SetAniBlend(Constants.BlendWalk);
        }
    }

    public void StopInoutADWS()
    {
        playerCtrl.CanMove = false;
    }
    public void StartInoutADWS()
    {
        playerCtrl.CanMove = true;
    }
    public void StopNavTask()
    {
        if (isNavGuide)
        {

            navTarget = null;
            nav.enabled = false;
            //
            playerCtrl.SetAniBlend(Constants.BlendIdle);
            isNavGuide = false;
        }

    }
    private void GetNpcPosTrans()
    {
        Transform map = GameObject.FindGameObjectWithTag(Tags.MapRoot).transform;
        npcPosTrans= map.GetComponent<MainCityMap>().NpcPosTrans;
    }

    public void OpenGuideWnd()
    {
        guideWnd.SetWndState();
 
    }

    public void CloseGuideWnd()
    {
        guideWnd.SetWndState(false);
    }


    void ErrorNavCode()
    {
        //float dis= (ctrl.transform.position - navTarget.position).magnitude;

        //if (nav.remainingDistance  < 0.5f)
    }

    internal AutoGuideCfg GetCurTaskData()
    {
        return this.agc;
    }



    public void RspGuide(GameMsg msg)
    {
      
        RspGuide data = msg.rspGuide;

        GameRoot.AddTips(Constants.Color( "获得奖励！",TxtColor.Blue));
        GameRoot.AddTips(Constants.Color("获得经验:" +agc.exp, TxtColor.Blue));
        GameRoot.AddTips(Constants.Color("获得金币:" +agc.coin, TxtColor.Blue));

        switch ( agc.actID )
        {
            case  0:
                {
                    GameRoot.AddTips("已完成任务："+resSvc.GetGuideCfg(data.guideid).ID);

                }
                break;
            case 1:
                {
                    EnterInstance();
                }
                break;
            case 2:
                {
                    //强化
                    OpenStrongWnd();
                }
                break;
            case 3:
                {
                    //体力
                    OpenAddPowerWnd();
                }
                break;
            case 4:
                {
                    //金币
                    OpenMKCoinWnd();
                }
                break;
            case 5:
                {
                    //聊天
                    chatWnd.SetWndState();
                }
                break;
            default: break;
        }

        GameRoot.Instance.SetPlayerDataByGuide(data);
        maincityWnd.RefreshUI();

        if (msg.pshTaskPrgs != null)
        {
            PshTaskPrgs(msg);
        }

    }




    #endregion



    #region 强化
    internal void RspStrong(GameMsg msg)
    {
        GameRoot.Instance.SetPlayerDataByStrong (msg);
        strongWnd.UpdateUI();

        if (msg.pshTaskPrgs != null)
        {
           PshTaskPrgs (msg);
        }
    }
    public void OpenStrongWnd()
    {
      Common_BeforeOpenWnd();
        strongWnd.SetWndState();
    }

    public void CloseStrongWnd()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        strongWnd.SetWndState(false);
    }
    #endregion



    #region 聊天
    public void PshChat(GameMsg msg)
    {
        PshChat data = msg.pshChat;
        chatWnd.AddChatMsg(data.name,data.chat);

        if (msg.pshTaskPrgs != null)
        {
            MainCitySys.Instance.PshTaskPrgs(msg);
        }

    }
    #endregion


    #region 任务
    public void OpenTaskRewardWnd()
    {
        Common_BeforeOpenWnd();
        if (taskWnd.GetWndState())
        {
            
        }
        else
        { 
           taskWnd.SetWndState();
            taskWnd.RefreshUI();
        }
     

    }

    internal void PshTaskPrgs(GameMsg msg)
    {
        
        PshTaskPrgs data = msg.pshTaskPrgs;
        GameRoot.Instance.SetPlayerDataByTaskPrgs(data);
        
        maincityWnd.RefreshUI();
        if (taskWnd.GetWndState())
        { 
            taskWnd.RefreshUI();
        }      
    }

    internal void RspTakeTaskReward(GameMsg msg)
    {
        RspTakeTaskReward data = msg.rspTakeTaskReward;
        GameRoot.Instance.SetPlayerDataByTaskReward(data);

        maincityWnd.RefreshUI();
        taskWnd.RefreshUI();
    }


    #endregion

    #region 副本
    /// <summary>
    /// 进入副本
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    internal void EnterInstance()
    {
        Common_BeforeOpenWnd();
        instanceWnd.SetWndState(true);
    }
    public void OpenInstanceWnd()
    {
        Common_BeforeOpenWnd();
        instanceWnd.SetWndState(true);
    }

    #endregion



    #region 购买、交易
    internal void OpenMKCoinWnd()
    {
        Common_BeforeOpenWnd();
        buyWnd.SetWndState();
        buyWnd.RefreshUI(10, BuyType.DIAMOND, 1000, GoodType.COIN);
    }

    internal void OpenAddPowerWnd()
    {
        Common_BeforeOpenWnd();
        buyWnd.SetWndState();
        buyWnd.RefreshUI(10, BuyType.DIAMOND, 100, GoodType.POWER);
    }
    internal void RspPower(GameMsg msg)
    {
        PshPower data = msg.pshPower;
        GameRoot.Instance.SetPlayerDataByPower(data);
        if (maincityWnd.gameObject.activeSelf)
        {
            maincityWnd.RefreshUI();
        }


    }

    internal void RspBuy(GameMsg msg)
    {
        PshTaskPrgs(msg);
        GameRoot.Instance.SetPlayerDataByBuy(msg);
        maincityWnd.RefreshUI();
        GameRoot.AddTips("购买成功！");
        buyWnd.btnSure.interactable = true;
        //
        if (msg.pshTaskPrgs != null)
        {
            PshTaskPrgs(msg);

        }
    }
    #endregion



    #region Common
    /// <summary>
    /// 打开WND之前的统一操作
    /// </summary>
    void Common_BeforeOpenWnd()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);    
        StopNavTask ();
    }
    #endregion

}

/// <summary>
/// 回到主城后的操作
/// </summary>
public enum EnterMainCityType
{
    None,
    /// <summary> 副本地图</summary>
    InstanceSys
}