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

public interface IEntity2DLayer
{ 
     Entity2DLayer E_Entity2DLayer { get; }
}
public interface IEntity2D :  ISpriteRenderer ,IRectTransform  , IEntity2DLayer
{
  
}

public interface IColliderEntity2D : IEntity2D, ICollider2D
{
}

/// <summary>这里用到的是位置，大小，颜色，旋转等的变化</summary>

public interface IEffectLife : IBegin, IStopAction, IHide, IClear

{

}



/// <summary>特效，需要多图的地址</summary>
public interface IEffectEntity2D : IInitTransform,IEffectLife

{
    
}



