/****************************************************
    文件：StaticAnimationEvent.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/29 20:51:50
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



/// <summary>
/// 动画上挂的方法名不用这种方式,难以链接理解
///  方法都是0引用,很容易失手删除掉
/// </summary>
public  class StaticAnimationEvent
{
    //用不了,转不成Action.这里需要一个吧一个类的方法转成字符串的方法.暂时没找到
    //但还是成开发员有链接提醒作用
    //不要静态化,会报错
    static XXXClass xXXClass;
    public  string HideMainBtn = ((Action)xXXClass.XXXMethod).Action2String();
    public  string HideMainBtn1 = (new Action(xXXClass.XXXMethod)).Action2String();


    public static void Test()
    {
        //Debug.Log(HideMainBtn);
        //Debug.Log(HideMainBtn1);
    }
    //
    class XXXClass
    {
        public void XXXMethod() {Debug.Log("XXXMethod打印");  }
    }
}



