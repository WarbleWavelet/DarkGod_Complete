/****************************************************
    文件：MonoBehaviourUtil.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/3 21:37:46
	功能：
*****************************************************/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public static partial class ExtendBehaviour //
{ 

}
public static partial class ExtendBehaviour //Injure
{

    public static float Injure(this MonoBehaviour mono,float change,float cur,Action injureCb,Action deadCb)
    {
        float after = cur + change;
        if (after <= 0)
        { 
            cur = 0;
            deadCb.DoIfNotNull();
        }
        else
        {
            cur=after;
            injureCb.DoIfNotNull();
        }
        return cur;
    }
    public static int Injure(this MonoBehaviour mono, int change, int cur, Action injureCb, Action deadCb)
    {
        int after = cur + change;
        if (after <= 0)
        {
            cur = 0;
            deadCb.DoIfNotNull();
        }
        else
        {
            cur = after;
            injureCb.DoIfNotNull();
        }
        return cur;
    }
}
public static partial class ExtendBehaviour // Framer
{
    public static float Framer(this MonoBehaviour mono, float timing, float time, Action timeCb)
    {
        timing += Time.fixedDeltaTime;
        if (timing >= time)
        {
            timing = 0f;
            timeCb();
        }
        return timing;
    }


    public static float Framer(this MonoBehaviour mono, float timing, float time, Action timingCb, Action timeCb)
    {

        if (timing < time)
        {
            timing += Time.fixedDeltaTime;
            timingCb();
        }
        else
        {
            timing = 0f;
            timeCb();
        }
        return timing;
    }
}
public static partial class ExtendBehaviour //StepTimer超过步长才会加到timing上
{
    //
    /// <summary>
    /// 命名比较形象
    /// StepTimer这个名字有点小,步长可能超过一个
    /// </summary>
    public static void StepTimer(this MonoBehaviour mono,
     ref float steping,
     float step,
     ref float timing, Action timingCb,
     float time, Action timeCb)
    {
        mono.Timer01(ref steping,step,ref timing,timingCb,time,timeCb);
    }

    /// <summary >
    /// 一级计算步长,比如技能冷却,显示是按固定的0.1变化.
    /// 比如直接timing += Time.deltaTime;文本显示的timing会是不确定的
    /// <para/>
    /// ref this和(x,x)不支持的话,玩不了,4.6好像支持
    /// 跑过一次功能,可以的
    /// </summary>
    public static void Timer01(this MonoBehaviour mono, 
        ref float timing01,
        float time01,  
        ref float timing, Action timingCb,
        float time, Action timeCb)
    {
         timing01 += Time.deltaTime;
        if (timing01 > time01)
        {
            timing += time01;
            timing01 = 0f;
            timingCb.DoIfNotNull();
        }
        if (timing > time)
        {
            timing01 = 0f;
            timing = 0f;
            timeCb.DoIfNotNull();
        }

    }
}
public static partial class ExtendBehaviour //Timer 
{
    public static float TimerRef(this MonoBehaviour mono,ref float timing, float time, Action timeCb)
    {
        timing += Time.deltaTime;
        if (timing >= time)
        {
            timing = 0f;
            timeCb();
        }
        return timing;
    }

    public static float Timer(this MonoBehaviour mono, float timing, float time, Action timeCb)
    {
        timing += Time.deltaTime;
        if (timing >= time)
        {
            timing = 0f;
            timeCb();
        }
        return timing;
    }


    public static float Timer(this MonoBehaviour mono, float timing,float time,Action timingCb,Action timeCb )
    {
       
        if (timing < time)
        {  
            timing += Time.deltaTime;
            timingCb();
        }
        else
        { 
            timing = 0f;
            timeCb();
        }
        return timing;
    }
}
public static partial class ExtendBehaviour  //Delay
{
    #region Delay
    /// <summary> seconds 时间后执行 onFinished </summary>
    public static void Delay(this MonoBehaviour monoBehaviour, float seconds, Action onFinished)
    {
        monoBehaviour.StartCoroutine(DelayCoroutine(seconds, onFinished));
    }

    private static IEnumerator DelayCoroutine( float seconds, Action onFinished)
    {
        yield return new WaitForSeconds(seconds);
        onFinished();
    }


    /// <summary>假设这里的脚本是B，A来调用B时，action是应该在A还是B</summary>
    public static void Delay(this MonoBehaviour monoBehaviour, System.Action action, float seconds)
    {

        //  string actionName = action.ToString() ;//这种直接给类型System.Action
        string actionName = action.Method.ToString() ;  //Void XXX()
        actionName = actionName.Substring(5) ; //XXX()
        actionName = actionName.Replace("()","") ; //XXX

        //Debug.LogFormat("actionName=={0}", actionName);
        try
        {
             monoBehaviour.Invoke( actionName, seconds);
        }
        catch (Exception)   
        {
            throw new System.Exception("Delay异常");
        }
      
    }
    #endregion
}