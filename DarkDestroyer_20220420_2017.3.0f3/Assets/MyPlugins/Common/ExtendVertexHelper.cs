/****************************************************
    文件：ExtendVertexHelper.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/27 20:57:2
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
 

public static class ExtendVertexHelper 
{

    /// <summary>将索引为vIdx的顶点的颜色设置为color</summary>
    public static void SetColorByIdx(this VertexHelper vh, Color color, int vIdx)
    {
        //得到第三个顶点(顶点索引为2，从0开始)的颜色
        UIVertex vertex = new UIVertex();
        vh.PopulateUIVertex(ref vertex, vIdx);
        //设置颜色为黄色
        vertex.color = color;
        vh.SetUIVertex(vertex, vIdx);
    }


    /// <summary>VertexHelper结构中有几个顶点索引</summary>
    public static int CurIndexCount(this VertexHelper vh)
    {
        return vh.currentIndexCount;
    }


    /// <summary>VertexHelper结构中有几个顶点</summary>
    public static int CurVertCount(this VertexHelper vh)
    {
        return vh.currentVertCount;
    }



}




