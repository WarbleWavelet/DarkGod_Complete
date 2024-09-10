/****************************************************
    文件：ExtendUnityAction.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/24 19:59:46
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public static partial class ExtendUnityAction
{
    public static void Example()
    {
        UnityAction action = () => { };
        UnityAction<int> actionWithInt = num => { };
        UnityAction<int, string> actionWithIntString = (num, str) => { };

        action.InvokeGracefully();
        actionWithInt.InvokeGracefully(1);
        actionWithIntString.InvokeGracefully(1, "str");
    }

    /// <summary>
    /// Call action
    /// </summary>
    /// <param name="selfAction"></param>
    /// <returns> call succeed</returns>
    public static bool InvokeGracefully(this UnityAction selfAction)
    {
        if (null != selfAction)
        {
            selfAction();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Call action
    /// </summary>
    /// <param name="selfAction"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool InvokeGracefully<T>(this UnityAction<T> selfAction, T t)
    {
        if (null != selfAction)
        {
            selfAction(t);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Call action
    /// </summary>
    /// <param name="selfAction"></param>
    /// <returns> call succeed</returns>
    public static bool InvokeGracefully<T, K>(this UnityAction<T, K> selfAction, T t, K k)
    {
        if (null != selfAction)
        {
            selfAction(t, k);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 获得随机列表中元素
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="list">列表</param>
    /// <returns></returns>
    public static T GetRandomItem<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count - 1)];
    }


    /// <summary>
    /// 根据权值来获取索引
    /// </summary>
    /// <param name="powers"></param>
    /// <returns></returns>
    public static int GetRandomWithPower(this List<int> powers)
    {
        var sum = 0;
        foreach (var power in powers)
        {
            sum += power;
        }

        var randomNum = UnityEngine.Random.Range(0, sum);
        var currentSum = 0;
        for (var i = 0; i < powers.Count; i++)
        {
            var nextSum = currentSum + powers[i];
            if (randomNum >= currentSum && randomNum <= nextSum)
            {
                return i;
            }

            currentSum = nextSum;
        }

        Log.E("权值范围计算错误！");
        return -1;
    }

    /// <summary>
    /// 根据权值获取值，Key为值，Value为权值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="powersDict"></param>
    /// <returns></returns>
    public static T GetRandomWithPower<T>(this Dictionary<T, int> powersDict)
    {
        var keys = new List<T>();
        var values = new List<int>();

        foreach (var key in powersDict.Keys)
        {
            keys.Add(key);
            values.Add(powersDict[key]);
        }

        var finalKeyIndex = values.GetRandomWithPower();
        return keys[finalKeyIndex];
    }

}





