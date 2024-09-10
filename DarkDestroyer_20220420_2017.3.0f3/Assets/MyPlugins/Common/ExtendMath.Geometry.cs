/****************************************************
    文件：ExtendMath.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/21 22:42:47
	功能：几何(处理向量积时,
          明白了几何是领域,
          数学，物理，力学是应用学科)
*****************************************************/

//using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExtendGeometry;
using Random = UnityEngine.Random;


public static partial class ExtendGeometry   //三角函数 多个参数 TrigonometricFunction
{
    /// <summary>正弦 弧度</summary>
    public static float Sin(float a, float b)
    {
        return  a.Sin()*b.Cos() + a.Cos()*b.Sin();
    }

    /// <summary>余弦 弧度</summary>
    public static float Cos(float a, float b)
    {
        return a.Cos() * b.Cos() - a.Sin() * b.Sin();
    }

    /// <summary>正切 弧度</summary>
    public static float Tan(float a, float b)
    {
        return (a.Tan() + b.Tan()) / (1-a.Tan() * b.Tan());
    }
}
public static partial class ExtendGeometry   //三角函数
{

    #region sin cos tan以及arc
    /// <summary>正弦 弧度</summary>
    public static float Sin(this float radian)
    {
        return Mathf.Sin(radian);
    }


    public static float Asin(this float value)
    {
        float radian= Mathf.Asin(value);
        return radian;
    }


    /// <summary>余弦 弧度</summary>
    public static float Cos(this float radian)
    {
        return Mathf.Cos(radian);
    }

    public static float Acos(this float value)
    {
        float radian  = Mathf.Acos(value);
        return radian;
    }


    /// <summary>正切 弧度</summary>
    public static float Tan(this float radian)
    {
        return Mathf.Tan(radian);
    }

    public static float Atan(this float value)
    {
        float radian = Mathf.Atan(value);
        return radian;
    }

    public static float Arctan(this float value)
    {
        float radian = value.Atan();
        return radian;
    }
    #endregion


    #region sin cos tan的倒数
    /// <summary>余割 1/sin</summary>
    public static float Csc(this float radian)
    {
        return radian.Sin().Reciprocal();
    }

    /// <summary>正割 1/cos</summary>
    public static float Sec(this float radian)
    {
        return radian.Cos().Reciprocal();
    }

    /// <summary>余切 1/tan</summary>
    public static float Cot(this float radian)
    {
        return radian.Tan().Reciprocal();
    }


    #endregion  


}
public static partial class ExtendGeometry   //三角函数 double
{

    /// <summary>正弦 弧度</summary>
    public static float Sin(this double radian)
    {
        return Mathf.Sin((float)radian);
    }


    public static float Asin(this double value)
    {
        float radian = Mathf.Asin((float)value);
        return radian;
    }


    /// <summary>余弦 弧度</summary>
    public static float Cos(this double radian)
    {
        return Mathf.Cos((float)radian);
    }

    public static float Acos(this double value)
    {
        float radian = Mathf.Acos((float)value);
        return radian;
    }


    /// <summary>正切 弧度</summary>
    public static float Tan(this double radian)
    {
        return Mathf.Tan((float)radian);
    }

    public static float Atan(this double value)
    {
        float radian = Mathf.Atan((float)value);
        return radian;
    }

    public static float Arctan(this double value)
    {
        float radian = value.Atan();
        return radian;
    }

}

public static partial class ExtendGeometry //Vector2 象限
{
    /// <summary>象限,平面直角坐标系（笛卡尔坐标系）</summary>
    public enum EQuadrant
    { 
        NULL=0,
        FIRST=1,
        SECOND=2,
        THIRD=3,
        FOUTH=4
    }
    public static EQuadrant Quadrant_Angle(this Vector2 v)
    {
        Vector2.Angle(v, Vector2.right);
        return EQuadrant.NULL;
    }
    public static EQuadrant Quadrant_SignedAngle(this Vector2 v)
    {
        float q = Vector2.SignedAngle( v, Vector2.right);
        //第一象限：0 ~-90
        //第二象限：-90 ~-180
        //第三象限：180 ~90
        //第四象限：90 ~0
        if(q.Between(-90,0)) return EQuadrant.FIRST;
        if(q.Between(-90,-180)) return EQuadrant.SECOND;
        if(q.Between(180,90)) return EQuadrant.THIRD;
        if(q.Between(90,0)) return EQuadrant.FOUTH;
        return EQuadrant.NULL;

    }
}

