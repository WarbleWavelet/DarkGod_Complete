/****************************************************
	文件：IStateMgr.cs
	作者：lenovo
	邮箱: 
	日期：2024/9/9 21:19:58
	功能：
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

/// <summary></summary>
public interface IStateMgr
{
	Dictionary<EAniState, IState> Fsm { get; }
	void AddState(EAniState targetState, IState state);

	void ChangeStaus(EntityBase entity, EAniState targetState, params object[] args);
}