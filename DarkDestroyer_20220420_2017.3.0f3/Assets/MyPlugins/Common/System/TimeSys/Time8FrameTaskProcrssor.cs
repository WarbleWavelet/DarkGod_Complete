using System;
using System.Collections.Generic;


#region ITimer
public interface ITime8FrameTaskProcrssor
{
    int AddFrameTask(Action callback, int delay, int count = 1);

    /// <summary> 删除定时任务<para /></summary>
    bool DeleteFrameTask(int id);
    /// <summary> 替换定时任务<para /></summary>
    bool ReplaceFrameTask(int id, Action callback, int delay, int count = 1);
    //
    int AddTimeTask(Action callback, double delay, PETimeUnit unit = PETimeUnit.MillSecond, int count = 1);
    /// <summary> 删除定时任务<para /></summary>
     bool DeleteTimeTask(int id);
    /// <summary>替换定时任务<para /></summary>
    bool ReplaceTimeTask(int id, Action callback, double delay, PETimeUnit unit = PETimeUnit.MillSecond, int count = 1);


}

#endregion
/// <summary>管理PETimeTask和PEFrameTask</summary>
public class Time8FrameTaskProcrssor :ITime8FrameTaskProcrssor
{


    #region FPC
    private List<TimeTask> _taskList= new List<TimeTask>();
    private List<TimeTask> _taskTmpList = new List<TimeTask>(); 
    //
    private List<FrameTask> _frame_taskList=new List<FrameTask>();
    private List<FrameTask> _frame_taskTmpList=new List<FrameTask>();

     private static readonly string _lock = "lock";
     private int _id;

     public int frameCounter = 0;
    //
     private Action<string> log;
    //当年出现C语言版本的Unix，32位的int按秒计算是68.1年。两个因素所以选1970
    public DateTime startDateTime = new DateTime ( 1970, 1, 1, 0, 0, 0,0 );

    /// <summary> 构造报空错误 <para /> </summary>
    public Time8FrameTaskProcrssor()//
    {
        //_taskList.Clear();
        //_taskTmpList.Clear();

        //_frame_taskList.Clear();
        //_frame_taskTmpList.Clear();
    }
    #endregion



    #region Life
    public void Init()
    {
        _id = -1;
    }

    public void Update()
    {
        LoadTaskTmpList();
        RunTaskList();

        Frame_LoadTaskTmpList();
        Frame_RunTaskList();
    }
    #endregion




    #region pub

    #region 时间 增 删 改
    public int AddTimeTask(Action callback, double delay, PETimeUnit unit = PETimeUnit.MillSecond, int count = 1)//默认毫秒
    {

        delay.UnitConversion( unit);
        //
        _id = _taskTmpList.GenerateId(_id,_lock);
        TimeTask task = new TimeTask(callback, GetUTCMillSeconds()+delay, count, delay, _id);
        //
        _taskTmpList.Add(task);

        return _id;
    }


    public bool DeleteTimeTask(int id)
    {
        bool isExisted = false;

        for (int i = 0; i < _taskList.Count; i++)
        {
            if (id == _taskList[i].id)
            {
                isExisted = true;
                _taskList.RemoveAt(i);
                break;
            }
        }

        for (int i = 0; i < _taskTmpList.Count; i++)
        {
            if (id == _taskTmpList[i].id)
            {
                isExisted = true;
                _taskTmpList.RemoveAt(i);
                break;
            }
        }
        return isExisted;
    }


    public bool ReplaceTimeTask(int id, Action callback, double delay, PETimeUnit unit = PETimeUnit.MillSecond, int count = 1)
    {
        delay.UnitConversion( unit);
        //       
        TimeTask task = new TimeTask(callback, GetUTCMillSeconds()+delay, count, delay, id);
        //

        //必在两个表之一
        bool isReplaced = false;
        for (int i = 0; i < _taskList.Count; i++)
        {
            if (id == _taskList[i].id)
            {
                isReplaced = true;
                _taskList[i] = task;
                break;
            }
        }
        if (isReplaced == false)
        {
            for (int i = 0; i < _taskTmpList.Count; i++)
            {
                if (id == _taskTmpList[i].id)
                {
                    isReplaced = true;
                    _taskTmpList[i] = task;
                    break;
                }
            }
        }

        return isReplaced;
    }
    #endregion

    #region 帧 增 删 改
    public int AddFrameTask(Action callback, int delay, int count = 1)//默认毫秒
    {
        //

        _id = _frame_taskTmpList.GenerateId (_id,_lock);
        FrameTask task = new FrameTask(callback, frameCounter + delay, count, delay, _id);
        //
        _frame_taskTmpList.Add(task);

        return _id;
    }



