/****************************************************
    文件：CircleImage.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/27 22:54:34
	功能：https://blog.csdn.net/qq_25978293/article/details/122915161
*****************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class CircleImage : Image
{
    /// <summary>
    /// 圆形由多少块三角形拼成
    /// </summary>
    [SerializeField]
    private int segements = 100;
    //显示部分占圆形的百分比.
    [SerializeField]
    private float showPercent = 1;
    private readonly Color32 GRAY_COLOR = new Color32(60, 60, 60, 255);
    private List<Vector3> _vertexList;
    private float checkRadius = 0;
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        _vertexList = new List<Vector3>();

        AddVertex(vh, segements);

        AddTriangle(vh, segements);
    }

    private void AddVertex(VertexHelper vh, int segements)
    {
        float width = rectTransform.rect.width;
        float heigth = rectTransform.rect.height;
        int realSegments = (int)(segements * showPercent);

        Vector4 uv = overrideSprite != null ? DataUtility.GetOuterUV(overrideSprite) : Vector4.zero;
        float uvWidth = uv.z - uv.x;
        float uvHeight = uv.w - uv.y;
        Vector2 uvCenter = new Vector2(uvWidth * 0.5f, uvHeight * 0.5f);
        Vector2 convertRatio = new Vector2(uvWidth / width, uvHeight / heigth);

        float radian = (2 * Mathf.PI) / segements;
        float radius = width * 0.5f;
        checkRadius = radius;
        Vector2 originPos = new Vector2((0.5f - rectTransform.pivot.x) * width, (0.5f - rectTransform.pivot.y) * heigth);
        Vector2 vertPos = Vector2.zero;

        //Color32 colorTemp = GetOriginColor();
        Color32 colorTemp = color;
        UIVertex origin = GetUIVertex(colorTemp, originPos, vertPos, uvCenter, convertRatio);
        vh.AddVert(origin);

        int vertexCount = realSegments + 1;
        float curRadian = 0;
        Vector2 posTermp = Vector2.zero;
        for (int i = 0; i < segements + 1; i++)
        {
            float x = Mathf.Cos(curRadian) * radius;
            float y = Mathf.Sin(curRadian) * radius;
            curRadian += radian;

            if (i < vertexCount)
            {
                colorTemp = color;
            }
            else
            {
                colorTemp = GRAY_COLOR;
            }
            //colorTemp = color;
            posTermp = new Vector2(x, y);
            UIVertex vertexTemp = GetUIVertex(colorTemp, posTermp + originPos, posTermp, uvCenter, convertRatio);
            vh.AddVert(vertexTemp);
            _vertexList.Add(posTermp + originPos);
        }
    }

    private Color32 GetOriginColor()
    {
        Color32 colorTemp = (Color.white - GRAY_COLOR) * showPercent;
        return new Color32(
            (byte)(GRAY_COLOR.r + colorTemp.r),
            (byte)(GRAY_COLOR.g + colorTemp.g),
            (byte)(GRAY_COLOR.b + colorTemp.b),
            255);
    }

    private void AddTriangle(VertexHelper vh, int realSegements)
    {
        int id = 1;
        for (int i = 0; i < realSegements; i++)
        {
            vh.AddTriangle(id, 0, id + 1);
            id++;
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

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out localPoint);
        return IsValid(localPoint);
    }

    private bool IsValid(Vector2 localPoint)
    {
        return localPoint.SqrMagnitude() <= checkRadius * checkRadius;
        //return GetCrossPointNum(localPoint, _vertexList) %2 == 1;
    }
}