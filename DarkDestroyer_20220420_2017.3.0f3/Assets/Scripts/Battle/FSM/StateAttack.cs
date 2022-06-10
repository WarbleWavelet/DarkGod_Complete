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
        entity.curState = AniState.Attack;
        entity.curSkillCfg = ResSvc.Instance.GetSkillCfg( (int)args[0] );
    }

    public void Exit(EntityBase entity, params object[] args)
    {

        PECommon.Log(this.GetType().ToString() + " Exit");

        entity.canCtrl = true;
        if (args == null)
        {

            Debug.Log("StateAttack.Exit有时报空");
        }
        else
        {
            int skillID = (int)args[0];
            entity.combo.ExitCurSkill(entity, entity.curSkillCfg);
        }
        entity.SetAction(Constants.ActionDefault);


    }

    public void Process(EntityBase entity, params object[] args)
    {
        PECommon.Log(this.GetType().ToString() + " Process");
        //
        int skillID = (int)args[0];
        entity.SkillAttack( skillID);
    }
}