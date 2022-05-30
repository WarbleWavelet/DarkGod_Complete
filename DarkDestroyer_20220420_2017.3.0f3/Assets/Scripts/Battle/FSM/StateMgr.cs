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

    public Dictionary<AniState, IState> dic = new Dictionary<AniState, IState>();
    public void Init()
    {
        PECommon.Log(this.GetType().ToString() + " Init");
        dic.Add(AniState.Idle,new StateIdle());
        dic.Add(AniState.Move,new StateMove());
    }

    public void ChaungeStaus(EntityBase entity, AniState targetState)
    {
        if (entity.curState == targetState)
        {
            return;
        }

        if (dic.ContainsKey(targetState))
        {
            dic[entity.curState].Exit(entity);
            dic[targetState].Enter(entity);
            dic[targetState].Process(entity);
        }
    }
}