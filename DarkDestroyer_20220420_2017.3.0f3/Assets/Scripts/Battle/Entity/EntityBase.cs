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
    public AniState curState = AniState.None;
    public BattleMgr battleMgr = null;
    public StateMgr stateMgr = null;
    public SkillMgr skillMgr = null;
    public Controller ctrl = null;
    public bool canCtrl=true;

    #region 战斗属性
   BattleProps props;

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

    public int HP
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }

    int hp;
    #endregion


    #region ChangeStaus
 public void Move()
    {
        stateMgr.ChangeStaus(this, AniState.Move);
    }

    public void Idle()
    {
        stateMgr.ChangeStaus(this, AniState.Idle);
    }
    public void Born()
    {
        stateMgr.ChangeStaus(this, AniState.Born);
    }
    #endregion
   


    public virtual void SetBlend(float value)
    {
        if (ctrl != null)
        {
            ctrl.SetBlend(value);
        }

    }

    public virtual void SetDir(Vector2 dir)
    {
        if (ctrl != null)
        {
            ctrl.Dir = dir;
        }

    }


    public virtual Vector2 GetInputDir()
    {
        return Vector2.zero;
    }

    public virtual void SetAction(int value)
    {
        if (ctrl != null)
        {
            ctrl.SetAction(value);
          
        }

    }


    #region Skill Atk
  public virtual void SetSkillFbx(string skillName, float lifeTime)
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

        }    
    }
 

    public void Attack(int skillID)
    {
        stateMgr.ChangeStaus(this, AniState.Attack, skillID);
    }
    #endregion


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

    public virtual void SetBattleProps(BattleProps props)
    {
        this.Props = props;
        HP = props.hp;
    }

}