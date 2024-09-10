/****************************************************
    文件：InfoWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/9 0:2:10
	功能：角色信息界面
*****************************************************/

using PEProtocol;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoWnd : WindowRoot 
{

    [Header("左")]
    public Text txtinfo;
    public RawImage imgChar;
    public Vector3 startPos;
    public Vector3 dragPos;

    [Header("右")]
    public Image imgExpprg;
    public Image imgPowerprg;
    public Text txtExpprg;
    public Text txtPowerprg;
    //
    public Text chardes;
    public Text txtFight;
    public Text txthp;
    public Text txthurt;
    public Text txtdef;
    public Button btnDetil;
    public Button btnClose;
    //


    [Header("详细信息")]
    public Transform detailWndTrans;
    public Button btnDetailClose;
    public Text dtxthp;
    public Text dtxtad;
    public Text dtxtap;
    public Text dtxtaddef;
    public Text dtxtapdef;
    public Text dtxtdodge;
    public Text dtxtpierce;
    public Text dtxtcritical;

    protected override void InitWnd()
    {
        base.InitWnd();
        RegTouchEvts();
        btnClose.onClick.AddListener(ClickBtnClose);
        btnDetailClose.onClick.AddListener(ClickBtnDetailClose);
        btnDetil.onClick.AddListener(ClickBtnDetail);
        RefreshUI();
    }

    private void RefreshUI()
    {
        PlayerData pd = GameRoot.Instance.PlayerData;
        SetText( txtinfo,pd.name+" LV:"+pd.lv);
        SetText( txtExpprg,pd.exp+"/"+PECommon.GetExpUpValByLV(pd.lv));
        SetText( txtPowerprg,pd.power+"/"+PECommon.GetPowerLimit(pd.lv));
        SetText( chardes,"暗影刺客");
        SetText( txtFight,PECommon.GetFightByProps(pd));
        SetText( txthp,pd.hp);
        SetText( txthurt,pd.ad+pd.ap);
        SetText( txtdef,pd.addef+pd.apdef);

        //
       imgExpprg.fillAmount=1.0f*pd.exp/PECommon.GetExpUpValByLV(pd.lv);
       imgPowerprg.fillAmount=1.0f*pd.power/PECommon.GetPowerLimit(pd.lv);
        //

        RefreshDetailUI(pd);


    }

    /// <summary>
    /// 详细属性
    /// </summary>
    /// <param name="pd"></param>

    private void RefreshDetailUI(PlayerData pd)
    {
        SetActive(detailWndTrans, false);
        SetText(dtxthp, pd.hp);
        SetText(dtxtad, pd.ad);
        SetText(dtxtap, pd.ap);
        SetText(dtxtaddef, pd.addef);
        SetText(dtxtapdef, pd.apdef);
        SetText(dtxtdodge, pd.dodge.ToString("0.0")+"%");
        SetText(dtxtpierce,  pd.pierce .ToString("0.0") + "%");
        SetText(dtxtcritical,  pd.critical.ToString("0.0") + "%");
    }

        public void ClickBtnClose()
    {
        if (audioSvc != null)//不这样报空指针，虽然不影响运行
        { 
          audioSvc.PlayUIAudio(Constants.UIClickBtn);
        }
        MainCitySys.Instance.CloseInfoWnd();
    }

    public void ClickBtnDetail()
    { 
    detailWndTrans.gameObject.SetActive(true);
    }
    public void ClickBtnDetailClose()
    {
        detailWndTrans.gameObject.SetActive(false);
    }

    void RegTouchEvts()
    {
        OnClickDown(imgChar.gameObject, (PointerEventData evt) =>
        {
            startPos = evt.position;
            MainCitySys.Instance.InitPlayerRotate();
        });


        OnDrag(imgChar.gameObject, (PointerEventData evt) =>
        {
            float speed = 0.5f;
            float rotate = -(evt.position.x - startPos.x)*speed;
            MainCitySys.Instance.SetPlayerRotate( rotate);
        });
    }
}