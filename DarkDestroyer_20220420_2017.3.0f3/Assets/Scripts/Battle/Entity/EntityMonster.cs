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
    public override void SetBattleProps(BattleProps props)
    {
        int lv = monsterData.lv;
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

        Props = _props;
    }
}