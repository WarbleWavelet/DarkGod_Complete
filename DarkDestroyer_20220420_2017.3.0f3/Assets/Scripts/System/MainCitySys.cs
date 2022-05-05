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
        
        resSvc.AsyncLoadScene(Constants.sceneMainCity, LoadMainCity);
    }

    void LoadMainCity()
    {
        PECommon.Log ("进入主城");
        GameRoot.Instance.ClearUIRoot();
        maincityWnd.SetWndState();
        audioSvc.PlayBgMusic(Constants.BGMainCity);
    }
}