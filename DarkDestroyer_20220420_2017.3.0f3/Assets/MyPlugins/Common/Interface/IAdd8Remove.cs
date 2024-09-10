/****************************************************
    文件：IAdd8Remove.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/15 19:7:1
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 用处1:PureMVC中:Proxy管理子节点Model常用
/// 
/// </summary>
public interface IAdd8Remove<T> where T : class  //,new那么需要一个无参构造
{
    void Add(T t);
    void Remove(T t);
}



