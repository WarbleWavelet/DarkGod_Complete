/****************************************************
	文件：ExtendAsync.cs
	作者：lenovo
	邮箱: 
	日期：2023/8/18 10:51:52
	功能：同类见 UniRx.cs
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
using System.IO;
using System.Threading;
using System.Text;


public static partial class ExtendAsync  //	ReadAllTextAsync 应该放在IO
{
	public static void Example_CharCountOfAllFiles(string path)
	{

		Debug.Log(path.ReadAllFileAsync().Result);
	}
	/// <summary>
	/// 统计所有文件的字符数
	/// </summary>
	public static async Task<int> ReadAllFileAsync(this string path )//= )
	{
		string[] files = System.IO.Directory.GetFiles(path);
		Task<int>[] countTasks = new Task<int>[files.Length];
		try
		{
			for (int i = 0; i < countTasks.Length; i++)
			{
				string filename = files[i];
				Task<int> t = filename.ReadAllTextLengthAsync();
				countTasks[i] = t;
			}
		}
		catch (Exception)
		{
			throw new  Exception("ReadAllTextAsync异常");    
		}


       // int[] counts = await _task.WhenAll(countTasks);  //取到所有结果返回
		//int c = counts.Sum();   //统计所有文件的字符数
		while (countTasks[0].Status!= TaskStatus.RanToCompletion)
		{ 
			 await Task.Delay(1000);
		}


        return countTasks[0].Result;
	}


	static async Task<int> ReadAllTextLengthAsync(this string filename)
	{
		//没能成功将版本.NET Standard	2.0改成支持 File.ReadAllTextAsync(filename).NET Standard	2.1
		//这需要unity2021,可选Standard	2.1
		//string a = await File.ReadAllTextAsync(filename);
		//
		//string s = File.ReadAllText(filename);
        string s = await filename.ReadAllTextAsync();
        return s.Length;
	}



	/// <summary>有卡主的bug</summary>
	private static async Task<string> ReadAllTextAsync(this string filePath)
	{
		byte[] buffer;
			using (FileStream fs = new FileStream(filePath,
			FileMode.Open, FileAccess.Read, FileShare.Read,
			bufferSize: 4096, 
			useAsync: true))
		{
            buffer = new byte[fs.Length];
			await fs.ReadAsync(buffer, 0, buffer.Length);
		}	
		return  Encoding.UTF8.GetString(buffer);
	}

}
public static partial class ExtendAsync  //各种Async比较
{
	//    协程缺点：
	//依赖monobehaviour
	//不能进行异常处理
	//方法返回值获取困难

	//c#原生Task：
	//优点：
	//不依赖monobehaviour
	//可以处理异常
	//缺点：
	//Task消耗大，设计跨线程操作
	//对单线程的WebGL(游戏试玩链接)支持不好,

	//uniTask
	//优点:
	//继承c#的task优点
	//基于值类型解决方案，0GC
	//默认使用主协程
	//大部分单线程
	//缺点:少部分API需要多线程 
	//NuGet安装失败;AssetsManager安装失败(包括git url);最终下载zip,把文件夹复制过来,添加程序集引用


}
public static partial class ExtendAsync//接口
{ 
#region 接口
//延时操作：Delay DelayFrame Yield NextFrame WaitForEndOfFrame
public interface IDelay {  }
public interface IDelayFrame { }
public interface INextFrame { }
public interface IWaitForEndOfFrameFrame { }



#region Wait When
//等待操作: Wait Until Wait Until Value Changed
/// <summary>Wait是同步的。</summary>
public interface IWait { }
public interface IWaitAll : IWait { }
public interface IWaitAny : IWait { }
//条件操作: When All When Any
/// <summary><When是异步的/summary>
public interface IWhen { }
/// <summary>所有task完成时，task才完成，用于等待多个任务执行结束</summary>
public interface IWhenAll:IWhen { }
public interface IWhenAny : IWhen { }
	#endregion


	#region 说明
	public interface IOnCompleted { void OnCompleted(Action action); }
	public interface IGetWaiter {  }
#endregion


//异步委托生成UniTask及相关的封装: UniTask.Void UniTask.Defer UniTask.Lazy
//取消：CancellationToken GetCancellationTokenOnDeatory()
//异常处理：Try Catch SuppressCancellationThrow
//超时处理：取消的变种，通过 CancellationTokenSouce.CancelAfterSlim(TimeSpan)设置超时并将CancellationToken 传递给异步方法
//Forget()
//事件处理：
//1.异步事件 Lamaba 表达式注册 使用 UniTask.Action 或 UniTask.UnityAction
public interface IEvent { }
//2.UGUI 事件转换为可等待事件
//AsAsyncEnumerable
//3.MonoBehaviour 消息事件都可以转换异步流
//异步Linq
//异步迭代器
//响应式组件

#endregion

}







