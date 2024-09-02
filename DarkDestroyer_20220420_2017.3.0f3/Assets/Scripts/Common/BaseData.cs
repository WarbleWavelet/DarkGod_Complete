/****************************************************
    文件：BaseData.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/8 15:59:36
	功能：数据配置类
*****************************************************/

using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BaseData <T>
{
    public int ID;
}


#region 地图
/**
-<item ID="10001">
    <mapName>荒野遗迹</mapName>
    <sceneName>SceneOrge</sceneName>
    <power>5</power>
    <mainCamPos>-13.19,18.87,14.69</mainCamPos>
    <mainCamRote>45,135,0</mainCamRote>
    <playerBornPos>-10,13.2,11.5</playerBornPos>
    <playerBornRote>0,145,0</playerBornRote>
    <monsterLst>#|1001,-4.39,13.3,3.79,-50,1|1001,-7.55,13.3, 3,0,1#|1001,18.86,13.6,3.7,-107.3,2|1001,14.35,13.35,5.95,-117.4,2|1001,15.11,13.35,1.63,-66.1,2#|1001,18.16,8.8,32,188,3|1001,11.8,8.8,30.8,145.5,3|1001,15.38,8.8,40.7,173.3,3|1001,9,8.9,38.6,145.5,3|2001,11.4,8.85,41,142,1</monsterLst>
    <exp>1250</exp>
    <coin>980</coin>
    <crystal>48</crystal>
</item>
    MapID,nmoster位置
    |一个
    #一波
**/
public class MapCfg : BaseData<MapCfg>
{
    public string sceneName;
    public string mapName;
    public Vector3 mainCamPos;
    public Vector3 mainCamRote;
    public Vector3 playerBornPos;
    public Vector3 playerBornRote;
    /// <summary>体力限制</summary>
    public int power;
    public List<MonsterData> monsterLst;
    //通关奖励
    public int exp;
    public int coin;
    public int crystal;
}
#endregion


#region 引导
/**
<item ID="1001">
    <npcID>0</npcID>
    <dilogArr>
        #0|智者您好，晚辈$name,前来拜会。
        #1|漫漫人生路，你我得以相遇也是一种缘分。我看你骨骼精奇，眉宇间正气凛然，将来定能成就一番事业。 
        #0|智者过誉了，晚辈阅历尚浅，学识浅薄，空有满腔热血，还请前辈多多教导。
        #1|教导谈不上，但我现有一事可交付与你，若你能办妥，对你而言也是一种历练。你可有意为之？ 
        #0|能为智者办事，是晚辈的福分，定当竭尽所能，请您明示。 
        #1|甚好，此事我已安排妥当，你去主城找凯伦将军，他会告诉你怎么做。
        #0|好的，晚辈即刻启程。
    </dilogArr>
    <actID>0</actID>
    <coin>65</coin>
    <exp>80</exp>
</item>
 */

public class AutoGuideCfg : BaseData<AutoGuideCfg>
{
    /// <summary>触发任务目标NPC索引号</summary>
    public int npcID;
    /// <summary>对话数组</summary>
    public string dilogArr;
    /// <summary>任务目标ID</summary>
    public int actID;
    public int coin;
    public int exp;

}
#endregion


#region  玩家突破
/**
	<item ID = "1" >

        < pos > 0 </ pos >
        < starlv > 1 </ starlv >
        < addhp > 20 </ addhp >
        < addhurt > 25 </ addhurt >
        < adddef > 18 </ adddef >
        < minlv > 1 </ minlv >
        < coin > 150 </ coin >
        < crystal > 5 </ crystal >
    </ item >
    **/
public class StrongCfg : BaseData<StrongCfg>
{
    public int pos;
    public int starlv;
    public int addhp;
    public int addhurt;
    public int adddef;
    public int minlv;
    public int coin;
    public int crystal;
}


#endregion


#region 任务奖励
/**
	<item ID="1">
		<taskName>智者点拨</taskName>
		<count>1</count>
		<exp>1130</exp>
		<coin>1280</coin>
	</item>
 * **/
public class TaskRewardCfg : BaseData<TaskRewardCfg>
{
    /// <summary>任务名</summary>
    public string taskName;
    /// <summary>金币</summary>
    public int coin;
    /// <summary>经验</summary>
    public int exp;
    /// <summary>需要完成的次数</summary>
    public int count;


}

public class TaskRewardData : BaseData<TaskRewardData>
{
    /// <summary>是否已经完成</summary>
    public TaskState state;
    /// <summary>已经完成的次数</summary>
    public int prgs;
}

