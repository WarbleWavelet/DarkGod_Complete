/****************************************************
    文件：WindowRoot.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/26 21:17:5
	功能：UI窗口基类
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

public class WindowRoot : MonoBehaviour 
{
    /// <summary>需要引用时</summary>
    public ResSvc resSvc;
    public AudioSvc audioSvc;

    /// <summary>
    /// 打开关闭窗口
    /// </summary>
    /// <param name="isActive"></param>
    public void SetWndState(bool isActive = true)
    { if ( gameObject.activeSelf != isActive )
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        if (isActive)
        {
            InitWnd();
        }
        else
        {
            ClearWnd();
        }
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    protected virtual void ClearWnd()
    {
        resSvc = null;
        audioSvc = null;
    }

    /// <summary>
    /// 初始化窗口
    /// </summary>
    protected virtual void InitWnd()
    {

        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }


    #region SetText
    protected void SetText(Text text,string content="")
    {
        text.text = content;
    
    }
    protected void SetText(Text text, int num = 0)
    {
        text.text = num.ToString();

    }
    protected void SetText(Transform trans, string content = "")
    {
        trans.GetComponent<Text>().text = content;

    }
    protected void SetText(Transform trans, int num = 0)
    {
        trans.GetComponent<Text>().text = num.ToString();

    }
    #endregion


    #region SetActive
    protected void SetActive(GameObject go,bool isActive=true)
    { 
        go.SetActive(isActive);
    }
    protected void SetActive(Transform trans, bool state = true)
    {
        trans.gameObject.SetActive(state);
    }
    protected void SetActive(RectTransform rect, bool state = true)
    {
        rect.gameObject.SetActive(state);
    }
    protected void SetActive(Text text, bool state = true)
    {
        text.gameObject.SetActive(state);
    }
    protected void SetActive(Image image, bool state = true)
    {
        image.gameObject.SetActive(state);
    }
    #endregion


}