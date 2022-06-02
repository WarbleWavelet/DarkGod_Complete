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



    public void Move()
    {
        stateMgr.ChangeStaus(this, AniState.Move);
       // SetBlend(Constants.BlendWalk);
    }

    public void Idle()
    {
        stateMgr.ChangeStaus(this, AniState.Idle);
      //  SetBlend(Constants.BlendIdle);
    }

    public void Attack( int skillID)
    {
        stateMgr.ChangeStaus( this, AniState.Attack, skillID );
    }

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

    public virtual void AttackEffect(int value)
    {
        if (skillMgr != null)
        {
            skillMgr.AttackEffect(this, value);

        }

    }


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
}