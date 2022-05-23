/****************************************************
	文件：TimerSvc.cs
	作者：WWS
	邮箱: 
	日期：2022/05/20 16:20   	
	功能：计时服务
*****************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class TimerSvc
{
    #region 内部类
    /// <summary>任务包</summary>
    public class TaskPack
    {
        /// <summary>线程id </summary>
        public int tid;
        /// <summary>回调</summary>
        public Action<int> cb;

        public TaskPack(int tid, Action<int> cb)
        {
            this.tid = tid;
            this.cb = cb;
        }
    }
    #endregion

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
    public Queue<TaskPack> tpQue = new Queue<TaskPack>();
    private static readonly string tpQueLock = "tpQueLock";

    public void Init()
    {
        //pt = new PETimer();
        pt = new PETimer(1000);//每隔1000ms执行一次
        //
        pt.SetHandle((Action<int> cb, int tid) => {
            if (cb != null)
            {
                lock (tpQueLock)
                {
                    tpQue.Enqueue(new TaskPack(tid, cb));
                }
            }

        });

        //
        pt.SetLog((string info) =>
        {
            PECommon.Log(info);
        });
        PECommon.Log("TimerSvc Inited");

    }


    public void Update()
    {
        //if (tpQue.Count > 0)//一帧拿一个
        while (tpQue.Count > 0)//一帧拿多个
        {
            TaskPack tp = null;
            lock (tpQueLock)
            {
                tp = tpQue.Dequeue();
            }

            if (tp != null)
            {
                tp.cb(tp.tid);
            }
        }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="cb">回调</param>
    /// <param name="delay">ms为标准单位</param>
    /// <param name="timeUnit"></param>
    /// <param name="count">循环次数，默认1,0是一直循环</param>
    /// <returns></returns>

    public int AddTimerTask(Action<int> cb, double delay, PETimeUnit timeUnit = PETimeUnit.Millisecond,int count=1)
    {
        return pt.AddTimeTask(cb, delay, timeUnit,count);
    }

    /// <summary>
    /// 当前时间
    /// </summary>
    /// <returns></returns>
    public long GetNowTime()
    {
        return (long)pt.GetMillisecondsTime();
    }
}

