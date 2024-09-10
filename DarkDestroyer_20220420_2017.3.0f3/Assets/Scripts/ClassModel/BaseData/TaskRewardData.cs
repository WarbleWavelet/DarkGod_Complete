/****************************************************
    文件：TaskRewardData.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/9 22:9:53
	功能：
*****************************************************/

using UnityEngine;

public class TaskRewardData : BaseData<TaskRewardData>
{
    /// <summary>是否已经完成</summary>
    public TaskState state;
    /// <summary>已经完成的次数</summary>
    public int prgs;
}