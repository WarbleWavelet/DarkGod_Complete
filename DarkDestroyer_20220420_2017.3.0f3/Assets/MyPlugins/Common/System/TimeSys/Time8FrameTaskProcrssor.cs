using System;
using System.Collections.Generic;


#region ITimer
public interface ITime8FrameTaskProcrssor
{
    int AddFrameTask(Action callback, int delay, int count = 1);

    /// <summary> ɾ����ʱ����<para /></summary>
    bool DeleteFrameTask(int id);
    /// <summary> �滻��ʱ����<para /></summary>
    bool ReplaceFrameTask(int id, Action callback, int delay, int count = 1);
    //
    int AddTimeTask(Action callback, double delay, PETimeUnit unit = PETimeUnit.MillSecond, int count = 1);
    /// <summary> ɾ����ʱ����<para /></summary>
     bool DeleteTimeTask(int id);
    /// <summary>�滻��ʱ����<para /></summary>
    bool ReplaceTimeTask(int id, Action callback, double delay, PETimeUnit unit = PETimeUnit.MillSecond, int count = 1);


}

#endregion
/// <summary>����PETimeTask��PEFrameTask</summary>
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
    //�������C���԰汾��Unix��32λ��int���������68.1�ꡣ������������ѡ1970
    public DateTime startDateTime = new DateTime ( 1970, 1, 1, 0, 0, 0,0 );

    /// <summary> ���챨�մ��� <para /> </summary>
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

    #region ʱ�� �� ɾ ��
    public int AddTimeTask(Action callback, double delay, PETimeUnit unit = PETimeUnit.MillSecond, int count = 1)//Ĭ�Ϻ���
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

        //����������֮һ
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

    #region ֡ �� ɾ ��
    public int AddFrameTask(Action callback, int delay, int count = 1)//Ĭ�Ϻ���
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

        //����������֮һ
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


    ///<summary>��ȡ��ǰʱ��<para /></summary>
    public double GetUTCMillSeconds()//����ʱ����
    {
        /**
            DateTime.Now����ʱ�䣬�й�������
            DateTime.UtcNowʱ���׼ʱ��
            ʵ�ʷ�������׼ʱ�䣬����������ڽ��б���ƫ��
        **/
        TimeSpan timeSpan = DateTime.UtcNow - startDateTime;
        return timeSpan.TotalMilliseconds;
    
    }




    #endregion


    #region pri

    #region ʱ�� ���� ����
    /// <summary>
    /// ���ػ������ʱ�б�<para />
    /// </summary>
    void LoadTaskTmpList()
    {
        if (_taskTmpList.Count <= 0) return;//һֱ��ӡ���������return
        for (int i = 0; i < _taskTmpList.Count; i++)
        {
            _taskList.Add(_taskTmpList[i]);
        }
        _taskTmpList.Clear();
    }
    /// <summary>
    /// ִ�ж�ʱ����<para />
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
                    if (task.callback != null)//��û����ʶҪ���ǿ�
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
                    i--;//�Ƴ�List�Զ�����ȥ�����Ի���Ҫ��ԭ���� 
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
                        //����0==ѭ��
                    }
                    task.destTime += task.delay;
                }


            }
        }
    }
    #endregion

    #region ֡ ���� ����
    /// <summary> ���ػ������ʱ�б�<para /> </summary>
    void Frame_LoadTaskTmpList()
    {
        if (_frame_taskTmpList.Count <= 0) return;//һֱ��ӡ���������return
        for (int i = 0; i < _frame_taskTmpList.Count; i++)
        {
            _frame_taskList.Add(_frame_taskTmpList[i]);
        }
        _frame_taskTmpList.Clear();
    }
    /// <summary>
    /// ִ�ж�ʱ����<para />
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
                    if (task.callback != null)//��û����ʶҪ���ǿ�
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
                    i--;//�Ƴ�List�Զ�����ȥ�����Ի���Ҫ��ԭ���� 
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
                        //����0==ѭ��
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
