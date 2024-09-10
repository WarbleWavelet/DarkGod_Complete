/****************************************************
    文件：ExtendAttribute.cs
	作者：lenovo
    邮箱: 
    日期：2023/8/20 11:32:57
	功能：
*****************************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;


//public static partial class ExtendAttribute
public static partial class ExtendAttribute //AttributeUsage
{
    /**  
    AttributeUsage
    翻译，属性用法 
    实际翻译，自定义特性
    用处，描述了如何使用一个自定义特性类；规定了特性可应用到的项目的类型
    。。。。。。
    语法如下：
    [AttributeUsage(
        validon,
        AllowMultiple=allowmultiple,
        Inherited=inherited
    )]
    参数 validon 规定特性可被放置的语言元素。
        它是枚举器 AttributeTargets 的值的组合，设置多个AttributeTargets 可以用 | 分隔，如AttributeTargets.Class | AttributeTargets.。
        默认值是 AttributeTargets.All。
    参数 allowmultiple（可选的）为该特性的 AllowMultiple 属性（property）提供一个布尔值。
        如果为 true，则该特性是多用的。
        默认值是 false（单用的）。
    参数 inherited（可选的）为该特性的 Inherited 属性（property）提供一个布尔值。
        如果为 true，则该特性可被派生类继承。默认值是 false（不被继承）。
     
    */
}
public static partial class ExtendAttribute
{

    public interface IAddTypeByAttribute
    {
        void Init(Attribute atb, Type type);
    }


    /// <summary>T用接口,比如IBulletModel,实际是实现了接口的类</summary>
    public static T GetInstance<T>(Type type) where T : class
    {
        object instance = Activator.CreateInstance(type);
        if (instance is T)
        {
            return instance as T;
        }
        else
        {
            Debug.LogError($"当前绑定类未继承{typeof(T)}接口，类名为：" + type);
            return null;
        }
    }


    /// <summary>
    ///  T xxxAttribute
    ///  Type 系统类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cb"></param>
    public static void InitData<T>(Action<T, Type> cb) where T : Attribute
    {
        Type[] types;

        types = ExtendReflection.GetTypes<T>();

        foreach (var type in types)
        {
            foreach (var attribute in Attribute.GetCustomAttributes(type, true))
            {
                if (attribute is T)
                {
                    T data = attribute as T;
                    cb(data, type);
                }
            }
        }
    }
}
public static partial class ExtendAttribute
{
    static List<object> _classLst = new List<object>();

    /// <summary>未完成</summary>
    public static List<object> GetType<T>(T t) 
    {
        //Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        //return types
        // .Where(t => t.GetCustomAttribute(typeof(HeroAttribute), false).Any())
        //     .ToList();

        return null;

    }

}
public static partial class ExtendAttribute  //用法举例
{

    #region 材料
    /// <summary>使得类+字段/属性==唯一性</summary>
    public enum EHeroType
    { 
        ADC,//射手
        AD,//物理
        AP //法术
    }
    class Hero
    { 
    
    }

    class Skill
    {

    }
    #endregion  



    #region 定义
    [AttributeUsage(AttributeTargets.Class)]
    public class HeroAttribute : Attribute
    {
        public EHeroType HeroType;


        #region 构造
        public HeroAttribute(EHeroType xXXEnum)
        {
            this.HeroType = xXXEnum;
        }
        #endregion
    }
    #endregion


    #region 使用
    [Hero(EHeroType.ADC)]
    public class HeroADC
    { 
    
    }

    [Hero(EHeroType.AD)]
    public class HeroAD
    {

    }

    [Hero(EHeroType.AP)]
    public class HeroAP
    {

    }
    #endregion



}




