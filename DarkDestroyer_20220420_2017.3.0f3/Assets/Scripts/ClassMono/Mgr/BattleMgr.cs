/****************************************************
    文件：BattleMgr.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 10:51:5
	功能：战场管理器
*****************************************************/

using PEProtocol;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMgr : MonoBehaviour 
{


    #region 字属
    [Header("BattleMgr")]

    public ResSvc resSvc;
    public AudioSvc audioSvc;
    public TimerSvc timer;
    //
    //
    public MapMgr mapMgr;
    public SkillMgr skillMgr;
    public StateMgr stateMgr;
    //
    public EntityPlayer playerEntity;//注入了stateMgr、playerCtrl
   public PlayerController playerCtrl;

    public Vector2 dir;

    public MapCfg mapCfg;

    Dictionary<string, EntityMonster> monsterDic = new Dictionary<string, EntityMonster>();


    [Header("Combo")]
    public double lastAtkTime = 0d;
    /// <summary>Combo的平A的SkillID</summary>
    public int[] comboArr = new int[] { Constants.AttackID1, Constants.AttackID2, Constants.AttackID3, Constants.AttackID4, Constants.AttackID5 };
    public int comboIndex = 0;


    [Header("MapMgr")]
    public Dictionary< int, TriggerData> monsterWaveDic = new Dictionary< int, TriggerData>();
    /// <summary>检测下一波敌人</summary>
    public bool ckeckWave = true;

    public bool isPauseGame = false;
    #endregion

    void Awake()
    {
        InitSvc();
        ckeckWave = true ;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    EndBattle(true, playerEntity.HP);
        //}
        if (ckeckWave && mapMgr != null && monsterDic.Count == 0)
        {
           bool isExist= mapMgr.SetNextMonsterWave();
            ckeckWave = false;

            if (isExist == false)
            {
                EndBattle( true, playerEntity.HP);
            }
        }
            // Monster AI
        if (monsterDic == null || monsterDic.Count == 0)  return;
        foreach (var item in monsterDic)
        {
            EntityMonster monster = item.Value;
            monster.GetGameObject().GetComponent<AIMonster>().TickAILogic();
            monster.ctrl.curState = monster.CurState;
            monster.ctrl.runAI = monster.aiMonster.runAI;

        }



    }

    #region 实例地图 场景 人物


    public void InitMap(int mapID, Action cb=null)
    {

        CtrlInit(mapID);
        //
       
        resSvc.AsyncLoadScene(mapCfg.sceneName, () => { InitScene(mapCfg); });
    }

    #region Init
  /// <summary>
    /// 控制顺序
    /// </summary>
    void CtrlInit(int mapID, Action cb = null)
    {
        InitSvc();
        mapCfg = resSvc.GetMapCfg(mapID);
        InitMgr_ToBattleRoot(); 
    }

    void InitMgr_ToBattleRoot( Action cb = null)
    { 
        mapMgr=gameObject.AddComponent<MapMgr>();

        //
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
        timer = TimerSvc.Instance;
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
        audioSvc.PlayBgMusic(Constants.InstanceHuangYe);
        //
        InstanceSys.Instance.playerCtrlWnd.SetWndState();

        //LoadMonsterByWave(0);
        DelayActiveMonster();

        //不同场景节点
        GameObject[] gos = GameObject.FindGameObjectsWithTag(Tags.Door);
        foreach (GameObject door in gos)
        {
            TriggerData trigger = door.AddComponent<TriggerData>();
            trigger.mapMgr = mapMgr;
            trigger.waveIdx = int.Parse( door.name.Substring( door.name.Length-1 , 1 ) )-1;
            monsterWaveDic.Add(   trigger.waveIdx, trigger);
            mapMgr.triggerDataLst.Add(trigger);
        }

        // AudioListener让位于 Player
        GameRoot.Instance.GetComponent<AudioListener>().enabled = false;
    }
 #endregion  
    #endregion


    #region Player
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
        InitPlayerBattleProps( playerEntity );

        //设置音效
        List<AudioSource> audioLst = new List<AudioSource>();
        audioLst.Add(playerCtrl.gameObject.GetComponent<AudioSource>());
        Transform t = playerCtrl.transform.Find("Bip_master");
        for (int i = 0; i < t.childCount; i++)
        {
            AudioSource audio=t.GetChild(i).GetComponent<AudioSource>();
            if (audio != null)
            { 
                audioLst.Add( audio);
            }
        }

        foreach (var item in audioLst)
        {
            item.volume = 0.25f;
        }
    }

    /// <summary>
    /// stateMgr注入逻辑实体类
    /// </summary>
    /// <param name="stateMgr"></param>
    private void InitEntityPlayer(StateMgr stateMgr, PlayerController ctrl)
    {
        playerEntity = new EntityPlayer
        {
            stateMgr = stateMgr,
            skillMgr = this.skillMgr,
            battleMgr = this,
            Name = ctrl.gameObject.name,
            combo = ctrl.gameObject.AddComponent<Combo>(),
            entityType=EntityType.Player,
            skillCalback = new SkillCalback()
        };
        playerEntity.SetCtrl(ctrl);

        //

    }

    void InitPlayerBattleProps(EntityBase entity)
    {
        PlayerData pd = GameRoot.Instance.PlayerData;
        BattleProps props = new BattleProps
        {
            hp = pd.hp,
            ad = pd.ad,
            ap = pd.ap,
            addef = pd.addef,
            apdef = pd.apdef,
            dodge = pd.dodge,
            critical = pd.critical,
            pierce = pd.pierce,

        };
        entity.SetBattleProps(props);
    }

    #endregion





    #region Player 攻击
  public void ReqReleaseSkill(int idx)
    {
        switch (idx)
        {
            case 0: ReleaseNormalAttack(); break;
            case 1: ReleaseSkill1(); break;
            case 2: ReleaseSkill2(); break;
            case 3: ReleaseSkill3(); break;
            default: break;
        }

    }

    
    /// <summary>
    /// 平A和连招
    /// </summary>
    void ReleaseNormalAttack()
    {
        PECommon.Log("ReleaseNormalAttack");

        switch ( playerEntity.CurState )
        {
            case EAniState.Attack:
                {
                    double curAtkTime = TimerSvc.Instance.GetNowTime();
                    if ( (curAtkTime - lastAtkTime) < Constants.ComboSpace && 
                        IsBelongCombo() )
                    {
                        bool isComboFull = comboIndex >= comboArr.Length - 1;
                        if (isComboFull)
                        {
                            ResetCombo();
                        }
                        else
                        {
                            PlayCombo(curAtkTime);
                        }
                    }
                }
                break;
            case EAniState.Idle:
            case EAniState.Move:
            case EAniState.None:
                {
                    comboIndex = 0; 
                    lastAtkTime = TimerSvc.Instance.GetNowTime();
                    //
                    playerEntity.StateAttack( Constants.AttackID1 );
                }
                break;
            default:break;
        }

    }
    bool IsBelongCombo()
    {
        return lastAtkTime != 0;
    }

    /// <summary>
    /// Reset Combo 时间和Index
    /// </summary>
   public void ResetCombo()
    {
        comboIndex = 0;
        lastAtkTime = 0;
    }

    void PlayCombo(double curAtkTime)
    {
        comboIndex++;
        lastAtkTime = curAtkTime;
        //
        int skillID = comboArr[comboIndex];
        playerEntity.combo.EnqueueComboQue(skillID);
    }


    void ReleaseSkill1()
    {
        PECommon.Log("ReleaseSkill1");
        playerEntity.StateAttack(Constants.SkillID1);
    }
    void ReleaseSkill2()
    {
        PECommon.Log("ReleaseSkill2");
        playerEntity.StateAttack(Constants.SkillID2);
    }
    void ReleaseSkill3()
    {
        PECommon.Log("ReleaseSkill3");
        playerEntity.StateAttack(Constants.SkillID3);
    }
    #endregion

    #region Player 移动
    public void SetPlayerMoveDir( Vector2 dir)
    {
        if (playerEntity ==null || playerEntity.canCtrl == false) 
            return;

        // 防止技能、连招被打断时，还在移动，尤其这时的速度是技能速度，所以很快
        if (playerEntity.CurState == EAniState.Idle || playerEntity.CurState == EAniState.Move)
        { 
            //PECommon.Log(dir.ToString());
            if (dir == Vector2.zero)
            {
                playerEntity.StateIdle();//动画的逻辑和表现
                playerEntity.SetDir(Vector2.zero);//转向和移动
            }
            else
            {
                playerEntity.StateMove();
                playerEntity.SetDir(dir);
            }
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
                go.name = cfg.mName + "_Wave" + data.mWave + "_Idx" + data.mIndex;
                go.transform.position = data.mBornPos;
                go.transform.localEulerAngles = data.mBornRot;
                go.transform.localScale = Vector3.one;
                
                //表现实体
                MonsterController mCtrl = go.GetComponent<MonsterController>();
                mCtrl.Init();

                //逻辑实体
                EntityMonster entity = InitEntityMonster(stateMgr, mCtrl,data);
                monsterDic.Add(go.name, entity);

                //加血条

                switch ( entity.monsterData.mCfg.mType)
                {
                    case MonsterType.Solider :
         
                        {

                            Transform hpRoot = go.transform.Find("hpRoot");
                            GameRoot.Instance.dynamicWnd.AddHpItemInfo(go.transform, hpRoot, go.name, entity.Props.hp);

                        }
                        break;
                    case MonsterType.Boss:
                        {

                            Transform t = GameRoot.Instance.transform.Find("PlayerCtrlWnd/RightTopPin");
                            Transform hpRoot = go.transform.GetChild(0).Find("hpRoot");
                            BattleSys.Instance.playerCtrlWnd.SetBossHPState(true, entity.Props.hp);
                        }
                        break;
                    default: break;
                }

                //print( go.name+"   "+go.GetInstanceID() );


            }
        }


        foreach (var item in monsterDic)
        {
            GameObject go = item.Value.GetGameObject();
            DontDestroyOnLoad( go );
            go.SetActive(false);
        }
    }



    private EntityMonster InitEntityMonster(StateMgr stateMgr, MonsterController ctrl, MonsterData data)
    {
         EntityMonster entityMonster = new EntityMonster
         {
            stateMgr = stateMgr,
            skillMgr = this.skillMgr,
            battleMgr = this,
            monsterData=data,
            Name=ctrl.gameObject.name,
            aiMonster=ctrl.gameObject.AddComponent<AIMonster>(),
            entityType=EntityType.Monster,
            skillCalback=new SkillCalback(),
             combo = ctrl.gameObject.AddComponent<Combo>(),

         };
        entityMonster.SetCtrl(ctrl);
        entityMonster.SetBattleProps( data.mCfg.props);
        entityMonster.aiMonster.Init( entityMonster,playerEntity);//这时playerEntity未赋值，所以到EntityInitPlayer
        //foreach (var item in monsterDic)
        //{
        //    EntityMonster monster = item.Value;
        //    monster.aiMonster.Init(monster, playerEntity);
        //}

        return entityMonster;
    }

    /// <summary>
    /// 获取Monster
    /// </summary>
    /// <returns></returns>
    public List<EntityMonster> GetEntityMonster()
    {
        List<EntityMonster> lst = new List<EntityMonster>();
        foreach (var item in monsterDic)
        {
            lst.Add(item.Value);
        }
        return lst;
    }


    /// <summary>
    /// delay后产生敌人
    /// </summary>
    /// <param name="state"></param>
    /// <param name="delay"></param>
   public void DelayActiveMonster(bool state =true)
    {
        
        timer.AddTimerTask((int tid)=> {
            foreach (var item in monsterDic)
            {
                EntityMonster entity = item.Value;

                entity.SetActive(state);
                entity.StateBorn();
                timer.AddTimerTask((int tid_1 ) => { 
                    entity.StateIdle();
                },Constants.DelayIdle);
            }
        
        }, Constants.DelayActive);
    }

    /// <summary>
    /// 敌人死亡
    /// </summary>
    /// <param name="key"></param>
    public void RemoveMonsterEntity(string key)
    {
        EntityMonster entity = null;
        if (monsterDic.TryGetValue(key, out entity))
        {

            switch ( entity.monsterData.mCfg.mType )
            {
                case  MonsterType.Solider:
                    {
                        GameRoot.Instance.dynamicWnd.RemoveHpItemInfo( key);
                    }
                    break;
                case MonsterType.Boss:
                    {
                        BattleSys.Instance.playerCtrlWnd.SetBossHPState(false);
                    }
                    break;
                default: break;
            }

            
            monsterDic.Remove(key);
        }
    }

    #endregion


    #region Monster 移动
    public void SetMonMoveDir(EntityMonster entity, Vector2 dir)
    {
        if (entity.canCtrl == false)
            return;


        //PECommon.Log(dir.ToString());
        if (dir == Vector2.zero)
        {
            entity.Idle();
        }
        else
        {
            entity.Move(dir);
        }
    }
    #endregion

    public bool CanRlsSkill()
    {
        return playerEntity.canRlsSkill;
    }


    /// <summary>
    /// 敌人打死了；玩家被打死；玩家自行退出战斗；玩家掉线
    /// </summary>
    public void EndBattle(bool isWin, int hp)
    {
        audioSvc.StopBGMusic();

        BattleSys.Instance.EndBattle(isWin,hp);

        
        
     
    }

}