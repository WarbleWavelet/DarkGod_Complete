/****************************************************
    文件：EnchanceWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/14 17:33:53
	功能：装备强化
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnchanceWnd : WindowRoot 
{

    public Button btnClose;
    public Transform posBtnTrans;

    public int selectIdx = 0;

    protected override void InitWnd()
    {
        base.InitWnd();
        btnClose.onClick.AddListener(CloseEnchanceWnd);

        RegClickEvts();
    }

    public void RegClickEvts()
    {
        for (int i = 0; i < posBtnTrans.childCount; i++)
        {
            Transform item = posBtnTrans.GetChild(i);
            Transform itemBg = item.GetChild(0);
            Transform itemName = item.GetChild(2);
            if (i == 0)
            {
                SelectBg(itemBg);
                selectIdx = 0;
            }
            else
            {
                NormalBg(itemBg);
            }


            OnClick(itemBg.gameObject, (PointerEventData evt) =>
            {
                audioSvc.PlayUIAudio(Constants.UIClickBtn);
//
                Transform itemSelect =posBtnTrans.GetChild(selectIdx);
            //
            Transform itemBgSelect = itemSelect.GetChild(0);
                NormalBg(itemBgSelect);
                //
                SelectBg(itemBg);
                //
                selectIdx = item.GetSiblingIndex();
            });
        } 
    }

    /// <summary>
    /// 写这里不写在EnchanceWnd是因为关闭时EnchanceWnd所在的节点也失效了，所以卸载不会物体失效的MainCitySys
    /// </summary>
    public void CloseEnchanceWnd()
    {
       MainCitySys.Instance.CloseEnchanceWnd();
       
    }

    void SelectBg(Transform t)
    {
        t.GetComponent<Image>().sprite = resSvc.LoadSprite(PathDefine.PosItemSelected);
        t.GetComponent<RectTransform>().localPosition = new Vector3(-3.3f, 0, 0);
        t.GetComponent<RectTransform>().sizeDelta = new Vector2(308.3f, 100);
    }

    void NormalBg(Transform t)
    { 
        t.GetComponent<Image>().sprite = resSvc.LoadSprite(PathDefine.PosItemNormal);
        t.GetComponent<RectTransform>().localPosition = new Vector3(-17.05f, 0, 0);
        t.GetComponent<RectTransform>().sizeDelta = new Vector2(280.9f, 100);

    }
}