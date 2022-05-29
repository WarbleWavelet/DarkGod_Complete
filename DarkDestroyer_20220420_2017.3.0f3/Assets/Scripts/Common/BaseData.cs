/****************************************************
    文件：BaseData.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/8 15:59:36
	功能：数据配置类
*****************************************************/

using System.ComponentModel;
using UnityEngine;

public class BaseData <T>
{
    public int ID;
}

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
}



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