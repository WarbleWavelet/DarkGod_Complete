/****************************************************
    文件：GunType.cs
	作者：lenovo
    邮箱: 
    日期：2024/4/22 19:20:28
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary></summary>
public enum GunType
{
    /// <summary>霰弹</summary>
    Shot,
    /// <summary>火箭</summary>
    Rocket,
    /// <summary>激光</summary>
    Laser,
}
public class Gun
{
    public static Vector2 GetDir2D(Vector2 muzzlePos)
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 dir = (mousePos - muzzlePos).normalized;
        return dir;
    }
}
public class RandAngleGun
{

    public static Quaternion GetDir(float left,float right, Vector3 axis)
    {
        float offset = Random.Range(left,right);
        return axis.AngleBase(offset);
    }
}
/// <summary>霰弹</summary>
public class ShotGun
{
    /// <summary>
    /// 得到角度
    /// <br/>axis朝向,一般是是Forward
    /// </summary>
    public static Quaternion[] GetDir(float bulletAngle,int bulletNum,Vector3 axis)
    {
        Quaternion[] dirArr=new Quaternion[bulletNum];
        int median = bulletNum / 2;
        for (int i = 0; i < bulletNum; i++)
        {
            if (median.IsOdd())
                dirArr[i] = axis.AngleBase(bulletAngle * (i - median+0.0f));//这样写对比明显
            else
                dirArr[i] = axis.AngleBase(bulletAngle * (i - median+0.5f));//偶数,中央线肯定左右各一个
        }
        return dirArr;
    }
}

public class RocketGun
{

}



