/****************************************************
    文件：ExtendRotate.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/20 19:58:31
	功能：
*****************************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;


public static partial class ExtendRotate  //转弯
{
   public static void Turn(this Transform t, EDir dir)//x，y偏移量，旋转方向
    {
        int step = 1;//步长
        int x;int y;
        switch (dir)
        {
            case EDir.Up:
                {
                    x = 0;
                    y = step;
                   t. Rotate(0);
                }
                break;
            case EDir.Left:
                {
                    x = -step;
                    y = 0;
                    t.Rotate(90);
                }
                break;
            case EDir.Down:
                {
                    x = 0;
                    y = -step;
                    t.Rotate(180);
                }
                break;
            case EDir.Right:
                {
                    x = step;
                    y = 0;
                    t.Rotate(-90);
                }
                break;
        }

    }


    /// <summary>0上,90左,180下,-90右</summary> 
    public static  void Rotate(this Transform t, int z)//旋转头部
    {
      t.localRotation = Quaternion.Euler(0, 0, z);
    }
}
public static partial class ExtendRotate  //随机数转弯
{
      #if NET_4_7_OR_NEWER
  public static  EDir RandDir((int, int, EDir)[] cbs, int min, int max)
    {
        (int, int, EDir) cur;
        int n = UnityEngine.Random.Range(min, max);
        EDir dir;
        for (int i = 0; i < cbs.Length; i++)
        {
            cur = cbs[i];
            if (cur.Item1 <= n && n < cur.Item2)
            {
                return cur.Item3;
            }
            if (cur.Item1 == cur.Item2 && n == cur.Item1)
            {
                return cur.Item3;
            }
        }
        return EDir.None;
    }


    /// <summary></summary>
    public static  (int, int) RandDir((int, int, (int, int))[] cbs, int min, int max)
    {
        (int, int, (int, int)) cur;
        int n = UnityEngine.Random.Range(min, max);
        EDir dir;
        for (int i = 0; i < cbs.Length; i++)
        {
            cur = cbs[i];
            if (cur.Item1 <= n && n < cur.Item2)
            {
                return cur.Item3;
            }
            if (cur.Item1 == cur.Item2 && n == cur.Item1)
            {
                return cur.Item3;
            }
        }
        return (0, 0);
    }
#endif
}
public static partial class ExtendRotate //顺着轴旋转
{
    /// <summary>那个飞机图的术语</summary>
    public enum ERotate
    {
        /// <summary>俯仰,绕x轴</summary>
        Pitch,
        /// <summary>偏航,绕y轴</summary>
        Yaw,
        /// <summary>翻滚,绕z轴</summary>
        Roll,
    }
    /// <summary>左右旋转</summary>
    public static void RotateLeftRight(this Transform t,float value)
    {
        t.eulerAngles = new Vector3(t.eulerAngles.x, value, t.eulerAngles.z);
    }

    /// <summary>左右旋转</summary>
    public static void RotateUpDown(this Transform t, float value)
    {
        t.eulerAngles = new Vector3( value,t.eulerAngles.y, t.eulerAngles.z);
    }


    /// <summary>左右旋转</summary>
    public static void RotateRoll(this Transform t, float value)
    {
        t.eulerAngles = new Vector3(t.eulerAngles.x, t.eulerAngles.y, value);
    }


    /// <summary>迎着轴针,右旋为正,左为负
    /// 注意z轴针想里面,所以视觉上是左旋为正
    /// </summary> 
    public static void Rotate(this Transform t, float value, ERotate rotate)
    {

        switch (rotate)
        {
            case ERotate.Pitch :
                {
                    t.eulerAngles = new Vector3(value,t.eulerAngles.x, t.eulerAngles.y);
                }
                break;
            case ERotate.Yaw:
                {
                    t.eulerAngles = new Vector3( t.eulerAngles.x,value, t.eulerAngles.z);
                }
                break;
            case ERotate.Roll:
                {
                    t.eulerAngles = new Vector3( t.eulerAngles.x, t.eulerAngles.y  ,value);
                }
                break;
            default:
                {

                }
                break;
        }

    }
}
public static partial class ExtendRotate
{
    /// <summary>
    /// 旋转枪口
    /// MM=min max最值
    /// </summary>
    public static void RotateMuzzle(this Transform t, Vector2 xRotateMM, Vector2 yRotateMM)//
    {
        float minXDir = xRotateMM.x;     
        float maxXDir = xRotateMM.y;
        float minYDir = yRotateMM.x;
        float maxYDir = yRotateMM.y;
        //限制,相当于手指不能能滑动到屏幕外
        float afterMounseX = Mathf.Clamp(Input.mousePosition.x, 0, Screen.width);
        float afterMounseY = Mathf.Clamp(Input.mousePosition.y, 0, Screen.height);
        //
        //第一句，当旋转X轴的值，等于，鼠标坐标在屏幕坐标系上的比例 乘以 观察到的eulerAngles.y最值
        //（为什么是y，因为只有y在变化）
        float rotateXAxis = (maxYDir / Screen.height) * afterMounseY;//上下，20=>(0,20)//上下靠旋转X轴
        float rotateYAxis = (maxXDir / Screen.width) * afterMounseX;//左右，70=>(0,70) //左右靠旋转y轴
                                                                    //(0,20)转(-60,20)；(0,70)转(-70,70)。屏幕坐标转世界坐标。去除数字太麻烦了，做过一次，放弃了
        rotateXAxis = -((Mathf.Clamp(rotateXAxis, minYDir, maxYDir)) * 4 - 20);
        rotateYAxis = (Mathf.Clamp(rotateYAxis, minXDir, maxXDir) - 35) * 2;
        //赋值
        t.eulerAngles = new Vector3(rotateXAxis, rotateYAxis, 0f);
    }



}




