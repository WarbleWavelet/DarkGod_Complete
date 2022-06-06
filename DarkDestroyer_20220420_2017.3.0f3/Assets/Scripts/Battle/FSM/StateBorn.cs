/****************************************************
    文件：StateBorn.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/6 12:20:55
	功能：出生状态
*****************************************************/

using UnityEngine;

public class StateBorn : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
        PECommon.Log(this.GetType().ToString() + " Enter");
        entity.curState = AniState.Born;

    }

    public void Exit(EntityBase entity, params object[] args)
    {
        PECommon.Log(this.GetType().ToString() + " Exit");
        throw new System.NotImplementedException();
    }

    public void Process(EntityBase entity, params object[] args)
    {
        PECommon.Log(this.GetType().ToString() + " Process");
        entity.SetAction(Constants.ActionBorn);
        TimerSvc.Instance.AddTimerTask((int tid) =>
        {
            entity.SetAction(Constants.ActionDefault);
        }, Constants.DelayDefault);
    }
}