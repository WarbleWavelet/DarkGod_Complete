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
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Common.CharacterRelationship_Sinmple
{
    //为节点添加拖拽事件
    public class ClickEvent : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.dragging)
                return;
            _isSelected = !_isSelected;
            GetComponent<Image>().color = (_isSelected == true) ? Color.red : _unselectColor;
            GameMgr mgr = GameMgr.Instance;
            if (mgr.LineDrawing == false)
            {
                mgr.FromNode = GetComponent<ImageNode>();
                mgr.LineDrawing = true;
            }
            else
            {
                mgr.ToNode = GetComponent<ImageNode>();
                Vector3 fromPos = mgr.FromNode.transform.localPosition;
                Vector3 toPos = mgr.ToNode.transform.localPosition;
                DrawLineSystem.Instance.SetLine(mgr.FromNode.transform.localPosition, mgr.ToNode.transform.localPosition, transform.TopCanvas().transform, GameObject.FindObjectOfType<SceneCtrl>().LinePrefab);
                mgr.ToNode = null;
            }


        }

        #region 属性
        bool _isSelected = false;
        Color _unselectColor;
        #endregion

        #region 生命周期
        void Start()
        {
            _isSelected = false;
            _unselectColor = GetComponent<Image>().color;



        }

        void Update()
        {

        }
        #endregion

    }

}


