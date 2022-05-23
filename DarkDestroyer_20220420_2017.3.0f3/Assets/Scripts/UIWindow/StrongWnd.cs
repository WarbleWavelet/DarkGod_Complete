/****************************************************
    文件：EnchanceWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/14 17:33:53
	功能：装备强化
*****************************************************/

using PEProtocol;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class StrongWnd : WindowRoot
{

    #region 属性
   [Header("EnchanceWnd左")]
    public Transform posBtnTrans;

    [Header("EnchanceWnd右")]
    public Image imgEquip;
    public Button btnClose;

    Image[] bgArr;
    PlayerData pd;

    [Header("星级")]
    public Text txtStar;
    public int maxStar = 10;
    public Transform starGrp;
    public Transform[] starArr;

    [Header("属性加成")]
    public Text txtHp;
    public Text txtAddHp;
    public Text txtHurt;
    public Text txtAddHurt;
    public Text txtDef;
    public Text txtAddDef;
    public Transform arror1;
    public Transform arror2;
    public Transform arror3;

    [Header("消耗")]
    public Transform costTransRoot;
    public Text txtNeedLv;
    public Text txtCostCoin;
    public Text txtCostCrystal;
    public Text txtCoin;
    public int curPosIdx = 0;
    public int curStarLv = 0;
    public int nextStarLv = 1;
    public Button btnStrong;
    #endregion
 


    protected override void InitWnd()
    {
        base.InitWnd();
        //
        btnClose.onClick.AddListener(CloseEnchanceWnd);
        btnStrong.onClick.AddListener(ClickStrongBtn);

        //
        starArr = new Transform[maxStar];
        for (int i = 0; i < maxStar; i++)
        {
            starArr[i] = starGrp.GetChild(i);
        }
        //
        pd = GameRoot.Instance.PlayerData;
        bgArr = new Image[posBtnTrans.childCount];
        RegClickEvts();
        RefreshItem(0);
    }



    public void RegClickEvts()
    {
        for (int i = 0; i < posBtnTrans.childCount; i++)
        {
            Transform item = posBtnTrans.GetChild(i);
            Transform itemBg = item.GetChild(0);
            Image img = itemBg.GetComponent<Image>();
            bgArr[i] = img;

            OnClick(img.gameObject, (object args) =>
            {
                audioSvc.PlayUIAudio(Constants.UIClickBtn);
                ClickPosItem((int)args);

            }, i);
        }
    }
    void ClickPosItem(int index)
    {

        curPosIdx = index;
        for (int i = 0; i < bgArr.Length; i++)
        {
            if (curPosIdx == i)
            {
                SelectPos(i);
            }
            else
            {
                NormalPos(i);
            }
        }
    }






    public void RefreshItem(int posIdx)
    {
        SetItemImgByPosIdx(posIdx);
        //
          curPosIdx=posIdx;
        pd = GameRoot.Instance.PlayerData;//刷新一下
        curStarLv = pd.strongArr[posIdx];
        SetText(txtStar, "星级 " + curStarLv.ToString());
        for (int i = 0; i < starGrp.childCount; i++)
        {
            Image img = starGrp.GetChild(i).GetComponent<Image>();
            if (i < curStarLv)
            {
                SetSprite(img, PathDefine.SpStarNull); ;
            }
            else
            {
                SetSprite(img, PathDefine.SpStarFull); ;
            }

        }
        //
        int hpSum = 0;
        int hurtSum = 0;
        int defSum = 0;
        int addHpSum = 0;
        int addHurtSum = 0;
        int addDefSum = 0;
        nextStarLv = curStarLv + 1;

       
        StrongCfg cfg = resSvc.GetStrongCfg((int)posIdx, curStarLv);
        StrongCfg nextCfg = resSvc.GetStrongCfg((int)posIdx, nextStarLv);
        string str2=nextCfg != null?"有":"无"  ;
        if (nextCfg != null)
        {
            SetActiveByStartLv(true);
            for (int i = 0; i < posBtnTrans.childCount; i++)
            {
                hpSum = resSvc.GetPropAddPreLv((PosType)i, nextStarLv, PropType.Hp);
                hurtSum = resSvc.GetPropAddPreLv((PosType)i, nextStarLv, PropType.Hurt);
                defSum = resSvc.GetPropAddPreLv((PosType)i, nextStarLv, PropType.Def);
                //
                addHpSum = nextCfg.addhp;
                addHurtSum = nextCfg.addhurt;
                addDefSum = nextCfg.adddef;
            }
            SetText(txtHp, "生命 +" + hpSum.ToString());
            SetText(txtHurt, "伤害 +" + hurtSum.ToString());
            SetText(txtDef, "防御 +" + defSum.ToString());
            SetText(txtAddHp, "+" + addHpSum.ToString());
            SetText(txtAddHurt, "+" + addHurtSum.ToString());
            SetText(txtAddDef, "+" + addDefSum.ToString());

            SetText(txtNeedLv, nextCfg.minlv);
            SetText(txtCoin, pd.coin);
            SetText(txtCostCoin,nextCfg.coin);
            SetText(txtCostCrystal, nextCfg.crystal + "/" + pd.crystal);
        }
        else
        {
            SetActiveByStartLv(false);
        }
    }



    /// <summary>
    /// 点击强化按钮（过滤不必要的请求）
    /// </summary>

    public void ClickStrongBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        if (curStarLv >= Constants.MaxStarLv)
        {
            GameRoot.AddTips("已达到最高星级");
            return;
        }

        StrongCfg cfg = resSvc.GetStrongCfg((int)curPosIdx, curStarLv);
        StrongCfg nextCfg = resSvc.GetStrongCfg((int)curPosIdx, nextStarLv);

        if (pd.lv < nextCfg.minlv)
        {
            GameRoot.AddTips("等级不足"); return;
        }
        else if (pd.crystal < nextCfg.crystal)
        {
            GameRoot.AddTips("水晶不足"); return;
        }

        else if (pd.coin < nextCfg.coin)
        {
            GameRoot.AddTips("金币不足"); return;
        }
        else
        {

            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqStrong,
                reqStrong = new ReqStrong
                {
                    pos = curPosIdx
                }
            };

            netSvc.SendMsg(msg);
        }
    }


    public void UpdateUI()
    {
        audioSvc.PlayUIAudio(Constants.FBItemEnter);
        RefreshItem(curPosIdx);
    }


    #region 辅助 
    void SelectPos(int index)
    {
        SetSprite(bgArr[index].GetComponent<Image>(), PathDefine.ItemArrorBG);
        bgArr[index].GetComponent<RectTransform>().localPosition = new Vector3(-3.3f, 0, 0);
        bgArr[index].GetComponent<RectTransform>().sizeDelta = new Vector2(308.3f, 100);

        RefreshItem(index);
    }

    void NormalPos(int index)
    {
        SetSprite(bgArr[index].GetComponent<Image>(), PathDefine.ItemPlatBG);
        bgArr[index].GetComponent<RectTransform>().localPosition = new Vector3(-17.05f, 0, 0);
        bgArr[index].GetComponent<RectTransform>().sizeDelta = new Vector2(280.9f, 100);
    }
    /// <summary>
    /// 图标
    /// </summary>
    /// <param name="posIdx"></param>
    private void SetItemImgByPosIdx(int posIdx)
    {
        if (resSvc == null)
            resSvc = ResSvc.Instance;
        SetSprite(imgEquip, PathDefine.ItemHead);

        switch (posIdx)
        {
            case 0:
                {
                    SetSprite(imgEquip, PathDefine.ItemHead);
                }
                break;
            case 1:
                {
                    SetSprite(imgEquip, PathDefine.ItemBody);
                }
                break;
            case 2:
                {
                    SetSprite(imgEquip, PathDefine.ItemWaist);
                }
                break;
            case 3:
                {
                    SetSprite(imgEquip, PathDefine.ItemHands);
                }
                break;
            case 4:
                {
                    SetSprite(imgEquip, PathDefine.ItemLeg);
                }
                break;
            case 5:
                {
                    SetSprite(imgEquip, PathDefine.ItemFeet);
                }
                break;
            default:break;
        }
    }


    /// <summary>
    /// 写这里不写在EnchanceWnd是因为关闭时EnchanceWnd所在的节点也失效了，所以卸载不会物体失效的MainCitySys
    /// </summary>
    public void CloseEnchanceWnd()
    {
        MainCitySys.Instance.CloseStrongWnd();

    }


    void SetActiveByStartLv(bool status)
    {
        SetActive(arror1, status);
        SetActive(arror2, status);
        SetActive(arror3, status);
        SetActive(txtAddHp, status);
        SetActive(txtAddHurt, status);
        SetActive(txtAddDef, status);
        SetActive(costTransRoot, status);
    }
    #endregion


}