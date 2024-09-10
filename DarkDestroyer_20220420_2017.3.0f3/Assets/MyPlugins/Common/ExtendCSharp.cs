/****************************************************
    文件：ExtendCSharp.cs
	作者：lenovo
    邮箱: 
    日期：2023/编程知识点
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendCSharp //修饰符
{
	/// <summary>C#的修饰符
	/// 首字母大写,不然修饰符级别高,会冲突
	/// </summary>
	public enum EModifier
	{
		/// <summary>
		/// 锁死程序集
		/// （内部）：限定的是只有在同一程序集中可访问，可以跨类
		/// </summary>
		Internal,
		/// <summary>
		/// 锁死继承
		/// （受保护）：限定的是只有在继承的子类中可访问，可以跨程序集
		/// </summary>
		Protected	,
		/// <summary>
		/// 受保护“或”内部修饰符修饰成员,
		/// 当父类与子类在同一个程序集中，internal成员可见。
		/// 当父类与子类不在同一个程序集中，子类不能访问父类internal成员，而子类可以访问父类的ptotected internal成员</summary>	
		Protected_Internal 
	}
	/// <summary></summary>	

}


public static partial class ExtendCSharp //约束 
{
    // ,new那么需要一个无参构造
    //
}
public static partial class ExtendCSharp //字段属性的区别
{
    //
    //AttributeUsage(AttributeTargets.Field)
    //AttributeUsage(AttributeTargets.Property)
    //
    //typeof(CLASS).GetField(FIELD).Name
    //typeof(CLASS).GetProperty(PROPERTY).Name
    //
    //interface可以写属性,不能写字段
}


public static partial class ExtendCSharp //接口与抽象类的一些对比
{
    /**
     * interface中的方法，修饰符只能用public，不能protected private，或者啥都不加（这种情况默认修饰符只能用public）
     *
     *
     * abstract类中，常用 的方法修饰符组合是
     * 01 public virtual        与 不一定实现重写，                      与不一定使用base.抽象类方法
     * 02 protected virtual     与 不一定实现重写，                      与不一定使用base.抽象类方法
     * 02 protected abstract    与 一定实现重写 protected override，     与不一定使用base.抽象类方法
     */
}
public static partial class ExtendCSharp
{ 
   //continue//继续跑，跳过，不用鸟这一轮循环
}
public static partial class ExtendCSharp
{
    static void AbstractFunc ()
    {
        /*
            抽象的注意事项：

            1.抽象类和抽象方法必须使用abstract关键字修饰
            2.抽象类中不一定有抽象方法,但是有抽象方法的一定在抽象类中
            3.抽象类不能实例化(实例 = 对象)[也就是不能创建对象],如果非要实例化,可以通过多态的形式创建,也就是 父类引用指向子类对象
            4.抽象类的子类必须重写父类(抽象类)中所有的抽象方法或将自己也变成一个抽象类
        */
        /*
            类和类之间具有共同特征，将这些共同特征抽取出来，形成的就是抽象类。
            如果一个类没有足够的信息来描述一个具体的对象，这个类就是抽象类。
            因为类本身就是不存在的，所以抽象类无法创建对象，也就是无法实例化。
            抽象类的子类还可以是抽象类可以是抽象类
            抽象类有构造方法，这个构造方法是给子类提供的。因为抽象就是被用来继承的，子类继承父类，子类的构造方法中的第一句默认是super();
        */

        /** 抽象方法与虚方法的区别
         *   抽象方法一定是在抽象类中声明的
         *   抽象方法要求继承该基类的派生类一定要重写(overried)的方法，该方法使用关键字abstract定义
         *   虚方法是可以直接被调用的，而抽象方法则不可以。
         */
    }

    static void InterfaceFunc()
    {
        /**
        接口和抽象类的区别：

        抽象类中的方法可以有方法体，就是能实现方法的具体功能，但是接口中的方法不行。
        抽象类中的成员变量可以是各种类型的，而接口中的成员变量只能是 public static final 类型的，所以一般不会在接口中定义成员。
        接口中不能含有静态代码块以及静态方法(用 static 修饰的方法)，而抽象类是可以有静态代码块和静态方法。
        一个类只能继承一个抽象类，而一个类却可以实现多个接口。
        //
        问：C# 接口方法私有化
        这是由于接口规范中的方法默认的访问权限是public，而类中的默认访问权限是default，
        也就是说private，因此导致权限范围收缩，两者权限并不相同，
        所以必须将类的权限调整为public才可以使上面的代码得以执行。
        */

        /**
        public interface IUISystem : QFramework.ISystem
        {
            Transform Canvas { get; set; }
        }

        public class UISystem : AbstractSystem, IUISystem
        {
             Transform IUISystem.Canvas { get { return _canvas; }  set { _canvas = value; } }
             Transform _canvas {  get;  set; }
        */
    }

    static void VirtualFunc()
    {
        /**
            基类方法必须定义为 virtual。
            如果派生类中的方法前面没有 new 或 override 关键字，则编译器将发出警告，该方法将有如存在 new 关键字一样执行操作。
            如果派生类中的方法前面带有 new 关键字，则该方法被定义为独立于基类中的方法。
            如果派生类中的方法前面带有 override 关键字，则派生类的对象将调用该方法，而不是调用基类方法。
            可以从派生类中使用 base 关键字调用基类方法。
            override、virtual 和 new 关键字还可以用于属性、索引器和事件中。
         */
    }


    /// <summary>术语</summary>
    static void Term()
    { 
        //实现，相对于接口
        //继承
        //重写，override，提供一个方法，需要就加内容
    }


    /// <summary>关键词</summary>
    static void Keys()
    {
        //const 字段只能在该字段的声明中初始化
        //readonly 一个只读字段可以在字段声明时或在构造函数内部进行初始化，一旦被赋予了初始值，它就不能被改变。

    }

    static void HashSet()
    {
        //可以存放null值，但是只能有一个null
        //HashSet不能保证元素的存取顺序一致
        //不能有重复的元素
        //HashSet线程不安全
        //没有带索引的方法，所以不能通过普通for循环进行遍历
    }
}




