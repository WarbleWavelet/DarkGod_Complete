/****************************************************
    文件：StateIdle.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 2:26:38
	功能：
*****************************************************/

using UnityEngine;

public class StateMove : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
        entity.curState = AniState.Move;
       // PECommon.Log(this.GetType().ToString() + " Enter");
    }

    public void Exit(EntityBase entity, params object[] args)
    {
       // PECommon.Log(this.GetType().ToString() + " Exit");
        entity.SetAniAction(Constants.ActionDefault);
        entity.SetAniBlend(Constants.BlendIdle);
    }

    public void Process(EntityBase entity, params object[] args)
    {
        //PECommon.Log(this.GetType().ToString() + " Process");
        entity.SetAniAction(Constants.ActionDefault);
        entity.SetAniBlend(Constants.BlendWalk);
    }
}