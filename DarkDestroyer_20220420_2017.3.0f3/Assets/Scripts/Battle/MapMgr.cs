/****************************************************
    文件：MapMgr.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 11:8:54
	功能：地图管理器
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;


public class MapMgr : MonoBehaviour
{

    [Header("MapMgr")]
    public BattleMgr battleMgr;
    /// <summary>第几波敌人</summary>
    public int curWaveIdx = 0;
    public List<TriggerData> triggerDataLst = new List<TriggerData>();

    /// <summary>
    /// 开局第一波
    /// </summary>
    /// <param name="battleMgr"></param>
    public void Init(BattleMgr battleMgr, Action cb = null)
    {

        this.battleMgr = battleMgr;


        // battleMgr.LoadMonsterByWave(0);
        this.battleMgr.LoadMonsterByWave(curWaveIdx);
        PECommon.Log(this.GetType().ToString() + " Init");

    }

    /// <summary>
    /// 过门
    /// </summary>
    /// <param name="triggerData"></param>
    /// <param name="waveIdx"></param>
    internal void TriggerMonsterBorn(TriggerData triggerData, int waveIdx)
    {
        if (battleMgr != null)
        {
            battleMgr.LoadMonsterByWave(waveIdx);
            battleMgr.DelayActiveMonster();
            battleMgr.ckeckWave = true;

            BoxCollider cld = triggerData.gameObject.GetComponent<BoxCollider>();
            cld.isTrigger = false;
        }
    }


    /// <summary>
    /// 开门
    /// </summary>
    /// <returns></returns>
    public bool SetNextMonsterWave()
    {
        curWaveIdx++;
        foreach (TriggerData triggerData in triggerDataLst)
        {
            if (triggerData.waveIdx == curWaveIdx)
            { 
                triggerData.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                return true;
            }
        }
        return false;

    }
}