public enum TaskState
{
    [Description("未定义")]
    None,
    [Description("未接受")]
    UnAccept,
    [Description("接受")]
    Accept,
    [Description("完成")]
    Done,
    [Description("完成领完奖励")]
    Got
}
#endregion


public enum NPCID
{
    WiseMan=0,

}


#region 技能
/**
	<item ID="101">
		<skillName>穿刺</skillName>
		<skillTime>900</skillTime>
		<aniAction>1</aniAction>
		<fx>dagger_skill1</fx>
	</item>
 * **/
public class SkillCfg: BaseData<SkillCfg>
{
    /// <summary>技能名</summary>
    public string skillName;
    /// <summary>技能时间</summary>
    public int skillTime;
    /// <summary>对应动画</summary>
    public int aniAction;
    /// <summary>特效名<summary>
    public string fx;
    //
    [Description("技能产生的移动")]
    public List<int> skillMoveLst;
    //以下两者索引一一对应
    /// <summary>一个技能几段伤害（范围 角度 延时）</summary>
    public List<int> skillActionLst;
    /// <summary>一个技能几段伤害的数值</summary>
    public List<int> skillDamageLst;
    //
    public DmgType dmgType;
    public float cdTime;
    /// <summary>可以连招</summary>
    public bool isCombo;
    /// <summary>穿过敌人</summary>
    public bool isCollide;
    /// <summary>技能中断，不可中断为霸体</summary>
    public bool isBreak;



}

/// <summary>
/// 兰陵王大招这种
/// </summary>
public class SkillMoveCfg : BaseData<SkillMoveCfg>
{

    [Description("节能产生移动的时间,单位毫秒")]
    public int moveTime;

    [Description("技能产生的移动")]
    public float moveDis;

    [Description("施法时间（多久后才会位移），单位毫秒")]
    public int delayTime;

}

/// <summary>
/// 技能判定(技能的距离,角度，是否延时)
/// </summary>
public class SkillActionCfg:BaseData<SkillActionCfg>
{
    /**
    < item ID = "1011" >
            < delayTime > 100 </ delayTime >
            < radius > 2.5 </ radius >
            < angle > 360 </ angle >
    </ item >
    **/
    public int delayTime;
    /// <summary>范围,距离</summary>
    public float radius;
    /// <summary>角度，视野，角度</summary>
    public int angle ;


}


#endregion


#region Monster     
/**
	<item ID="1001">
		<mName>铁甲战士</mName>
		<resPath>PrefabNPC/MonsterSoldier_1</resPath>
	</item>
**/

public class MonsterCfg:BaseData<MonsterCfg>
{
    public string mName;
    public string resPath;
    public BattleProps props;
    public int skillID;
    public float atkDis;
    /// <summary>技能能被打断</summary>
    public bool isStop;
    public MonsterType mType;
}


public class MonsterData : BaseData<MonsterData>
{
    /**
        <item ID="1001">
            <mName>铁甲战士</mName>
            <resPath>PrefabNPC/MonsterSoldier_1</resPath>
            <skillID>201</skillID>
            <atkDis>2</atkDis>
            <hp>2000</hp>
            <ad>100</ad>
            <ap>80</ap>
            <addef>5</addef>
            <apdef>10</apdef>
            <dodge>40</dodge>
            <pierce>5</pierce>
            <critical>35</critical>
        </item>
    **/
    public Vector3 mBornRot;
    public Vector3 mBornPos;
    /// <summary>第几波</summary>
    public int mWave;
    /// <summary>第几个</summary>
    public int mIndex;
    /// <summary>monster的cfg</summary> 
    public MonsterCfg mCfg;
    public int lv;
}

#endregion



#region 战斗需要的属性
/// <summary>
/// 不同等级的敌人，属性的复用，一级*1，两级*2之类
/// </summary>
public class BattleProps
{
    /*
    //含义看数据库
    public int id;
    public string name;
    public int exp;
    public int lv;
    public int power;
    public int coin;
    public int diamond;
    public int crystal;
    //
    public int guideid;
    public int[] strongArr;
    public long time;
    public string[] taskRewardArr;   // ID | 已经完成次数 | 是否已经领取奖励
    public int instance;
    //
    */
    public int hp;
    public int ad;
    public int ap;
    public int addef;
    public int apdef;
    public int dodge;
    public int critical;
    public int pierce;
    //

}



public enum MonsterType
{
    None=0,
    Solider=1,
    Boss=2

}
#endregion