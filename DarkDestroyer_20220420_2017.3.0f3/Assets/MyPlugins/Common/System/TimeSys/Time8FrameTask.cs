

using System;
//时间任务数据类
public class TimeTask
{
    public Action callback;//要定时的任务
    public double destTime;//延时到游戏时间结束
    public int count;//执行次数
    public double delay;//延时几秒
    public int id;//索引

    public TimeTask(Action callback, double destTime, int count, double delay, int id)
    {
        this.id = id;
        this.callback = callback;
        this.destTime = destTime;
        this.count = count;
        this.delay = delay;
    }
}
//帧任务数据类
public class FrameTask
{
    public Action callback;//要定时的任务
    public int destFrame;//有定时任务时，_frameCounter+delay
    public int count;//执行次数
    public int delay;//延时几帧
    public int id;//索引

    public FrameTask(Action callback, int destFrame, int count, int delay, int id)
    {
        this.id = id;
        this.callback = callback;
        this.destFrame = destFrame;
        this.count = count;
        this.delay = delay;
    }
}

/// <summary></summary>
public enum PETimeUnit//单位
{
    /// <summary>1000ms=1s</summary>
    MillSecond,
    Second,
    Minute,
    Hour,
    Day
}
