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
        PECommon.Log(this.GetType().ToString() + MethodBase.GetCurrentMethod().Name);
        entity.curState = AniState.Hit;
        entity.skillCalback.DeleteSkillCbLst();
    }

    public void Exit(EntityBase entity, params object[] args)
    {
        PECommon.Log(this.GetType().ToString() + MethodBase.GetCurrentMethod().Name);
    }

    public void Process(EntityBase entity, params object[] args)
    {
        PECommon.Log(this.GetType().ToString() +"_"+MethodBase.GetCurrentMethod().Name);
        //
        if (entity.entityType == EntityType.Player)
        {
            entity.canRlsSkill = false;
        }
        //
        entity.SetDir( Vector2.zero);
        entity.SetAniAction(Constants.ActionHit);

        string[] hitClipNameArr = { "hit", "Hit", "HIT" };
        int time = (int)( GetHitAniTime( entity, hitClipNameArr) *1000.0f);
        TimerSvc.Instance.AddTimerTask(( int tid) => {
            entity.SetAniAction(Constants.ActionDefault);//动画器
            entity.StateIdle();//状态机
        },time);
        //TODO 玩家 敌人 恢复状态的时间不同
    }


    /// <summary>
    /// Hit hit HIT 都可以
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="arr"></param>
    /// <returns></returns>
    private float GetHitAniTime(EntityBase entity, string[] arr)
    {
        AnimationClip[] clipArr = entity.GetAniClips();

        for (int i = 0; i < clipArr.Length; i++)
        {
            AnimationClip clip = clipArr[i];
            bool state = false;
            for (int j = 0; j < arr.Length; j++)
            {
                state = state || clip.name.Contains(arr[j]);
            }
            //
            if (state)
            {
                return clip.length;
            }
        }
        return 1f;
    }
}