    public bool DeleteFrameTask(int id)
    {
        bool isExisted = false;

        for (int i = 0; i < _frame_taskList.Count; i++)
        {
            if (id == _frame_taskList[i].id)
            {
                isExisted = true;
                _frame_taskList.RemoveAt(i);
                break;
            }
        }

        for (int i = 0; i < _frame_taskTmpList.Count; i++)
        {
            if (id == _taskTmpList[i].id)
            {
                isExisted = true;
                _frame_taskTmpList.RemoveAt(i);
                break;
            }
        }
        return isExisted;
    }

    public bool ReplaceFrameTask(int id, Action callback, int delay, int count = 1)
    {
        //       
        FrameTask task = new FrameTask(callback, frameCounter + delay, count, delay, id);
        //

        //必在两个表之一
        bool isReplaced = false;
        for (int i = 0; i < _frame_taskList.Count; i++)
        {
            if (id == _frame_taskList[i].id)
            {
                isReplaced = true;
                _frame_taskList[i] = task;
                break;
            }
        }
        if (isReplaced == false)
        {
            for (int i = 0; i < _frame_taskTmpList.Count; i++)
            {
                if (id == _frame_taskTmpList[i].id)
                {
                    isReplaced = true;
                    _frame_taskTmpList[i] = task;
                    break;
                }
            }
        }

        return isReplaced;
    }
    #endregion


    public void SetLog(Action<String> log)
    {
        if (log != null)
        {
            this.log = log;
        }     
    }


    ///<summary>获取当前时间<para /></summary>
    public double GetUTCMillSeconds()//计算时间间隔
    {
        /**
            DateTime.Now本地时间，中国东八区
            DateTime.UtcNow时间标准时间
            实际服务器标准时间，到具体国家在进行本地偏移
        **/
        TimeSpan timeSpan = DateTime.UtcNow - startDateTime;
        return timeSpan.TotalMilliseconds;
    
    }




    #endregion


    #region pri

    #region 时间 加载 运行
    /// <summary>
    /// 加载缓存的临时列表<para />
    /// </summary>
    void LoadTaskTmpList()
    {
        if (_taskTmpList.Count <= 0) return;//一直打印输出，所以return
        for (int i = 0; i < _taskTmpList.Count; i++)
        {
            _taskList.Add(_taskTmpList[i]);
        }
        _taskTmpList.Clear();
    }
    /// <summary>
    /// 执行定时任务<para />
    /// </summary>  
    void RunTaskList()
    {
        if (_taskList.Count <= 0) return;
        for (int i = 0; i < _taskList.Count; i++)
        {
            TimeTask task = _taskList[i];
            if ( GetUTCMillSeconds() < task.destTime)
            {
                continue;
            }
            else
            {
                try
                {
                    if (task.callback != null)//我没有意识要检查非空
                    {
                        task.callback();
                    }
                }
                catch (Exception e)
                {
                    Log(e.ToString());
                }


                if (task.count == 1)
                {
                    _taskList.Remove(task);
                    //idList.Remove(task._id);
                    i--;//移除List自动接上去，所以还需要从原索引 
                }
                else
                {
                    if (task.count != 0)
                    {
                        task.count--;
                        //idList.Remove(task._id);
                    }
                    else
                    {
                        //定义0==循环
                    }
                    task.destTime += task.delay;
                }


            }
        }
    }
    #endregion

    #region 帧 加载 运行
    /// <summary> 加载缓存的临时列表<para /> </summary>
    void Frame_LoadTaskTmpList()
    {
        if (_frame_taskTmpList.Count <= 0) return;//一直打印输出，所以return
        for (int i = 0; i < _frame_taskTmpList.Count; i++)
        {
            _frame_taskList.Add(_frame_taskTmpList[i]);
        }
        _frame_taskTmpList.Clear();
    }
    /// <summary>
    /// 执行定时任务<para />
    /// </summary>  
    void Frame_RunTaskList()
    {
        if (_frame_taskList.Count <= 0)
        {
            frameCounter = 0;
            return;
        }
        frameCounter++;
        for (int i = 0; i < _frame_taskList.Count; i++)
        {
            FrameTask task = _frame_taskList[i];
            if (frameCounter < task.destFrame)
            {
                continue;
            }
            else
            {
                try
                {
                    if (task.callback != null)//我没有意识要检查非空
                    {
                        task.callback();
                    }
                }
                catch (Exception e)
                {
                    Log(e.ToString());
                }


                if (task.count == 1)
                {
                    _frame_taskList.Remove(task);
                    i--;//移除List自动接上去，所以还需要从原索引 
                }
                else
                {
                    if (task.count != 0)
                    {
                        task.count--;
                        //idList.Remove(task._id);
                    }
                    else
                    {
                        //定义0==循环
                    }
                    task.destFrame += task.delay;
                }


            }
        }
    }
    #endregion
    private void Log(string log)
    {
        this.log(log);
    }


    #endregion


}
