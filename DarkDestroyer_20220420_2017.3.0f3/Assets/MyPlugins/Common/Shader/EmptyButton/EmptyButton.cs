/****************************************************
    文件：EmptyButton.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/27 17:40:20
	功能：https://blog.csdn.net/qq_25978293/article/details/123056879
*****************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmptyButton : MaskableGraphic
{
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }
}



