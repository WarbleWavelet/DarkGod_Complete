/****************************************************
    文件：InstanceWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/27 0:44:51
	功能：副本UI
*****************************************************/

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
    protected override void InitWnd()
    {
        base.InitWnd();
        if (isFirst)
        {
            InitWndOnce();
            isFirst = false;
        }
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
}