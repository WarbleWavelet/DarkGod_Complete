/****************************************************
    文件：ExtendMath.Algebraic.Structure.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/21 22:13:44
	功能：代数结构
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;


public static partial class ExtendAlgebraicStructure
{
}
public static partial class ExtendAlgebraicStructure  //代数结构
{
    /// <summary>代数结构</summary>
    public enum EAlgebraicStructure
    {
        Vector, 向量,
        Martix, Martirces, 矩阵,
        LinearSpace, 线性空间,
        Quaternions, 四元数,
        Boolean, 布尔,
        Groups, 群,
        Ring, 环,
        Area, 域,
        Grid, 格,
        Mod, 模,
        OrderedSet, 有序集 
        //
        //代数结构还可以与附加的非代数性质的结构(必须与代数结构兼容)共存，
        ,偏序
        ,拓扑
        //
        ,拓扑组//群操作相一致的拓扑群。
        ,冯诺依曼代数//一个具有弱算子拓扑的希尔伯特空间上算子的代数。
    }
}

public static partial class ExtendAlgebraicStructure //环
{
    public enum E环
    { 
      交换诺特环,戴德金环 ,诺比乌斯环,
        阿廷环,诺特环  , 诺特半素环,有一个阿廷半单的分式环 ,
        满足多项式等式的环
    } 
}

public static partial class ExtendAlgebraicStructure //域
{
    public enum E域
    {
        伽罗瓦域
    }
    }

public static partial class ExtendAlgebraicStructure //模
{ 

}
public static partial class ExtendAlgebraicStructure //群论
{
    //数学家发明了各种概念来把群分解成更小的、更好理解的部分，比如置换群、子群、商群和单群等。
    //伽罗瓦群，解决了五次方程问题  ;研究当时代数学的中心问题即五次以上的一元多项式方程是否可用根式求解

    /// <summary>群的种类</summary>
    public enum E群
    {
        /// <summary>操作的顺序,实数加实数,虚数加虚数</summary>
        实数加法群,
        复数加法群,
        /// <summary>缩放</summary>
        正实数乘法群,
        /// <summary>缩放,旋转(涉及i就是旋转)</summary>
        复数乘法群,
        布尔群,
        伽罗瓦群   ,
        连续群,Lie群,
        //
        拓扑群,
        李群,  //一个具有相容光滑流形结构的拓扑群。
        代数群,
        算术群 ,
        //
        结构群,
        一般线性群 ,
        置换群 ,
        半群,幺半群 ,
        有限群,无限群 ,
         //
        有序群,//有序环和有序域：局部有序的每一类结构。
        阿基米德群,//拥有阿基米德性质的线性有序群。

    }

    //同态,从群到群并且保持运算的函数
}

public static partial class ExtendAlgebraicStructure //拓扑
{
    //拓扑学Topology
    //研究几何图形或空间在连续改变形状后还能保持不变的一些性质的学科。
    //它只考虑物体间的位置关系而不考虑它们的形状和大小。
    //在拓扑学里，重要的拓扑性质包括连通性与紧致性。
    //Topology，直译是“地志学”，最早指研究地形、地貌相类似的有关学科。
    //拓扑学是由几何学与集合论里发展出来的学科，研究空间、维度与变换等概念。
    //这些词汇的来源可追溯至哥特佛莱德·莱布尼茨，他在17世纪提出“位置的几何学”（geometria situs）和“位相分析”（analysis situs）的说法。
    //莱昂哈德·欧拉的柯尼斯堡七桥问题与欧拉示性数被认为是该领域最初的定理。
    //例如在拓扑学中，对于凸多面体，欧拉示性数可以通过公式 V−E+F 计算得出，其中 V 是顶点数，E 是边数，F 是面数。
    //
    //在代数拓扑中，欧拉示性数（Euler characteristic）是一个拓扑不变量（事实上，是同伦不变量），对于一大类拓扑空间有定义。
    //
    //七桥问题，四色问题 ,欧拉定理 ,莫比乌斯环
    //纽结问题,空间中一条自身不相交的封闭曲线，会发生打结现象。要问一个结能否解开（即能否变形成平放的圆圈），或者问两个结能否互变，并且不只做个模型试试，还要给出证明，那就远不是件容易的事了（见纽结理论）。
    //维数概念,什么是曲线？朴素的观念是点动成线，随一个参数（时间）连续变化的动点所描出的轨迹就是曲线。可是，皮亚诺在1890年竟造出一条这样的“曲线”，它填满整个正方形。这激发了关于维数概念的深入探讨，经过20～30年才取得关键性的突破。
    //向量场问题,考虑光滑曲面上的连续的切向量场，即在曲面的每一点放一个与曲面相切的向量，并且其分布是连续的，其中向量等于0的地方叫作奇点。例如，地球表面上每点的风速向量就组成一个随时间变化的切向量场，而奇点就是当时没风的地方。从直观经验看出，球面上的连续切向量场一定有奇点，而环面上却可以造出没有奇点的向量场。 进一步分析，每个奇点有一个“指数”，即当动点绕它一周时，动点处的向量转的圈数；此指数有正负，视动点绕行方向与向量转动方向相同或相反而定。球面上切向量场，只要奇点个数是有限的，这些奇点的指数的代数和（正负要相消）恒等于2；而环面上的则恒等于0。这2与0恰是那两个曲面的欧拉数，这不是偶然的巧合。这是拓扑学中的庞加莱-霍普夫定理。
    //不动点问题
    //纤维丛理论,拓扑学中的一种理论。把微分流形及以其上每点为原点的线性独立的切向量组全体总括在一起得到纤维丛的概念。
    public enum ETopology
    {
    紧收敛拓扑,
   点集拓扑,
代数拓扑 ,
同伦论,
微分拓扑,
超导现象 ,
    }
}

