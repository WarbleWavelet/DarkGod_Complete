/****************************************************
    文件：ExtendQUEUE.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/17 20:54:2
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static class ExtendQueue
{
    public static Queue Add(this Queue queue, object obj)
    { 
        queue.Enqueue(obj);
        return queue;
    }

    public static Queue Remove(this Queue queue)
    {
        queue.Dequeue();
        return queue;
    }

    public static object Get(this Queue queue)
    {
       return queue.Peek();
    }

}




