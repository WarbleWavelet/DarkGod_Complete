/****************************************************
    文件：GLL.cs
	作者：lenovo
    邮箱: 
    日期：2024/3/30 11:59:27
	功能：Group Level Layer Depth（UI的，2D物体的，3D物体的，用Entity是不想跟Object抢名字）
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

#region EntityXD
public enum Entity2DLayer
{
    BACKGROUND = -1,
    PLANE = 0,
    EFFECT = 1
}
public enum Entity2DLevel
{

}
public enum Entity2DGroup
{

}


public enum Entity3DLayer
{
    BACKGROUND = -1,
    BULLET=0
}
public enum Entity3DLevel
{

}
public enum Entity3DGroup
{

}

#endregion



