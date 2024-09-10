/****************************************************
    文件：EntityPlayer.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 3:1:47
	功能：玩家逻辑实体类
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class EntityPlayer :EntityBase
{

    public override Vector2 GetInputDir()
    {
        return battleMgr.GetInputDir();
    }


    #region 最近敌人
    /// <summary>
    /// 最近敌人的方向
    /// </summary>
    /// <returns></returns>
    public override Vector2 CalcTargetDir()
    {
        EntityMonster to = GetClosedTarget();
        if (to != null)
        {
            Vector3 fromPos = this.GetPos();
            Vector3 toPos = to.GetPos();

            Vector2 dir = new Vector2(toPos.x - fromPos.x, toPos.z - fromPos.z);

            return dir.normalized;
        }
        else
        {
            return Vector2.zero;
        }
    }

    /// <summary>
    /// 最近敌人
    /// </summary>
    /// <returns></returns>

    EntityMonster GetClosedTarget()
    {
        List<EntityMonster> lst = battleMgr.GetEntityMonster();

        if (lst == null || lst.Count == 0)
        {
            return null;
        }
        //
        EntityPlayer from = this;
        EntityMonster tarTo = lst[0];
        float minDis = Vector2.Distance(from.GetPos(), tarTo.GetPos());
        //
        if (lst.Count == 1)
            return tarTo;
        //
        for (int i = 1; i < lst.Count; i++)
        {
            EntityMonster to = lst[i];
            float dis = Vector2.Distance(from.GetPos(), to.GetPos());

            if (dis < minDis)
            {
                minDis = dis;
                tarTo = to;
            }
        }

        return tarTo;

    }
    #endregion

    public override void SetUIHpVal(int oldVal, int newVal)
    {
        BattleSys.Instance.playerCtrlWnd.SetPrgHP( newVal);
    }

    public override void SetUIDodge()
    {
        GameRoot.Instance.dynamicWnd.SetPlayerDodge();
    }


    public override void SetName(string name )
    {
        BattleSys.Instance.playerCtrlWnd.SetName(name);
    }
}