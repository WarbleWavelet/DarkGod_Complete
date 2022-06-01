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
        SkillMoveCfg moveCfg = resSvc.GetSkillMoveCfg(cfg.skillMove);
        //
        entity.SetSkillFbx(cfg.fx, cfg.skillTime);
        SetState(entity, cfg);
        SetSkillMove(entity, moveCfg);
    }

    void SetState(EntityBase entity, SkillCfg cfg)
    { 
        entity.SetAction(cfg.aniAction);

        timerSvc.AddTimerTask((tid) => {
            entity.Idle();
        }, cfg.skillTime);
    }

    void SetSkillMove(EntityBase entity, SkillMoveCfg moveCfg)
    { 
        float speed = 1000.0f*moveCfg.moveDis / (moveCfg.moveTime );
        entity.SetSkillMove(true, speed);

        timerSvc.AddTimerTask((tid) => {
            entity.SetSkillMove(false);
        }, moveCfg.moveTime);
    }
}