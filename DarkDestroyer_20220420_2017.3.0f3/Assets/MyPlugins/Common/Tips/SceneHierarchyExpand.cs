/****************************************************
    文件：SceneHierarchyExpand.cs
	作者：lenovo
    邮箱: 
    日期：2024/4/7 21:24:36
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


#if UNITY_EDITOR
public class SceneHierarchyExpand : MonoBehaviour  
{

    [Header("要展开的子孙节点深度")]
    [SerializeField] private int childDeep = 2;

    [Header("一般不改")]
    [SerializeField] private int lastChildCount = 0;
    [SerializeField] private float timing = 0f;
    [SerializeField] private float time = 2f;


    #region 生命
    private void Awake()
    {
        childDeep = 2;
        timing = 0f;
        time = 2f;
        lastChildCount = transform.childCount;

    }


    private void Update()
    {
        timing = this.Timer(timing,time,() =>
        {    
             /**
            //没用,有的是孙节点
            // 检查子节点数量是否有变化
            if (lastChildCount != transform.childCount)
            {
                lastChildCount = transform.childCount;
                // 子节点数量变化时，调用事件
            }
             **/
                transform.Expend(childDeep);

        });

    }
    #endregion



    #region pri


    #endregion



}

#endif




