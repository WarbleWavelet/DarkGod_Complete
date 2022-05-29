/****************************************************
    文件：BattleMgr.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 10:51:5
	功能：战场管理器
*****************************************************/

using System;
using UnityEngine;

public class BattleMgr : MonoBehaviour 
{


    MapMgr mapMgr;
    SkillMgr skillMgr;
    StateMgr stateMgr;
    public ResSvc resSvc;
    public void InitMap(int mapID)
    {
        mapMgr=gameObject.AddComponent<MapMgr>();
        skillMgr = gameObject.AddComponent<SkillMgr>();
        stateMgr = gameObject.AddComponent<StateMgr>();
        mapMgr.Init();
        skillMgr.Init();
        stateMgr.Init();

        resSvc = ResSvc.Instance;
        MapCfg cfg = resSvc.GetMapDataCfg(mapID);


        resSvc.AsyncLoadScene(cfg.sceneName, () =>{ InitScene(); });


    }

    private void InitScene()
    {
        GameRoot.AddTips("进入副本地图");
    }
}