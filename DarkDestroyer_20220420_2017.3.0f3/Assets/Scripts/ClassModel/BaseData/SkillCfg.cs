/****************************************************
    文件：SkillCfg.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/7 22:27:6
	功能：
*****************************************************/

using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

#region 技能
/**
	<item ID="101">
		<skillName>穿刺</skillName>
		<skillTime>900</skillTime>
		<aniAction>1</aniAction>
		<fx>dagger_skill1</fx>
	</item>
 * **/
public class SkillCfg : BaseData<SkillCfg>
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






#endregion
