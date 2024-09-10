/****************************************************
    文件：ExtendList.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/18 21:59:8
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendList //与ILIst的区别
{
    //List实现了Add Remove
    //
    //IList没实现了Add Remove   
    //接收不同类型的参数
}
public static partial class ExtendList //Clear null
{
    public static void Test_Clear(this List<object> lst)
    {
        lst.Clear();//地址还存在
        lst = null;//内存回收
    }
}
public static partial class ExtendList //Capitity,
{
    public static void NewCount(this List<object> lst)
    {
        lst=new List<object>(10);   //这里是容量,Count==0
    }
}
public static partial class ExtendList
{
    public static void Foreach_Example(this List<object> lst,Action action)
    {
       // lst.ForEach(obj => obj.action()); //如果是实现继承相同的方法
        lst.ForEach(obj=>action());
    }

    /// <summary>按_property（）排序该list</summary>
    public static List<object> Order(this List<object> lst, object _property)
    {
        lst = lst.OrderByDescending(orderPara => _property).ToList(); //按速度安排角色先后行动
        return lst;
        //举例sys.CurActAILst = sys.CurActAILst.OrderByDescending(p => p.actRate).ToList(); 
    }

    /// <summary>防止从小到大删除出现，new List<string>{...,"blue","blue",...}</summary>
    public static List<object> RemoveAll(this List<object> lst, object reomoveItem)
    {
        for (int i = lst.Count-1; i >0 ; i--)
        {
            if (lst[i].Equals(reomoveItem))
            { 
                lst.RemoveAt(i);
            }
        }
        return lst;
    }
}




