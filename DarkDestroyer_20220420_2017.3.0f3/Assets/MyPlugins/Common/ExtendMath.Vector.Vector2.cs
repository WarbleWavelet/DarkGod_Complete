/****************************************************
    文件：ExtendUnityOthers.cs
	作者：lenovo
    邮箱: 
    日期：2023/6/20 17:16:44
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using System.Runtime.CompilerServices;
using static ExtendVector;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using EAngleUnitType = ExtendGeometry.EAngleUnitType;
using static ExtendMath_Unit;
using static ExtendLinearAlgebra;
//using static ExtendLinearAlgebraByNumerics;
using System.IO;

    /// <summary>
    /// 很想拓展属性,不被允许
    /// 
    /// </summary>
    public struct Vector2Inherit 
    {
    /// <summary>anchor常用</summary>
    private static readonly Vector2 halfVector = new Vector2(0.5f, 0.5f);
    /// <summary>sizeDelta常用</summary>
    private static readonly Vector2 hundredVector = new Vector2(100f, 100f);
        public static Vector2 half => halfVector;
        public static Vector2 hundred => hundredVector;
    }
public static partial class ExtendVector2
{




}
public static partial class ExtendVector2 //几个律
{

    //交换 分配 结合
    public static void DotProductRule(Vector2 v1,Vector2 v2,Vector2 v3,float k)
    {
        bool b_True;
        b_True= v1.DotProduct(v2) == v2.DotProduct(v2);
        b_True= v1.DotProduct(v2.AddV2(v3)) == v1.DotProduct(v2)+v1.DotProduct(v3);
        //
        b_True = v1.Mult(k).DotProduct(v2) == v1.DotProduct(v2.Mult(k)) ;
        b_True = v1.Mult(k).DotProduct(v2) == v1.DotProduct(v2)*k;
    }


    /// <summary>
    /// v不与坐标系方向关系密切
    /// </summary>
    public static void CrossProductRule1(Vector2 a, Vector2 b, Vector2 c, float k)
    {
        bool b_True;
        b_True = a.CrossProduct(b) == -b.DotProduct(b);
        b_True = a.CrossProduct(a) == 0;
        b_True = a.CrossProduct(b) == -b.DotProduct(b);
        b_True = a.CrossProduct(b.AddV2(c)) == a.CrossProduct(c) + a.CrossProduct(c);
        b_True = a.CrossProduct(b*k) == (a.CrossProduct(b))*(k);
    }

    /// <summary>
    /// 与坐标系方向关系密切
    /// </summary>
    public static void CrossProductRule2(Vector3 x, Vector3 y, Vector3 z, float k,ExtendCoordinates.ECoordinates eCoordinates)
    {
        if (eCoordinates == ExtendCoordinates.ECoordinates.LEFT)
        { 
            //左手坐标系下,正为顺时针,负为逆时针
            bool b_True;
            b_True = x.CrossProduct(y) == z;
            b_True = y.CrossProduct(x) == -z;
            b_True = x.CrossProduct(z) == -y;
            b_True = z.CrossProduct(x) == y;
            b_True = y.CrossProduct(z) == x;
            b_True = z.CrossProduct(y) == -x;
      
        }

    }

}
public static partial class ExtendVector2 //长度的  长度的 平方  Head=  单位向量
{

    #region 长度,长度的平方


    /// <summary>V2的长度的平方 x2+y2</summary>
    public static float Pow2(this Vector2 v)
    {

        return v.sqrMagnitude;
    }
    /// <summary>V2的长度 (x2+y2).Sqrt()  ||v||</summary>
    public static float Length(this Vector2 v)
    {
        return v.magnitude;
    }
    #endregion



    #region Head=  单位向量
    /// <summary> 单位向量  英文上面^常叫xxHead</summary>
    public static Vector2 Head(this Vector2 v)
    {
        return v.UnitVector();
    }


    /// <summary>单位向量</summary>
    public static Vector2 UnitVector(this Vector2 v)
    {
        return v.Divide(v.Length());
    }
    #endregion
}

public static partial class ExtendVector2 // 加减乘除.静态类用不了operator
{

    #region 说明

    /// <summary>结合意义时平行四边形法则或三角形法则</summary>
    public static Vector2 AddV2(this Vector2 v1, Vector2 v2)
    {
        float x = v1.x + v2.x;
        float y = v1.y + v2.y;
        return new Vector2(x, y);
    }
    public static Vector2 SubV2(this Vector2 v, Vector2 v2)
    {
        float from = v.x - v2.x;
        float to = v.y - v2.y;
        return new Vector2(from, to);
    }
    public static Vector2 MultV2(this Vector2 v, Vector2 v2)
    {
        float from = v.x * v2.x;
        float to = v.y * v2.y;
        return new Vector2(from, to);
    }
    public static Vector2 DivideV2(this Vector2 v, Vector2 v2)
    {
        float from = v.x / v2.x;
        float to = v.y / v2.y;
        return new Vector2(from, to);
    }
    #endregion
    #region 说明
    public static Vector2 Add(this Vector2 v, float para)
    {
        float x = v.x + para;
        float y = v.y + para;
        return new Vector2(x, y);
    }
    public static Vector2 Sub(this Vector2 v, float para)
    {
        float x = v.x - para;
        float y = v.y - para;
        return new Vector2(x, y);
    }
    public static Vector2 Mult(this Vector2 v, float para)
    {
        float x = v.x * para;
        float y = v.y * para;
        return new Vector2(x, y);
    }
    public static Vector2 Divide(this Vector2 v, float para)
    {
        float x = v.x / para;
        float y = v.y / para;
        return new Vector2(x, y);
    }
    #endregion  
}
public static partial class ExtendVector2 // 投影 点击 叉积 模 乘  顺逆时针 
{


    #region 投影

    /// <summary>
    /// 投影长度
    /// from在to上的投影
    /// _|_的长度</summary> 
    public static float ProjectionLength(this Vector2 from, Vector2 to)
    {
       return from.Length() * from.DotProduct(to); //from乘以cos(与to的cos)
    
    }

    /// <summary>按投影比率计算的投影向量
    /// _|_</summary>
    public static Vector2 ProjectionHorizontal(this Vector2 from, Vector2 to)
    {
        float projectionLength = from.ProjectionLength(to);
        float ratio = projectionLength / to.Length();
        Vector2 projectionHorizontal = to.Mult(ratio);
        return projectionHorizontal;

    }


    /// <summary>
    /// 施密特正交化
    /// 平行四边形法则计算出平行四边形的另一边
    /// </summary>
    public static Vector2 ProjectionVertical(this Vector2 from, Vector2 to)
    {
        Vector2 projectionVertical = from - from.ProjectionHorizontal(to);
        return projectionVertical;

    }
    #endregion



    public static float Mod(this Vector2 v)
    {
        return (v.x.Pow2() + v.y.Pow2()).Sqrt(); 
    }


    #region DotProduct
    /// <summary>标量,给出多种命名习惯的需要</summary>
    public static float Scalar(this Vector2 a, Vector2 b, EVectorCalcType vectorCalcType = EVectorCalcType.COORDINATE)
    {
        return a.DotProduct(b, vectorCalcType);
    }


    /// <summary>
    /// ab*cos 相当于缩放 
    /// <br/>标积/内积/数量积/点积
    /// <br/>找夹角
    /// <br/>因为cos有正负,可以判断是在左边还是右边</summary>
    public static float DotProduct(this Vector2 a, Vector2 b,EVectorCalcType vectorCalcType= EVectorCalcType.COORDINATE)
    {

        switch (vectorCalcType)
        {
            case EVectorCalcType.TRIANGLE: return a.DotProduct_Triangle(b);
            case EVectorCalcType.COORDINATE: return a.DotProduct_Coordinate(b);
            default: throw new System.Exception("异常");
        }
    }

    #endregion


    #region CrossProduct 返回Vector
    public static Vector3 CrossProduct(this Vector3 a, Vector3 b, EVectorCalcType vectorCalcType = EVectorCalcType.COORDINATE)
    {

        switch (vectorCalcType)
        {
            //case EVectorCalcType.TRIANGLE: return from.CrossProduct_Triangle(to);
            case EVectorCalcType.COORDINATE: return a.CrossProduct_Coordinate(b);
            case EVectorCalcType.MATRIX: return a.CrossProduct_Matrix(b);
            default: throw new System.Exception("异常:未定义");
        }

    }
    #endregion

    #region CrossProduct 返回float
    /// <summary>
    /// absin 
    /// <br/>向量积，数学中又称积、叉积，物理中称矢积、叉乘
    /// <br/>也就是a*a边上的高,1/2 * absin=两个向量围成的三角形的面积
    /// <br/>  以b在x坐标正方向,|to|sin=b对应的高,也就是垂直于|from|上的垂线
    /// <br/>  判断点是否在三角形内部
    /// 
    /// </summary>
    public static float CrossProduct(this Vector2 a, Vector2 b, EVectorCalcType vectorCalcType= EVectorCalcType.TRIANGLE)
    {

        switch (vectorCalcType)
        {
            case EVectorCalcType.TRIANGLE : return a.CrossProduct_Triangle(b);
            case EVectorCalcType.COORDINATE : return a.CrossProduct_Coordinate(b);
            default: throw new System.Exception("异常");
        }

    }



    #endregion


    #region pri
    static float DotProduct_Coordinate(this Vector2 a, Vector2 b)
    {
        return Vector2.Dot(a, b); //lhs.from * rhs.from + lhs.to * rhs.to;
    }


    static float DotProduct_Triangle(this Vector2 a, Vector2 b)
    {
        float radian = Vector2.Angle(a, b).Degree2Radian();
        float cos = radian.Cos();

        return a.Mod() * b.Mod() *cos; 
   }

    /// <summary>ab*sin 矢积/外积/向量积/叉积</summary>
    static float CrossProduct_Triangle(this Vector2 a,Vector2 b)
    { 
        return  a.Mod() * b.Mod() * Vector2.SignedAngle(a,b).Degree2Radian().Sin();  //sin 
    }

    /// <summary>
    /// 二维叉积,垂直于平面的伪向量
    /// <br/>辅助理解,所以三维的ixj=k,ixk=j,jxk=i就是表示相互垂直
    /// </summary>
     static float CrossProduct_Coordinate(this Vector2 a, Vector2 b)
    {
        //axb=(x1,y1) from (x2,y2)=(x1y2-x2y1)
        float x = a.x * b.y - b.x * a.y;
        return x;
    }

    /// <summary>
    /// 坐标轴从from转到to,垂直向量需要顺逆时针旋转多少度
    /// </summary> 
    static Vector3 CrossProduct_Coordinate(this Vector3 from, Vector3 to)
    {
        return Vector3.Cross(from,to); //用API
    }

    static float DotProduct_Matrix(this Vector3 from, Vector3 to)
    {
        Matrix3x1 fromMatrix = new Matrix3x1(from, EMatrixValueType.COLUM);
        Matrix3x1 toMatrix = new Matrix3x1(to, EMatrixValueType.COLUM);
        Matrix1x1 resMatrix = fromMatrix.DotProduct(toMatrix);
        //
        return resMatrix[0,0];
    }
    static Vector3 CrossProduct_Matrix(this Vector3 from, Vector3 to)
    {
        Matrix3x1 fromMatrix = new Matrix3x1(from, EMatrixValueType.COLUM);
        Matrix3x1 toMatrix = new Matrix3x1(to, EMatrixValueType.COLUM);
        Matrix3x1 resMatrix = fromMatrix.CrossProduct(toMatrix); //3x3对偶矩阵 x 3x1的to
        //
        Vector3 res = resMatrix.Matrix3x1ToV3();
        return res;

    }


    #endregion



    public static float DegreeOffset(this Vector2 from, Vector2 to)
    {
        float radian = from.DotProduct(to).Acos();
        float degree = radian.Radian2Degree();
        return degree;
    }

    public static float RadianOffset(this Vector2 from, Vector2 to, ExtendCoordinates.ECoordinates coordinates = ExtendCoordinates.ECoordinates.LEFT)
    {
       return RadianOffset(from,to,coordinates);
    }

    /// <summary>
    /// 时针方向
    /// <br/>Unity默认左手坐标系
    /// </summary>    
    public static EDir ClockDir(this Vector2 from, Vector2 to, ExtendCoordinates.ECoordinates coordinates = ExtendCoordinates.ECoordinates.LEFT)
    {
        Vector3 radian;
        if (coordinates == ExtendCoordinates.ECoordinates.RIGHT)
        {
            radian = Vector3.Cross(from, to);

        }
        else
        {
            radian = Vector3.Cross(to, from);
        }


        if (radian.z > 0)
        {
            return EDir.CLOCKWISE;
        }
        else if (radian.z < 0)
        {
            return EDir.CONTRACLOCKWISE;
        }
        return EDir.MIDDLECLOCKWISE;
    }


}


public static partial class ExtendVector2 //Set
{
      #if NET_4_7_2_OR_NEWER
    public static Vector2 SetX(ref this Vector2 v, float value)
    {
        Vector2 pos = v;
        v = new Vector2(value, pos.y);
        return v;
    }



    public static Vector2 SetY(ref this Vector2 v, float value)
    {
        Vector2 pos = v;
        v = new Vector2(pos.x, value);
        return v;
    }

    public static Vector2 SetXY(ref this Vector2 v, float x, float y)
    {
        Vector2 pos = v;
        v = new Vector3(x, y);
        return v;
    }

    /// <summary>ref直接动v的内存</summary>
    public static Vector2 Mult(ref this Vector2 v, float x, float y)
    {
        v.x *= x;
        v.y *= y;

        return v;
    }
    /// <summary>ref直接动v的内存</summary>
    public static Vector2 Mult(ref this Vector2 v, Vector2 v1)
    {
        return v.Mult(v1.x,v1.y);
    }

}
public static partial class ExtendVector2  //Reversal X  Y 
{

    /// <summary>取反</summary>
    public static Vector2 Reversal(ref this Vector2 dir)
    {
        Vector2 pos= new Vector2(-dir.x, -dir.y);
        dir = pos;
        return dir;
    }

    public static Vector2 ReversalX(ref this Vector2 dir)
    {
        Vector2 pos = new Vector2(-dir.x, dir.y);
        dir = pos;
        return dir;
    }

    public static Vector2 ReversalY(ref this Vector2 dir)
    {
        Vector2 pos = new Vector2(dir.x, -dir.y);
        dir = pos;
        return dir;
    }
#endif

}
public static partial class ExtendVector2
{


}
public static partial class ExtendVector2//Vector2Int
{
    public static Vector2Int LeftUp(this Vector2Int v)
    {
        return new Vector2Int(-1,1);
    }

    public static Vector2Int LeftDown(this Vector2Int v)
    {
        return new Vector2Int(-1, -1);
    }

    public static Vector2Int RightUp(this Vector2Int v)
    {
        return new Vector2Int(1, 1);
    }

    public static Vector2Int RightDown(this Vector2Int v)
    {
        return new Vector2Int(1, -1);
    }

}






