/****************************************************
    文件：SkillCalback.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/20 14:3:17
	功能：技能位移伤害中断
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class SkillCalback
{

    [Header("SkillCalback")]
    List<int> skillMoveCbLst = new List<int>();
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
    public void DeleteSkillCbLst()
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