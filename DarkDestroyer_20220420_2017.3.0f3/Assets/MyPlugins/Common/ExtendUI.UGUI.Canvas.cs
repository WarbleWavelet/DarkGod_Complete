/****************************************************
    文件：ExtendAsync.cs
	作者：lenovo
    邮箱: 
    日期：2023/8/18 10:51:52
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public static partial class ExtendCanvas 
{
    //Canvas
    //
    //CanvasScaler
    //
    //
    //
    //GraphicRaycaster
}
public static partial class ExtendCanvas //CanvasScaler
{




    public static void AdaptBgWhenExpandMode(this Canvas canvas)
    {
        Vector2 screenSize = canvas.CanvasSize();
        Sprite sprite;
        SpriteRenderer sr;
        Image image;
        //设置图片铺满整个屏幕
        //float expandScaleFactor = Mathf.Min(screenSize.x / inst.ImageWidth, screenSize.y / inst.ImageHeight);
        //float shrinkScaleFactor = Mathf.Max(screenSize.x / inst.ImageWidth, screenSize.y / inst.ImageHeight);
        //float resetScale = shrinkScaleFactor / expandScaleFactor;   //shrinkScaleFactor为我们想要的缩放，expandScaleFactor为我们默认设置的缩放
        //inst.m_Transform.localScale = new Vector3(resetScale, resetScale, 1);
    }


    public static CanvasScaler SetCanvasScaler(this CanvasScaler cs)
    {
        // 像素大小不变
        cs.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        {
            cs.scaleFactor = 1;
            cs.referencePixelsPerUnit = 100;//100表示每个单位对应100个像素。
        }
        //根据屏幕大小缩放
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        {
            cs.referenceResolution = new Vector2(800, 600);
            cs.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            {
                cs.matchWidthOrHeight = 0; //0-1
                cs.referencePixelsPerUnit = 100;

            }
            cs.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand; //ui 缩小到能在屏幕中显示完所有的ui元素.缺点:即背景图片无法铺满整个屏幕
            {
                cs.referencePixelsPerUnit = 100;
            }
            cs.screenMatchMode = CanvasScaler.ScreenMatchMode.Shrink;//部分ui超出屏幕能显示的区域
            {
                cs.referencePixelsPerUnit = 100;
                //......
            }

        }
        // 物理大小不变
        cs.uiScaleMode = CanvasScaler.ScaleMode.ConstantPhysicalSize;
        {

            cs.physicalUnit = CanvasScaler.Unit.Centimeters;//厘米
            cs.physicalUnit = CanvasScaler.Unit.Millimeters; //米
            cs.physicalUnit = CanvasScaler.Unit.Inches;//英寸
            cs.physicalUnit = CanvasScaler.Unit.Picas;//六分之一英寸
            cs.physicalUnit = CanvasScaler.Unit.Points;//72/1分英寸,磅
            cs.fallbackScreenDPI = 100;
            cs.defaultSpriteDPI = 100;
            cs.referencePixelsPerUnit = 100;
        }

        return cs;
    }
        /// <summary>
        /// UGUI系统中的一个组件,实现UI界面在不同分辨率下的自适应显示
        /// <br/>根据设定的参考分辨率和屏幕分辨率的比例，计算出缩放比例，并将其应用到画布上。
        /// </summary>

    public static CanvasScaler CanvasScalerSelf(this CanvasScaler cs)
    {
        Canvas canvas = Camera.main.transform.FindTop(GameObjectName.Canvas).gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;//CanvasScaler前置
        canvas.renderMode = RenderMode.ScreenSpaceCamera; //CanvasScaler前置
        canvas.renderMode = RenderMode.WorldSpace;
        //

        return cs;
    }
}



