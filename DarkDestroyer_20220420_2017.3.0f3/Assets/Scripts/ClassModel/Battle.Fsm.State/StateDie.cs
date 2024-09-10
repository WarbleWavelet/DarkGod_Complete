/****************************************************
    文件：StateDie.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/6 15:24:54
	功能：
*****************************************************/

using UnityEngine;

public class StateDie : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
        //PECommon.Log(this.GetType().ToString() + " Enter");
        entity.CurState=EAniState.Die;
        entity.combo.RemoveSkillCB( entity );
    }

    public void Exit(EntityBase entity, params object[] args)
    {
       // PECommon.Log(this.GetType().ToString() + " Exit");
    }

    public void Process(EntityBase entity, params object[] args)
    {
       // PECommon.Log(this.GetType().ToString() + " Process");
        //
        if ( entity.isDead==true )
        { 
            return;
        }
        entity.SetAniAction(Constants.ActionDie);
        TimerSvc.Instance.AddTimerTask((int tid) =>
        {
            entity.SetActive(false);
            if (entity.entityType == EntityType.Player)
            { 
                GameRoot.Instance.GetComponent<AudioListener>().enabled = true;        
            }

          

        }, Constants.DelayDieAniLength);
        entity.isDead = true;



    }
}