/****************************************************
    文件：ParabolaPath.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/18 6:35:7
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendSpriteRenderer//面板属性  sortingLayer id,name,vaklue //SortingLayer[] layers
{
    public static int SortingLayerID(this SpriteRenderer sr)
    { 
         return sr.sortingLayerID;
    }
    public static string SortingLayerName(this SpriteRenderer sr)
    {
        return sr.sortingLayerName; 
    }

    #region SortingLayer  Index Value


   #region SortingLayerIndex
    public static int SortingLayerIndex(this string sortingLayerName)
    {
       return SortingLayer.GetLayerValueFromName(sortingLayerName);
    }

    public static int SortingLayerIndex(this int id)
    {
        return SortingLayer.GetLayerValueFromID(id);
    }

    public static int SortingLayerIndex(this SpriteRenderer sr)
    {
        int id = sr.sortingLayerID;
        return SortingLayer.GetLayerValueFromID(id);
    }
    #endregion

    #region  SortingLayerValue 同名Index
    public static int SortingLayerValue(this string sortingLayerName)
    {
        return SortingLayer.GetLayerValueFromName(sortingLayerName);
    }

    public static int SortingLayerValue(this int id)
    {
        return SortingLayer.GetLayerValueFromID(id);
    }

    public static int SortingLayerValue(this SpriteRenderer sr)
    {
        int id = sr.sortingLayerID;
        return SortingLayer.GetLayerValueFromID(id);
    }
    #endregion

    #region  SortingLayerInt 同名Index
    public static int SortingLayerInt(this string sortingLayerName)
    {
        return SortingLayer.GetLayerValueFromName(sortingLayerName);
    }

    public static int SortingLayerInt(this int id)
    {
        return SortingLayer.GetLayerValueFromID(id);
    }

    public static int SortingLayerInt(this SpriteRenderer sr)
    {
        int id = sr.sortingLayerID;
        return SortingLayer.GetLayerValueFromID(id);
    }
    #endregion
    #endregion





    #region SortingLayer.layers
    public static SortingLayer[] SortingLayersArr(this SpriteRenderer sr)
    {
        return SortingLayer.layers;
    }
    public static string SortingLayersToString(this SpriteRenderer sr)
    {
        string str = "";
        foreach (SortingLayer layer in SortingLayer.layers)
        {
            str += layer.id + "\t" + layer.name + "\t" + layer.value + "\n";
        }

        Debug.Log(str);
        return str;
    }
    #endregion  



}
public static partial class ExtendSpriteRenderer//面板属性  sortingOrder   OrderInLayer
{
    public static int OrderInLayer(this SpriteRenderer sr)
    {
       return sr.sortingOrder;
    }

    public static SpriteRenderer OrderInLayer(this SpriteRenderer sr,int sortingOrder)
    {
         sr.sortingOrder= sortingOrder;
        return sr;
    }

}
public static partial class ExtendSpriteRenderer//Fade
{
    /// <summary>
    ///  fadePerFrame,每一帧变化量
    /// </summary>
    public static IEnumerator FadeAlpha(this SpriteRenderer sr, float fadePerFrame)
    {
        while (sr.color.a > 0)
        {
            Color cur = sr.color;
#if NET_4_7_2_OR_NEWER
            cur.SetAlpha(sr.color.a - fadePerFrame);
#endif
            sr.color = cur;
            yield return new WaitForFixedUpdate();
        }
    }
}
public static partial class ExtendSpriteRenderer//Ratio
{

    /// <summary>Collder根据SpriteRenderer大小改变大小</summary>
    static void Example()
    {
        Sprite sprite=Resources.Load<Sprite>("");
        SpriteRenderer SpriteRenderer=new SpriteRenderer ();
        CapsuleCollider2D _capsuleCollider=new CapsuleCollider2D ();
        Vector2 ratio = SpriteRenderer.Ratio(sprite);
        _capsuleCollider.Adapt(ratio);
    }

    /// <summary>Collder根据SpriteRenderer大小改变大小</summary>

    public static Vector2 Ratio(this SpriteRenderer sr, Sprite sprite)
    {
        float oldX = sr.BoundsSizeX();
        float oldY = sr.BoundsSizeY();
        sr.sprite = sprite;
        float newX = sr.BoundsSizeX();
        float newY = sr.BoundsSizeY();
        float xRatio = newX / oldX;
        float yRatio = newY / oldY;
        return new Vector2(xRatio,yRatio);
    }
}
public static partial class ExtendSpriteRenderer//bounds
{



    /// <summary>中心点坐标</summary>
    public static Vector3 BoundsCenter(this SpriteRenderer sr)
    {
        return sr.bounds.center;
    }

    public static float BoundsCenterX(this SpriteRenderer sr)
    {
        return sr.bounds.center.x;
    }

    public static float BoundsCenterY(this SpriteRenderer sr)
    {
        return sr.bounds.center.y;
    }


    /// <summary>碰撞体大小</summary>
    public static Vector3 BoundsSize(this SpriteRenderer sr)
    {
        return sr.bounds.size;
    }

    /// <summary>宽</summary>
    public static float BoundsSizeX(this SpriteRenderer sr)
    {
        return sr.bounds.size.x;
    }

    /// <summary>高</summary>
    public static float BoundsSizeY(this SpriteRenderer sr)
    {
        return sr.bounds.size.y;
    }

    /// <summary>深</summary>
    public static float BoundsSizeZ(this SpriteRenderer sr)
    {
        return sr.bounds.size.z;
    }

    /// <summary>最小点的位置：左下角</summary>
    public static Vector3 BoundsMin(this SpriteRenderer sr)
    {
        return sr.bounds.min;
    }


    /// <summary>最大点的位置：右上角</summary>
    public static Vector3 BoundsMax(this SpriteRenderer sr)
    {
        return sr.bounds.max;
    }

    /// <summary>最大点的位置：右上角X</summary>
    public static float BoundsMaxX(this SpriteRenderer sr)
    {
        return sr.bounds.max.x;
    }

    /// <summary>最大点的位置：右上角Y</summary>
    public static float BoundsMaxY(this SpriteRenderer sr)
    {
        return sr.bounds.max.y;
    }

    /// <summary>最小点的位置：左下角X</summary>
    public static float BoundsMinX(this SpriteRenderer sr)
    {
        return sr.bounds.min.x;
    }

    /// <summary>最小点的位置：左下角Y</summary>
    public static float BoundsMinY(this SpriteRenderer sr)
    {
        return sr.bounds.min.y;
    }

    /// <summary>高度</summary>
    public static float BoundsHeight(this SpriteRenderer sr)
    {
        return (sr.bounds.max.y - sr.bounds.min.y);
    }

    /// <summary>宽度</summary>
    public static float BoundsWidth(this SpriteRenderer sr)
    {
        return (sr.bounds.max.x - sr.bounds.min.x);
    }
}



public static partial class ExtendSpriteRenderer//Roll
{      
  /// <summary>水平滚动</summary>    
    public static SpriteRenderer RollX(this SpriteRenderer sr, float speed = 0.02f)
    {
        sr.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
        sr.material.mainTextureOffset += new Vector2(Time.deltaTime * speed, 0);
        return sr;

    }
    /// <summary>竖直滚动</summary>
    public static SpriteRenderer RollY(this SpriteRenderer sr, float speed = 0.02f)
    {
        sr.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
        sr.material.mainTextureOffset += new Vector2( 0, Time.deltaTime * speed);
        return sr;

    }

}



