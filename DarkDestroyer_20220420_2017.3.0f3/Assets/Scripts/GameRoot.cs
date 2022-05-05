/****************************************************
    文件：GameRoot.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/23 17:24:44
	功能：游戏启动入口
*****************************************************/

using PEProtocol;
using System;
using UnityEngine;

public class GameRoot : MonoBehaviour 
{

    #region 属性 字段

    public static GameRoot Instance;



    public LoadingWnd loadingWnd;
    public DynamicWnd dynamicWnd;

    /// <summary>保存数据信息</summary>
    PlayerData playerData=null;
    public PlayerData PlayerData
    {
        get
        {
            return playerData;
        }
    }
    #endregion


    #region 生命

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PECommon.Log("GameStart",LogType.Log);
        ClearUIRoot();
        Init();
    }

    #endregion
  

    /// <summary>
    /// 控制初始化模块的顺序(乱了就空指针错误)
    /// </summary>
    void Init()
    {
        NetSvc netSvc = GetComponent<NetSvc>();
        netSvc.InitSvc();
        //
        ResSvc res = GetComponent<ResSvc>();
        res.InitSvc();
        AudioSvc audio = GetComponent<AudioSvc>();
        audio.InitSvc();

        LoginSys login=GetComponent<LoginSys>();
        login.InitSys();
        
        dynamicWnd.Init();


        login.EnterLogin();
        
        MainCitySys  maincitySys=GetComponent<MainCitySys>();
        maincitySys.InitSys();
    }

    /// <summary>
    /// 桌面消息
    /// </summary>
    /// <param name="tips"></param>
    public static void AddTips(string tips)
    {
       Instance.dynamicWnd.AddTips(tips);
    }



    /// <summary>
    /// 从注册到登录
    /// </summary>
  public  void ClearUIRoot()
    {
        Transform canvas = transform.Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }

        dynamicWnd.SetWndState();
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="rspLogin"></param>
    public void SetPlayerData(RspLogin rspLogin)
    {
        this.playerData = rspLogin.playerData;
    }

    internal void SetPlayerName(string name)
    {
        playerData.name = name;
    }
}