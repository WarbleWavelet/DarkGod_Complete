/****************************************************
    文件：CharacterRelationship.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/1 21:45:19
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Common.CharacterRelationship_Sinmple
{
    //为节点添加拖拽事件
    public class DragEvent : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private RectTransform panelRectTransform;

        private void Awake()
        {
            panelRectTransform = transform as RectTransform;
        }
        public void OnDrag(PointerEventData eventData)
        {
            Vector3 pos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(panelRectTransform, eventData.position, eventData.enterEventCamera, out pos);
            transform.position = pos;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            GameMgr.Instance.FromNode = GetComponent<ImageNode>();

        }

        public void OnEndDrag(PointerEventData eventData)
        {
        }
    }

}


