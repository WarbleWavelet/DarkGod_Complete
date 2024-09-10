/****************************************************
    文件：IsPointInQuad.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/27 22:6:56
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>判断一个点是否在矩形内部</summary>

public class IsPointInQuad : MonoBehaviour
{
    #region 属性

    #endregion

    #region 生命

    /// <summary>首次载入</summary>
    void Awake()
    {
            
    }
        

    /// <summary>Go激活</summary>
    void OnEnable ()
    {
            
    }

    /// <summary>首次载入且Go激活</summary>
    void Start()
    {
        /*
         * 00 10 11 10
         * 0.5,0.5 
         * 2,2
         */
       Debug.Log( IsInside(new Vector2(0,1), new Vector2(1,0),new Vector2(0.5f,0.5f)));
       Debug.Log( IsInside(new Vector2(0,1), new Vector2(1,0),new Vector2(2,2))  );
    }

        /// <summary>固定更新</summary>
    void FixedUpdate()
    {
            
    }

    void Update()
    {
            
    }

        /// <summary>延迟更新。适用于跟随逻辑</summary>
    void LateUpdae()
    {
            
    }

    /// <summary> 组件重设为默认值时（只用于编辑状态）</summary>
    void Reset()
    {
            
    }
      

    /// <summary>当对象设置为不可用时</summary>
    void OnDisable()
    {
            
    }


    /// <summary>组件销毁时调用</summary>
    void OnDestroy()
    {
            
    }
    #endregion 

    #region 系统

    #endregion 

    #region 辅助
    /// <summary>判断一个点是否在矩形内部</summary>
    public static bool IsInside(Vector2 leftTop, Vector2 rightBottom, Vector2 point)
    { 
        double x1 = leftTop.x;
        double y1 = leftTop.y; 
        double x4 = rightBottom.x; 
        double y4 = rightBottom.y;
        double x = point.x; 
        double y = point.y;


        //默认:1点在左上,4点在右下
        if (x <= x1)
        {//在矩形左侧
            return false;
        }
        if (x >= x4)
        {//在矩形右侧
            return false;
        }
        if (y >= y1)
        {//在矩形上侧
            return false;
        }
        if (y <= y4)
        {//在矩形下侧
            return false;
        }
        return true;
    }

    public static bool IsInside(
          double x1, double y1, double x4, double y4
        , double x2, double y2, double x3, double y3
        , double x,  double y)
    {
        //矩形边平行于x轴或y轴
        if (y1 == y2)
        {
            return IsInside( 
                  new Vector2( (float)x1, (float)y1)
                , new Vector2( (float)x4, (float)y4)
                , new Vector2( (float)x, (float)y));
        }
        //坐标变换，把矩形转成平行，所有点跟着动
        double a = Math.Abs(y4 - y3);
        double b = Math.Abs(x4 - x3);
        double c = Math.Sqrt(a * a + b * b);
        double sin = a / c;
        double cos = b / c;

        double x11 = cos * x1 + sin * y1;
        double y11 = -x1 * sin + y1 * cos;
        double x44 = cos * x4 + sin * y4;
        double y44 = -x4 * sin + y4 * cos;
        double xx = cos * x + sin * y;
        double yy = -x * sin + y * cos;
        //旋转完成，又变成上面一种平行的情况
        return IsInside( 
              new Vector2((float)x11, (float)y11)
            , new Vector2((float)x44, (float)y44)
            , new Vector2((float)xx,  (float)yy));
    }
    #endregion

}



