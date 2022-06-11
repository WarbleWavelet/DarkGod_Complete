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
        PECommon.Log(this.GetType().ToString() + " Enter");
        //
        entity.curState = AniState.Attack;
        int skillID = (int)args[0];
        entity.curSkillCfg = ResSvc.Instance.GetSkillCfg(skillID );
    }

    public void Exit(EntityBase entity, params object[] args)
    {

        PECommon.Log(this.GetType().ToString() + " Exit");
        //
        entity.canCtrl = true;
        AddCombo(  entity, args);
        entity.SetAniAction(Constants.ActionDefault);


    }

    public void Process(EntityBase entity, params object[] args)
    {
        PECommon.Log(this.GetType().ToString() + " Process");
        //
        int skillID = (int)args[0];
        entity.SkillAttack(skillID);
    }

    void AddCombo(EntityBase entity, params object[] args )
    { 
            if (args == null)
        {

            Debug.Log("StateAttack.Exit有时报空");
        }
        else
        {
           
            entity.combo.ExitCurSkill(entity, entity.curSkillCfg);
        }
    }
}