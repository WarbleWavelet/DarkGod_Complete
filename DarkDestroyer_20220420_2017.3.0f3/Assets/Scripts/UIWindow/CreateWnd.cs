/****************************************************
    文件：CreateWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/28 19:2:45
	功能：
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

public class CreateWnd : WindowRoot
{

    public InputField iptName;
    public Button btnRand;
    public Button btnEnter;
    protected override void InitWnd()
    {
        base.InitWnd();
        GameRoot.AddTips("进入CreateWnd");
        JustForSee();
        btnRand.onClick.AddListener(ClickRandBtn);
        btnEnter.onClick.AddListener(ClickEnterBtn);
        RandPlayerName();
    }

    /// <summary>
    /// 方便看UI的引用
    /// </summary>

    private void JustForSee()
    {
        iptName=transform.Find("RightPin/iptName").GetComponent<InputField>();
        btnRand = transform.Find("RightPin/iptName/btnRand").GetComponent<Button>();
        btnEnter = transform.Find("RightPin/btnEnter").GetComponent<Button>();

    }

    /// <summary>
    /// 点击“进入游戏”
    /// </summary>
    private void ClickEnterBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        if (iptName.text != "")
        {

        }
        else
        {
            GameRoot.AddTips("名字不符合规范");
        }
    }


    /// <summary>
    /// 随机玩家名字
    /// </summary>
    private void RandPlayerName()
    {
        
        iptName.text=resSvc.GetRDName(false);
    }

    /// <summary>
    /// 点击“骰子”
    /// </summary>

    void ClickRandBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        iptName.text = resSvc.GetRDName(false);
    }
}