public static partial class ExtendGeometry //Angle=Degree,Radian,四元数,Vector2,Vector3 
{
    //简单的说: 点乘判断角度，叉乘判断方向。
    //形象的说: 当一个敌人在你身后的时候，
    //点乘得到你当前的面朝向的方向和你到敌人的方向的所成的角度大小
    //叉乘可以判断你是往左转还是往右转更好的转向敌人
    public static float Vector2To(this float angle, EAngleUnitType angleType)
    {

        throw new System.Exception("异常");
    }


    public static float Angle2Degree(this float angle, EAngleUnitType angleType)
    {

        float degree;
        switch (angleType)
        {
            case EAngleUnitType.DEGREER:
                {
                    degree = angle;
                }
                break;
            case EAngleUnitType.RADIAN:
                {
                    degree = angle.Radian2Degree();
                }
                break;
            default: throw new System.Exception("异常");


        }
        return degree;
    }

    public static float Angle2Radian(this float angle, EAngleUnitType angleType)
    {

        float radian;
        switch (angleType)
        {
            case EAngleUnitType.DEGREER:
                {
                    radian = angle.Degree2Radian();
                }
                break;
            case EAngleUnitType.RADIAN:
                {
                    radian = angle;
                }
                break;
            default: throw new System.Exception("异常");


        }
        return radian;
    }
    /// <summary>角度转弧度
    /// <br/>360/2PI</summary>
    public static float Degree2Radian(this float degree)
    { 
      float  radian= degree * Mathf.Deg2Rad;
       // float radian = degree * (2 * Mathf.PI / 360.0f);
        return radian;
    }


    /// <summary>弧度转角度
    /// <br/>360/2PI</summary>
    public static float Radian2Degree(this float radian)
    {
        float degree= radian * Mathf.Rad2Deg;
        // float degree = radian * (360.0f/2 * Mathf.PI  );
        return degree;
    }
}

public static partial class ExtendGeometry  //圆
{


    /// <summary>以某点为圆心，沿着它一圈圆形地实例物体</summary>
    public static void InstantiateByCircle(this GameObject prefab
        , UnityEngine.Transform centerTrans
        , int numberOfObjects = 20
        , float radius = 200f)//200UGUI,5world
    {

        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = centerTrans.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
           GameObject.Instantiate(prefab, pos, rot).SetParent(centerTrans);
        }

    }


    /// <summary>前置在两个整数之间（包括整数）</summary>
    public static int Clamp(this int value, int min,int max)
    {
        if (value <= min)
        {
            value = min;
        }
        if (value >= max)
        {
            value = max;
          
        }   
        return value;
    }

}

public static partial class ExtendGeometry//抛物线
{
    //https://segmentfault.com/l/1190000018336439?u_atoken=f31ea849-c257-4cd1-9456-420acfcc88ca&u_asession=01UKvey9sCLWYSWr9GkHyc0A3SKRhOG3D-b0XkM_aLYITOu24WsUkgKIfIZFUOPUHSt2mIJH9znX2lgQFxweOzU9sq8AL43dpOnCClYrgFm6o&u_asig=05L1BB7kVEHlXh2i084REUj-TMF8b6WkMYeJPXx8qUvKNtKR9LQqNXaCGoyiRPUyPiCv3USIUqojtmAfpXgqMGlbyxJvPy22_8IuMMsjuhn0MCBGKDvLS0zzIAOHrx6_VYLpjWH5zzRq3jfCXPefSZtxrfRjlD99S-xTONXUsfRkmKgHAmcPgCrIroNKVtc-BaksmHjM0JOodanL5-M1Qs1RzGr5Ic_z7G-XR9vKzPE20fjB8pLehTGmwu8kg03v1geXvMrMnDPH61DXRYqTRH73N_VXolUsldXTTIzu2-ux3UpLHxH1iRKZmnjAu0Zefw&u_aref=5wOphva1FZ0LwHniBwIARJJQU3E%3D
    //碧蓝航线战列炮，求圆心，半径，来确定炮弹的轨迹
    // 求导弹初始速度
    public static Vector3 InitVelocity(Vector3 from, Vector3 to, float height = 10, float gravity = -9.8f)
    { 
        float topY = Mathf.Max(from.y, to.y) + height;
        float d1 = topY - from.y;
        float d2 = topY - to.y;
        float g2 = 2 / -gravity;
        float t1 = Mathf.Sqrt(g2 * d1);
        float t2 = Mathf.Sqrt(g2 * d2);
        float t = t1 + t2;
        float vX = (to.x - from.x) / t;
        float vZ = (to.z - from.z) / t;
        float vY = -gravity * t1;
        Vector3 v0 = new Vector3(vX, vY, vZ);
        return v0;
    }


