/****************************************************
    文件：IEntyty.IEntity2D.cs
	作者：lenovo
    邮箱: 
    日期：2024/3/30 11:56:16
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



/// <summary>主要是组件的父节点,方便找它上面的一些属性
/// 命名灵感来自于MVVM的ViewModel(我这里只有Transfrom,没有做ViewModel)
/// </summary>
public interface IViewTransfrom
{
    Transform ViewTransfrom();
}
public interface IEntity3D :  ISpriteRenderer
{
    Entity2DLayer E_Entity2DLayer { get; }
}

public interface IColliderEntity3D : IEntity3D, ICollider
{
}