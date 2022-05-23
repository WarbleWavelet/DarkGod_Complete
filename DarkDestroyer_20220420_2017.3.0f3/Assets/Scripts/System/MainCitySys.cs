/****************************************************
    文件：MainCitySys.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/4 23:23:1
	功能：主城业务系统
*****************************************************/

using PEProtocol;
using System;
using UnityEngine;
using UnityEngine.AI;

public class MainCitySys : SystemRoot
{
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

        //maincityWnd = transform.Find("MainCityWnd").GetComponent<MainCityWnd>();
        PECommon.Log("Init MainCitySys");
    }





    #region Scene
    public void EnterMainCity()
    {
        MapCfg cfg = ResSvc.Instance.GetMapDataCfg(Constants.MainCityMapID);
        resSvc.AsyncLoadScene(cfg.sceneName, () => {
            PECommon.Log("进入主城");
            //
            if (resSvc != null)
            {
                LoadPlayer(cfg);
            }
            else
            {
                throw new System.Exception("异常");
            }
            GameRoot.Instance.ClearUIRoot();
            maincityWnd.SetWndState();
            audioSvc.PlayBgMusic(Constants.BGMainCity);
            //
            charCamTrans = GameObject.FindGameObjectWithTag(Tags.CharShowCam).transform;
            //
            GetNpcPosTrans();
            nav = playerCtrl.transform.GetComponent<NavMeshAgent>();
            strongWnd.RefreshItem(0);

        });
    }




    #endregion


    #region Player
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
            playerCtrl.SetBlend(Constants.BlendIdle);
        }
        else
        {
            playerCtrl.SetBlend(Constants.BlendWalk);
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

    internal void OpenMKCoinWnd()
    {
        buyWnd.SetWndState();
        buyWnd.RefreshUI(10,BuyType.DIAMOND,1000,GoodType.COIN);
    }

    internal void OpenAddPowerWnd()
    {
       
        buyWnd.SetWndState();
        buyWnd.RefreshUI(10, BuyType.DIAMOND, 100, GoodType.POWER);
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


        Transform player = playerCtrl.transform;

        charCamTrans.localPosition = player.position + player.forward * 2.8f + new Vector3(0f, 1.2f, 0f);//人物前上方
        charCamTrans.localEulerAngles = new Vector3(0f, 180f + player.localEulerAngles.y, 0f);//相机对着人物
        charCamTrans.localScale = Vector3.one;


        charCamTrans.gameObject.SetActive(state);


    }



    public void OpenInfoWnd()
    {
        StopNavTask();
        SetCharShowCamState(true);
        infoWnd.SetWndState();


    }

    public void CloseInfoWnd()
    {

        SetCharShowCamState(false);
        infoWnd.SetWndState(false);
    }
    #endregion


    #region AutoTask
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
        GameRoot.Instance.SetPlayerDataByBuy(msg);
         maincityWnd.RefreshUI();
        GameRoot.AddTips("购买成功！");
        buyWnd.btnSure.interactable = true;
    }

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
            playerCtrl.SetBlend(Constants.BlendWalk);


        }

    }

  public void StopNavTask()
    {
        if (isNavGuide)
        {

            navTarget = null;
            nav.enabled = false;
            //
            playerCtrl.SetBlend(Constants.BlendIdle);
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


    #region Rsp
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
                    // 智者
                }
                break;
            case 1:
                {
                    //副本
                }
                break;
            case 2:
                {
                    //强化
                }
                break;
            case 3:
                {
                    //体力
                }
                break;
            case 4:
                {
                    //金币
                }
                break;
            case 5:
                {
                    //聊天
                }
                break;
            default:
                {

                }
                break;
        }

        GameRoot.Instance.SetPlayerDataByGuide(data);
        maincityWnd.RefreshUI();

    }

    internal void RspStrong(GameMsg msg)
    {
        GameRoot.Instance.SetPlayerDataByStrong (msg);
        strongWnd.UpdateUI();
    }
    #endregion

    #endregion



    #region 强化
    public void OpenStrongWnd()
    {
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

    }
    #endregion


}