/****************************************************
    文件：SkillMgr.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 11:6:20
	功能：技能管理器
*****************************************************/

using UnityEngine;

public class SkillMgr :MonoBehaviour
{

    ResSvc resSvc;
    TimerSvc timerSvc;
    public void Init()
    {
        timerSvc = TimerSvc.Instance;
        resSvc = ResSvc.Instance;
        PECommon.Log(this.GetType().ToString()+" Init");
    }

    public void AttackEffect(EntityBase entity, int skillID)
    {
        skillID += 100;
        SkillCfg cfg=resSvc.GetSkillCfg(skillID);
        entity.SetAction(cfg.aniAction);
        entity.SetSkillFbx(cfg.fx, cfg.skillTime );
        //

        timerSvc.AddTimerTask((tid) => {
            entity.Idle();
        }, cfg.skillTime);
    }
}