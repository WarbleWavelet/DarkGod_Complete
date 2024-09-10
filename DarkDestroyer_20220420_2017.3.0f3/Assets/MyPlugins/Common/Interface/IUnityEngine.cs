/****************************************************
    文件：IUnityEngine.cs
	作者：lenovo
    邮箱: 
    日期：2024/3/30 11:52:21
	功能：UnityEngine的一些基础类
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public interface ITransform
{
    Transform Transform { get; }
   
}

public interface IGameObject
{
    GameObject GameObject { get; }
}
public interface IRectTransform
{
    RectTransform RectTransform { get; }
}



#region UI
public interface IImage  
{
    UnityEngine.UI.Image Image { get; }
}

public interface IText
{
    UnityEngine.UI.Text Text { get; }
}
#endregion



#region 2D
public interface ISpriteRenderer 
{
    SpriteRenderer SpriteRenderer { get; }
}

public interface ICollider2D 
{
   Collider2D Collider2D { get; }

}
#endregion

#region 3D
public interface ISkinnedMeshRenderer
{
    SkinnedMeshRenderer SkinnedMeshRenderer { get; }
}
public interface ICollider
{
    Collider Collider { get; }

}

public interface IAnimator
{
    Animator Animator { get; }
}
#endregion



#region 1 
/// <summary>SpriteRenderer,Image </summary>
public interface ISpritePath
{
    string SpritePath { get; }

}


/// <summary>爆炸等特效需要多张图</summary>
public interface ISpritesPath
{
    string SpritesPath { get; }
    /// <summary>有可能让上面的系统持有，不然重复</summary>
    //Sprite[] Sprites { get; }

}
#endregion

