using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time8FrameTaskProcrssorMgr :MonoBehaviour
{
    public Time8FrameTaskProcrssor pt;

    public void Init()
    {
        pt=new Time8FrameTaskProcrssor();
        pt.Init();
        pt.SetLog((string log)=> 
        {
            print("初始化"+log);
        });
    }

    #region 定时任务 增 删 改
    public int AddTimeTask(Action callback, float delay, PETimeUnit unit = PETimeUnit.MillSecond, int count = 1)//默认毫秒
    {
        return pt.AddTimeTask(callback, delay, unit, count);
    }

    public bool DeleteTimeTask(int id)
    {
        return pt.DeleteTimeTask(id);
    }
    public bool ReplaceTimeTask(int id, Action callback, float delay, PETimeUnit unit = PETimeUnit.MillSecond, int count = 1)
    {
        return pt.ReplaceTimeTask(id,callback, delay, unit, count);
    }
    #endregion
    
    #region 帧 增 删 改
    public int AddFrameTask(Action callback, int delay, int count = 1)//默认毫秒
    {
        return pt.AddFrameTask(callback,delay,count);
    }
    public bool DeleteFrameTask(int id)
    {
        return pt.DeleteFrameTask(id);
    }
    public bool ReplaceFrameTask(int id, Action callback, int delay, int count = 1)
    {
        return pt.ReplaceFrameTask(id, callback, delay, count);
    }
    #endregion
   
    private void Update()
    {
        if (pt==null)
        {
            return;
        }
        pt.Update();
    }
}
