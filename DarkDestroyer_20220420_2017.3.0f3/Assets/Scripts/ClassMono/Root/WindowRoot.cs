/****************************************************
    文件：WindowRoot.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/26 21:17:5
	功能：UI窗口基类
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowRoot : MonoBehaviour 
{

    [Header("WindowRoot")]
    /// <summary>需要引用时</summary>

    [ReadOnly]public ResSvc resSvc;
    [ReadOnly]public AudioSvc audioSvc;
    [ReadOnly]public NetSvc netSvc;
    [ReadOnly]public TimerSvc timerSvc;

    #region Wnd

    public void OpenWnd()
    {
        SetWndState(true);
    }
    public void CloseWnd()
    {
        SetWndState(false);
    }

    /// <summary>
    /// 打开关闭窗口
    /// </summary>
    /// <param name="isActive"></param>
    public void SetWndState(bool isActive = true)
    { 
        if ( gameObject.activeSelf != isActive )
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
    public bool GetWndState()
    {
        return gameObject.activeSelf;
    }
    /// <summary>
    /// 关闭窗口
    /// </summary>
    protected virtual void ClearWnd()
    {
        //resSvc = null;
        //audioSvc = null;
        //netSvc = null;
        //timerSvc = null;
    }

    /// <summary>
    /// 初始化窗口(每次打开窗口都执行，注意按钮事件多次添加等)
    /// </summary>
    protected virtual void InitWnd()
    {
       
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
        netSvc = NetSvc.Instance;
        timerSvc = TimerSvc.Instance;
        timerSvc = TimerSvc.Instance;


    }
    #endregion




    protected Transform GetTrans(Transform t,string path)
    {
        if (t != null)
        {
           return t.Find(path);
        }
        else
        {
            return transform.Find(path);
        }
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


    #region SetSprite
    protected void SetSprite(Image image, string path)
    {
        image.sprite = resSvc.LoadSprite(path,true);

    }

    protected void SetSprite(Transform t, string path)
    {
        Image image = t.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = resSvc.LoadSprite(path, true);
        }


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
    protected void SetActive(Button button, bool state = true)
    {
        button.gameObject.SetActive(state);
    }

    protected void SetActive(Image image, bool state = true)
    {
        image.gameObject.SetActive(state);
    }
    #endregion


    #region OnClickDown OnClick OnClickUp OnDrag
    protected void OnClickDown(GameObject go,Action<PointerEventData> cb)
    {
        PEListener listener = go.GetOrAddComponent<PEListener>();
        listener.OnClickDown = cb;
    }

    protected void OnClick(GameObject go, Action<object> cb,object args)
    {
        PEListener listener = go.GetOrAddComponent<PEListener>();
        listener.OnClick = cb;
        listener.Args = args;
    }


    protected void OnClickUp(GameObject go, Action<PointerEventData> cb)
    {
        PEListener listener = go.GetOrAddComponent<PEListener>();
        listener.OnClickUp = cb;
    }
    protected void OnDrag(GameObject go, Action<PointerEventData> cb)
    {
        PEListener listener = go.GetOrAddComponent<PEListener>();
        listener.OnClickDrag = cb;
    }
    #endregion

}