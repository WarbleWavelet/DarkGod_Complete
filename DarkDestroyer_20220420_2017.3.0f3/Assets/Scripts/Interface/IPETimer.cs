/****************************************************
    文件：IPETimer.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/9 22:43:17
	功能：
*****************************************************/

using System;
using UnityEngine;

public interface IPETimer
{
    void SetLog(Action<string> log);
    void SetHandle(Action<Action<int>, int> handle);
    void Update();
    void Reset();
    int GetYear();
    int GetMonth();
    int GetDay();
    int GetWeek();
    DateTime GetLocalDateTime();
    double GetMillisecondsTime();
    string GetLocalTimeStr();
}
