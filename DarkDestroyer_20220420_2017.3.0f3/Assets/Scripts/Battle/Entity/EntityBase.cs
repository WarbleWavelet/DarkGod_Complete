/****************************************************
    文件：Entity.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 2:23:10
	功能：实体基类
*****************************************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase
{

    #region 字段 属性
    public AniState curState = AniState.Idle;
    public BattleMgr battleMgr = null;
    public StateMgr stateMgr = null;
    public SkillMgr skillMgr = null;
    protected Controller ctrl = null;
    /// <summary>攻击时不移动</summary>
    public bool canCtrl = true;
    /// <summary>基础属性</summary>
    BattleProps props;    
    int hp;
    string name;
    #region 属性

    public BattleProps Props
    {
        get
        {
            return props;
        }

       protected set
        {
            props = value;
        }
    }
    //

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

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }


    #endregion


    public Combo combo;
    public SkillCfg curSkillCfg;
    #endregion

    public virtual void SetBattleProps(BattleProps props)
    {
        this.Props = props;
        HP = props.hp ;
    }




    #region State ChangeStaus
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


    #region 动画器
    public virtual void SetAniBlend(float value)
    {
        if (ctrl != null)
        {
            ctrl.SetBlend(value);
        }

    }
    public virtual void SetAniAction(int value)
    {
        if (ctrl != null)
        {
            ctrl.SetAction(value);
          
        }

    }

    public AnimationClip[] GetAniClips()
    {
        return ctrl.ani.runtimeAnimatorController.animationClips;
    }
    #endregion


    #region Skill Atk  移动
    public virtual void SetDir(Vector2 dir)
    {
        if (ctrl != null)
        {
            ctrl.Dir = dir;
        }
    }

    /// <summary>
    /// 连招改方向
    /// </summary>
    /// <param name="dir"></param>
    public virtual void SetAtkDir(Vector2 dir)
    {
        if (ctrl != null)
        {
            ctrl.SetComboDir(dir);
        }
    }

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
            ctrl.SetSkillMoveState(move, speed);
        }
     }



    public void SkillAttack(int skillID)
    { 
        if (skillMgr != null)
        {
            skillMgr.SkillAttack(this, skillID);
            combo.ExitCurSkill(this, curSkillCfg);
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
    public void SetUIDodge( )
    { 
        GameRoot.Instance.dynamicWnd.SetDodge( Name );
    }


    /// <summary>
    /// 设置血条的prg
    /// </summary>
    /// <param name="oldVal"></param>
    /// <param name="newVal"></param>
    public void SetUIHpVal(int oldVal, int newVal)
    {
        if (GameRoot.Instance.dynamicWnd != null && GameRoot.Instance != null)
        { 
            GameRoot.Instance.dynamicWnd.SetHpVal( GetTrans().name,  oldVal,  newVal);
        }
       
    }
    #endregion

}