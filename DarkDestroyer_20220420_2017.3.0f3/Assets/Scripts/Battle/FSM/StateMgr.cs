/****************************************************
    文件：StateMgr.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 11:4:26
	功能：状态管理器
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class StateMgr :MonoBehaviour
{

    public Dictionary<AniState, IState> fsm = new Dictionary<AniState, IState>();
    public void Init()
    {
        PECommon.Log(this.GetType().ToString() + " Init");
        fsm.Add(AniState.Born,new StateBorn());
        fsm.Add(AniState.Idle,new StateIdle());
        fsm.Add(AniState.Move,new StateMove());
        fsm.Add(AniState.Attack,new StateAttack());
        fsm.Add(AniState.Die,new StateDie());
        fsm.Add(AniState.Hit,new StateHit());
    }

    public void ChangeStaus(EntityBase entity, AniState targetState, params object[] args)
    {
        if (entity.curState == targetState)
        {
            return;
        }

        if (fsm.ContainsKey(targetState))
        {
            if (entity.curState != AniState.None)
            { 
                fsm[entity.curState].Exit(entity, args);
            }                                
            
            fsm[targetState].Enter(entity, args);
            fsm[targetState].Process(entity, args);
        }
    }
}