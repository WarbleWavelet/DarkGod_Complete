/****************************************************
    文件：LoginWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/26 20:13:17
	功能：
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class LoginWnd : WindowRoot
{
    public InputField iptAcct;
    public InputField iptPass;
    /// <summary>公告</summary>
    public Button btnNotice;
    public Button btnEnter;

    public CreateWnd createWnd;


     void Start()
    {
        btnNotice=transform.Find("btnNotice").GetComponent<Button>();
        iptAcct = transform.Find("rightPin/iptbg1/iptAcct").GetComponent<InputField>();
        iptPass = transform.Find("rightPin/iptbg2/iptPass").GetComponent<InputField>();
        btnEnter = transform.Find("rightPin/btnEnter").GetComponent<Button>();

        btnEnter.onClick.AddListener(ClickEnterBtn);
        btnNotice.onClick.AddListener(ClickNoticeBtn);

    }
    protected override void InitWnd()
    {
        base.InitWnd();
        if (PlayerPrefs.HasKey("Acct") && PlayerPrefs.HasKey("Pass"))
        {
            iptAcct.text=PlayerPrefs.GetString("Acct");
            iptPass.text = PlayerPrefs.GetString("Pass");
        }
        else
        {
            iptAcct.text = "";
            iptPass.text = "";
        }
    }

    /// <summary>
    /// 点击进入游戏
    /// </summary>
    void ClickEnterBtn()
    {
        audioSvc.PlayUIAudio(Constants.UILoginBtn);

        string acct=iptAcct.text;
        string pass=iptPass.text;

        if (acct != "" && pass != "")
        {
            PlayerPrefs.SetString("Acct",acct);
            PlayerPrefs.SetString("Pass",pass);
       LoginSys.Instance.RspLogin();

        }
        else
        {
            GameRoot.AddTips("账号或密码为空");
        }
        //新客户

        //老客户
 
    }
    /// <summary>
    /// 点击公告
    /// </summary>
    void ClickNoticeBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        GameRoot.AddTips("公告功能还在开发中");
    }
}