    // 求下一帧导弹的位置：
    public static Vector3 NextPosition(Vector3 position, Vector3 velocity, float gravity, float time)
    {
        float dY = 0.5f * gravity * time * time;
        return position + velocity * time + new Vector3(0, dY, 0);
    }

}

public static partial class ExtendMGeometry  //矩阵 的运算  接口
{
    #region 加减乘除
    /// <summary>加</summary>
    public interface IAdd<T>
    {
        T Add(T t1, T t2);
    }
    /// <summary>减</summary>
    public interface ISub<T>
    {
        T Sub(T t1, T t2);
    }

    /// <summary>乘</summary>
    public interface IMultiply<T>
    {
        T Multiply(T t1, T t2);
    }

    /// <summary>除</summary>
    public interface IDivide<T>
    {
        T Divide(T t1, T t2);
    }
    #endregion


    #region 各种积


    #region 说明
    /// <summary>标积/内积/数量积/点积
    ///  向量a在向量b方向上的投影与向量b的模的乘积
    /// </summary>
    public interface IDotProduct<T>
    {
        T DotProduct(T t1, T t2);
    }
    public interface IInnerProduct<T>
    {
        T InnerProduct(T t1, T t2);
    }
    #endregion

    #region 说明
    /// <summary>矢积/外积/向量积/叉积
    /// <br/>数学中又称外积、叉积
    /// <br/>物理中称矢积、叉乘
    /// <radian/>	l×m=n，其中|n|=|l||m|·sinθ，c的方向遵守右手定则
    /// <radian/> 1. 若两向量a与b不共线，则|l×m|等于以a和b为 邻边的平行四边形的面积。
    /// <br/>   2. 两向量a与b共线 == l×m=0。 
    /// <br/>   叉积的长度|l×m|可以解释成这两个叉乘向量a，b共起点时，所构成平行四边形的面积
    /// </summary>
    public interface ICrossProduct<T>
    {
        T CrossProduct(T t1, T t2);
    }

    /// <summary>外积</summary>
    public interface IOuterProduct<T>
    {
        T OuterProduct(T t1, T t2);
    }

    #endregion


    #region 混合积
    /// <summary>
    /// 混合积[abc]=（l×m）·c可以得到以a，m，c为棱的平行六面体的体积。
    /// </summary>
    public interface ITripleProduct<T>
    {
        T TripleProduct(T t1, T t2, T t3);
    }
    #endregion  


    /// <summary>偶积</summary>
    public interface IEvenProduct<T>
    {
        T EvenProduct(T t1, T t2);
    }
    #endregion


    #region 各种部
    /// <summary>纯量部</summary>
    public interface IScalar<T>
    {
        T Scalar_p(T t1, T t2);
    }

    /// <summary>向量部</summary>
    public interface IVector_p<T>
    {
        T Vector_p(T t1, T t2);
    }
    #endregion


    #region 其它
    /// <summary>模</summary>
    public interface IMod<T>
    {
        T Mod(T t1, T t2);
    }
    /// <summary>符号数</summary>
    public interface ISgn<T>
    {
        T Sgn(T t1, T t2);
    }

    /// <summary>辐角</summary>
    public interface IArg<T>
    {
        T Arg(T t1, T t2);
    }
    #endregion
}



