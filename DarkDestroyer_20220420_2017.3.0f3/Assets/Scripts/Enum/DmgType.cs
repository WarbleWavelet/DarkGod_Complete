/****************************************************
    文件：DmgType.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/7 22:22:18
	功能：
*****************************************************/

using UnityEngine;

#region 伤害类型
/// <summary>
/// 伤害类型
/// </summary>
public enum DmgType
{
    /// <summary>  </summary>
    None,
    /// <summary>  AD是Attack Damage的缩写，意思为物理伤害。 </summary>
    AD,
    /// <summary> ADC是Attack Damage Carry的缩写，是指物理伤害输出类型英雄的简称。 </summary>
    ADC,
    /// <summary>  AP是Ability Power的缩写，主要指法术伤害。  </summary>
    AP,
    /// <summary>  APC是Ability Power Carry的缩写，是使用法术造成大量法术伤害的英雄的简称。</summary>
    APC,
    /// <summary>TD，（ture damage，真实伤害）</summary>
    TD,
    /// <summary> TD，（ture damage，真实伤害）.造成大量法术伤害的英雄的简称。 </summary>
    TDC
}
#endregion
