/****************************************************
    文件：Combo.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/10 16:17:26
	功能：平A连招
*****************************************************/


using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour 
{
    // 111 112 113  114 115 0(没有技能)
      Queue<int> comboQue=new Queue<int>();
    /// <summary>StateIde.Progress</summary>
    public int nextSkillID=0;


    /// <summary>
    /// 按键进行连招的记录，BattleMgr.ReleaseNormalAttack()
    /// </summary>
    /// <param name="skillID"></param>
    public void EnqueueComboQue(int skillID) 
    {
        comboQue.Enqueue(skillID);
    }

    /// <summary>
    /// StateAttack.Exit()
    /// </summary>
    /// <returns></returns>
    public void ExitCurSkill(EntityBase entity, SkillCfg cfg)
    {
       // SkillCfg cfg = ResSvc.Instance.GetSkillCfg(skillID);
        if (cfg.isCombo)
        {
            if (comboQue.Count > 0)
            {
                nextSkillID = comboQue.Dequeue();
            }
            else
            {
                nextSkillID = Constants.NoComboNextSkillID;
            }
        }
        //

    }

}