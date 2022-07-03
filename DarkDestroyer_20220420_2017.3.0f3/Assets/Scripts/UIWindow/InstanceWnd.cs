/****************************************************
    文件：InstanceWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/27 0:44:51
	功能：副本UI
*****************************************************/

using PEProtocol;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceWnd : WindowRoot 
{

    #region 单例
    private static InstanceWnd _instance;      

    public static InstanceWnd Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InstanceWnd();
            }
            return _instance;
        }
    }
    #endregion

    [Header("副本")]
    public PlayerData pd;
    /// <summary>显示看的</summary>
    public int instance=0;
    bool isFirst = true;
    protected override void InitWnd()
    {
        base.InitWnd();
        if (isFirst)
        {
            InitWndOnce();
            InitInstanceData();
            isFirst = false;
            
           
        }
        pd = GameRoot.Instance.PlayerData;
        
        RefreshUI();
    }


    [Header("TaskWnd")]

    public Button btnClose;

    void InitWndOnce()
    {
        btnClose.onClick.AddListener(ClickBtnClose);
        pointOffset = new Vector3(22, 106, 0);
    }




    private void ClickBtnClose()
    {
        SetWndState(false);
    }

    public Transform instanceRoot;
    public Transform pointTrans
;
    public Transform[] instanceTransArr;
    public Vector3 pointOffset;

    void InitInstanceData()
    {
        int childCount = instanceRoot.childCount;
        instanceTransArr = new Transform[childCount];
        for (int i = 0; i < childCount ; i++)
        {
            int index = i;//直接用iBug
            instanceTransArr[index] = instanceRoot.GetChild(index);
            instanceTransArr[index].gameObject.GetComponent<Button>().onClick.AddListener(
                () =>ClickInstanceItem(index)
            );
        }
    }


   /// <summary>
   /// 点击副本
   /// </summary>
   /// <param name="childIndex"></param>
    private void ClickInstanceItem(int childIndex)
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        this.instance = childIndex;
        int instance = ChildIndexToInstanceID(childIndex);
        int power=resSvc.GetMapCfg(instance).power;
        if (pd.power >= power)
        {
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqInstanceFight,
                reqInstanceFight = new ReqInstanceFight
                {
                    instance = instance,
                }
            };

            netSvc.SendMsg(msg);
        }
        else
        {
            GameRoot.AddTips(ErrorCode.LackPower.ToDes());
        }
    }

    void RefreshUI()
    {
        
        instance = InstanceIDToChildIndex(pd.instance);
        if (instance > instanceTransArr.Length) return;
        for (int i = 0; i < instanceTransArr.Length; i++)
        {

            if (i <= instance)
            {
                SetActive(instanceTransArr[i], true);
                if (i == instance)
                {
                    pointTrans.GetComponent<RectTransform>().localPosition = instanceTransArr[i].GetComponent<RectTransform>().localPosition + pointOffset;
                }
            }
            else if (i > instance)
            {
                SetActive(instanceTransArr[i], false);
            }

        }
    }

    #region 数据转换
    //0,10001
    //6,10007
    int InstanceIDToChildIndex(int itemID)
    {
        return itemID % 1000 - 1;
    }

    int ChildIndexToInstanceID(int childIndex)
    { 
        return childIndex +10001;
    }


    #endregion



}