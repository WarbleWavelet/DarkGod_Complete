/****************************************************
    文件：Test_Sr.cs
	作者：lenovo
    邮箱: 
    日期：2024/6/20 21:13:39
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

    public class Test_Sr : MonoBehaviour
    {
    #region 属性
    SpriteRenderer _sr;

    [SerializeField] SortingLayer[] _sortingLayers;
    [SerializeField] LayerMask _layerMask;
        #endregion

        #region 生命

        /// <summary>首次载入</summary>
        void Awake()
        {
            
        }
        

        /// <summary>Go激活</summary>
        void OnEnable ()
        {
            
        }

    /// <summary>首次载入且Go激活</summary>
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
          
        _layerMask = UnityEngine.LayerMask.GetMask("XXX");
    int layer=    UnityEngine.LayerMask.NameToLayer("XXX");//-1没找到
     string name=   UnityEngine.LayerMask.LayerToName(1);  //ELayer


        Debug.Log(_layerMask+","+ layer+","+name);
    }
         /// <summary>固定更新</summary>
        void FixedUpdate()
        {
            
        }

        void Update()
        {
            
        }

         /// <summary>延迟更新。适用于跟随逻辑</summary>
        void LateUpdae()
        {
            
        }

        /// <summary> 组件重设为默认值时（只用于编辑状态）</summary>
        void Reset()
        {
            
        }
      

        /// <summary>当对象设置为不可用时</summary>
        void OnDisable()
        {
            
        }


        /// <summary>组件销毁时调用</summary>
        void OnDestroy()
        {
            
        }
        #endregion 

        #region 系统

        #endregion 

        #region 辅助

        #endregion

    }



