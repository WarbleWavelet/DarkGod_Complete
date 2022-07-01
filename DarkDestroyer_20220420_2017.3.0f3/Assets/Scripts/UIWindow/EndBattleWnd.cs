/****************************************************
    文件：EndBattleWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/30 15:49:53
	功能：战斗结算界面
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndBattleWnd : WindowRoot
{

    [Header("EndBattleWnd")]
    public EndBattleType endBattleType= EndBattleType.None;
    public Button btnClose;    
    public Button btnExit;

    [Header("战斗结算")]    
    /// <summary>战斗结算</summary>
    public Transform transEvaluation;    
    public Text txtTime;
    /// <summary>用时</summary>
    public Text txtHp;
    public Text txtReward;
    public Image imgEvaluation;

    public Button btnEnter;
    //



    bool isFirst = true;
    protected override void InitWnd()
    {
        base.InitWnd();
        if (isFirst)
        {
            BindUI();
            isFirst = false;
        }
        RefreshUI();
    }



    public void SetEndBattleType(EndBattleType type)
    {
        endBattleType = type;
    }

    protected  void BindUI()
    {
        btnExit=transform.Find("btnExit").GetComponent<Button>();
        btnClose = transform.Find("btnClose").GetComponent<Button>();
        //

        txtTime= transform.Find("CenterPin/Time/txtTime").GetComponent<Text>();
        txtHp= transform.Find("CenterPin/Hp/txtHp").GetComponent<Text>();
        txtReward= transform.Find("CenterPin/Reward/txtReward").GetComponent<Text>();
        imgEvaluation = transform.Find("CenterPin/imgEvaluation").GetComponent<Image>();
        transEvaluation = transform.Find("CenterPin");
        btnEnter = transform.Find("CenterPin/btnEnter").GetComponent<Button>();
        //
        btnClose.onClick.AddListener( CliclBtnClose );
        btnExit.onClick.AddListener(CliclBtnExit);
        btnEnter.onClick.AddListener(CliclBtnEnter);
    }

    private void CliclBtnExit()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        

        BattleSys.Instance.mainCitySys.EnterMainCity();
        BattleSys.Instance.DestroySelf();
    }

    private void CliclBtnEnter()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
    }

    private void CliclBtnClose()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        SetWndState(false);
        BattleSys.Instance.playerCtrlWnd.SetWndState(true);
        //
        BattleSys.Instance.battleMgr.isPauseGame = false;

        endBattleType=EndBattleType.None;

    }


    
    internal void SetEndBattleWndState(EndBattleType type, bool state)
    { 
        SetWndState(true);
        endBattleType = type;       
        RefreshUI();
    }

    private void RefreshUI()
    {

        switch (endBattleType)
        {
            case EndBattleType.Win:
                {
                    SetActive(transEvaluation, true);
                }
                break;
            case EndBattleType.Lose:
                {
                    SetActive( btnExit);
                    SetActive(btnClose,false);
                    BattleSys.Instance.battleMgr.isPauseGame = true;
                    SetActive(transEvaluation, false);
                }
                break;
            case EndBattleType.Pause:
                {
                    SetActive(btnClose);
                    SetActive(btnExit);
                    SetActive(transEvaluation, false);

                    BattleSys.Instance.battleMgr.isPauseGame = true;
                }
                break;
            default:break;
        }

    }
}


public enum EndBattleType
{
    None,
    /// <summary>01 TimeScale；02 Monster StateIdle()</summary>
    Pause,
    Exit,
    Win,
    Lose 

}