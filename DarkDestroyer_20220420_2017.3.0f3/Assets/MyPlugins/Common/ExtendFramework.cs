/****************************************************
    文件：ExtendFramework.cs
	作者：lenovo
    邮箱: 
    日期：2024/6/2 12:25:1
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public static partial class ExtendFramework //尝试MVVM的实现
{ 

}
public static partial class ExtendFramework 
{
    /// <summary>架构Architecture是理论</summary>
    public enum E架构
    {
        /// <summary>
        /// Customer-Server
        /// <br/>起源于局域网
        ///  <br/>App之类
        /// </summary>
        CS,
        /// <summary>
        /// Browser-Server
        /// <br/>也叫三层CS
        /// <br/>网页版之类
        /// </summary>
        BS
    }
    /// <summary>框架是理论的落地</summary>
 
    public enum EFramework
    {
        /// <summary>
        /// 模型(model)一视图(view)一控制器(controller)
        /// <br/> JavaBean+Jsp+Servlet: JavaBean+Jsp耦合,属于MV耦合
        ///  使用MVC构建整体的Web架构
        ///  MVC是基于观察者设计模式的，Model作为一个主题，View作为观察者，当一个Model变化时，会通知更新一个或多个依赖的View 
        /// </summary>
        MVC,


        /// <summary>
        /// PureMVC在此基础上新增了Mediator来处理View，
        /// Command来处理Controller，
        /// Proxy来处理Model，
        /// 同时还有Facade做一个整体管理，Notification用于相互通信（详细的后续介绍）。
        /// </summary>
        PureMVC,


        SpringMVC,


        /// <summary>Model-View-Presenter，
        /// 为了完全切断View跟Model之间的联系
        /// 在MVP模式中，View负责视图的显示,Model负责提供数据，Presenter则主要负责逻辑业务的处理。
        /// MVP是Web架构整体的解决方案
        /// Presenter主持人
        /// MVP 设计模式核心就是，通过定义一个 View，将 UI 抽象出来，它不必关心数据的具体来源，也不必关心点击按钮之后业务逻辑的实现，它只关注 UI 交互。这就是典型的分离关注点。
        ///</summary>
        ///
        MVP,


        /// <summary>
        /// Model-View-ViewModel 
        /// View和Model之间的通信都是通过Presenter进行完成的
        /// 为了彻底解决View和Model的耦合问题)
        /// MVVM主要用于构建基于事件驱动的 UI 平台(界面)，适用于前端开发领域中数据与界面相混合的情况，
        /// 所以它只专注于视图层，抽象出视图的状态和行为，实现了用户界面的UI(View)和数据(Model)的解耦。  
        /// 使用MVVM解决View层DOM和data的耦合问题
        /// MVVM可以看做是基于中介者设计模式和观察者设计模式，View和Model通过ViewModel这个中介者对象进行交互，解耦了View和Model的同时实现数据双向绑定。 同时ViewModel 作为一个主题对象，View和Model为两个观察者(或者可以理解为View为主题时，Model为观察者，反之。这里的Model View起到一个注册，通知的作用，对于观察者模式的定义，ModelView是主题的行为，但实际变化的是View或者Model，
        /// 当Model变化时，ViewModel由数据绑定通知并更新与之相关的多个View，反之，当View变化时，ViewModel由DOM监听通知更新相关的多个Model。 
        /// vm中利用diff算法,虚拟DOM等方式实现了一套数据响应式机制自动相应
        /// MVVM的核心思想就是解耦，View与ViewModel应该感受不到彼此的存在。
        ///  <para/>
        /// View只关心怎样渲染，而ViewModel只关心怎么处理逻辑，整个架构由数据进行驱动。
        /// 不仅View与ViewModel彼此解耦，ViewModel与ViewModel之间也是解耦的。
        /// <para/>
        /// 【视图模型】mvvm模式的核心，它是连接view和model的桥梁。它有两个方向：
        /// <br/>一是将【模型】转化成【视图】，即将后端传递的数据转化成所看到的页面。实现的方式是：数据绑定。
        /// <br/>二是将【视图】转化成【模型】，即将所看到的页面转化成后端的数据。实现的方式是：DOM 事件监听。这两个方向都实现的，我们称之为数据的双向绑定。
        /// </summary>
        MVVM,



        /// <summary>
        /// Entity-Component-System，即实体-组件-系统。
        /// 是一种软件架构模式，主要用于游戏开发。 </summary>
        /// Unity官方的ECS框架：Dots(全称为：Data-Oriented-Tech-Stack——多线程式数据导向型技术堆栈)
        /// ECS：提供了默认情况下写出高性能的代码的方法
        /// JobSystem：unity的多线程解决方案，提供了编写简单且安全的多线程代码的方法
        /// Burst：一种基于LLVM为基础的后端编译技术。能将c#代码编译为高度优化的本机代码 作者：MisakaNo10086 https://www.bilibili.com/read/cv16047480/ 出处：bilibili
        ECS,


        /// <summary>
        /// SSM（Spring+SpringMVC+MyBatis）框架集由Spring、MyBatis两个开源框架整合而成
        /// （SpringMVC是Spring中的部分内容） </summary>
        SSM_JSP
    }

}

public static partial class ExtendMVVM //放外面,用到了 BindableProperty
{


}



