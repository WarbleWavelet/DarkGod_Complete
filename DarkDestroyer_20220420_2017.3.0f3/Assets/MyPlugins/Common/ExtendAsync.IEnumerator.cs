/****************************************************
    文件：ExtendAsync.IEnumetor.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/30 20:50:44
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExtendAsync;
using Random = UnityEngine.Random;
 

public static partial class ExtendIEnumerator 
{


}

public static partial class ExtendIEnumerator//IEnumerator
{
    class IEnumeratorAsync : IDelay
    {
        IEnumerator DelayFrame()
        {
            yield return new WaitForFixedUpdate();
        }
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1f);
        }
    }

}
public static partial class ExtendIEnumerator//IEnumerator
{

    public static void Example()
    {
        // StartCoroutine(/ 这里直接调用方法，添加参数 /)
        // 另一种是StartCoroutine(/ 这里填写”字符串的方法名字”，方法参数 /)
        // 但是第一种方法不能通过StopCoroutine(/ 这里填写”字符串的方法名”/)来结束协程，只能通过StopAllCoroutines来结束。
        // 后一种则可以通过StopCoroutine来结束对正在执行的协程的调用。
    }

    static IEnumerator CaculateResult()
    {
        for (int i = 0; i < 10000; i++)
        {
            //内部循环计算
            //在这里的yield会让改内部循环计算每帧执行一次，而不会等待10000次循环结束后再跳出
            yield return null;
        }
        //如果取消内部的yield操作，仅在for循环外边写yield操作，则会执行完10000次循环后再结束，相当于直接调用了一个函数，而非协程。
        //前面内部的10000加上现在的1次，10001次
        yield return null;
    }

}


public static partial class ExtendIEnumerator//IEnumerator 
{
    //https://blog.csdn.net/beihuanlihe130/article/details/76098844
    //枚举器。
    //协程，协助主线程，跟线程区分开
    //有较为耗时的操作时，可以将该操作分散到几帧或者几秒内完成，而不用在一帧内等这个操作完成后再执行其他操作。
    //
    //当某一个脚本中的协程在执行过程中，如果我们将该脚本的enable设置为false，协程不会停止。
    //只有将挂载该脚本的物体设置为SetActive(false)时才会停止。     
    //
    //Unity在调用StartCoroutine()后不会等待协程中的内容返回，会立即执行后续代码。
}
public static partial class ExtendIEnumerator	//yield
{

    //yield return null; // 下一帧再执行后续代码
    //yield return 0; //下一帧再执行后续代码
    //yield return 6;//(任意数字) 下一帧再执行后续代码
    //yield break; //直接结束该协程的后续操作
    //yield return asyncOperation;//等异步操作结束后再执行后续代码
    //yield return StartCoroution(/*某个协程*/);//等待某个协程执行完毕后再执行后续代码
    //yield return WWW();//等待WWW操作完成后再执行后续代码
    //yield return new WaitForEndOfFrame();//等待帧结束,等待直到所有的摄像机和GUI被渲染完成后，在该帧显示在屏幕之前执行
    //yield return new WaitForSeconds(0.3f);//等待0.3秒，一段指定的时间延迟之后继续执行，在所有的Update函数完成调用的那一帧之后（这里的时间会受到Time.timeScale的影响）;
    //yield return new WaitForSecondsRealtime(0.3f);//等待0.3秒，一段指定的时间延迟之后继续执行，在所有的Update函数完成调用的那一帧之后（这里的时间不受到Time.timeScale的影响）;
    //yield return WaitForFixedUpdate();//等待下一次FixedUpdate开始时再执行后续代码
    //yield return new WaitUntil()//将协同执行直到 当输入的参数（或者委托）为true的时候....如:yield return new WaitUntil(() => frame >= 10);
    //yield return new WaitWhile()//将协同执行直到 当输入的参数（或者委托）为false的时候.... 如:yield return new WaitWhile(() => frame < 10);

}


