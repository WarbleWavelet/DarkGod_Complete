/****************************************************
    文件：SkillActionCfg.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/9 22:10:58
	功能：
*****************************************************/

using UnityEngine;

/// <summary>
/// 技能判定(技能的距离,角度，是否延时)
/// </summary>
public class SkillActionCfg : BaseData<SkillActionCfg>
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
    public int angle;


}