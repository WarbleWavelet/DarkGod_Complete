/****************************************************
    文件：Trigger2DComponent.cs
    作者：lenovo
    邮箱: 
    日期：2024/4/6 19:37:56
    功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>提供一个Trigger,处理外部的事件</summary>
[RequireComponent(typeof(Collider2D)) ]       
public class Trigger2DComponent : MonoBehaviour
{
            
    public List<Action<Collider2D>> EnterLst = new List<Action<Collider2D>>();
    public List<Action<Collider2D>> ExitLst = new List<Action<Collider2D>>();
    public List<Action<Collider2D>> StayLst = new List<Action<Collider2D>>();

    /// <summary>火箭就不用rigidbody2D</summary>
    public Trigger2DComponent InitComponent(Rigidbody2D rigidbody2D=null)
    {
        if (rigidbody2D != null)
        { 
            rigidbody2D.gravityScale = 0; //保证是Trigger

        }
        return this;
    }


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        EnterLst.ForeachAction(otherCollider);
    }

    private void OnDisable()
    {
        //没必要回收 =null
        EnterLst.Clear();
        ExitLst.Clear();
        StayLst.Clear();
    }
}



                              