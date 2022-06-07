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
    }

    public void Exit(EntityBase entity, params object[] args)
    {
        PECommon.Log(this.GetType().ToString() + MethodBase.GetCurrentMethod().Name);
    }

    public void Process(EntityBase entity, params object[] args)
    {
        PECommon.Log(this.GetType().ToString() +"_"+MethodBase.GetCurrentMethod().Name);
        //
        entity.SetDir( Vector2.zero);
        entity.SetAction(Constants.ActionHit);


        int time = (int)( GetHitAniTime( entity )*1000.0f);
        TimerSvc.Instance.AddTimerTask(( int tid) => {
            entity.SetAction(Constants.ActionDefault);//动画器
            entity.Idle();//状态机
        },time);
        //TODO 玩家 敌人 恢复状态的时间不同
    }

    private float GetHitAniTime(EntityBase entity)
    {
        AnimationClip[] clipArr = entity.ctrl.ani.runtimeAnimatorController.animationClips;

        for (int i = 0; i < clipArr.Length; i++)
        {
            AnimationClip clip = clipArr[i];
            bool state = clip.name.Contains("Hit");
            state = state || clip.name.Contains("hit");
            state = state || clip.name.Contains("HIT");

            if (state)
            {
                return clip.length;
            }
        }
        return 1f;
    }
}