/****************************************************
    文件：Entity.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 2:23:10
	功能：实体基类
*****************************************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IControllerProcessor
{
    Controller Ctrl { get; set; }
    void SetAniBlend(float value);
    void SetAniAction(int value);

     AnimationClip[] GetAniClips();
}
public interface IStateProcessor
{
    StateMgr StateMgr { get; set; }
    void StateMove();
    void StateIdle();
    void StateBorn();
    void StateAttack(int skillID);
    void StateDie();
}

public class EntityBase :IStateProcessor  , IControllerProcessor
{

    #region fpc
    public StateMgr stateMgr = null;
    public StateMgr StateMgr { get { return stateMgr; }set { stateMgr = value; } }
    public Controller ctrl = null;
    public Controller Ctrl { get { return ctrl; } set { ctrl = value; } }
    /// <summary>基础属性</summary>
    BattleProps props;    
    public BattleProps Props{   get { return props;  } protected set { props = value; }}


     //



    public AniState curState = AniState.Idle;
    public bool isDead = false;
    public BattleMgr battleMgr = null;
    public SkillMgr skillMgr = null;


    /// <summary>攻击时不移动</summary>
    public bool canCtrl = true;



    public EntityState entityState=EntityState.None;
    public SkillCalback skillCalback;
    public Combo combo;
    public SkillCfg curSkillCfg;
    public EntityType entityType=EntityType.None;

    /// <summary>到IState中赋值，到技能释放中使用。</summary>
    public bool canRlsSkill = true;
    //
    int hp;

    public int HP
    {
        get
        {
            return hp;
        }

        set
        {
            //PECommon.Log("HP change to " + value) ;
            SetUIHpVal(hp, value);
            hp = value;
        }
    }

    string name;
    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
            SetName(name);
        }
    }


    #endregion










    public AniState GetState()
    {
        return curState;
    }

    public virtual void SetBattleProps(BattleProps props)
    {
        this.Props = props;
        HP = props.hp ;
    }

    public void Idle()
    {
        StateIdle();//动画的逻辑和表现
        SetDir(Vector2.zero);//转向和移动

    }


    public void Move( Vector2 dir)
    {
        StateMove();
        SetDir(dir);
    }


    #region IStateProcessor


    public void StateMove()
    {
        stateMgr.ChangeStaus(this, AniState.Move, null);
    }

    public void StateIdle()
    {
        stateMgr.ChangeStaus(this, AniState.Idle, null);
    }
    public void StateBorn()
    {
        stateMgr.ChangeStaus(this, AniState.Born, null);
    }
    public void StateAttack(int skillID)
    {
        stateMgr.ChangeStaus(this, AniState.Attack, skillID);
    }

    public void StateDie( )
    {
        stateMgr.ChangeStaus(this, AniState.Die, null);
    }

    public void StateHit()
    {
        stateMgr.ChangeStaus(this, AniState.Hit, null);
    }
    #endregion


    #region IControllerProcessor
    public virtual void SetAniBlend(float value)
    {
        if (ctrl != null)
        {
            ctrl.SetAniBlend(value);
        }

    }
    public virtual void SetAniAction(int value)
    {
        if (ctrl != null)
        {
            ctrl.SetAniAction(value);
          
        }

    }

    public AnimationClip[] GetAniClips()
    {
        return ctrl.ani.runtimeAnimatorController.animationClips;
    }
    #endregion


    #region Skill Atk  移动 方向


    /// <summary>
    /// true连招改方向 false最近敌人
    /// </summary>
    /// <param name="dir"></param>
    public virtual void SetAtkDir(Vector2 dir, AtkDirType type= AtkDirType.NearTarget)
    {
        if (ctrl != null)
        {
            switch (type)
            {
                case AtkDirType.NearTarget:
                    {
                        ctrl.SetNearTargetDir(dir);
                    }
                    break;
                case AtkDirType.ChangeComboDir:
                    {
                        ctrl.SetComboDir(dir);
                    }
                    break;
                default:break;
            }

        }
    }


    /// <summary>
    /// 设置isMove，间接控制表现层ctrl
    /// </summary>
    /// <param name="dir"></param>
    public virtual void SetDir(Vector2 dir)
    {
        if (ctrl != null)
        {
            ctrl.Dir = dir;
        }
    }


    /// <summary>
    /// 会被重写
    /// </summary>
    /// <returns></returns>
    public virtual Vector2 GetInputDir()
    {
        return Vector2.zero;
    }

    public virtual void SetFX(string skillName, float lifeTime)
    {
        if (ctrl != null)
        {
            ctrl.SetSkillFbx(skillName, lifeTime);

        }

    }

    public virtual void SetSkillMove(bool move, float speed=0f)
    {
        if (ctrl != null)
        {
            ctrl.SetSkillMove(move, speed);
        }
     }



    public void SkillAttack(int skillID)
    { 
        if (skillMgr != null)
        {
            skillMgr.SkillAttack(this, skillID);
            //if ( combo != null&& curSkillCfg !=null)
            //{ 
            //    combo.ExitCurSkill(this);//StateAttack.Exit用
            //}
            
        }    
    }



    #endregion


    #region Untiy
    public void SetCtrl(Controller ctrl)
    {
        this.ctrl = ctrl;
    }

    public GameObject GetGameObject()
    {
        return ctrl.gameObject;
    }

    public Vector3 GetPos()
    {
        if (ctrl != null)
        { 
            return ctrl.transform.position;
        }

        throw new System.Exception("异常");

    }

    public Transform GetTrans()
    {
        if (ctrl != null)
        {
            return ctrl.transform;
        }

        throw new System.Exception("异常");
    }




    public void SetActive(bool state = true)
    {
        if (ctrl != null)
        {
            ctrl.gameObject.SetActive(state);
        }
    }
    #endregion
  

    #region UI
 /// <summary>
    /// 飘字
    /// </summary>
    public void SetUIHurt(int dmg )
    {
        GameRoot.Instance.dynamicWnd.SetHurt( Name, dmg );
    }

    /// <summary>
    /// 飘字
    /// </summary>
    /// <param name="dmg"></param>
    public void SetUICritical(int dmg)
    { 
        GameRoot.Instance.dynamicWnd.SetCritical(Name, dmg);
    }

    /// <summary>
    /// 飘字
    /// </summary>
    public virtual void SetUIDodge( )
    { 
        GameRoot.Instance.dynamicWnd.SetDodge( Name );
    }


    /// <summary>
    /// 设置血条的prg,头上或UI上，多态显示
    /// </summary>
    /// <param name="oldVal"></param>
    /// <param name="newVal"></param>
    public virtual void SetUIHpVal(int oldVal, int newVal)
    {
        if (GameRoot.Instance.dynamicWnd != null && GameRoot.Instance != null)
        {
            GameRoot.Instance.dynamicWnd.SetHpVal( GetTrans().name,  oldVal,  newVal);
        }


        if (BattleSys.Instance.playerCtrlWnd != null && BattleSys.Instance != null)
        {
            BattleSys.Instance.playerCtrlWnd.SetBossHPVal(oldVal, newVal);
        }

    }

    public virtual void SetName(string name)
    {

    }
    #endregion


    public virtual Vector2 CalcTargetDir()
    { 
        return Vector2.zero;
    }

    public AudioSource GetAudio()
    {
        if (ctrl != null)
        { 
           return ctrl.gameObject.GetComponent<AudioSource>();
        }
        return null;
     
    }

    /// <summary>
    /// 处于技能可中断状态？
    /// </summary>
    /// <returns></returns>
    public virtual bool GetBreakState()
    {
        return true;
    }
}


public enum EntityType
{ 
    None,
    Player,
    Monster
}
public  enum AtkDirType
{
    ChangeComboDir,
    NearTarget
}
/// <summary></summary>
public enum EntityState
{
    None,
    /// <summary>霸体，不可控制，可受伤害</summary> 
    EndureState,
}