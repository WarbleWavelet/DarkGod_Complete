/****************************************************
    文件：GuidePostEvent.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/8 19:40:7
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using static ExtendDesignPattern;

public class GuidePostEvent : MonoBehaviour, IPointerClickHandler
{
    //监听点击
    public void OnPointerClick(PointerEventData eventData)
    {
        //事件穿透
        PassEvent(eventData, ExecuteEvents.pointerClickHandler);
    }

    public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function)
      where T : IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        GameObject current = data.pointerCurrentRaycast.gameObject;
        for (int i = 0; i < results.Count; i++)
        {
            if (current != results[i].gameObject)
            {
                //在这里判断事件穿透检测到的对象是不是我们新手引导需要点击的对象
                GuideStep step = results[i].gameObject.GetComponent<GuideStep>();
                if (step != null)
                {
                    //再判断引导id是否是属于当前的引导id,毕竟我们一个面板上也许会出现很多
                    //引导对象
                    int currentStepId = GuideManager.Instance.GetStepID();
                    bool exists = Array.Exists<int>(step.StepID, element => element == currentStepId);
                    if (exists)
                        ExecuteEvents.Execute(results[i].gameObject, data, function);
                }
            }
        }
    }
}


public class GuideManager : SingletonLazyNotMono<GuideManager>
{
    static int _stepID=0;
    internal int GetStepID()
    {
        return _stepID;
    }


}
