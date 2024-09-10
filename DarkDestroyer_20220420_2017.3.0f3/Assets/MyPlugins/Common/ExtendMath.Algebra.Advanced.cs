/****************************************************
    文件：ExtendMath.Algebra.Advanced.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/17 19:7:57
	功能：高等代数
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static partial class ExtendAdvancedAlgebra
{
    //
    //引进了比如最基本的有集合、向量和向量空间等。这些量具有和数相类似的运算的特点，不过研究的方法和运算的方法都更加繁复。
    //集合是具有某种属性的事物的全体；
    //向量是除了具有数值还同时具有方向的量；
    //向量空间也叫线性空间，是由许多向量组成的并且符合某些特定运算的规则的集合。向量空间中的运算对象已经不只是数，而是向量了，其运算性质也有很大的不同了。
    //
    //高等代数是大学数学专业开设的专业课，
    //线性代数是大学中除了数学专业以外的理科、工科和部分医科专业开设的课程。
    //
    //作为大学课程的高等代数，只研究它们的基础。高次方程组发展成为一门比较现代的数学理论－代数几何。



    /// <summary>高等代数</summary>
    public enum E高等代数
    {
        一次方程组, 线性方程组, 线性代数理论, //关于向量空间、线性变换、型论、不变量论和张量代数等内容的一门高等代数分支学科，
        一元多次方程, 多项式方程, 多项式理论,   //研究只含有一个未知量的任意次方程的一门高等代数分支学科。
    }

    public enum E大学高等代数课程
    { 
        线性代数,
        多项式代数    
    }
    /// <summary>线性代数</summary>
    public enum E线性代数
    { 
    
    }
}



