/****************************************************
    文件：LoginSys.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/23 17:27:45
	功能：注册系统
*****************************************************/

using PEProtocol;
using UnityEngine;

public class LoginSys : SystemRoot
{

    public LoginWnd loginWnd;
    public CreateWnd createWnd;

    public static LoginSys Instance;




    void Start()
    {
        loginWnd = transform.Find("Canvas/LoginWnd").GetComponent<LoginWnd>();
    }
    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        PECommon.Log("Init Login", LogType.Log);
    }

    /// <summary>
    /// 进入登录场景
    /// </summary>
    public void EnterLogin()
    {

        resSvc.AsyncLoadScene(Constants.sceneLogin, () => { OpenLoginWnd(); });

        TestAddTips();
    }

    /// <summary>
    /// 打开登录窗口
    /// </summary>
    public void OpenLoginWnd()
    {
        loginWnd.SetWndState();
        
        audioSvc.PlayBgMusic (Constants.BGLogin);
    }

    /// <summary>
    /// 登陆成功的回调
    /// </summary>
    public void RspLogin(GameMsg msg)
    {
        //GameRoot.AddTips("登录成功");
        GameRoot.Instance.SetPlayerData(msg.rspLogin);

        if (msg.rspLogin.pd.name == null ||msg.rspLogin.pd.name == "" )
        {
            createWnd.SetWndState(true);
        }
        else
        {
            //TODO  
            //GameRoot.AddTips("进入主程");
            MainCitySys.Instance.EnterMainCity();

        }


        loginWnd.SetWndState(false);
    }

    public void RspRename(GameMsg msg)
    {
        GameRoot.Instance.SetPlayerName(msg.rspRename.name);
        
        // TODO 跳转场景
        MainCitySys.Instance.EnterMainCity();

    }

    /// <summary>
    /// 测试AddTips
    /// </summary>
    void TestAddTips()
    {
        GameRoot.AddTips("注");
        GameRoot.AddTips("注册");
        GameRoot.AddTips("注册系");
        GameRoot.AddTips("注册系统");
    }
}