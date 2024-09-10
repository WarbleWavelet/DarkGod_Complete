/****************************************************
	文件：StateMgr.cs
	作者：lenovo
	邮箱: 
	日期：2022/5/29 11:4:26
	功能：状态管理器
*****************************************************/

using System.Collections.Generic;
using UnityEngine;




public class StateMgr :MonoBehaviour, IStateMgr
{

	 Dictionary<EAniState, IState> _fsm = new Dictionary<EAniState, IState>();

	public Dictionary<EAniState, IState> Fsm
	{
		get
		{
		   return _fsm;
		}
	}

	public void Init()
	{
		PECommon.Log(this.GetType().ToString() + " Init");
		AddState(EAniState.Born,new StateBorn());
		AddState(EAniState.Idle,new StateIdle());
		AddState(EAniState.Move,new StateMove());
		AddState(EAniState.Attack,new StateAttack());
		AddState(EAniState.Die,new StateDie());
        AddState(EAniState.Hit,new StateHit());
	}

	public void AddState(EAniState targetState, IState state)
	{
		Fsm.Add(targetState, state);
	}

	public void ChangeStaus(EntityBase entity, EAniState targetState, params object[] args)
	{
		if (entity.CurState == targetState)
		{
			return;
		}

		if (Fsm.ContainsKey(targetState))
		{
			if (entity.CurState != EAniState.None)
			{ 
				Fsm[entity.CurState].Exit(entity, args);
			}                                
			
			Fsm[targetState].Enter(entity, args);
			Fsm[targetState].Process(entity, args);
		}
	}
}