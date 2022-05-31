/****************************************************
    文件：BattleMgr.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 10:51:5
	功能：战场管理器
*****************************************************/

using System;
using UnityEngine;

public class BattleMgr : MonoBehaviour 
{


    [Header("BattleMgr")]

    ResSvc resSvc;
    AudioSvc audioSvc;
    //
    //
    MapMgr mapMgr;
    SkillMgr skillMgr;
    StateMgr stateMgr;
    //
    EntityPlayer playerEntity;//注入了stateMgr、playerCtrl
    PlayerController playerCtrl;

    public Vector2 dir;


    #region 实例地图 场景 人物


    public void InitMap(int mapID)
    {
        InitMgr_ToBattleRoot();
        InitSvc();
        //
        MapCfg cfg = resSvc.GetMapDataCfg(mapID);
        resSvc.AsyncLoadScene(cfg.sceneName, () => { InitScene(cfg); });
    }
    void InitMgr_ToBattleRoot()
    { 
        mapMgr=gameObject.AddComponent<MapMgr>();
        skillMgr = gameObject.AddComponent<SkillMgr>();
        stateMgr = gameObject.AddComponent<StateMgr>();
        mapMgr.Init();
        skillMgr.Init();
        stateMgr.Init();
    }
    void InitSvc()
    {
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }

    private void InitScene(MapCfg cfg)
    {
        GameRoot.AddTips("进入副本地图");
        GameObject map = GameObject.FindGameObjectWithTag(Tags.MapRoot);
        //
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one;
        //
        LoadPlayer(cfg);
        audioSvc.PlayBgMusic(Constants.BGHuangYe);
        //
        InstanceSys.Instance.playerCtrlWnd.SetWndState();
    }


    /// <summary>
    /// 加载游戏主角
    /// </summary>
    /// <param name="cfg"></param>
    void LoadPlayer(MapCfg cfg)
    {
        GameObject player = resSvc.LoadPrefab(PathDefine.AssassinBattlePlayerPrefab, true);
        //
        player.transform.position = cfg.playerBornPos;
        player.transform.localEulerAngles = cfg.playerBornRote;
        Camera.main.transform.position = cfg.mainCamPos;
        Camera.main.transform.localEulerAngles = cfg.mainCamRote;
        //
        playerCtrl = player.GetComponent<PlayerController>();
        playerCtrl.Init();
        //
        InitEntityPlayer(this.stateMgr, playerCtrl);
    }

    /// <summary>
    /// stateMgr注入逻辑实体类
    /// </summary>
    /// <param name="stateMgr"></param>
    private void InitEntityPlayer(StateMgr stateMgr, PlayerController playerCtrl)
    {
        playerEntity = new EntityPlayer
        {
            stateMgr = stateMgr,
            ctrl = playerCtrl,
        };
    }


    #endregion

    #region 移动 攻击


    void ReleaseNormalAttack()
    {
        PECommon.Log("ReleaseNormalAttack");
        playerEntity.SetAction(0);
    }
    void ReleaseSkill1()
    {
        PECommon.Log("ReleaseSkill1");
        playerEntity.SetAction(1);
    }
    void ReleaseSkill2()
    {
        PECommon.Log("ReleaseSkill2");
        playerEntity.SetAction(2);
    }
    void ReleaseSkill3()
    {
        PECommon.Log("ReleaseSkill3");
        playerEntity.SetAction(3);
    }


    public void ReqReleaseSkill(int idx)
    {

        switch (idx)
        {
            case 0:
                {
                    ReleaseNormalAttack();

                }
                break;
            case 1:
                {
                    ReleaseSkill1();
                }
                break;
            case 2:
                {
                    ReleaseSkill2();
                }
                break;
            case 3:
                {
                    ReleaseSkill3();
                }
                break;
            default: break;
        }

    }

    public void SetMoveDir(Vector2 dir)
    {
        //PECommon.Log(dir.ToString());
        if (dir == Vector2.zero)
        {
            playerEntity.Idle();//动画的逻辑和表现
            playerEntity.SetDir(Vector2.zero);//转向和移动
        }
        else
        {
            playerEntity.Move();
            playerEntity.SetDir(dir);
        }
    }
    #endregion


}