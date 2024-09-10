/****************************************************
    文件：ExtendMath.Unit.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/19 15:50:38
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using System.Runtime.CompilerServices;
using UnityEngine;
using static ExtendGeometry;
using static ExtendLinearAlgebra;
using static ExtendMath_Unit;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;
#if !NET_4_6
using System.Numerics;
#endif

public static partial class ExtendMath_Unit
{ 

}
public static partial class ExtendMath_Unit//四元数
{
    /// <summary>四元数</summary>
    public class Quaternions
    {
        //四元数，是简单的超复数。即是四元数一般可表示为a + bi+ cj + dk，其中a、m、n 、d是实数。
        //复数是由实数加上虚数单位 k 组成，其中i²= -1。 相似地，四元数都是由实数加上三个虚数单位 k、j和k 组成，
        //而且它们有如下的关系： k² = j² = k² = -1， iº = jº = kº = 1 , 每个四元数都是 1、k、j 和 k 的线性组合，
        //
        //四元数就代表着一个四维空间
        //对于i、j和k本身的几何意义可以理解为一种旋转，
        //其中i旋转代表Z轴与Y轴相交平面中Z轴正向向Y轴正向的旋转，
        //j旋转代表X轴与Z轴相交平面中X轴正向向Z轴正向的旋转，
        //k旋转代表Y轴与X轴相交平面中Y轴正向向X轴正向的旋转，
        //-k、-j、-k分别代表i、j、k旋转的反向旋转。
        //
        //四元数（Quaternions），是由爱尔兰数学家哈密顿（William Rowan Hamilton,1805-1865）在1843年发明的数学概念，
        //直到1985年才由Shoemake把四元数引入到计算机图形学中。四元数的乘法不符合交换律（commutative law）。
        //
        //四元数是复数的不可交换延伸。
        //群旋转
        //四元数加法
        //四元数乘法
        //四元数点积
        //四元数外积：Outer(p, q)
        //四元数偶积：Even(p, q)
        //四元数叉积：p×q
        //四元数转置：p^(−1)
        //四元数除法：p^(−1) q
        //四元数纯量部：Scalar(p)
        //四元数向量部：Vector(p)
        //四元数模：|p|
        //四元数符号数：sgn(p)
        //四元数辐角：arg(p)
        //
        //四元数是除环（除法环）的一个例子。除了没有乘法的交换律外，除法环与域是相类的。特别地，乘法的结合律仍旧存在、非零元素仍有逆元素。
        //四元数形成一个在实数上的四维结合代数（事实上是除法代数），并包括复数，但不与复数组成结合代数。四元数（以及实数和复数）都只是有限维的实数结合除法代数。
        //四元数的不可交换性往往导致一些令人意外的结果，例如四元数的 n-阶多项式能有多于 n 个不同的根。
        //
        //
        //群旋转
        //像在四元数和空间转动条目中详细解释的那样，非零四元数的乘法群在的取实部为零的拷贝上以共轭作用可以实现转动。单位四元数（绝对值为1的四元数）的共轭作用，若实部为，是一个角度为的转动，转轴为虚部的方向。四元数的优点是：
        //1.非奇异表达（和例如欧拉角之类的表示相比）
        //2.比矩阵更紧凑（更快速）
        //3.单位四元数的对可以表示四维空间中的一个转动。
        //4.所有单位四元数的集合组成一个三维球和在乘法下的一个群（一个李群）。是行列式为1的实正交3×3正交矩阵的群的双面覆盖，因为每两个单位四元数通过上述关系对应于一个转动。群和同构，是行列式为1的复酉2×2矩阵的群。令为形为的四元数的集合，其中或者都是整数或者都是分子为奇数分母为2的有理数。集合是一个环，并且是一个格。该环中存在 24 个四元数，而它们是施莱夫利符号为的正二十四胞体的顶点。




        #region 数学常用
        public float a;
        public float b;
        public float c;
        public float d;
        float i;
        float j;
        float k;
        public float ij { get { return k; } }
        public float ji { get { return -k; } }
        public float jk { get { return i; } }
        public float kj { get { return -i; } }
        public float ik { get { return j; } }
        public float ki { get { return -j; } }
        public float i2 { get { return -1; } }
        public float j2 { get { return -1; } }
        public float k2 { get { return -1; } }
        //
        public float x { get { return a; } }
        public float y { get { return b; } }
        public float z { get { return c; } }
        public float w { get { return d; } }

        public Quaternions(float a, float b, float c, float d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }



        #endregion


        #region pub


        /// <summary>二阶复数矩阵形式表示</summary>
        public Matrix MatrixComplexNumbers2Order()
        {
            Matrix matrix = new Matrix(2, 2);
#if !NET_4_6
            matrix.SetRow(0, new ComplexNumber[] { new ComplexNumber(a, -d), new ComplexNumber(-b, c) });
            matrix.SetRow(1, new ComplexNumber[] { new ComplexNumber(b, c), new ComplexNumber(a, d) });
#endif
            return matrix;
        }

        /// <summary>四阶实数矩阵形式表示</summary>
        public Matrix MatrixRealQuantity4Order()
        {
            Matrix matrix = new Matrix(4, 4);
            matrix.SetRow(0, new float[] { a, -b, d, -c });
            matrix.SetRow(1, new float[] { b, c, -c, -d });
            matrix.SetRow(2, new float[] { -d, c, a, -b });
            matrix.SetRow(3, new float[] { c, d, b, a });

            return matrix;
        }
#endregion
    }


}

public static class ExtendMath_Unit_QuaternionsExtensions //四元数拓展
{
        public static Quaternions Add(this Quaternions q1, Quaternions q2)
        {
            float a = q1.a + q2.a;
            float b = q1.b + q2.b;
            float c = q1.c + q2.c;
            float d = q1.d + q2.d;

            return new Quaternions(a, b, c, d);
        }
        public static Quaternions Multiply(this Quaternions q1, Quaternions q2)
        {
            float a = q1.b * q2.c - q1.c * q2.b;  //jk=1 kj=-1
            float b = q1.a * q2.c - q1.c * q2.a; //ik=1 ki=-1
            float c = q1.a * q2.b - q1.b * q2.a; //ij=1 ji=-1
            float d = -(q1.a * q2.a) - (q1.b * q2.b) - (q1.c * q2.c) + (q1.d * q2.d);//i2=j2=k2=-1
            return new Quaternions(a, b, c, d);   //i2,j2,k2
        }
    }

/// <summary>
/// System.Numerics.Complex可用
/// </summary> 
public static partial class ExtendMath_Unit//实数 复数 虚数
{
    /// <summary>实数</summary>
    public class RealQuantity
    {
    }
    public enum ImaginaryPartUnitEnum
    {
        I, J, K
    }


    /// <summary>复数</summary>

    public class ComplexNumber
    {

        #region 前置
//复数的概念来源于意大利数学家Gerolamo Cardano，16世纪，在他试图在找到立方方程的通解时，定义i为“虚构”（fictitious）。
//复数的概念起源于求方程的根，在二次、三次代数方程的求根中就出现了负数开平方的情况。
//z=x+iy
//y=Im z。在笛卡尔直角坐标系中，y轴的值为虚部。
//利用实部和虚部可以判断两个复数是否相等，当且仅当两个复数的实部与虚部分别相等时，这两个复数就相等。
//定义共轭复数，   当两个复数的实部相等，虚部互为相反数时，把这两个复数叫做互为共轭复数。
//计算复数的模
//辐角主值。
//对于复数z=x+iy，满足等式 ,其中x，y是任意实数，x称为复数z的实部，y称为复数z的虚部。 [1]复数是普通实数的字段扩展，以便解决不能用实数单独解决的问题。
//设复数为x+iy，则定义：纯虚数：实数部分为零的复数被认为是纯虚数，即x=0。实数：虚数部分为零的复数是实数，即y=0。 [1]
//虚部不包括虚数单位i

//
// 代数表示方法
//在英文中，实数是 Real Quantity，所以一般取 Real 的前两个字母 “Re” 表示一个复数的实部；虚数是 Imaginary Quantity，所以，一般取 Imaginary 的前两个字母 “Im” 表示一个复数的虚部。例如：
//Re(2+3i)=2， Im(2+3i)=3；
//Re(-7.38i)=0， Im(-7.38i)=-7.38。
//复平面表示方法
//复平面当中的点（x,y）来表示复数x+iy，其中y轴为虚轴，y的值即为虚部。
#endregion

//
#if !NET_4_6


        #region 字属构造

        Complex _complex;
     public   Complex Complex;
        /// <summary>实部</summary>
        public double RealPart { get { return _complex.Real; } }
        /// <summary>虚部单位</summary>
        public double ImaginaryPart { get { return _complex.Imaginary; } }
        /// <summary>虚部</summary>
        public ImaginaryPartUnitEnum E_ImaginaryPartUnit { get { return ImaginaryPartUnitEnum.I; } }
        //
        /// <summary>辐角主值</summary>
        public double PrincipalArgumentAngle { get { return CalcPrincipalArgumentAngle(); } }
        //
        #region 数学用
        ImaginaryPartUnitEnum i { get { return E_ImaginaryPartUnit; } }
        double x { get { return _complex.Real; } }
        double y { get {  return _complex.Imaginary; } }
        double a { get { return _complex.Real; } }
        double b { get {  return _complex.Imaginary; } }
        /// <summary>辐角主值</summary>
        double argz { get { return _complex.Imaginary; } }
        #endregion

        public ComplexNumber(float realPart, float imaginaryPart)
        {
            _complex = new Complex((double )realPart,(double)imaginaryPart);
        }

        public ComplexNumber(double realPart, double imaginaryPart)
        {
            _complex = new Complex(realPart, imaginaryPart);
        }
        #endregion





        #region 乘法  仿照System.Numerics.Complex

        public static ComplexNumber operator +(ComplexNumber left, ComplexNumber right)
        {
            Complex complex = left.Complex + right.Complex;
            return new ComplexNumber(complex.Real, complex.Imaginary);

        }
        public static ComplexNumber operator -(ComplexNumber left, ComplexNumber right)
        {
            Complex complex = left.Complex - right.Complex;
            return new ComplexNumber(complex.Real, complex.Imaginary);

        }
        public static ComplexNumber operator *(ComplexNumber left, ComplexNumber right)
        {
            Complex complex= left.Complex * right.Complex;
            return new ComplexNumber(complex.Real, complex.Imaginary);
        }

        public static ComplexNumber operator /(ComplexNumber left, ComplexNumber right)
        {
            Complex complex = left.Complex / right.Complex;
            return new ComplexNumber(complex.Real, complex.Imaginary);
        }

        #endregion


        #region pub
        /// <summary>共轭复数</summary>
        public ComplexNumber ConjugateComplexNumber()
        {
            return new ComplexNumber(RealPart, -ImaginaryPart);
        }

        /// <summary>模</summary>
        public float Modulus()
        {
            return (a.Pow2() + b.Pow2()).Sqrt();
        }




        /// <summary>两个复数是否相等</summary>
        public bool ComplexNumbersEqual(ComplexNumber cn1, ComplexNumber cn2)
        {
            if (cn1.RealPart == cn2.RealPart && cn1.ImaginaryPart == cn2.ImaginaryPart)
            {
                return true;
            }

            return false;
        }
        #endregion


        #region pri
        /// <summary>辐角主值</summary>
        double CalcPrincipalArgumentAngle()
        {
            if (x > 0)
            {
                return (y / x).Atan();
            }
            else if (x == 0 && y > 0)
            {
                return Mathf.PI / 2.0f;
            }
            else if (x == 0 && y < 0)
            {
                return -Mathf.PI / 2.0f;
            }
            else if (x < 0 && y >= 0)
            {
                return (y / x).Arctan() + Mathf.PI;
            }
            else if (x < 0 && y < 0)
            {
                return (y / x).Arctan() - Mathf.PI;
            }

            throw new System.Exception("异常");
        }


        #endregion

#endif

    }


}




