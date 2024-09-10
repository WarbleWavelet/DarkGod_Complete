/****************************************************
    文件：MonsterCfg.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/7 22:27:32
	功能：
*****************************************************/

using UnityEngine;

#region Monster     
/**
	<item ID="1001">
		<mName>铁甲战士</mName>
		<resPath>PrefabNPC/MonsterSoldier_1</resPath>
	</item>
**/

public class MonsterCfg : BaseData<MonsterCfg>
{
    public string mName;
    public string resPath;
    public BattleProps props;
    public int skillID;
    /// <summary>攻击距离</summary>
    public float atkDis;
    /// <summary>技能能被打断</summary>
    public bool isStop;
    public MonsterType mType;
}




#endregion