/****************************************************
    文件：StateIdle.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 2:26:38
	功能：
*****************************************************/

using UnityEngine;

public class StateIdle : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
        entity.curState = AniState.Idle;
        entity.SetDir( Vector2.zero );
        
        //清空打断连招后的回调
        entity.combo.endSkillIDCb = -1;
        if (entity.entityType == EntityType.Player)
        { 
         
        }
       
       // PECommon.Log(this.GetType().ToString()+" Enter");
    }

    public void Exit(EntityBase entity, params object[] args)
    {
      //  PECommon.Log(this.GetType().ToString() + " Exit");
    }

    public void Process(EntityBase entity, params object[] args)
    {
      //  PECommon.Log(this.GetType().ToString() + " Process");
        bool isCombo = false;
        if (entity.combo != null)//Monster没Combo
        { 
            isCombo = entity.combo.nextSkillID != Constants.NoComboNextSkillID;
        }
             
        if ( isCombo)
        {
            entity.StateAttack(entity.combo.nextSkillID);
        }
        else
        {
            if (entity.entityType == EntityType.Player)
            {
                entity.canRlsSkill = true;
            }
            //
            if ( entity.GetInputDir()!=Vector2.zero)
            {
                entity.StateMove();
                entity.SetDir(entity.GetInputDir());
            }
            else
            {
                 entity.SetAniAction(Constants.ActionDefault);
                entity.SetAniBlend(Constants.BlendIdle);
            }
        }

    }


}