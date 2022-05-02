/****************************************************
    文件：GameRoot.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/23 17:24:44
	功能：游戏启动入口
*****************************************************/

using UnityEngine;

public class GameRoot : MonoBehaviour 
{

    #region 单例

    public static GameRoot Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    #region 登录
    public LoadingWnd loadingWnd;
    public DynamicWnd dynamicWnd;
    #endregion
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("GameStart");
        ClearUIRoot();
        Init();
    }

    /// <summary>
    /// 控制初始化模块的顺序(乱了就空指针错误)
    /// </summary>
    void Init()
    {
        ResSvc res = GetComponent<ResSvc>();
        res.InitSvc();
        AudioSvc audio = GetComponent<AudioSvc>();
        audio.InitSvc();

        LoginSys login=GetComponent<LoginSys>();
        login.InitSys();
        
        dynamicWnd.Init();


        login.EnterLogin();


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
    void ClearUIRoot()
    {
        Transform canvas = transform.Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }

        dynamicWnd.SetWndState();
    }
}