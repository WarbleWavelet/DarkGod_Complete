/****************************************************
    文件：ExtendMath.Algebra.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/17 14:47:40
	功能：代数
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static partial class ExtendAlgebra//抽象代数
{
    public enum E表示
    {
        群的置换表示,群和结合代数的线性表示,有限群的线性表示,酉表示 , 线性表示, 群的射影表示
    }
}
public static partial class ExtendAlgebra//代数 
{
    // 代数，是研究数、数量、关系、结构与代数方程（组）的通用解法及其性质的数学分支。
    //  这些结构以公理(见公理法 AXIOMATICMETHOD) 为特征。
    //  公理法 AXIOMATICMETHOD)
    //  特别重要的是结合律和交换律
    //
    // 伽罗瓦理论。
    // 证明了不可能有解五次方程的代数公式。
    // 证明了用直尺和圆规不能解决某些著名的几何问题(立方加倍，三等分一个角)
    //
    //对称代数、张量代数
    //有系统的、更普遍的方法，以解决各种数量关系的问题
    //比如，如果你认为“代数学”是指解bx+k=0这类用符号表示的代数方程的技巧。这种“代数学”是在十六世纪才发展起来的。
    //多于一个变量的代数方程理论属于代数几何学
    //

}

public static partial class ExtendAlgebra
{
    //所以初等代数的一个重要内容就是代数式。
    //由于事物中的数量关系的不同，大体上初等代数形成了整式、分式和根式这三大类代数式。
    //
    //代数式是数的化身，因而在代数中，它们都可以进行四则运算，服从基本运算定律，
    //而且还可以进行乘方（这里仅限于有理数指数幂）和开方两种新的运算。通常把这六种运算叫做代数运算，以区别于只包含四种运算的算术运算。
    //
    //将算术中讨论的整数和分数的概念扩充到有理数的范围，使数包括正负整数、正负分数和零
    //数的概念在一次扩充到了实数，进而又进一步扩充到了复数。
    //
    //那么到了复数范围内是不是仍然有代数方程没有解，还必须把复数再进行扩展呢？数学家们说：不用了。
    //这就是代数里的一个著名的定理——代数基本定理。这个定理简单地说就是n次方程有n个根。
    public enum E代数
    { 
            初等代数,
            抽象代数, 近世代数,
            //
            代数拓扑学,代数数论,代数几何    ,
            布尔代数,同调代数,德若当代数,李代数 ,
        四元数代数 ,
        张量代数
    }
    public enum EOperation
    { 
          加,减,乘,除,
          乘方,开方
    }

    /// <summary>代数式</summary>
    public enum EAlgebraicExpression
    {
        分式,
        根式,
        有理式
    }

    /// <summary>有理式</summary>
    public enum ERationalExpression
    { 

        /// <summary>整式</summary>
        INTEGRALEXPRESSION,
    }

    /// <summary>整式</summary>
    public enum EIntegralExpression
    { 
        /// <summary>单项式</summary>
        MONOMIAL,
        /// <summary>多项式</summary>
        POLYNOMIAL
    }


    public enum E数
    { 
        整数,分数,
        负整数,正整数,负分数,正分数,零,
        有理数,无理数,实数,复数 ,
        四元数
    }
//
}




