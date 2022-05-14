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
    public Action<PointerEventData> onClick;
    public Action<PointerEventData> onClickUp;
    public Action<PointerEventData> onDrag;

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        { 
            onDrag(eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClickDown != null)
        {
            onClickDown(eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onClick != null)
        { 
            onClick( eventData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onClickUp != null)
        {
            onClickUp(eventData);
        }
    }
}
