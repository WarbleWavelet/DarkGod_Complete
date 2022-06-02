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


    /// <summary>
    /// 放技能的效果
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="skillID"></param>
    public void AttackEffect(EntityBase entity, int skillID)
    {
        skillID += 100;
        SkillCfg cfg=resSvc.GetSkillCfg(skillID);
        SkillMoveCfg moveCfg = resSvc.GetSkillMoveCfg(cfg.skillMoveLst[0]);
        //
        entity.SetSkillFbx(cfg.fx, cfg.skillTime);
        //
        entity.canCtrl = false;
        entity.SetDir(Vector2.zero);
        //
        CalcState(entity, cfg);
        CalcSkillMove(entity, moveCfg);
    }


    /// <summary>
    /// 状态变化
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cfg"></param>
    void CalcState(EntityBase entity, SkillCfg cfg)
    { 
        entity.SetAction(cfg.aniAction);

        timerSvc.AddTimerTask((tid) => {
            entity.Idle();
        }, cfg.skillTime);
    }

    /// <summary>
    /// 技能产生的位移变化
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="moveCfg"></param>

    void CalcSkillMove(EntityBase entity, SkillMoveCfg moveCfg)
    { 
        float speed = 1000.0f*moveCfg.moveDis / (moveCfg.moveTime );


        if (moveCfg.delayTime > 0)
        {
            timerSvc.AddTimerTask((tid) =>
            {
                entity.SetSkillMove(true, speed);
            }, moveCfg.delayTime);
            //
            timerSvc.AddTimerTask((tid) => {
                entity.SetSkillMove(false);
            }, moveCfg.delayTime+moveCfg.moveTime);
        }
        else
        {
            entity.SetSkillMove(true, speed);
            //
            timerSvc.AddTimerTask((tid) => {
                entity.SetSkillMove(false);
            }, moveCfg.moveTime);
        }




    }
}