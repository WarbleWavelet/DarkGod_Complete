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
    public PlayerController ctrl;
    public InfoWnd infoWnd;
    public Transform charCamTrans;

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
            //
            charCamTrans = GameObject.FindGameObjectWithTag(Tags.CharShowCam).transform;
        });
    }


    /// <summary>
    /// 角色信息面板的展示
    /// </summary>
    /// <param name="state"></param>
    public void SetCharShowCamState(bool state = true)
    {

        if (charCamTrans == null)
        {
            charCamTrans = GameObject.FindGameObjectWithTag(Tags.CharShowCam).transform;
        }

        //if (state)
        //{
        //    Transform player = ctrl.transform;

        //    charCamTrans.localPosition = player.position + player.forward*3.8f + new Vector3(0f, 1.95f, 0f);//任务前上方
        //    charCamTrans.localEulerAngles = new Vector3(0f, 180 + player.position.y, 0f);//相机对着人物
        //    charCamTrans.localScale = Vector3.one;
        //}

        charCamTrans.gameObject.SetActive(state);


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
        //
        ctrl = player.GetComponent<PlayerController>();
        ctrl.Init();
    }


    /// <summary>
    /// 通过摇杆控制移动方向
    /// </summary>
    /// <param name="dir"></param>
   public void SetMoveDir(Vector2 dir)
    {
        if (dir == Vector2.zero)
        {
            ctrl.SetBlend(Constants.BlendIdle);
        }
        else
        {
            ctrl.SetBlend(Constants.BlendWalk);
        }
        ctrl.Dir = dir;

    }

    public void OpenInfoWnd()
    {

        SetCharShowCamState(true);
        infoWnd.SetWndState();

        
    }

    public void CloseInfoWnd()
    {

        SetCharShowCamState(false);
        infoWnd.SetWndState(false);
    }
}