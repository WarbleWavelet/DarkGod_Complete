/****************************************************
    文件：ExtendClass.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/24 20:9:6
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendClass
{
    public static void Example_ReapeatConst( )
    { 
    }
    public static void RepeatConstException(this Type type) 
    {
        //var type = typeof(className);//不知道className怎么做参数
        var hs = new HashSet<int>();
        var fis = type.GetFields();
        foreach (var fi in fis)
        {
            var value = fi.GetRawConstantValue();
            if (value is int)
            {
                if (!hs.Add((int)value))
                {
                    Debug.LogError($"{type.Name}中有重复项，重复值为:{value}");
                }
            }
            else
            {
                Debug.LogError($"属性：{fi.Name}.类型错误，此类所有常量必须是int类型");
            }
        }
    }

}
public static partial class ExtendClass//true false
{
    public static bool IsTrue(this bool para)
    { 
          return para==true;
    }

    public static bool IsFalse(this bool para)
    {
        return para == false;
    }
}
public static partial class ExtendClass //get set
{

    /**
     * 字段
     * 属性,unity细节器+ [SerializeField]也不显示
     */
    class A
    {

         private byte age;
        public byte Age
        {
            get { return age; }
            set { age = value; }
        }
        //
        private byte sex;
        public byte Sex
        {
            get;
            set;
        }
        //
#if NET_4_7_OR_NEWER
        private byte name;
        public byte Name
        {
            get => name;
            set => name  =value;
        }

#endif

    }

}
public static partial class ExtendClass //Null
{
    public static void Example()
    {
        var simpleClass = new object();

        if (simpleClass.IsNull()) // simpleClass == null
        {
            // do sth
        }
        else if (simpleClass.IsNotNull()) // simpleClasss != null
        {
            // do sth
        }
    }

    public static bool IsNull<T>(this T selfObj) where T : class
    {
        return null == selfObj;
    }

    public static bool IsNotNull<T>(this T selfObj) where T : class
    {
        return null != selfObj;
    }

    //public static bool NotNull(this CoroutineController ctrl)
    //{
    //    if (ctrl != null)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
    public static void CkeckNull<T>(this T t) where T : class
    {
        if (null == t)
        {

            Debug.LogErrorFormat($"CkeckNull：{t}为未赋值，为空");
        }
    }

}







