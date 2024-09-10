using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class TimerSysUtil 
{
    // Start is called before the first frame update
  static  void Start()
    {
        {//Mono 
            Time8FrameTaskProcrssorMgr timerSys;        
        }

        //
        Time8FrameTaskProcrssor pETimer;
        TimeTask pETimeTask;
    }
    public static int GenerateId(this List<FrameTask> taskList,  int id, string obj)
    {
        lock (obj)//多线程显示唯一id就要锁
        {
            id++;

            while (true)
            {
                //超出int最大值
                if (id == int.MaxValue)
                {
                    id = 0;
                }
                //是否用过了， 
                bool isUsed = false;
                for (int i = 0; i < taskList.Count; i++)
                {
                    if (id == taskList[i].id)
                    {
                        isUsed = true;
                        break;
                    }
                }
                if (isUsed) id++;
                else break;
            }
        }

        return id;
    }

    public static int GenerateId(this List<TimeTask> taskList, int id, string obj)
    {
        lock (obj)//多线程显示唯一id就要锁
        {
            id++;

            while (true)
            {
                //超出int最大值
                if (id == int.MaxValue)
                {
                    id = 0;
                }
                //是否用过了， 
                bool isUsed = false;
                for (int i = 0; i < taskList.Count; i++)
                {
                    if (id == taskList[i].id)
                    {
                        isUsed = true;
                        break;
                    }
                }
                if (isUsed) id++;
                else break;
            }
        }

        return id;
    }

    /// <summary>
    /// 单位换算成毫秒<para />
    /// <param name="delay">数值<para /></param>
    /// <param name="unit">delay的时间单位<para /></param>
    /// <returns>返回true换算为毫秒的delay</returns>
    /// </summary>  
   public static double UnitConversion(this double delay, PETimeUnit unit = PETimeUnit.MillSecond)//
    {
        switch (unit)
        {
            case PETimeUnit.MillSecond: break;
            case PETimeUnit.Second: delay = delay * 1000f; break;
            case PETimeUnit.Minute: delay = delay * 1000f * 60f; break;
            case PETimeUnit.Hour: delay = delay * 1000f * 60f * 60f; break;
            case PETimeUnit.Day: delay = delay * 1000f * 60f * 60f * 24f; break;
            default: { throw new Exception("异常"); }
        }

        return delay;
    }




}
