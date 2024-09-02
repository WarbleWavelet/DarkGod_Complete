/****************************************************
    文件：StateAttack.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/1 15:5:55
	功能：攻击
*****************************************************/

using UnityEngine;
public class StateAttack : IState
{

    public void Enter(EntityBase entity, params object[] args)
    {
      //  PECommon.Log(this.GetType().ToString() + " Enter");
        //
        entity.curState = AniState.Attack;
        int skillID = (int)args[0];
        entity.curSkillCfg = ResSvc.Instance.GetSkillCfg(skillID );
    }

    public void Exit(EntityBase entity, params object[] args)
    {

      //  PECommon.Log(this.GetType().ToString() + " Exit");
        //
        if (entity.combo != null)
        {
            if ( entity.curSkillCfg.isBreak == false)
            {
                entity.entityState = EntityState.None;
            }
            entity.combo.ExitCurSkill(entity);
        }
        //

        
    }

    public void Process(EntityBase entity, params object[] args)
    {
      //  PECommon.Log(this.GetType().ToString() + " Process");
        //
        if (entity.entityType == EntityType.Player)
        {
            entity.canRlsSkill = false;
        }
        int skillID = (int)args[0];
        entity.SkillAttack(skillID);
    }



}