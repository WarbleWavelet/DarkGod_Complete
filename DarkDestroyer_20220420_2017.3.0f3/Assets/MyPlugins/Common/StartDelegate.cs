/****************************************************
    文件：StartDelegate.cs
	作者：lenovo
    邮箱: 
    日期：2023/6/12 22:13:22
	功能：对delegate的入门
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

    public static partial class StartDelegate 
    {
        #region 属性

        #endregion


        #region 系统

        #endregion 

        #region 辅助

        #endregion

    }

//通过一个买票的例子     https://www.cnblogs.com/wudiwushen/archive/2010/04/20/1703368.html
//小明让小张两个请求（也就是委托），请求是一条链，可以不断加
public static partial class StartDelegate
{
    //小张类
    public class MrZhang
    {
        //其实买车票的悲情人物是小张
        public static void BuyTicket()
        {
            Console.WriteLine("NND,每次都让我去买票，鸡人呀！");
        }

        public static void BuyMovieTicket()
        {
            Console.WriteLine("我去，自己泡妞，还要让我带电影票！");
        }
    }

    //小明类
    class MrMing
    {
        //声明一个委托，其实就是个“命令”
        //EventHandler是推荐命名
        public delegate void BugTicketEventHandler();

        public static void Main(string[] args)
        {
            //这里就是具体阐述这个命令是干什么的，本例是MrZhang.BuyTicket“小张买车票”
            BugTicketEventHandler myDelegate = new BugTicketEventHandler(MrZhang.BuyTicket);

            myDelegate += MrZhang.BuyMovieTicket;
            //这时候委托被附上了具体的方法
            myDelegate();
            Console.ReadKey();
        }
    }

}



