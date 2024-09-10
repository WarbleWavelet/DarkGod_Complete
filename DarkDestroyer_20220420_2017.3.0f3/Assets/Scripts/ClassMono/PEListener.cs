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
	/// <summary>还没用到</summary>
	Action<PointerEventData> _onClickEnter;
	public Action<PointerEventData> OnClickDown;
	/// <summary>点击不需要太复杂的参数</summary>
	public Action<object> OnClick;
	public Action<PointerEventData> OnClickUp;
	public Action<PointerEventData> OnClickDrag;
	public object Args;



	#region 系统
	public void OnPointerDown(PointerEventData eventData)
	{
		if (OnClickDown != null)
		{
			OnClickDown( eventData);
		}
	}


	public void OnDrag(PointerEventData eventData)
	{
		if (OnClickDrag != null)
		{
            OnClickDrag(eventData);
		}
	}


	public void OnPointerUp(PointerEventData eventData)
	{
		if (OnClickUp != null)
		{
			OnClickUp(eventData);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (OnClick != null)
		{
			OnClick(Args);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (_onClickEnter != null)
		{
			_onClickEnter(eventData);
		}
	}
	#endregion  

}
