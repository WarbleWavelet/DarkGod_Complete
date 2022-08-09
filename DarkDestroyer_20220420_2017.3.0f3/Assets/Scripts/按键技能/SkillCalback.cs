/****************************************************
    文件：SkillCalback.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/20 14:3:17
	功能：技能位移、伤害点中断.加在实体上（区别于动作打点）
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class SkillCalback
{

    [Header("SkillCalback")]
    /// <summary>中断技能的位移</summary>
    List<int> skillMoveCbLst = new List<int>();
    /// <summary>中断技能的上海店</summary>
    List<int> skillActionCbLst = new List<int>();

    public void AddSkillMoveCb(int id)
    {
        skillMoveCbLst.Add(id);
    }

    public void AddSkillActionCb(int id)
    {
        skillActionCbLst.Add(id);
    }

    public void RemoveSkillMoveCb(int id)
    {
        foreach (var item in skillMoveCbLst)
        {
            if (item == id)
            {
                skillMoveCbLst.Remove(id);
                break;
            }
        }

    }

    public void RemoveSkillActionCb(int id)
    {
        foreach (var item in skillActionCbLst)
        {
            if (item == id)
            {
                skillActionCbLst.Remove(id);
                break;
            }
        }
    }

    /// <summary>
    /// 比如取消正在计时的回调，清理表
    /// </summary>
    public void ClearSkillCbLst()
    {
        skillActionCbLst.Clear();
        skillMoveCbLst.Clear();
    }


    /// <summary>
    /// 清空被打断的效果,StateHit时调用
    /// </summary>
    public void DeleteTaskBySkillCbLst()
    {
        foreach (var item in skillActionCbLst)
        {
            TimerSvc.Instance.DelTask(item);
        }

        foreach (var item in skillMoveCbLst)
        {
            TimerSvc.Instance.DelTask(item);
        }
    }
}