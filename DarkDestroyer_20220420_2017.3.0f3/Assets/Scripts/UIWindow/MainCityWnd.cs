/****************************************************
    文件：MainCityWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/4 23:20:23
	功能：主城市UI界面
*****************************************************/

using PEProtocol;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCityWnd : WindowRoot
{

    #region 属性字段
   [Header("左上")]
    public Image imgPowerPrg;
    public Text txtFight;

    public Text txtPower;
    public Text txtLevel;
    public Text txtName;
    public Button btnAddPower;


    [Header("右上")]
    public Button btnGuide;
    public Button btnCharge;
    public Button btnVip;


    [Header("右下")]
    public Button btnMenu;
    public Animation aniMenu;
    public bool menuState=false;
    public Button btnStrong;
    public Button btnMKCoin;
    public Button btnTask;
    public Button btnInstance;


    [Header("左下")]
    public Image imgTouch;
    public Image imgDirBg;
    public Image imgDirPoint;
    /// <summary>BG位置确定后Point的位置</summary>
    public Vector2 startPos=Vector2.zero;
    /// <summary>BG默认的位置</summary>
    public Vector2 defaultPos=Vector2.zero;
    /// <summary>摇杆点的运动半径</summary>
    float pointDis;

    [Header("下")]
    public Text txtExpPrg;
    public Transform expPrgTrans;
    public Button btnChat;
    public ChatWnd chatWnd;

    [Header("NPC")]
    public GuideWnd guideWnd;
    public AutoGuideCfg curTaskData;

    #endregion
 
    #region 生命
    protected override void InitWnd()
    {
        base.InitWnd();
        //

        btnAddPower.onClick.AddListener(ClickBtnAddPower);
        btnMenu.onClick.AddListener(ClickBtnMenu);
        btnGuide.onClick.AddListener(ClickBtnGuide);
        btnStrong.onClick.AddListener(ClickBtnStrong);
        btnMKCoin.onClick.AddListener(ClickBtnMKCoin);
        btnChat.onClick.AddListener(ClickBtnChat);
        btnTask.onClick.AddListener(ClickBtnTask);
        btnInstance.onClick.AddListener(ClickBtnInstance);
        //
        defaultPos = imgDirBg.transform.position;
        pointDis = AdaptDirPoint();//放这里，运行时改变无效
        SetActive(imgDirPoint,false);
        RegisterTouchEvets();
        
        //
        RefreshUI();
       
    }



    void Update()
    {
        //pointDis = AdaptDirPoint();//放这里，运行时改变有效
       // RefreshUI();
    }

    public void RefreshUI()
    {
        PlayerData pd = GameRoot.Instance.PlayerData;
        SetText(txtFight, PECommon.GetFightByProps(pd));
        SetText(txtPower,"体力:"+pd.power+"/"+PECommon.GetPowerLimit(pd.lv));
        imgPowerPrg.fillAmount = (pd.power * 1.0f) / (1.0f*PECommon.GetPowerLimit(pd.lv));

        SetText(txtName, pd.name);
        SetText(txtLevel, pd.lv);
        //
        AdaptExpPrg(pd);
        //
        curTaskData = resSvc.GetGuideCfg(pd.guideid);
        if (curTaskData != null)
        {
            SetGuideBtnCoin(curTaskData.npcID);
        }
    }


    #endregion

    #region Click
    public void ClickBtnMenu()
    {

        audioSvc.PlayUIAudio(Constants.UIExtenBtn);
        menuState = !menuState;

        AnimationClip clip = null;
        if (menuState)
        {
            clip = aniMenu.GetClip("OpenMCMenu");
        }
        else
        {
            clip = aniMenu.GetClip("CloseMCMenu");
        }

        aniMenu.Play(clip.name);
    }


    public void ClickHeadBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIOpenPage);
        MainCitySys.Instance.OpenInfoWnd();
    }





    private void ClickBtnChat()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        chatWnd.OpenChatWnd();
    }

    public void ClickBtnMKCoin()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);

        MainCitySys.Instance.OpenMKCoinWnd();
    }
    private void ClickBtnAddPower()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        MainCitySys.Instance.OpenAddPowerWnd();
    }
    private void ClickBtnStrong()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        MainCitySys.Instance.OpenStrongWnd();
    }

    private void ClickBtnTask()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        MainCitySys.Instance.OpenTaskRewardWnd();
    }

    public void ClickBtnInstance()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        MainCitySys.Instance.OpenInstanceWnd();
    }
    #endregion

    #region 摇杆
    /// <summary>
    /// 适配摇杆点的半径
    /// </summary>
    /// <returns></returns>
    float AdaptDirPoint()
    {
        return 1.0f * Screen.height / Constants.ScreenStandardHeight * Constants.ScreenOPDis;
    }
    public void RegisterTouchEvets()
    {

        OnClickDown(imgTouch.gameObject, (PointerEventData evt) => {
            startPos = evt.position;
            SetActive(imgDirPoint);
          
            imgDirBg.transform.position = evt.position;
            //
            MainCitySys.Instance.StopNavTask();
        });

        OnClickUp(imgTouch.gameObject, (PointerEventData evt) => {
            imgDirBg.transform.position = defaultPos;
            imgDirPoint.transform.localPosition = Vector2.zero;
            SetActive(imgDirPoint, false);
            MainCitySys.Instance.SetMoveDir(Vector2.zero);
        });

        OnDrag(imgTouch.gameObject, (PointerEventData evt) => {
            SetActive(imgDirPoint);
            //
            Vector2 dir = evt.position - startPos;
            float len = dir.magnitude;
            if (len > pointDis)
            {
                Vector2 clampDir = Vector2.ClampMagnitude(dir, pointDis);
                imgDirPoint.transform.position = clampDir + startPos;
            }
            else
            {
                imgDirPoint.transform.position = evt.position;
            }
            MainCitySys.Instance.SetMoveDir(dir.normalized);

        });
    }
    #endregion

    #region 经验条


    /// <summary>
    /// 适配经验条
    /// </summary>
    void AdaptExpPrg(PlayerData pd)
    {
        GridLayoutGroup grid = expPrgTrans.GetComponent<GridLayoutGroup>();
        float rate = 1f * Constants.ScreenStandardHeight / Screen.height;
        float width = rate * Screen.width;
        float itemWidth = (width - 78 - 5.83f - 6.5f - 9 * 3.8f) / 10;
        grid.cellSize = new Vector2(itemWidth, 8.9f);
        //
        int expValPrg = (int)(1f * pd.exp / PECommon.GetExpUpValByLV(pd.lv) * 100);
        SetText(txtExpPrg, expValPrg + "%");
        int index = expValPrg / 10;

        for (int i = 0; i < expPrgTrans.childCount; i++)
        {
            Image expItem = expPrgTrans.GetChild(i).GetComponent<Image>();
            if (i < index)
            {
                expItem.fillAmount = 1;
            }
            else
            {
                if (i == index)
                {
                    expItem.fillAmount = (expValPrg % 10 * 1f) / 10;
                }
                else
                    expItem.fillAmount = 0;
                {
                }
            }
        }

    }
    #endregion



    #region 任务引导
    private void ClickBtnGuide()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        if (curTaskData != null)
        {
            MainCitySys.Instance.RunNavTask( curTaskData);
        }
        else
        {
            GameRoot.AddTips("开发中：");
        }

    }

    /// <summary>
    /// 设置btnGuide的图片
    /// </summary>
    /// <param name="npcID"></param>
    private void SetGuideBtnCoin(int npcID)
    {

        string spPath = "";//spritePath

        switch (npcID)
        {
            case Constants.NPCArtisan:
                {
                    spPath = PathDefine.ArtisanHead;
                }
                break;
            case Constants.NPCGeneral:
                {
                    spPath = PathDefine.GeneralHead;
                }
                break;
            case Constants.NPCTrader:
                {
                    spPath = PathDefine.TraderHead;
                }
                break;
            case Constants.NPCWiseMan:
                {
                    spPath = PathDefine.WiseManHead;
                }
                break;
            default:
                {
                    spPath = PathDefine.TaskHead;
                }
                break;
        }
        SetSprite(btnGuide.GetComponent<Image>(), spPath);

    }



    #endregion
}