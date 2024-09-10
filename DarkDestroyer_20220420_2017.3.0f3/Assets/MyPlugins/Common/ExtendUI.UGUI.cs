/****************************************************
    文件：ExtendUGUI.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/25 11:55:7
	功能：UGUI,现在UI系统
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public static partial class ExtendUGUI//底层机制
{

    #region 模块
    class 事件数据
    { 
        //是事件数据类的父类，其中包括EventSystem、InputModule和当前选中GameObject的引用
        BaseEventData baseEventData;
        //点位事件数据，其中包含当前位置，滑动距离，点击时间以及不同状态下GameObject的引用
        PointerEventData pointerEventData;
        //滚轮事件数据，只记录滚动的方向数据。
        AxisEventData axisEventData;    
    }

    //输入检测模块规定了对事件的处理逻辑和细节，如处理鼠标点击事件，拖拽和移动等
    class 事件输入
    {
        BaseInput baseInput;
        BaseInputModule baseInputModule;
        //面向标准鼠标键盘
        StandaloneInputModule standaloneInputModule;
        //面向触摸平台和移动设备的输入检测模块
        TouchInputModule touchInputModule;
    }
    class 事件检测
    { 
        BaseRaycaster baseRaycaster;
        //预留了2D的层级次序，以便在后面的碰撞结果排序时，以这个层级次序为依据进行排序
        Physics2DRaycaster physics2DRaycaster;
        //射线碰撞检测结果则是以距离大小为依据进行排序的
        PhysicsRaycaster physicsRaycaster;
        //UGUI元素点位检测的类，它被放在Core渲染块里。它主要针对ScreenSpaceOverlay模式下的输入点位进行碰撞检测，因为这个模式下的检测并不依赖于射线碰撞，而是通过遍历所有可点击的UGUI元素来进行检测比较，从而判断该响应哪个UI元素的。因此GraphicRaycaster类是比较特殊的。
        GraphicRaycaster graphicRaycaster;
    }
    class 事件调度
    {
        //事件逻辑处理模块的主要逻辑都集中在EventSystem类中，其余类都只对它起辅助作用。EventInterfaces类、EventTrigger类、EventTriggerType类定义了事件回调函数，ExecuteEvents类编写了所有执行事件的回调接口。EventSystem主要逻辑基本上都在处理由射线碰撞检测后引起的各类事件。比如，判断事件是否成立，若成立，则发起事件回调，若不成立，则继续轮询检查，等待事件的发生。EventSystem类是事件处理模块中唯一继承MonoBehavior类并在Update帧循环中做轮询的。也就是说，所有UI事件的发生都是通过EventSystem轮询监测并且实施的。EventSystem类通过调用输入事件检测模块、检测碰撞模块来形成自己的主逻辑部分，因此EventSystem是整个事件模块的入口。最主要的还是Update里面的逻辑。
    EventSystem eventSystem;
        //ExcuteEvent 
        EventTrigger eventTrigger;
    }

    class UI模块
    {
        //  Culling
        //Layout
        //MaterialModifiers
        //VertexModifiers
        // Utility
    }
    #endregion  


}
static partial class ExtendUGUI//Button
{

    /// <summary>懒得命名节点</summary>
    public static Button GetButtonByText(this Button[] btns, string str)
    {
        foreach (Button btn in btns)
        {
            if (btn.GetComponentInChildren<Text>().text == str)
            {
                return btn;
            }
        }

        throw new System.Exception("异常");
    }
}
public static partial class ExtendUGUI//CanvasSize观察坐标
{
    public static Vector2Int CanvasSizeInt(this Canvas canvas)
    {
        Vector2 v = canvas.CanvasSize();
        int x = (int)v.x;
        int y = (int)v.y;
        return new Vector2Int(x, y);
    }

    /// <summary>canvas中心为原点</summary>
    public static Vector2 CanvasSize(this Canvas canvas)
    {
        //canvas.GetComponent<Rect>()  这种报错
        float x = canvas.GetComponent<RectTransform>().rect.xMax*2f; 
        float y = canvas.GetComponent<RectTransform>().rect.yMax*2f;

        return new Vector2(x, y);
    }
}
public static partial class ExtendUGUI
{
    /*  https://blog.uwa4d.com/archives/fillrate.html
        Fill Rate(填充率)是指显卡每帧每秒能够渲染的像素数。
        在每帧绘制中，如果一个像素被反复绘制的次数越多，那么它占用的资源也必然更多。
        目前在移动设备上，FillRate 的压力主要来自半透明物体。
        因为多数情况下，半透明物体需要开启 Alpha Blend 且关闭 ZTest和 ZWrite，
        同时如果我们绘制像 alpha=0 这种实际上不会产生效果的颜色上去，也同样有 Blend 操作，这是一种极大的浪费。
        因此，今天我们为大家推荐两则UGUI 降低填充率的技巧，希望大家能受用。

        在Unity中，与能直接看到的Verts/Tris/Batches数据不同，填充率并不能被直接统计到
    */

    public static void Example()
    {
        //QFramework.Empty4Raycast();

    }


    /// <summary>
    /// 从上到下排序
    /// <para/>需要预制体已经设置好PosY的值
    /// <para/>（预制体的Posy=xxx，实力出来的第一个就是Pos=xxx）
    /// </summary> 
    public static RectTransform VerticalGroup(this RectTransform rect,int idx)
    {
        float offset = rect.rect.height * idx;
        rect.anchoredPosition -= offset * Vector2.up;

        return rect;
    }


}
public static partial class ExtendUGUI //Slider
{
    public static Slider AddValueChangeListener(this Slider slider, Action<float> action)
    {
        slider.onValueChanged.AddListener( change=>action(change));
        return slider;
    }

    public static Slider RemoveValueChangeListener(this Slider slider, Action<float> action)
    {
        slider.onValueChanged.RemoveListener(change => action(change));
        return slider;
    }
}
//public static partial class ExtendUGUI



