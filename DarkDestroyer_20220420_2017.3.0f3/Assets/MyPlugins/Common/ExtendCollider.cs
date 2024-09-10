/****************************************************
    文件：ExtendCollider.cs
	作者：lenovo
    邮箱: 
    日期：2024/2/28 0:4:9
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static partial class ExtendCollider 
{
    /// <summary>Collder根据SpriteRenderer大小改变大小</summary>

    public static CapsuleCollider2D Adapt(this CapsuleCollider2D c,Vector2 ratio)
    {
        Vector2 size = c.size;
        size.MultV2(ratio);
        c.size = size;

        return c;
    }

}




