/****************************************************
    文件：Extendobject.cs
	作者：lenovo
    邮箱: 
    日期：2023/8/28 22:17:16
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static partial class ExtendobjectCSharp 
{
    public static void Example()
    {
        { 
            //GameObject go=null;
            //go.NewIfNull();
        
        }
    }

    #region 辅助
    /// <summary>
    ///  未成功
    /// ==null就New
    /// <para />:IEnumerable返回的还是null
    /// </summary>
     static T NewIfNull<T,T1>(this T t , T1 key) where    T: IEnumerable
    {

        return t;
    }

    /// <summary>
    /// ==null就New
    /// <para />缺点 
    /// <para />01 t.NewIfNull()不行。需要 t=t.NewIfNull()
    /// <para />02 非结构，非值不能用ref
    /// </summary>
    public static T NewIfNull<T>( this T t) where T : new ()
    {
        if (t == null)
        {
            t = new T();
        }
        return t;
    }
    #endregion

}




