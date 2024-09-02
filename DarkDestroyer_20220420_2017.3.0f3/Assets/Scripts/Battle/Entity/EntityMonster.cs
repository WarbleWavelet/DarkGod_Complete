/****************************************************
    文件：EntityMonster.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 11:39:46
	功能：
*****************************************************/

using UnityEngine;

public class EntityMonster : EntityBase 
{

    public MonsterData monsterData;
    public AIMonster aiMonster;
    public override void SetBattleProps(BattleProps props)
    {
        int lv = monsterData.lv;
        /// <summary>基础属性</summary>
        BattleProps _props = new BattleProps
        {
            hp = lv * props.hp,
            ad = lv * props.ad,
            ap = lv * props.ap,
            addef = lv * props.addef,
            apdef = lv * props.apdef,
            dodge = lv * props.dodge,
            critical = lv * props.critical,
            pierce = lv * props.pierce
        };
        HP = _props.hp;
        Props = _props;
    }

    public override Vector2 CalcTargetDir()
    {
        EntityMonster from = this;
        EntityPlayer to = battleMgr.playerEntity;
        //
        if (to == null || to.curState == AniState.Die)
        {
            return Vector2.zero;
        }
        else
        {
            Vector3 fromPos = from.GetPos();
            Vector3 toPos = to.GetPos();
            Vector2 toDir=new Vector2(toPos.x - fromPos.x, toPos.z - fromPos.z).normalized;
            return toDir;
        }
    }

    public override bool GetBreakState()
    {
        if (monsterData.mCfg.isStop)//全局中断
        {
            if (curSkillCfg != null)
            {
                return curSkillCfg.isBreak;//局部中断状态

            }
            else
            {
                return true;
            }
        }
        else
        { 
        
            return false;
        }

    }

    public override void SetUIHpVal(int oldVal, int newVal)
    {

        switch (monsterData.mCfg.mType)
        {
            case MonsterType.Solider:
                {
                    if (GameRoot.Instance.dynamicWnd != null && GameRoot.Instance != null)
                    {
                        GameRoot.Instance.dynamicWnd.SetHpVal(GetTrans().name, oldVal, newVal);
                    }
                }
                break;
            case MonsterType.Boss:
                {
                    if (BattleSys.Instance.playerCtrlWnd != null && BattleSys.Instance != null)
                    {
                        BattleSys.Instance.playerCtrlWnd.SetBossHPVal(oldVal, newVal);
                    }
                }
                break;
            default: break;
        }



    }
}