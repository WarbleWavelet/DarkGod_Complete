/****************************************************
    文件：InstanceWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/27 0:44:51
	功能：副本UI
*****************************************************/

using PEProtocol;
using System;
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
    public bool isFirst = true;
    public PlayerData pd;
    public int instance=0;
    protected override void InitWnd()
    {
        base.InitWnd();
        if (isFirst)
        {
            InitWndOnce();
            isFirst = false;
            pointOffset = new Vector3(22, 106, 0);
            InitInstanceData();
        }
        pd = GameRoot.Instance.PlayerData;
      
        RefreshUI();
    }


    [Header("TaskWnd")]

    public Button btnClose;

    void InitWndOnce()
    {
        btnClose.onClick.AddListener(ClickBtnClose);
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
            int index = i;
            instanceTransArr[index] = instanceRoot.GetChild(index);
            instanceTransArr[index].gameObject.GetComponent<Button>().onClick.AddListener(() =>ClickInstanceItem(index));
        }
    }

    private void ClickInstanceItem(int idx)
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        instance = idx;
    }

    void RefreshUI()
    {
         instance = pd.instance % 1000 - 1;
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
}