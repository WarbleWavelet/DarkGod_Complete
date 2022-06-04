/****************************************************
    文件：BattleMgr.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 10:51:5
	功能：战场管理器
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr : MonoBehaviour 
{


    [Header("BattleMgr")]

   public ResSvc resSvc;
   public AudioSvc audioSvc;
    //
    //
  public  MapMgr mapMgr;
  public  SkillMgr skillMgr;
  public  StateMgr stateMgr;
    //
   public EntityPlayer playerEntity;//注入了stateMgr、playerCtrl
    PlayerController playerCtrl;

    public Vector2 dir;

    public MapCfg mapCfg;
    
    public List<GameObject> monsterLst = new List<GameObject>();

    #region 实例地图 场景 人物
    void Awake()
    {
            InitSvc();

      
    }

    public void InitMap(int mapID)
    {

        CtrlInit(mapID);
        //
       
        resSvc.AsyncLoadScene(mapCfg.sceneName, () => { InitScene(mapCfg); });
    }

    /// <summary>
    /// 控制顺序
    /// </summary>
    void CtrlInit(int mapID)
    {
        InitSvc();
        mapCfg = resSvc.GetMapCfg(mapID);
        InitMgr_ToBattleRoot(); 
    }

    void InitMgr_ToBattleRoot()
    { 
        mapMgr=gameObject.AddComponent<MapMgr>();
        skillMgr = gameObject.AddComponent<SkillMgr>();
        stateMgr = gameObject.AddComponent<StateMgr>();
        skillMgr.Init();
        stateMgr.Init();
        mapMgr.Init(this);

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
        LoadPlayerByMapCfg(cfg);
        audioSvc.PlayBgMusic(Constants.BGHuangYe);
        //
        InstanceSys.Instance.playerCtrlWnd.SetWndState();

       //LoadMonsterByWave(0);
    }


    /// <summary>
    /// 加载游戏主角,一个地图就一个玩家
    /// </summary>
    /// <param name="cfg"></param>
    void LoadPlayerByMapCfg(MapCfg cfg)
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
            skillMgr=this.skillMgr,
            battleMgr=this
        };
    }


    #endregion

    #region 移动 攻击


    void ReleaseNormalAttack()
    {
        PECommon.Log("ReleaseNormalAttack");
        playerEntity.Attack(0);
    }
    void ReleaseSkill1()
    {
        PECommon.Log("ReleaseSkill1");
        playerEntity.Attack(1);
    }
    void ReleaseSkill2()
    {
        PECommon.Log("ReleaseSkill2");
        playerEntity.Attack(2);
    }
    void ReleaseSkill3()
    {
        PECommon.Log("ReleaseSkill3");
        playerEntity.Attack(3);
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
        if (playerEntity.canCtrl == false) 
            return;


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

    public Vector2 GetInputDir()
    {
        return BattleSys.Instance.GetInputDir();
    }
    #endregion


    #region Monster
    /// <summary>
    /// 加载该地图第几波怪物
    /// </summary>
    /// <param name="wave">第几波</param>
    public void LoadMonsterByWave(int wave)
    {
        for (int i = 0; i < mapCfg.monsterLst.Count; i++)
        {

            MonsterData data = mapCfg.monsterLst[i];
            
            if (data.mWave == wave)
            {
                //物体
                MonsterCfg cfg = resSvc.GetMonsterCfg(data.ID);
                GameObject go = resSvc.LoadPrefab(cfg.resPath, true);
                go.name = cfg.mName + "_" + data.mWave + "_" + data.mIndex;
                go.transform.position = data.mBornPos;
                go.transform.localEulerAngles = data.mBornRot;
                go.transform.localScale = Vector3.one;
                
                //表现实体
                MonsterController mCtrl = go.GetComponent<MonsterController>();
                mCtrl.Init();

                //逻辑实体
                EntityMonster entity = InitEntityMonster(stateMgr, mCtrl);
                monsterLst.Add(go);
                // DontDestroyOnLoad(go);
                //
                // go.SetActive(true);

                print( go.name+"   "+go.GetInstanceID() );
            }
        }

        for (int i = 0; i < monsterLst.Count; i++)
        {
            if (monsterLst[i] != null)
                DontDestroyOnLoad(monsterLst[i]);
        }
    }



    private EntityMonster InitEntityMonster(StateMgr stateMgr, MonsterController monsterCtrl)
    {
         EntityMonster entityMonster = new EntityMonster
         {
            stateMgr = stateMgr,
            ctrl = monsterCtrl,
            skillMgr = this.skillMgr,
            battleMgr = this
        };

        return entityMonster;
    }
    #endregion
}