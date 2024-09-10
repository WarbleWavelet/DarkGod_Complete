/****************************************************
    文件：SkillMoveCfg.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/9 22:10:35
	功能：
*****************************************************/

using System.ComponentModel;
using UnityEngine;

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