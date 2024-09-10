/****************************************************
    文件：CustomShapeImage.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/27 22:56:40
	功能：
*****************************************************/


using Random = UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Sprites;
using UnityEngine;
using UnityEngine.UI;

public class CustomShapeImage : Image
{
    struct TriangleIndex
    {
        public int x;
        public int y;
        public int z;
    }
    /// <summary>
    /// 顺序顶点
    /// </summary>
    [SerializeField]
    public List<Vector2> m_Points = new List<Vector2>();
    private Vector2 m_Center = Vector2.one * 0.5f;
    private List<TriangleIndex> m_Triangles = new List<TriangleIndex>();
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        //if (activeSprite == null)
        //{
        //    base.OnPopulateMesh(vh);
        //    return;
        //}
        //GenerateSprite(vh, preserveAspect);
        vh.Clear();
        m_Triangles.Clear();
        AddVertex(vh);

        AddTriangle(vh);
    }

    private void AddVertex(VertexHelper vh)
    {
        float width = rectTransform.rect.width;
        float heigth = rectTransform.rect.height;
        Vector2 wh = new Vector2(width, heigth);
        Vector4 uv = overrideSprite != null ? DataUtility.GetOuterUV(overrideSprite) : Vector4.zero;
        float uvWidth = uv.z - uv.x;
        float uvHeight = uv.w - uv.y;
        Vector2 uvCenter = new Vector2(uvWidth * 0.5f, uvHeight * 0.5f);
        Vector2 convertRatio = new Vector2(uvWidth / width, uvHeight / heigth);

        Vector2 originPos = new Vector2((0.5f - rectTransform.pivot.x) * width, (0.5f - rectTransform.pivot.y) * heigth);

        for (int i = 0; i < m_Points.Count; i++)
        {
#if !NET_4_6
            UIVertex vertexTemp = GetUIVertex(color, (m_Points[i] - m_Center) * wh + originPos, m_Points[i] * wh - originPos, Vector2.zero, convertRatio);
            vh.AddVert(vertexTemp);
#endif 
        }
    }

    private UIVertex GetUIVertex(Color32 col, Vector3 pos, Vector2 uvPos, Vector2 uvCenter, Vector2 uvScale)
    {
        UIVertex vertexTemp = new UIVertex();
        vertexTemp.color = col;
        vertexTemp.position = pos;
        vertexTemp.uv0 = new Vector2(uvPos.x * uvScale.x + uvCenter.x, uvPos.y * uvScale.y + uvCenter.y);
        return vertexTemp;
    }

    private void AddTriangle(VertexHelper vh)
    {
        if (m_Points.Count < 3)
        {
            return;
        }

        for (int i = 0; i < m_Points.Count - 2; i++)
        {
            for (int j = i + 1; j < m_Points.Count - 1; j++)
            {
                TriangleIndex triangle = new TriangleIndex() { x = i, y = j, z = j + 1 };
                m_Triangles.Add(triangle);
                vh.AddTriangle(i, j, j + 1);
            }
        }
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out localPoint);
        return IsValid(localPoint);
    }

    private bool IsValid(Vector2 localPoint)
    {
        foreach (var v in m_Triangles)
        {
            Vector2 v1 = localPoint - m_Points[v.x];
            Vector2 v2 = localPoint - m_Points[v.y];
            Vector2 v3 = localPoint - m_Points[v.z];
            float c1 = CrossVec2(v1, v2);
            float c2 = CrossVec2(v1, v3);
            bool b = (c1 * c2) >= 0;
            if (b)
                return true;
        }
        return false;
        //return GetCrossPointNum(localPoint, _vertexList) %2 == 1;
    }

    private float CrossVec2(Vector2 v1, Vector2 v2)
    {
        return v1.x * v2.y - v2.x * v1.y;
    }
}



