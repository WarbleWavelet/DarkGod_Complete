/****************************************************
    文件：ISkillEffectProcessor.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/7 23:7:13
	功能：
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public interface ISkillEffectProcessor
{

    Dictionary<string, GameObject> SkillDic { get; set; }
    void SetSkillFbx(string skillName, float lifeTime);
    /// <summary>  添加特效Go </summary>
    void AddSkillFbx(GameObject go);
}