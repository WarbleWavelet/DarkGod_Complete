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
    public void Enter(EntityBase entity)
    {
        entity.curState = AniState.Move;
        PECommon.Log(this.GetType().ToString() + " Enter");
    }

    public void Exit(EntityBase entity)
    {
        PECommon.Log(this.GetType().ToString() + " Exit");
    }

    public void Process(EntityBase entity)
    {
        PECommon.Log(this.GetType().ToString() + " Process");
        entity.SetBlend(Constants.BlendWalk);
    }
}