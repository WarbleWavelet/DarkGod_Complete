/****************************************************
    文件：ITimeTaskProcessor.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/9 22:44:14
	功能：
*****************************************************/

using System;
using UnityEngine;

public interface ITimeTaskProcessor
{
    int AddTimeTask(Action<int> callback, double delay, PETimeUnit timeUnit = PETimeUnit.Millisecond, int count = 1);
    void DeleteTimeTask(int tid);
    bool ReplaceTimeTask(int tid, Action<int> callback, float delay, PETimeUnit timeUnit = PETimeUnit.Millisecond, int count = 1);

}
