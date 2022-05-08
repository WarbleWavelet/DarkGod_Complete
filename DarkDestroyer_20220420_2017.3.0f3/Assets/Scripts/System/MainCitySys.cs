/****************************************************
    文件：MainCitySys.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/4 23:23:1
	功能：主城业务系统
*****************************************************/

using System;
using UnityEngine;

public class MainCitySys : SystemRoot
{
    public static MainCitySys Instance;
    public MainCityWnd maincityWnd;

    public override void InitSys()
    {
        base.InitSys();
        Instance = this;

        //maincityWnd = transform.Find("MainCityWnd").GetComponent<MainCityWnd>();
        PECommon.Log("Init MainCitySys");
    }

    public void EnterMainCity()
    {
        MapCfg cfg = ResSvc.Instance.GetMapDataCfg(Constants.MainCityMapID);
        resSvc.AsyncLoadScene(cfg.sceneName, () => {
            PECommon.Log("进入主城");
            //
            LoadPlayer(cfg);
            GameRoot.Instance.ClearUIRoot();
            maincityWnd.SetWndState();
            audioSvc.PlayBgMusic(Constants.BGMainCity);
        });
    }



    /// <summary>
    /// 加载游戏主角
    /// </summary>
    /// <param name="mapData"></param>
    void LoadPlayer(MapCfg mapData)
    {
        GameObject player = resSvc.LoadPrefab(PathDefine.AssassinCityPlayerPrefab, true);
        //
        player.transform.position = mapData.playerBornPos;
        player.transform.localEulerAngles = mapData.playerBornRote;
        player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Camera.main.transform.position = mapData.mainCamPos;
        Camera.main.transform.localEulerAngles = mapData.mainCamRote;
    }
}