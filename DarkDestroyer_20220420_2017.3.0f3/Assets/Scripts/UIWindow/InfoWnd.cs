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
using UnityEngine.UI;

public class InfoWnd : WindowRoot 
{

    public Text txtinfo;
    public RawImage charshow;
    //
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

    protected override void InitWnd()
    {
        base.InitWnd();
        btnClose.onClick.AddListener(ClickBtnClose);
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


}


    public void ClickBtnClose()
    {
        if (audioSvc != null)//不这样报空指针，虽然不影响运行
        { 
          audioSvc.PlayUIAudio(Constants.UIClickBtn);
        }
      
        this.SetWndState(false);
    }
}