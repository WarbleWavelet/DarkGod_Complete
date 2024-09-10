/****************************************************
    文件：ExtendComponent.Transform.RectTransform.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/4 16:2:51
	功能：
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public static partial class ExtendRectTransform
{
    public static void Example(Transform t,GameObject go,RectTransform rect)
    {
        t.Rect();
        go.Rect();

        
    }

}

public static partial class ExtendRectTransform
{
    public static Slider Slider(this RectTransform rect)
    { 
        Slider slider=rect.GetComponentInChildren<Slider>();
        if (slider == null)
        {
            throw new System.Exception("异常");
        }
        return slider;  
    }

    public static Text Text(this RectTransform rect)
    {
        Text text = rect.GetComponentInChildren<Text>();
        if (text == null)
        {
            throw new System.Exception("异常");
        }
        return text;
    }

    public static Image Image(this RectTransform rect)
    {
        Image image = rect.GetComponentInChildren<Image>();
        if (image == null)
        {
            throw new System.Exception("异常");
        }
        return image;
    }

    public static Button Button(this RectTransform rect)
    {
        Button button = rect.GetComponentInChildren<Button>();
        if (button == null)
        {
            throw new System.Exception("异常");
        }
        return button;
    }

}
public static partial class ExtendRectTransform
{






    /// <summary>
    /// 获取UI的长宽，可以使用sizeDelta，但是Unity官方对于sizeDelta是这样解释的：Anchors在同一点的时候，sizeDelta相当于获取长宽，但是Anchors不在同一点时，表示的只是UI真实大小比Anchors矩形大多少
    /// <para/>  offsetMax = ui矩形右上角坐标 - 锚点矩形右上角坐标
    /// <br/>  offsetMin = ui矩形左下角坐标 - 锚点矩形左下角坐标
    /// <br/>  sizeDelta = offsetMax - offsetMin;
    /// <br/>  sizeDelta = ((ui矩形右上角坐标 - 锚点矩形右上角坐标) - (ui矩形左下角坐标 - 锚点矩形左下角坐)
    /// <para/> 右上相对坐标-左下相对坐标=相对值
    /// </summary>     
    public static Vector2 SizeDelta(this RectTransform rect)
    {
        return rect.sizeDelta;
    }

    public static RectTransform SetSizeDelta(this RectTransform rect, Vector2 v)
    {
        rect.sizeDelta = v;
        return rect;
    }

    public static RectTransform SetSizeDeltaX(this RectTransform rect, float x)
    {
        var sizeDelta = rect.sizeDelta;
        sizeDelta.x = x;
        rect.sizeDelta = sizeDelta;
        return rect;
    }

    public static RectTransform SetSizeDeltaY(this RectTransform rect, float y)
    {
        var sizeDelta = rect.sizeDelta;
        sizeDelta.y = y;
        rect.sizeDelta = sizeDelta;
        return rect;
    }

    public static float SizeDeltaX(this RectTransform rect)
    {
        return rect.sizeDelta.x;
    }
    public static float SizeDeltaY(this RectTransform rect)
    {
        return rect.sizeDelta.y;
    }
}


public static partial class ExtendRectTransform
{
          #if NET_4_7_2_OR_NEWER
    /// <summary>anchoredPosition是属性，不能ref out</summary>
    public static RectTransform SetAnchoredPosX(this RectTransform rect,float x)
    {
        var pos = rect.anchoredPosition;
        pos.SetX(x);
        rect.anchoredPosition = pos;
        return rect;
    }


    /// <summary>anchoredPosition是属性，不能ref out</summary>
    public static RectTransform SetAnchoredPosY(this RectTransform rect, float y)
    {
        var pos = rect.anchoredPosition;
        pos.SetY(y);
        rect.anchoredPosition = pos;
        return rect;
    }
#endif
}
public static partial class ExtendRectTransform
{
    public static RectTransform Rect(this Transform trans)
    {
        return trans.GetComponent<RectTransform>();
    }

    public static RectTransform Rect(this GameObject go)
    {
        return go.GetComponent<RectTransform>();
    }
    public static RectTransform Reset(this RectTransform rect)
    {
        rect.localRotation = Quaternion.identity ;
        rect.localPosition = Vector3.zero;
        rect.localScale = Vector3.one;
        //
        rect.anchorMin = Vector2Inherit.half;
        rect.anchorMax = Vector2Inherit.half;
        rect.anchoredPosition = Vector3.zero;
        //
        rect.sizeDelta = Vector2Inherit.hundred;
        rect.pivot = Vector2Inherit.half;

        return rect;
    }


    /// <summary>
    /// UI顶点与锚点的相对坐标（左下相对左下，右上相对右上）
    ///<para /> https://blog.csdn.net/qq_42004290/article/details/122741181
    /// </summary>
    public static RectTransform Offset(this RectTransform rect,Vector2 v1,Vector2 v2)
    {
        rect.offsetMin = v1;
        rect.offsetMax = v2;
        return rect;
    }
    /// <summary>平铺开</summary>
    public static RectTransform Stretch(this RectTransform rect)
    {
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        rect.localScale = Vector3.one;

        return rect;
    }
      #if NET_4_7_2_OR_NEWER
    /// <summary>平铺开</summary>
    public static RectTransform StretchAnchor(this RectTransform rect)
    {
        //Debug.LogFormat($"rect.sizeDelta:{rect.sizeDelta},ScreenUtil.Size():{ScreenUtil.Size()}");
        Vector2 minSize = (rect.position.V3ToV2() - rect.sizeDelta * 0.5f )/ ScreenUtil.CurResolution();  
        Vector2 maxSize = (rect.position.V3ToV2() + rect.sizeDelta * 0.5f ) / ScreenUtil.CurResolution();
        if (minSize.x < 0 || maxSize.y < 0 || maxSize.x < 0 || maxSize.y < 0)
        {
            Debug.LogError("错了");
        }
        rect.anchorMin = minSize;                                                 
        rect.anchorMax = maxSize;
        rect.sizeDelta = Vector2.zero;//不加会变大

        //Debug.LogFormat($"rect.anchorMin:{rect.anchorMin},rect.anchorMax:{rect.anchorMax}");  

        return rect;
    }
#endif
}
public static partial class ExtendRectTransform //案例
{
    /// <summary>基于参照物baseRect（和放置物不同面板placeParent）和间隔offset，上下左右放置物体placeRect</summary>
    public static Component BaseBy(this Component t
        , RectTransform placeParent
        , RectTransform baseRect
        , EDir dir
        , float offset) 
    {
        RectTransform placeRect = t.GetComponent<RectTransform>();
        LayoutElement placeLayout = placeRect.GetComponent<LayoutElement>();//placeRect要有组件。
        //
        float horizontal_LocalX = 0f;
        float horizontal_LocalY=0f;
        float vertical_LocalY=0f;
        float vertical_LocalX = 0f;
        if (dir == EDir.LEFT || dir == EDir.RIGHT)
        { 
            float base_HalfW = Mathf.Abs(baseRect.rect.xMin);
            float panel_HalfW = placeParent.rect.width / 2;
            float text_AfterW = panel_HalfW - (base_HalfW + offset);
            placeLayout.preferredWidth = (text_AfterW < placeParent.rect.width) ? text_AfterW : -1;
            //
             horizontal_LocalX = (panel_HalfW - text_AfterW / 2);
             horizontal_LocalY = placeRect.transform.World2Local(baseRect).y;        
        }
        if (dir == EDir.UP || dir == EDir.DOWN)
        {
            float base_HalfH = Mathf.Abs(baseRect.rect.yMin);
            float panel_HalfH = placeParent.rect.height / 2;
            float text_AfterH = panel_HalfH - (base_HalfH + offset);
            placeLayout.preferredWidth = (text_AfterH < placeParent.rect.width) ? text_AfterH : -1;
            //
             vertical_LocalY = (panel_HalfH - text_AfterH / 2);
             vertical_LocalX = placeRect.transform.World2Local(baseRect).x;
        }


        switch (dir)
        {

            case EDir.LEFT:
                {
                    placeRect.transform.localPosition = new Vector3(-horizontal_LocalX, horizontal_LocalY);
                }
                break;
            case EDir.RIGHT:
                {
                    placeRect.transform.localPosition = new Vector3(horizontal_LocalX, horizontal_LocalY);
                }
                break;
            case EDir.UP:
                {
                    placeRect.transform.localPosition = new Vector3(vertical_LocalX, vertical_LocalY);
                }
                break;
            case EDir.DOWN:
                {
                    placeRect.transform.localPosition = new Vector3(vertical_LocalX, -vertical_LocalY);
                }
                break;
            default: break;
        }
        return placeRect;
    }

}



public static partial class ExtendRectTransform //rect
{
    //Transform 的本地空间中计算的矩形。
    //Unity 会自动将这些内容附加到 UI 元素。可在 Inspector 中操作矩形的各个方面，如位置、尺寸、旋转和缩放。 这在脚本中是只读的。

    public static void Example_Rect(RectTransform rect)
    { 
         rect.DrawText();//在OnGUI中使用
    
    }
    public static void ToString_Common(this RectTransform rect)
    {
        string str = "";
        str += "位置" + rect.rect.position ;
        str += "\n 中心位置" + rect.rect.center;
        str += "\n 矩形的X坐标" + rect.rect.x;
        str += "\n 矩形的Y坐标" + rect.rect.y; 
        str += "\n 最小点（左下）的位置" + rect.rect.min ;
        str += "\n 最大点（右上）的位置" + rect.rect.max ;
        str += "\n 矩形的最小X坐标" + rect.rect.xMin ;
        str += "\n 矩形的最大X坐标" + rect.rect.xMax ;
        str += "\n 矩形的最小Y坐标" + rect.rect.yMin ;
        str += "\n 矩形的最大Y坐标" + rect.rect.yMax ;
        str += "\n 宽" + rect.rect.width ;
        str += "\n 高" + rect.rect.height ;
        str += "\n 宽和高" + rect.rect.size ;
        GUI.Label(new Rect(Screen.width / 2f
            , Screen.height / 2f
            , Screen.width 
            , Screen.height), "rect:\n"+str);
    }

    /// <summary>在OnGUI中使用</summary>
    public static void DrawText(this RectTransform rect)
    {
        GUI.Label(new Rect(Screen.width / 2f, Screen.height / 2f, 150, 80), "Rect : " + rect.rect);
        //GUI.Label(new RectTransform(20, 20, 150, 80), "RectTransform : " + rect.rect); //左上角，难看到
    }

}
public static partial class ExtendRectTransform //Anchor，轴心
{
    public static void Example_Anchor(RectTransform rect)
    {
        rect.SetAnchor(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
    }

    //Anchor，锚点
    //锚定方式就是最上面的，左上，左下，中间的那个可视化AnchorPresets
    public static RectTransform SetAnchorMin(this RectTransform rect, Vector2 min)
    {
        if (IsLegalAnchorVector2(min) == false)
        {
            throw new System.Exception("Anchor异常");
        }
        rect.anchorMin = min;
        return rect;
    }
    public static RectTransform SetAnchorMax(this RectTransform rect, Vector2 max)
    {
        if (IsLegalAnchorVector2(max) == false)
        {
            throw new System.Exception("Anchor异常");
        }
        rect.anchorMax = max;
        return rect;
    }
    public static RectTransform SetAnchor(this RectTransform rect, Vector2 min, Vector2 max)
    {
        if (IsLegalAnchorVector2(min) == false || IsLegalAnchorVector2(max))
        {
            throw new System.Exception("Anchor异常");
        }
        rect.anchorMin = min;
        rect.anchorMax = max;
        return rect;
    }

    public static RectTransform AnchorPosX(this RectTransform selfRectTrans
    , float anchorPosX)
    {
        var anchorPos = selfRectTrans.anchoredPosition;
        anchorPos.x = anchorPosX;
        selfRectTrans.anchoredPosition = anchorPos;
        return selfRectTrans;
    }

    public static RectTransform AnchorPosY(this RectTransform selfRectTrans
        , float anchorPosY)
    {
        var anchorPos = selfRectTrans.anchoredPosition;
        anchorPos.y = anchorPosY;
        selfRectTrans.anchoredPosition = anchorPos;
        return selfRectTrans;
    }


    #region 辅助
    static bool IsLegalAnchorVector2(Vector2 pos)
    {
        if (    pos.x <= 1f && pos.x >= 0f)
        {
            if (pos.y <= 1f && pos.y >= 0f)
            { 
                 return true;
            }
           
        }
        return false;
    }
    #endregion


}


public static partial class ExtendRectTransform
{
    //此 RectTransform 中围绕其旋转的标准化位置。
    public static RectTransform SetPivot(this RectTransform rect,Vector2 pivot )
    { 
        rect.pivot=pivot;
        return rect;
    }

    /// <summary>
    /// 轴心与自身的相对关系Vector2(0f,0f)左下=>Vector2(1f,1f)右上
    /// <para />可以超出，没有意义
    /// <para />设置的是Pivot，而PosX，PosY保持不变，所以变化的是物体的视觉位置（看上去动了）
    /// </summary>
    public static RectTransform SetPivot(this RectTransform rect,EDir dir)
    {
        switch ( dir )
        {
            case EDir.LEFTTOP :      rect.pivot = new Vector2(0f,1f); break;
            case EDir.LEFT :         rect.pivot = new Vector2(0f,0.5f); break;
            case EDir.LEFTBOTTOM :   rect.pivot = new Vector2(0f,0f); break;
            case EDir.TOP :          rect.pivot = new Vector2(0.5f,1f); break;
            case EDir.CENTER :       rect.pivot = new Vector2(0.5f,0.5f); break;
            case EDir.BOTTOM :       rect.pivot = new Vector2(0.5f,0f); break;
            case EDir.RIGHTTOP :     rect.pivot = new Vector2(1f,1f); break;
            case EDir.RIGHT :        rect.pivot = new Vector2(1f,0.5f); break;
            case EDir.RIGHTBOTTOM :  rect.pivot = new Vector2(1f,0f); break;
            default: throw new System.Exception("SetPivot异常"); break;
        }

        return rect;
    }
}
public static partial class ExtendRectTransform
{

    public static Vector2 GetPosInRootTrans(this RectTransform selfRectTransform
        , Transform rootTrans)
    {
        return RectTransformUtility
            .CalculateRelativeRectTransformBounds(rootTrans, selfRectTransform)
            .center;
    }





    public static Vector2 GetWorldSize(this RectTransform selfRectTrans)
    {
        return RectTransformUtility
            .CalculateRelativeRectTransformBounds(selfRectTrans)
            .size;
    }




}