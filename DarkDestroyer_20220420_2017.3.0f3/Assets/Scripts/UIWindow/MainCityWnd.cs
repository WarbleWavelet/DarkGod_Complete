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
using UnityEngine.UI;

public class MainCityWnd : WindowRoot
{
    public Image imgPowerPrg;
   // public Image imgExpPrg;
    public Text txtFight;
    public Text txtExpPrg;
    public Text txtPower;
    public Text txtLevel;
    public Text txtName;
    public Transform expPrgTrans;

    protected override void InitWnd()
    {
        base.InitWnd();

        RefreshUI();
       
    }

    private void RefreshUI()
    {
        PlayerData pd = GameRoot.Instance.PlayerData;
        SetText(txtFight, PECommon.GetFightByProps(pd));
        SetText(txtPower,"体力:"+pd.power+"/"+PECommon.GetPowerLimit(pd.lv));
        imgPowerPrg.fillAmount = (pd.power * 1.0f) / PECommon.GetPowerLimit(pd.lv);
        SetText(txtName, pd.name);
        SetText(txtLevel, pd.lv);

        AdaptExpPrg();
    }

    void AdaptExpPrg()
    {
        GridLayoutGroup grid= expPrgTrans.GetComponent<GridLayoutGroup>();
        float rate =1f* Constants.ScreenStandardHeight / Screen.height;
        float width = rate * Screen.width;
        float itemWidth = (width - 78 - 5.83f - 6.5f - 9 * 3.8f) / 10;
        grid.cellSize = new Vector2(itemWidth, 8.9f) ;

    }

    void Update()
    {
        RefreshUI();
    }
}