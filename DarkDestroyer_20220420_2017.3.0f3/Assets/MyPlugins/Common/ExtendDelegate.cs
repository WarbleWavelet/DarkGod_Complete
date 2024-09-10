/****************************************************
    文件：ExtendAction.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/31 19:50:44
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;





/// <summary>
/// 【delegate】，函数容器（我形象叫为 动作链）,可以用+=，-=来加减函数
/// <br/> 分为命名方法委托、多播委托、匿名委托
/// <para/>【Action】，官方语法糖，delegate的"简称",简就简在无返回值
/// <para/> 【Func<>】，Delegate增加了返回值,而且必选要有返回值
/// <para/> 【event】，Delegate增加了权限限制（私有化），比如限制赋值（会清空原来的所有的注册函数） 。
/// <br/>   Action像字段，event像属性 
/// </summary>
/// 
public static partial class ExtendDelegate
{


    public static void Example()
    {
        if(false)
        { 
            Action<int> intAction = (para) => { Debug.Log(para.ToString()); };
            Action<int,int> intAction2 = (p1,p2) => { Debug.Log(p1+p2); };
            Action<int,int,int> intAction3 = (p1,p2,p3) => { Debug.Log(p1 + p2 + p3); };
            intAction.DoIfNotNull(99);
            intAction2.DoIfNotNull(99,1);
            intAction3.DoIfNotNull(99,2,2);        
        }

        DelegateAssignment delegateAssignment;
    }

    /// <summary>委托的赋值</summary>
    class DelegateAssignment

    {
        public delegate void Delegate_Void();
        public delegate void Delegate_IntPara(int a);

        void Example_A()
        {
            if (true) //delegate用法
            {
                Delegate_Void voidDelegate = new Delegate_Void(Function_Void);
                Delegate_IntPara intDelegate = new Delegate_IntPara(Function_Int);
                //静态类中的方法
                Delegate_Void staticClassVoidFunctionDelegate = new Delegate_Void(A.Function_Void);
                Delegate_IntPara staticClassIntFunctionDelegate = new Delegate_IntPara(A.Function_Int);
                //实例类中的方法
                ClassB b = new ClassB();
                Delegate_Void classVoidFunctionDelegate = new Delegate_Void(b.Function_Void);
                Delegate_IntPara classIntFunctionDelegate = new Delegate_IntPara(b.Function_Int);
            }
            if (true)//Action的赋值，用法跟delegate差不多
            {
                Action voidAction = new Action(Function_Void);
                Action voidAction1 = new Action(A.Function_Void);
                ClassB b = new ClassB();
                Action _delegate_ClassVoid = new Action(b.Function_Void);
            }
            if (true)//Func的赋值
            {
                Func<int>  returnIntFunc = new Func<int>(Function_ReturnInt);
                ClassB b = new ClassB();
                returnIntFunc = new Func<int>(A.Function_ReturnInt);
                returnIntFunc = new Func<int>(b.Function_ReturnInt);
            }
        }

        #region pri


        static void Function_Void()
        { 
    
        }

        static void Function_Int(int i)
        {

        }

        static int Function_ReturnInt()
        {
            return 0;
        }



        #endregion

        #region 内部类


        static class A
        {
           public static void Function_Void()
            {

            }

          public  static void Function_Int(int i)
            {

            }
            public static int Function_ReturnInt()
            {
                return  0;
            }
        }

         class ClassB
        {
            public  void Function_Void()
            {

            }

            public  void Function_Int(int i)
            {

            }
            public  int Function_ReturnInt()
            {
                return 0;
            }
        }
        #endregion
    }




    #region DoIfNotNull(this Action cb)

    public static Action ReturnIfNotNull(this object o)
    {
        Action cb = o as Action;
        if (cb != null)
        {
            return cb; 
        }
        return null; //有的为null也不介意，反而作为判断条件
    }
    #endregion  
    #region DoIfNotNull(this Action cb)


    /// <summary>不为空就执行</summary>
    public static Action DoIfNotNull(this Action cb)
    {
        if (cb != null)
        {
            cb();
        }
        return cb;
    }

    /// <summary>不为空就执行</summary>
    public static Action<T> DoIfNotNull<T>(this Action<T> cb,T t) 
    {
        if (cb != null)
        {
            cb(t);
        }
        return cb;
    }

    public static Action<T1,T2> DoIfNotNull<T1, T2>(this Action<T1, T2> cb
        , T1 t1
        , T2 t2)
    {
        if (cb != null)
        {
            cb(t1,t2);
        }
        return cb;
    }

    public static Action<T1, T2,T3> DoIfNotNull<T1, T2, T3>(this Action<T1, T2, T3> cb
        , T1 t1
        , T2 t2
        , T3 t3)
    {
        if (cb != null)
        {
            cb(t1, t2,t3);
        }
        return cb;
    }
    #endregion




    #region DoIfNotNull(this object o,Action cb)

    /// <summary>o不为空，就执行cb</summary>
    public static object DoIfNotNull(this object o,Action cb)
    {
        if (o != null)
        {
            cb();
        }
        return o;
    }

    /// <summary>
    /// o不为空，就执行cb
    /// <para/> 之如写作_destroyCase.DoIfNotNull(() => _destroyCase.Injure(bullet.GetAttack())) ;
    /// <br/> 就可以用object DoIfNotNull(this object o,Action cb)
    /// </summary>
    //public static object DoIfNotNull(this object o, Action<int> cb1, Func<int>  cb2)
    //{
    //    if (o != null)
    //    {
    //        cb1(cb2());
    //    }
    //    return o;
    //}
    #endregion


    #region DoOrThrowNullException
    public static object DoOrThrowNullException(this object o, Action cb)
    {
        if (o != null)
        {
            cb();
            return o;
        }
        else
        {
            throw new System.Exception("异常DoOrThrowNullException");
        }
    }
    #endregion
}



public static partial class ExtendDelegate
{
    /// <summary>运行不了,对开发者有提醒作用</summary>
    public static string Action2String(this Action cb)
    {
        return cb.GetMethodInfo().Name;
    }
}



public static partial class ExtendDelegate  //ForeachAction
{
    public static void ForeachAction<T>(this List<Action<T>> lst, T t)
    {
        if (lst==null || lst.Count ==0)
        {
            return;
        }
        for (int i = 0; i < lst.Count; i++)
        {
            lst[i](t);
        }
    }
}
public static partial class ExtendDelegate  //ReturnIfNotEquals ,失败,没能达到理想效果
{

    //public static Action ReturnIfEquals<T>(this T a, T b) where T : Enum
    //{
    //    if (a.Equals(b))
    //    {
    //        Action cb = new Action(() => { return; });
    //    }
    //    return null;
    //}


    //public static Action ReturnIfNotEquals<T>(this T a,T b) where T:  Enum
    //{
    //    if (!a.Equals(b))
    //    {
    //        return new Action(()=> { return; });
    //    }
    //    return null;
    //}


}




