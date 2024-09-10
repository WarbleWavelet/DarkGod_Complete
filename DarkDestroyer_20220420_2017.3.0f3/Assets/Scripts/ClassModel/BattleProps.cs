/****************************************************
    文件：BattleProps.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/7 22:44:59
	功能：
*****************************************************/

using UnityEngine;

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
    None = 0,
    Solider = 1,
    Boss = 2

}
#endregion