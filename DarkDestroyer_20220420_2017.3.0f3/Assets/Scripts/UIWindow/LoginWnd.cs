/****************************************************
    文件：LoginWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/26 20:13:17
	功能：
*****************************************************/

using PEProtocol;
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



     void JustForNte()
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
        JustForNte();
    }



    #region Click
    /// <summary>
    /// 点击进入游戏
    /// </summary>
    public void ClickEnterBtn()
    {
        audioSvc.PlayUIAudio(Constants.UILoginBtn);

        string _acct = iptAcct.text;
        string _pass = iptPass.text;

        if (_acct != "" && _pass != "")
        {
            PlayerPrefs.SetString("Acct", _acct);
            PlayerPrefs.SetString("Pass", _pass);
            //
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqLogin,
                reqLogin = new ReqLogin
                {
                    acct = _acct,
                    pass = _pass
                }
            };
            netSvc.SendMsg(msg);

            // LoginSys.Instance.RspLogin();

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
    #endregion

}