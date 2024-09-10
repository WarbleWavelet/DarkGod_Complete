

using System;
//ʱ������������
public class TimeTask
{
    public Action callback;//Ҫ��ʱ������
    public double destTime;//��ʱ����Ϸʱ�����
    public int count;//ִ�д���
    public double delay;//��ʱ����
    public int id;//����

    public TimeTask(Action callback, double destTime, int count, double delay, int id)
    {
        this.id = id;
        this.callback = callback;
        this.destTime = destTime;
        this.count = count;
        this.delay = delay;
    }
}
//֡����������
public class FrameTask
{
    public Action callback;//Ҫ��ʱ������
    public int destFrame;//�ж�ʱ����ʱ��_frameCounter+delay
    public int count;//ִ�д���
    public int delay;//��ʱ��֡
    public int id;//����

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
public enum PETimeUnit//��λ
{
    /// <summary>1000ms=1s</summary>
    MillSecond,
    Second,
    Minute,
    Hour,
    Day
}
