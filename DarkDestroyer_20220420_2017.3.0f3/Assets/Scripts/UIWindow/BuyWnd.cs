/****************************************************
    文件：BuyWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/18 23:24:41
	功能：购买交易界面
*****************************************************/

using PEProtocol;
using System;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class BuyWnd : WindowRoot 
{

    [Header("BuyWnd")]
    public Button btnClose;
    public Button btnSure;
    public Text txtShow;
    private int goodCnt;
    private int buyCnt;
    private BuyType buyType;
    private GoodType goodType;
    bool isFirst = true;

    protected override void InitWnd()
    { 
        base.InitWnd();
        btnSure.interactable = true;
        if (isFirst)
        {
            btnClose.onClick.AddListener(ClickBtnClose);
            btnSure.onClick.AddListener(ClickBtnSure);
            isFirst = false;
        }
    }

    private void ClickBtnSure()
    {
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.ReqBuy,
            reqBuy = new ReqBuy
            {
                buyType = this.buyType,
                buyCnt = this.buyCnt,
                goodType=this.goodType,
                goodCnt=this.goodCnt
            }
        };

        netSvc.SendMsg(msg);
        btnSure.interactable = false;
        SetWndState(false);
    }

    private void ClickBtnClose()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        SetWndState(false);
    }

    public void RefreshUI(int buyCnt,BuyType buyType,int goodCnt,GoodType goodType)
    {
        this.buyType = buyType;
        this.buyCnt = buyCnt;
        this.goodType = goodType;
        this.goodCnt = goodCnt;
        string _cost=Constants.Color(this.buyCnt + EnumExtension.ToDes(this.buyType), TxtColor.Red);
        string _get =Constants.Color(this.goodCnt + EnumExtension.ToDes(this.goodType), TxtColor.Green);//怕get关键字

        SetText(txtShow, "是否花费" + _cost + "购买" + _get + "?");
    }





}




