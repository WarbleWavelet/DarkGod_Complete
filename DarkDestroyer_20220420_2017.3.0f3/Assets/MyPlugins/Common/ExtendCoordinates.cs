/****************************************************
    文件：ExtendCoordinates.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/17 16:57:43
	功能：转坐标
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;

public static partial class ExtendCoordinates //鼠标
{
   public static Vector3 FollowMouseV(this Vector3 targetPos)//跟随鼠标移动
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetPos);//转屏幕
        screenPos = Input.mousePosition - Camera.main.transform.position;//屏幕中操作
        targetPos = Camera.main.ScreenToWorldPoint(screenPos);//转世界

        return targetPos;
    }
}
public static partial class ExtendCoordinates //数学
{
    /// <summary>左右手坐标系</summary>
    public enum ECoordinates
    {
        /// <summary>
        /// 左手坐标系 (Unity采用的)
        /// 正方体的左底前角
        /// x左右,y上下,z前后
        /// </summary>
        LEFT,
        /// <summary>
        /// 右手坐标系
        ///  正方体的左底后角
        ///  x左右,y上下,z前后
        /// </summary>
        RIGHT,
    }
    //Angle 泛概念的角度
    //Radian 弧度
    //Degree 与弧度相应的角度
    //直线坐标系：物体在一条直线上运动，只需建立直线坐标系。
    //平面直角坐标系：物体在某一平面内运动。
    //笛卡尔坐标系   CartesianCoordinates
    //极坐标系 PolarCoordinates
    //二维坐标系 TwoDimensionalCoordinates
    //三维坐标系 ThreeDimensionalCoordinates 

    interface ITwoDimensionalCoordinates {  }
    interface IThreeDimensionalCoordinates{  }
    class CartesianCoordinates :IThreeDimensionalCoordinates
    {
        float x;
        float y;
        float z;
    }
    class PolarCoordinates : ITwoDimensionalCoordinates
    {
        /// <summary>极点</summary>
        Vector2 _polarPoint;
        /// <summary>极轴</summary>
        Vector2 _polarAxis;
        /// <summary>单位长度</summary>
        float _unitLength;
        /// <summary>一般逆时钟</summary>
        EDir _dir{ get { return EDir.CONTRACLOCKWISE; } }

        /// <summary>
        ///  极径是距离
        /// </summary>
        Vector2 GetCoordinateByRadian(float polarRadius, float radianPolarAngle)
        {
            float x = polarRadius * radianPolarAngle.Cos();
            float y = polarRadius * radianPolarAngle.Sin();
            return new Vector2(x, y);
        }
        Vector2 GetCoordinateByAngle(float polarRadius, float degreePolarAngle)
        {
            float radianPolarAngle = degreePolarAngle.Degree2Radian();
            return GetCoordinateByRadian(polarRadius, radianPolarAngle);
        }

        
    }
}
public static partial class ExtendCoordinates//unity
{


    #region 2Vector3
    /** 
     * 
     *World*************************************************
     *(0,0,0)
     * transform.position/ transform.rotation
     * transform.position/ transform.rotation
     * 游戏场景的原点世界坐标为(0,0,0)     * 
     * 
     *local局部坐标*************************************************
     *   世界坐标<=局部坐标
     * 
     *屏幕坐标Screen*************************************************
     * (0-1920),x (0-1080)  
     * Screen.currentResolution=(一般)1920 x 1080 @ 60Hz
     *  也等于 (缩放为1就可以成立)
     *  int w = Mathf.Ceil(ScalableBufferManager.widthScaleFactor * Screen.currentResolution.width);
     *  int h = Mathf.Ceil(ScalableBufferManager.heightScaleFactor * Screen.currentResolution.height);
     *  new Vector2(w,h)=1920 , 1080
     * 
     * 
     * 屏幕的左下角为(0,0), 右上角为(Screen.width, Screen.height)
     * 与游戏画面的分辨率有关，注意是游戏屏幕画面的分辨率
     * 获取鼠标位置(Input.mousePosition)时的坐标即为屏幕坐标，返回的为Vector3(x, y,0)
     * 
     * 
     *观察坐标: EyeSpace*************************************************
     *在Game视图中的画面是由摄像机提供的，而基于某一个摄像机的坐标系即为观察坐标系
     *即把摄像机的位置作为原点位置
     * canvas的RectW和RecrH,Game窗口的Resolution设置
     * 大小等于(这里中能get)        
     * float x = canvas.GetComponent<RectTransform>().rect.xMax*2f; 
     * float y = canvas.GetComponent<RectTransform>().rect.yMax*2f;


     *视口坐标: View Port*************************************************  
     * 左下角为(0,0)，右上角为(1,1) 
     * 联系Camera的,Camera就有ViewportRect,所以设计分屏功能时可以通过设置摄像机所占的视口空间来控制
     * 
     *
     *Ray射线*************************************************
     *2D的时候设置z,才能射线检测
     *   
     *   

     */

    // 平面上是XZ坐标，Y不变
    /**
     * 
     */



}
public static partial class EyeCoordinate//世界局部坐标转换
{
    /// <summary>
    /// 世界坐标childPos转化为相对于parent的局部坐标
    /// <para />如果parent大小缩放了，childPos会受影响
    /// </summary>
    public static Vector3 World2Local(this Transform parent, Vector3 childPos)
    {
        return parent.InverseTransformPoint(childPos);
    }

