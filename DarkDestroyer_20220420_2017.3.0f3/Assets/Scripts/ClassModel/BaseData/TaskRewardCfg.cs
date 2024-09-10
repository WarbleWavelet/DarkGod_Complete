/****************************************************
    文件：TaskRewardCfg.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/7 22:26:41
	功能：
*****************************************************/

using UnityEngine;

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




#endregion
