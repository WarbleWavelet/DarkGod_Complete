/****************************************************
    文件：SkillMgr.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 11:6:20
	功能：技能管理器
*****************************************************/

using System;
using System.Collections.Generic;
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


    #region 攻击
    /// <summary>
    /// 动画 伤害 特效
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="skillID"></param>
    public void SkillAttack(EntityBase entity, int skillID)
    {
        AttackEffect(entity, skillID);
        AttackDamage(entity, skillID);
    }

   

    /// <summary>
    /// 
    /// </summary>
    /// <param name="from">攻击发起者</param>
    /// <param name="skillCfg"></param>
    /// <param name="actionIdx">skillActionLst和<para/>skillDamageLst的索引</param>
    private void SkillAction(EntityBase from, SkillCfg skillCfg, int actionIdx)
    {

        List<EntityMonster> entityLst = from.battleMgr.GetEntityMonster();
        SkillActionCfg action = resSvc.GetSkillActionCfg(skillCfg.skillActionLst[actionIdx] );
        int damage=skillCfg.skillDamageLst[actionIdx];

        for (int i = 0; i < entityLst.Count; i++)
        {
            EntityMonster to= entityLst[i];
            bool inRange = InRange(from.GetPos(), to.GetPos(), action.radius);
            bool inAngle = InAngle(from.GetTrans(), to.GetTrans(), action.angle);

            if (inRange && inAngle)
            {
                CalcDamage(from, to,skillCfg, damage);
            
            }
        }
    }
   

    private void CalcDamage( EntityBase from, EntityBase  to, SkillCfg skillCfg, int damage)
    {
        int dmgSum = damage;
        switch (skillCfg.dmgType)
        {
            case DmgType.None :
                {

                }
                break;
            case DmgType.AD:
                {
                    dmgSum = CalcDamage_AD( from,   to,  skillCfg,  damage);
                }
                break;
            case DmgType.ADC:
                {

                }
                break;
            case DmgType.AP:
                {
                    CalcDamage_AP(from, to, skillCfg, damage );
                }
                break;
            case DmgType.APC:
                {

                }
                break;
            case DmgType.TD:
                {

                }
                break;
            case DmgType.TDC:
                {

                }
                break;
            default: break;
        }

        CalcDamage_Res( to,  dmgSum);
    }



    #endregion

    #region 辅助

    /// <summary>
    /// 放技能的效果
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="skillID"></param>
    public void AttackEffect(EntityBase entity, int skillID)
    {
         SetAtkDir( entity );
        //
        SkillCfg cfg = resSvc.GetSkillCfg(skillID);
        if (cfg != null)
        {
            //Stop DirMove
            entity.canCtrl = false;
            entity.SetDir(Vector2.zero);
            //SkillMove
            SkillMoveCfg moveCfg = resSvc.GetSkillMoveCfg(cfg.skillMoveLst[0]);
            CalcSkillMove(entity, moveCfg);
            //Ani
            entity.SetAniAction(cfg.aniAction);
            //FX
            entity.SetFX(cfg.fx, cfg.skillTime);
           //State
            CalcState(entity, cfg);
            
        }
      
    }

    /// <summary>
    /// 连招时控制方向
    /// </summary>
    /// <param name="entity"></param>

    private void SetAtkDir(EntityBase entity)
    {

        Vector2 dir = ((EntityPlayer)entity).GetInputDir();
        if (dir == Vector2.zero)
        {
            FindMonster();
        }
        else
        {
            entity.SetAtkDir(dir);
        }
    }

    private void FindMonster()
    {

        
    }



    /// <summary>
    /// 计算伤害
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="skillID"></param>
    public void AttackDamage(EntityBase entity, int skillID)
    {
        SkillCfg skillCfg = resSvc.GetSkillCfg(skillID);
        List<int> actionLst = skillCfg.skillActionLst;


        int sum = 0;//计时从一开始，不清0
        for (int i = 0; i < actionLst.Count; i++)
        {
            SkillActionCfg action = resSvc.GetSkillActionCfg(actionLst[i]);
            sum += action.delayTime;
            int actionIdx = i;
            if (sum > 0)
            {
                timerSvc.AddTimerTask((int tid) =>
                {
                    SkillAction(entity, skillCfg, actionIdx);
                }, sum);
            }
            else
            {
                SkillAction(entity, skillCfg, actionIdx);
            }
        }
    }

    /// <summary>
    /// 在距离内
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    bool InRange(Vector3 from, Vector3 to, float range)
    {
        return Vector3.Distance(from, to) <= range;

    }

    /// <summary>
    /// 在视野内
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    bool InAngle(Transform from, Transform to, float angle)
    {
        if (angle == 360)
        {
            return true;
        }

        Vector3 forward = from.forward;
        Vector3 dir = (to.position - from.position).normalized;
        float ang = Vector3.Angle(forward, dir);

        if (ang <= angle / 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// 状态变化
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cfg"></param>
    void CalcState(EntityBase entity, SkillCfg cfg)
    {
        entity.SetAniAction(cfg.aniAction);

        timerSvc.AddTimerTask((tid) => {
            entity.StateIdle();
        }, cfg.skillTime);
    }

    /// <summary>
    /// 技能产生的位移变化
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="moveCfg"></param>

    void CalcSkillMove(EntityBase entity, SkillMoveCfg moveCfg)
    {
        float speed = 1000.0f * moveCfg.moveDis / (moveCfg.moveTime);


        if (moveCfg.delayTime > 0)
        {
            timerSvc.AddTimerTask((tid) =>
            {
                entity.SetSkillMove(true, speed);
            }, moveCfg.delayTime);
            //
            timerSvc.AddTimerTask((tid) => {
                entity.SetSkillMove(false);
            }, moveCfg.delayTime + moveCfg.moveTime);
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



    #region CalcDamage
    int CalcDamage_AD(EntityBase from, EntityBase to, SkillCfg skillCfg, int damage)
    {
        int dmgSum = damage;
        float rate = 0f;
        rate = PETools.RDInt(1, 100);
        if (rate < to.Props.dodge)
        {

            //print("闪避"+rate);
            to.SetUIDodge();
            return 0;
        }
        dmgSum += from.Props.ad;
        //
        bool isCritical = false;
        rate = PETools.RDInt(1, 100);
        if (rate < to.Props.critical)
        {

            // print("暴击" + rate);
            rate = PETools.RDInt(1, 100);
            dmgSum = (int)(dmgSum * (1f + rate / 100f));
            //
            isCritical = true;

        }
        //
        int def = (int)((1f - from.Props.pierce / 100.0f) * to.Props.addef);
        dmgSum -= def;
        //print("护甲" + def);

        //print("最终伤害"+dmgSum);
        if (isCritical)
            to.SetUICritical(dmgSum);
        else
            to.SetUIHurt(dmgSum);

        return dmgSum;
    }
    int CalcDamage_AP(EntityBase from, EntityBase to, SkillCfg skillCfg, int damage)
    {
        int dmgSum = damage;
        dmgSum += from.Props.ap;
        dmgSum -= to.Props.apdef;
        return dmgSum;
    }
    private void CalcDamage_Res(EntityBase to, int dmgSum)
    {
        if (dmgSum < 0)
        {
            dmgSum = 0;
        }

        if (dmgSum >= to.HP)
        {
            to.HP = 0;
            to.StateDie();
            to.battleMgr.RemoveMonsterEntity(to.Name);
        }
        else
        {
            to.HP -= dmgSum;
            to.StateHit();
        }
    }
    #endregion


    #endregion


}