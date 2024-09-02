/****************************************************
    文件：TimerSvc.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/20 15:10:53
	功能：计时服务
*****************************************************/

using System;
using UnityEngine;

public class TimerSvc : SystemRoot 
{

    #region 单例
    private static TimerSvc _instance;      

    public static TimerSvc Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TimerSvc();
            }
            return _instance;
        }
    }
    #endregion


    PETimer pt;
    public override void InitSys()
    {
        base.InitSys();
        _instance = this;
       pt=new PETimer();
     
        pt.SetLog((string info) =>
        {
            PECommon.Log(info);
        });
        PECommon.Log("TimerSvc Init");
    }

    void Update()
    {
        if (pt != null)
        { 
            pt.Update();
        }
       
    }

    public int AddTimerTask(Action<int> cb, double delay,PETimeUnit timeUnit=PETimeUnit.Millisecond)
    {
       return pt.AddTimeTask(cb,delay,timeUnit);
    }

    public double GetNowTime()
    {
        return pt.GetMillisecondsTime();
    }

    public void DelTask(int tid)
    {
        pt.DeleteTimeTask(tid);
    }


}