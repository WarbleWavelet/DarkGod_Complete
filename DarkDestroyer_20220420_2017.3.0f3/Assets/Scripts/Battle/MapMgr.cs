/****************************************************
    文件：MapMgr.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 11:8:54
	功能：地图管理器
*****************************************************/

using UnityEngine;


public class MapMgr : MonoBehaviour 
{

    [Header("MapMgr")]
    public BattleMgr battleMgr;
    public int wave = 0;

    
    public void Init(BattleMgr battleMgr)
    {
    
        this.battleMgr = battleMgr;

        // battleMgr.LoadMonsterByWave(0);
        this.battleMgr.LoadMonsterByWave( wave );
        PECommon.Log(this.GetType().ToString() + " Init");

    }
}