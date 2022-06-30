/****************************************************
    文件：StateHit.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/6 20:21:50
	功能：受击
*****************************************************/

using UnityEngine;
using System.Reflection;
using System;

public class StateHit : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
      //  PECommon.Log(this.GetType().ToString() + MethodBase.GetCurrentMethod().Name);
        entity.curState = AniState.Hit;
        entity.combo.RemoveSkillCB(entity);
    }

    public void Exit(EntityBase entity, params object[] args)
    {
      //  PECommon.Log(this.GetType().ToString() + MethodBase.GetCurrentMethod().Name);


        
    }

    public void Process(EntityBase entity, params object[] args)
    {
       // PECommon.Log(this.GetType().ToString() +"_"+MethodBase.GetCurrentMethod().Name);
        //
        if (entity.entityType == EntityType.Player)
        {
            entity.canRlsSkill = false;
        }
        ProcessPlayer(entity);
        ProcessMonster(entity);
        //TODO 玩家 敌人 恢复状态的时间不同
    }

    void ProcessPlayer(EntityBase entity)
    {
        if (entity.entityType != EntityType.Player) return;
        entity.canRlsSkill = false;
        entity.SetDir(Vector2.zero);
        entity.SetAniAction(Constants.ActionHit);

        //
        int time = (int)(GetHitAniTime(entity) * 1000.0f);
        TimerSvc.Instance.AddTimerTask((int tid) => {
            entity.SetAniAction(Constants.ActionDefault);//动画器
            entity.StateIdle();//状态机
        }, time);
    }
    void ProcessMonster(EntityBase entity)
    {
        if (entity.entityType != EntityType.Monster) return;
        entity.ctrl.runAI = false;
        ((EntityMonster) entity).aiMonster.runAI = entity.ctrl.runAI;
        entity.SetDir(Vector2.zero);
        entity.SetAniAction(Constants.ActionHit);

        //
        int time = (int)(GetHitAniTime(entity) * 1000.0f);
        TimerSvc.Instance.AddTimerTask((int tid) => {
            entity.SetAniAction(Constants.ActionDefault);//动画器
            entity.StateIdle();//状态机
            entity.ctrl.runAI = true;
            ((EntityMonster)entity).aiMonster.runAI = entity.ctrl.runAI;
        }, time );
    }
        #region 说明
        /// <summary>
        /// Hit hit HIT 都可以
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        private float GetHitAniTime(EntityBase entity)
    {
        AnimationClip[] clipArr = entity.GetAniClips();
        string[] arr = { "hit", "Hit", "HIT" };

        for (int i = 0; i < clipArr.Length; i++)
        {
            AnimationClip clip = clipArr[i];
            for (int j = 0; j < arr.Length; j++)
            {
                if (clip.name.Contains(arr[j]))
                {
                    return clip.length;
                }
            }
            //

        }
        return 0.25f;
    }
    #endregion


}