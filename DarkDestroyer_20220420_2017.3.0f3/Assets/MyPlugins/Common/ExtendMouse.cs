/****************************************************
    文件：ExtendMouse.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/11 13:40:45
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendMouse
{
    /// <summary>
    ///  //跟随鼠标移动
    /// </summary>
    /// <param name="targetPos"></param>
    public static Vector3 FollowMouse(this Vector3 targetPos)
    {

            Vector3 screenPos = Camera.main.WorldToScreenPoint(targetPos);//转屏幕
            screenPos = Input.mousePosition - Camera.main.transform.position;//屏幕中操作
            targetPos = Camera.main.ScreenToWorldPoint(screenPos);//转世界

            return targetPos;
    }


    /// <summary>操纵物在world,输入在UGUI</summary>
    public static Transform FollowMouse(this Transform target)
    {
        Vector3 targetPos = target.position;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetPos);//转屏幕
        screenPos = Input.mousePosition - Camera.main.transform.position;//屏幕中操作
        targetPos = Camera.main.ScreenToWorldPoint(screenPos);//转世界
#if NET_4_7_2_OR_NEWER
        targetPos.SetZ(0);
#endif
        target.position = targetPos;

        return target;
    }

    public  static Vector3 LimitDistance(Vector3 movePoint, Vector3 basePoint, float maxDistance)//让movePoint始终在距离内
    {
        if (Vector3.Distance(movePoint, basePoint) > maxDistance)
        {
            Vector3 pos = (movePoint - basePoint).normalized;//归一
            movePoint = basePoint + maxDistance * pos;
        }

        return movePoint;
    }



}



