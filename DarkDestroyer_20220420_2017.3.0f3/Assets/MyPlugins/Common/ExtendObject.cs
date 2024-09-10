/****************************************************
    文件：ExtednObject.cs
	作者：lenovo
    邮箱: 
    日期：2023/9/12 14:20:56
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;


public static partial class ExtendObject //销毁
{
    /// <summary>
    /// 特点:异步,下一帧,统一销毁
    /// <br/>优点:不卡主线程</summary>
    public static void Destroy(Object o)
    { 
         UnityEngine.Object.Destroy(o);
    }

    /// <summary>
    /// 特点:
    /// <br/>当前帧销毁,
    /// <br/>会立即调用被删除对象的OnDestory（）方法
    /// <para/>缺点:卡顿主线程
    /// </summary>
    public static void DestroyImmediate(Object o)
    {
        UnityEngine.Object.DestroyImmediate(o);
    }
}
public static partial class ExtendObject
{

    #region 辅助
    /// <summary>
    /// 销毁后有些访问得到，有些属性访问不到
    /// 这里统一用 UnityEngine.Object转后来解决
    /// 当gameOjbect被destroy之后, test2 as UnityEngine.Object会返回null
    /// https://www.bilibili.com/video/BV1hp4y1L7dR/?spm_id_from=333.337.search-card.all.click&vd_source=54db9dcba32c4988ccd3eddc7070f140
    /// </summary>
    public static bool IsNullObject(this Object obj)
    {
        return (UnityEngine.Object)obj == null;
    }

    public static bool IsNotNullObject(this Object obj)
    {
        return !obj.IsNullObject();
    }
    #endregion

}




