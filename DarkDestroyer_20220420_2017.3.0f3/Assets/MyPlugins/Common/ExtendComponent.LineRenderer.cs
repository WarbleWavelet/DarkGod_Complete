/****************************************************
    文件：ExtendComponent.LineRenderer.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/10 22:28:44
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static partial class ExtendLineRenderer
{
    /// <summary>线段,矩形区域</summary>
    public static LineRenderer Draw(this LineRenderer lr,Vector3 from ,Vector3 to)
    {
        lr.SetPosition(0,from);
        lr.SetPosition(1,to);
        return lr;
    }

    public static LineRenderer Draw(this LineRenderer lr, Transform from, Transform to)
    {
        lr.SetPosition(0, from.position);
        lr.SetPosition(1, to.position);
        return lr;
    }

    public static LineRenderer Draw(this LineRenderer lr, Component from, Component to)
    {
        lr.SetPosition(0, from.transform.position);
        lr.SetPosition(1, to.transform.position);
        return lr;
    }

}



