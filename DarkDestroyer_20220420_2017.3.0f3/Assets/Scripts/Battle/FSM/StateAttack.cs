/****************************************************
    文件：StateAttack.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/1 15:5:55
	功能：攻击
*****************************************************/

using UnityEngine;
public class StateAttack : IState
{

    public void Enter(EntityBase entity, params object[] args)
    {
        PECommon.Log(this.GetType().ToString() + " Enter");
        entity.curState = AniState.Attack;
    }

    public void Exit(EntityBase entity, params object[] args)
    {
       entity.canCtrl = true;
        PECommon.Log(this.GetType().ToString() + " Exit");
        entity.SetAction(Constants.ActionDefault);
    }

    public void Process(EntityBase entity, params object[] args)
    {
        PECommon.Log(this.GetType().ToString() + " Process");
        entity.AttackEffect( (int)args[0] );
    }
}