/****************************************************
    文件：ExtendAnonymous.cs
	作者：lenovo
    邮箱: 
    日期：2023/8/28 13:17:9
	功能： 匿名 Linq 反射
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public static partial class ExtendRaycasterBase
{


    /// <summary>
    /// 是否忽略反转图形。（X、Y轴反转）
    /// <br/>默认勾选，在勾选状态下，例如一个Button对象，当对其X轴或者Y轴设置Rotation=180后，将无法对该Button对象进行点击操作；取消勾选后，可以重新响应点击操作
    /// </summary>
    public static GraphicRaycaster IgnoreReversedGraphics(this GraphicRaycaster gr,bool _bool=true)
    {
        gr.ignoreReversedGraphics = _bool;
        return gr;
    }
    /// <summary>
    /// 射线被哪些类型的碰撞器阻挡（在Canvas的Screen Space-Overlay覆盖渲染模式【Render Mode】下无效）
    /// <br/>默认是None，会忽略挡在UI前面的3D/2D碰撞器对点击操作进行响应，即在3D/2D物体与UI对象重复的地方进行点击依然会响应；
    /// <br/>若选择 Two D，那么点击被2D碰撞器覆盖的部分UI对象将不会响应，Three D， All相应
    /// </summary>
    public static GraphicRaycaster BlockingObjects(this GraphicRaycaster gr)
    {
        gr.blockingObjects = GraphicRaycaster.BlockingObjects.None;
        gr.blockingObjects = GraphicRaycaster.BlockingObjects.TwoD;
        gr.blockingObjects = GraphicRaycaster.BlockingObjects.ThreeD;
        gr.blockingObjects = GraphicRaycaster.BlockingObjects.All;
        return gr;
    }

    /// <summary>
    /// 射线被哪些层级的碰撞器阻挡（在覆盖渲染模式下无效）
    /// <para/>例如在Button前有一个Cube，而Cube设置为Cube层，
    /// <br/>Blocking Mask将Cube层不勾选，那么即使Blocking Object选择Three D 或者 All，那么通过点击Cube也可以让Button响应，
    /// <br/>反之如果勾选了Cube层，那么Three D/All就会对UI响应有影响
    /// <para/>因为在覆盖模式下UI始终显示在最前面，不存在阻挡，而上面两个参数都是UI在后面的情况，所以需要对Render Mode进行限制
    /// </summary>
    public static GraphicRaycaster BlockingMask(this GraphicRaycaster gr,string maskName)
    {
#if !NET_4_6
        gr.blockingMask=LayerMask.NameToLayer(maskName);
#endif
        return gr;
    }

}




