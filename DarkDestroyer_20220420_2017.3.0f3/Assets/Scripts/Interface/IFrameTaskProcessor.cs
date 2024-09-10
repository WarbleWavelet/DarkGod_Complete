/****************************************************
    文件：IFrameTaskProcessor.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/9 22:43:41
	功能：
*****************************************************/

using System;
using UnityEngine;

public interface IFrameTaskProcessor
{
    int AddFrameTask(Action<int> callback, int delay, int count = 1);
    void DeleteFrameTask(int tid);
    bool ReplaceFrameTask(int tid, Action<int> callback, int delay, int count = 1);
}
