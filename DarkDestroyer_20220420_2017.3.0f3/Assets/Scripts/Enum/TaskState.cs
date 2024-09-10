/****************************************************
    文件：TaskState.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/7 22:43:10
	功能：
*****************************************************/

using System.ComponentModel;
using UnityEngine;

/// <summary></summary>
public enum TaskState
{
    /// <summary>未定义</summary>
    [Description("未定义")]
    None,
    /// <summary>未接受</summary>
    [Description("未接受")]
    UnAccept,
    /// <summary>接受</summary>
    [Description("接受")]
    Accept,
    /// <summary>完成</summary>
    [Description("完成")]
    Done,
    /// <summary>完成领完奖励</summary>
    [Description("完成领完奖励")]
    Got
}