    /// <summary>
    /// child的世界坐标转化为相对于parent的局部坐标
    /// <para />如果parent大小缩放了，childPos会受影响
    /// </summary>
    public static Vector3 World2Local(this Transform parent, Transform child)
    {
        return parent.World2Local(child.position);
    }

    /// <summary>
    /// child的世界坐标转化为相对于parent的局部坐标
    /// <para />如果parent大小缩放了，childPos会受影响
    /// </summary>
    public static Vector3 World2Local<T>(this Transform parent, T t) where T : Component
    {
        return parent.World2Local(t.transform.position);
    }
}

/// <summary>疑问unity</summary>
public static  partial class EyeCoordinate
{
    //public static Rect Rect { get 
    //{
    //    //数值上相等
    //    //float x = canvas.GetComponent<RectTransform>().rect.xMax * 2f;
    //    //float y = canvas.GetComponent<RectTransform>().rect.yMax * 2f;
    //    int w =  (int) Mathf.Ceil(ScalableBufferManager.widthScaleFactor * Screen.currentResolution.width);
    //    int h = (int) Mathf.Ceil(ScalableBufferManager.heightScaleFactor * Screen.currentResolution.height);
    //    return new Rect(  new Vector2(0,0),new Vector2(w,h));
    //}
}
public static partial class ExtendCoordinates //SWV
{

    public static Vector3 View2World(this Vector3 from, Camera camera = null)
    {
        camera = Camera.main.MainCameraIfNull();
        return camera.ViewportToWorldPoint(from);
    }


    public static Vector3 View2Screen(this Vector3 from, Camera camera = null)
    {
        camera = Camera.main.MainCameraIfNull();
        return camera.ViewportToScreenPoint(from);
    }


    public static Vector3 World2View(this Vector3 from, Camera camera = null)
    {
        camera = Camera.main.MainCameraIfNull();
        return camera.WorldToViewportPoint(from);
    }


    public static Vector3 World2Screen(this Vector3 from, Camera camera = null)
    {
        camera = Camera.main.MainCameraIfNull();
        return camera.WorldToScreenPoint(from);
    }

    public static Vector3 Screen2View(this Vector3 from, Camera camera = null)
    {
        camera = Camera.main.MainCameraIfNull();
        return camera.ScreenToViewportPoint(from);
    }

    public static Vector3 Screen2World(this Vector3 from, Camera camera = null)
    {
        camera = Camera.main.MainCameraIfNull();
        return camera.ScreenToWorldPoint(from);
    }
    #endregion


    #region V2
    public static Vector2 View2World(this Vector2 from, Camera camera = null)
    {
        camera = Camera.main.MainCameraIfNull();
        return camera.ViewportToWorldPoint(from);
    }


    public static Vector2 View2Screen(this Vector2 from, Camera camera = null)
    {
        camera = Camera.main.MainCameraIfNull();
        return camera.ViewportToScreenPoint(from);
    }


    public static Vector2 World2View(this Vector2 from, Camera camera = null)
    {
        camera = Camera.main.MainCameraIfNull();
        return camera.WorldToViewportPoint(from);
    }


    public static Vector2 World2Screen(this Vector2 from, Camera camera = null)
    {
        camera = Camera.main.MainCameraIfNull();
        return camera.WorldToScreenPoint(from);
    }

    public static Vector2 Screen2View(this Vector2 from, Camera camera = null)
    {
        camera = Camera.main.MainCameraIfNull();
        return camera.ScreenToViewportPoint(from);
    }

    public static Vector2 Screen2World(this Vector2 from, Camera camera = null)
    {
        camera = Camera.main.MainCameraIfNull();
        return camera.ScreenToWorldPoint(from);
    }
    #endregion

    #region 2Ray


    public static Ray Screen2Ray(this Vector3 from)
    {
        return Camera.main.ScreenPointToRay(from);
    }

    public static Ray View2Ray(this Vector3 from)
    {
        return Camera.main.ViewportPointToRay(from);
    }
    #endregion
}


public static partial class ExtendCoordinates 
{
#if NET_4_7_2_OR_NEWER

    /// <summary>比如2D视图,鼠标点哪里,就朝向哪里,走向哪里</summary>
    public static Vector3 Screen2World(ref this Vector3 v, Camera camera = null)
    {
        if (camera == null)
        {
             camera = Camera.main;
        }

        Vector3 cur = v;
        Vector3 after=camera.ScreenToWorldPoint(cur);
        v=after;
        return v;
    }
#endif




}




