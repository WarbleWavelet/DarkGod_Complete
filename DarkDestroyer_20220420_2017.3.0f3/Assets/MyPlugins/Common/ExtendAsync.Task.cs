/****************************************************
    文件：ExtendAsync.Task.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/30 20:49:45
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static ExtendAsync;
using Random = UnityEngine.Random;
 
public static partial class ExtendTask
    {


    }

public static partial class ExtendTask //Task
{
    class TaskAsync : IDelay, IEvent, IWhenAll, IWhenAny, IWaitAll, IWaitAny, IOnCompleted
    {
        public Task _task;

        async void Delay()
        {
            //执行耗时的操作

            Debug.Log(Time.time);
            await Task.Delay(TimeSpan.FromSeconds(1D)); //模拟一个异步操作
            Debug.Log(Time.time);
        }

        public void OnCompleted(Action action)
        {
            _task.GetAwaiter().OnCompleted(action);
        }

        #region IWhenAll,IWhenAny
        async void WhenAll(IEnumerable<Task> tasks)
        {
             await Task.WhenAll(tasks);
        }

        async void WhenAny(IEnumerable<Task> tasks)
        {
            await Task.WhenAny(tasks);
        }
        #endregion

        #region 说明
        public static async Task<T2> CallFuncAsync<T1, T2>(T1 t, Func<T1, T2> func)
        {
            return func.Invoke(t);
        }
        public static async Task<string> GetStringAsync(int value)
        {
            return await Task.Run(() => "xpy0928");
        }

        public static async Task MainAsync()
        {
            //string value = await CallFuncAsync<int, string>(30, async (s) => await GetStringAsync(s));
            string value = await CallFuncAsync<int, string>(30, s => GetStringAsync(s).Result);
            // string value = await CallFuncAsync(30,  GetStringAsync);
        }

        static async Task<int> AsyncMethod()
        {
            return await Task.Run(() =>
            {
                return 1 + 1;
            });
        }
        public async Task<int> AsyncMethod1()
        {
            var task = Task.Run(() =>
            {
                return 1 + 1;
            });
            return task.Result;
        }

        #endregion


    }
}
public static partial class ExtendTask	 //Task
{
    class Example_A
    {
        public async void DoSomethingAsync()
        {

            string data = await GetDataAsync();

            Debug.Log(data);
        }

        async Task<string> GetDataAsync()
        {
            //执行耗时的操作
            await Task.Delay(1000);//模拟一个异步操作
            return "Data";
        }
    }

}
public static partial class ExtendTask
{
    public static async Task<T2> CallFuncAsync<T1, T2>(T1 t, Func<T1, T2> func)
    {
        return func.Invoke(t);
    }

    public static async Task<string> GetStringAsync(int value)
    {
        return await Task.Run(() => "xpy0928");
    }

    public static async Task MainAsync()
    {
        //string value = await CallFuncAsync<int, string>(30, async (s) => await GetStringAsync(s));
        string value = await CallFuncAsync<int, string>(30, s => GetStringAsync(s).Result);
        // string value = await CallFuncAsync(30,  GetStringAsync);
    }
}
public static partial class ExtendTask
{
    /** 
	await   等待 alwaysAwait? 只有这方法块有返回值才会有下一步
   在异步方法中不能用ref和out关键字，因为可能在异步完成时或者之后才会被设置达不到我们预期
	 */
    /** 
 
	是不是将方法用async关键字标识就是异步方法了呢？
	是不是没有await关键字的存在async就没有存在的意义了呢？
	用异步方法的条件是什么呢，为什么会有这个条件限制？
	只能调用.NET Framework内置的用await标识的Task，能否自定义实现呢？
	在lambda表达式中是否可以用async和await关键字来实现异步呢（即异步lambda表达式）？
	 */
    static async Task<int> AsyncMethod()
    {
        return await Task.Run(() =>
        {
            return 1 + 1;
        });
    }
    public static async Task<int> AsyncMethod1()
    {
        var task = Task.Run(() =>
        {
            return 1 + 1;
        });
        return task.Result;
    }
}



