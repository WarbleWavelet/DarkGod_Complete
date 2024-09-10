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
        lock (obj)//���߳���ʾΨһid��Ҫ��
        {
            id++;

            while (true)
            {
                //����int���ֵ
                if (id == int.MaxValue)
                {
                    id = 0;
                }
                //�Ƿ��ù��ˣ� 
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
        lock (obj)//���߳���ʾΨһid��Ҫ��
        {
            id++;

            while (true)
            {
                //����int���ֵ
                if (id == int.MaxValue)
                {
                    id = 0;
                }
                //�Ƿ��ù��ˣ� 
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
    /// ��λ����ɺ���<para />
    /// <param name="delay">��ֵ<para /></param>
    /// <param name="unit">delay��ʱ�䵥λ<para /></param>
    /// <returns>����true����Ϊ�����delay</returns>
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
            default: { throw new Exception("�쳣"); }
        }

        return delay;
    }




}
