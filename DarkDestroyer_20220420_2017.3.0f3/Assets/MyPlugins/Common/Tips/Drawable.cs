/****************************************************

	�ļ���
	���ߣ�WWS
	���ڣ�2023/07/18 07:06:36
	���ܣ�https://www.bilibili.com/video/BV11W4y1W7A6/?spm_id_from=333.788&vd_source=54db9dcba32c4988ccd3eddc7070f140

*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.UIElements;





/// <summary>��ҪUGUI</summary>
public class Drawable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler
{
    public bool IsDrag { get; private set; }
    private Vector2 offset = Vector2.zero;
    private Vector2 oldPosition;
    [SerializeField]
    private GameObject DragTarget;
    [SerializeField]
    private CanvasGroup canvasGroup;//interface���Թ���

    private void Awake()
    {
        DragTarget = gameObject;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvasGroup == null || canvasGroup.interactable == true)
        {
            offset = eventData.position - oldPosition;
            oldPosition = eventData.position;
            DragTarget.transform.position += new Vector3(offset.x, offset.y, 0);

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDrag = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        oldPosition = eventData.position;
    }
}
