/****************************************************
    文件：ExtendPhysics2D.cs
	作者：lenovo
    邮箱: 
    日期：2024/4/22 15:21:1
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
public static partial class ExtendPhysics2D 
{
    public static void IgnoreLayerCollision(this Physics2D physics2D,int layer1,int layer2,bool ignore=false)
    {
        Physics2D.IgnoreLayerCollision(layer1, layer2, ignore);
    }


}



