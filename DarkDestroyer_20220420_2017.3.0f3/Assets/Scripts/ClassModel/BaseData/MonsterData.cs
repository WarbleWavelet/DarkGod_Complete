/****************************************************
    文件：MonsterData.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/7 22:46:29
	功能：
*****************************************************/

using UnityEngine;

public class MonsterData : BaseData<MonsterData>
{
    /**
        <item ID="1001">
            <mName>铁甲战士</mName>
            <resPath>PrefabNPC/MonsterSoldier_1</resPath>
            <skillID>201</skillID>
            <atkDis>2</atkDis>
            <hp>2000</hp>
            <ad>100</ad>
            <ap>80</ap>
            <addef>5</addef>
            <apdef>10</apdef>
            <dodge>40</dodge>
            <pierce>5</pierce>
            <critical>35</critical>
        </item>
    **/
    public Vector3 mBornRot;
    public Vector3 mBornPos;
    /// <summary>第几波</summary>
    public int mWave;
    /// <summary>第几个</summary>
    public int mIndex;
    /// <summary>monster的cfg</summary> 
    public MonsterCfg mCfg;
    public int lv;
}