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
      
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onClickDown != null)
        { 
            onClickDown( eventData);
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


public class NewBehaviourScript204 : MonoBehaviour,

    IInitializePotentialDragHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler
{
    /// <summary>初始一次</summary>
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        print("Init");
    }
    /// <summary>依赖于Drag</summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("Drag");
        RectTransform rect = GetComponent<RectTransform>();
        Vector3 pos = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, eventData.position, eventData.enterEventCamera, out pos);
        rect.position = pos;

    }
    /// <summary>依赖于Drag</summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        print("End");
    }
    public void OnDrop(PointerEventData eventData)
    {
        print("Drop");
    }
}

