/****************************************************
    文件：ExtendAsync.Thread.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/22 19:19:31
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public static partial class ExtendThread
{
    //线程的基本概念、创建和启动线程的方法、
    //线程的执行状态和生命周期、
    //线程之间的通信机制，
    //线程安全的编程实践。
    //
    //线程是操作系统能够进行运算调度的最小单位，被包含在进程之中，是进程中的实际运作单位
    //线程有时被称为轻量级进程（Lightweight Process），是进程的一个实体，是CPU调度和分派的基本单位，
    //它是比进程更小的能独立运行的基本单位，
    //线程自己不拥有系统资源，只拥有一点在运行中必不可少的资源（如程序计数器、一组寄存器和栈）
    //可以与同属一个进程的其他线程共享进程所拥有的全部资源。
    //
    //新建（New）→ 创建（Created）
    //创建（Created）→ 启动（Started）
    //启动（Started）→ 运行（Running）
    //运行（Running）→ 阻塞（Blocked）/ 等待（Waiting）/ 睡眠（Sleeping）   Thread.Sleep(1000);
    //阻塞（Blocked）/ 等待（Waiting）/ 睡眠（Sleeping）→ 运行（Running）
    //运行（Running）→ 终止（Terminated）
    //t.Resume();
    //        thread.Abort();
    //thread.Interrupt();
 
      
}
public static partial class ExtendThread 
{

    public static void Test(this  Transform t)
    {
        List<Text> texts = t.GetComponentsInChildren<Text>().ToList();
        List<Thread> threads = new List<Thread>();
        for (int i = 0; i < texts.Count; i++)
        {
            Thread thread = _newThread(texts[i],null);
            threads.Add(thread);
        }
      
    }
    public static int CurThreadID()
    {
       return Thread.CurrentThread.ManagedThreadId;
    }

    public static Thread NewThread()
    {
        ThreadStart ts = new ThreadStart(() =>
        {

            Debug.Log("Thread.New()" + Thread.CurrentThread.ManagedThreadId);
        });
        Thread t = new Thread(ts);
        return t;

    }

    public static Thread Strat(this Thread t)
    {
        t.Start();


        return t;
    }



        public static void Kill(this Thread thread)
    {
        thread.Abort();
        thread.Interrupt();

    }


    #region pri
    /// <summary>
    /// 大概210,220开头
    /// </summary>
    static Thread _newThread(Text text, Action cb = null)
    {
        ThreadStart ts = new ThreadStart(() =>
        {
            int i = 0;
            text.text = Thread.CurrentThread.ManagedThreadId.ToString() + "\t" + (i++);
        });
        Thread t = new Thread(ts);

        t.Start();
          return t;
    }


         static Thread AddThread( Action cb = null)
        {
            ThreadStart ts = new ThreadStart(() =>
            {

                Debug.Log(Thread.CurrentThread.ManagedThreadId); ;
            });
            Thread t = new Thread(ts);

            t.Start();

            return t;
        }
    #endregion


      

}



