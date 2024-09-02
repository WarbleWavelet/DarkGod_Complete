/****************************************************
    文件：GuideWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/12 23:9:36
	功能：引导对话UI
*****************************************************/

using PEProtocol;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GuideWnd : WindowRoot
{

    [Header("对话")]
    public Text txtName;
    public Text txtTalk;
    public Image imgIcon;

    public AutoGuideCfg agc;
    public string[] dialogArr;
    public int dialogIdx = 1;
    public PlayerData pd;
    public Button btnNext;
    protected override void InitWnd()
    {
        base.InitWnd();
        agc = MainCitySys.Instance.GetCurTaskData();
        pd = GameRoot.Instance.PlayerData;
        //
        dialogArr=agc.dilogArr.Split('#');
        ParseDialog();
    
        btnNext.onClick.AddListener(ClickBtnNext); 
    }

    private void ClickBtnNext()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);  
        if (dialogIdx >= dialogArr.Length)
        {
            dialogIdx = 1;
            dialogArr = null;
            btnNext.onClick.RemoveAllListeners();
           
            //
            GameMsg msg = new GameMsg
            {
                cmd=(int)CMD.ReqGuide,
                reqGuide = new ReqGuide
                {
                    guideid = agc.ID
                }
            };
            netSvc.SendMsg(msg);
            SetWndState(false);//会影响netSvc
            //
            return;
        }
        ParseDialog();  
        dialogIdx++;
    }

    void ParseDialog()
    {

        string[] talkArr=dialogArr[dialogIdx].Split('|');


        if (int.Parse(talkArr[0]) == 0)
        {

            SetText(txtName, pd.name);
            SetSprite(imgIcon, PathDefine.SelfIcon);
        }
        else
        {
            switch (agc.npcID)
            {
                case 0:
                    {
                        SetText(txtName, "智者");
                        SetSprite(imgIcon, PathDefine.WiseManIcon);
                    }
                    break;
                case 1:
                    {
                        SetText(txtName, "将军");
                        SetSprite(imgIcon, PathDefine.GeneralIcon);
                    }
                    break;
                case 2:
                    {
                        SetText(txtName, "工匠");
                        SetSprite(imgIcon, PathDefine.ArtisanIcon);
                    }
                    break;
                case 3:
                    {
                        SetText(txtName, "商人");
                        SetSprite(imgIcon, PathDefine.TraderIcon);
                    }
                    break;

                default:
                    {
                        SetText(txtName, "大鱼");
                        SetSprite(imgIcon, PathDefine.GuideIcon);
                    }
                    break;
            }


        }

        imgIcon.SetNativeSize();
        SetText(txtTalk, talkArr[1].Replace("$name",pd.name));

      


    }
}