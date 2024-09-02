/****************************************************
    文件：Combo.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/10 16:17:26
	功能：平A连招，原写在Entity里面。拆分一下
*****************************************************/


using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour 
{
    // 111 112 113  114 115 0(没有技能)
      Queue<int> comboQue=new Queue<int>();

    /// <summary>StateIde.Progress</summary>
    public int nextSkillID=0;

    /// <summary>技能结束时的回调</summary>
    public int endSkillIDCb=-1;


    /// <summary>
    /// 按键进行连招的记录，BattleMgr.ReleaseNormalAttack()
    /// </summary>
    /// <param name="skillID"></param>
    public void EnqueueComboQue(int skillID) 
    {
        comboQue.Enqueue(skillID);
    }

    /// <summary>
    /// StateAttack.Exit(),放出连招或0
    /// </summary>
    /// <returns></returns>
    public void ExitCurSkill(EntityBase entity)
    {
        entity.canCtrl = true;
        SkillCfg cfg = entity.curSkillCfg;

        if (cfg != null)
        {
            if (cfg.isBreak)
            {
                entity.entityState = EntityState.None;
            }
            if (cfg.isCombo)
            {
                if (comboQue.Count > 0)
                {
                    nextSkillID = comboQue.Dequeue();
                }
                else
                {
                    nextSkillID = Constants.SkillIDDefault;
                }
            }
            entity.curSkillCfg = null;
        }
        //

        entity.SetAniAction(Constants.ActionDefault);
    }

    public int GetComboQueCount()
    { 
    return comboQue.Count;
    }

    public void ClearComboQue()
    {
         comboQue.Clear();
    }
    

    /// <summary>
    /// 清空连招
    /// </summary>
    /// <param name="entity"></param>
    public void RemoveSkillCB(EntityBase entity)
    {
        entity.SetSkillMove(false);
        entity.SetDir(Vector2.zero);


        //连招被打断，删除连招的回调
        if (entity.combo.endSkillIDCb != -1)
        {
            TimerSvc.Instance.DelTask(entity.combo.endSkillIDCb);
            entity.combo.endSkillIDCb = -1;
        }


        //分开写是方便调试
        if (entity.entityType == EntityType.Monster)
        {
            //清空技能
            entity.skillCalback.DeleteTaskBySkillCbLst();
            //清空连招
            if (entity.combo.nextSkillID != 0 && entity.combo.GetComboQueCount() > 0)
            {
                entity.combo.nextSkillID = 0;
                entity.combo.ClearComboQue();
            }
            entity.battleMgr.ResetCombo();
        }
        else
        {

            //清空技能
            entity.skillCalback.DeleteTaskBySkillCbLst();
            //清空连招
            if (entity.combo.nextSkillID != 0 && entity.combo.GetComboQueCount() > 0)
            {
                entity.combo.nextSkillID = 0;
                entity.combo.ClearComboQue();
            }
            entity.battleMgr.ResetCombo();
        }
    }
}