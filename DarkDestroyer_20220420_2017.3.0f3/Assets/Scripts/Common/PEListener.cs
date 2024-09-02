/****************************************************
    文件：PEListener.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/7 13:35:27
	功能：监听Touch
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PEListener : MonoBehaviour,
    IPointerEnterHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerClickHandler,
    IDragHandler
    
{

    public Action<PointerEventData> onClickDown;
    /// <summary>点击不需要太复杂的参数</summary>
    public Action<object> onClick;
    public Action<PointerEventData> onClickUp;
    public Action<PointerEventData> onDrag;

    public object args;



    public void OnPointerDown(PointerEventData eventData)
    {
        if (onClickDown != null)
        {
            onClickDown( eventData);
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onDrag(eventData);
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (onClickUp != null)
        {
            onClickUp(eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick(args);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }
}
