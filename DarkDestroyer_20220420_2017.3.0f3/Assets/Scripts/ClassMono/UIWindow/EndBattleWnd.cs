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
    int instanceID;
    int costTime;
    int remainHP;


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
        BattleSys.Instance.DestroySelf();
        BattleSys.Instance.mainCitySys.EnterMainCity(EnterMainCityType.InstanceSys);


        
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

                    SetActive(btnClose,false);
                    SetActive(btnExit,false);
                    //
                    MapCfg cfg = resSvc.GetMapCfg(this.instanceID);
                    int min = costTime / (24*60);
                    int sec = costTime % (24 * 60);

                    string str = "";
                    str += Constants.Color(" 金币"+ cfg.coin, TxtColor.Yellow);
                    str += Constants.Color(" 水晶"+ cfg.crystal, TxtColor.Blue);
                    str += Constants.Color(" 经验"+ cfg.exp, TxtColor.Green); 
                    SetText( txtTime, "通关时间" + min + ":" + sec);
                    SetText(txtHp, "剩余体力:"+remainHP);
                    SetText(txtReward, str);

                    //
                    timerSvc.AddTimerTask((int tid) =>
                    {
                        audioSvc.PlayUIAudio(Constants.InstanceWin);
                        //
                        SetActive(transEvaluation, true);                     
                        transEvaluation.GetComponent<Animation>().Play();
                        //

                        timerSvc.AddTimerTask((int tid1) =>
                        {
                            audioSvc.PlayUIAudio(Constants.FBItemEnter);
                            timerSvc.AddTimerTask((int tid2) =>
                            {
                                audioSvc.PlayUIAudio(Constants.FBItemEnter);
                                timerSvc.AddTimerTask((int tid3) =>
                                {
                                    audioSvc.PlayUIAudio(Constants.FBItemEnter);
                                    timerSvc.AddTimerTask((int tid4) =>
                                    {
                                        audioSvc.PlayUIAudio(Constants.InstanceWin);
                                    }, 300);
                                }, 270);
                            }, 270);
                        }, 325);
                    }, 1000);
                }
                break;
            case EndBattleType.Lose:
                {
                    audioSvc.PlayBgMusic(Constants.InstanceLose,false);
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


    public void SetEndBattleData( int instanceID, int costTime, int remainHP)
    { 
        this.instanceID = instanceID;
        this.remainHP = remainHP;
        this.costTime = costTime;
       
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