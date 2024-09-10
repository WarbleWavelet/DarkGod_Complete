/****************************************************
    文件：ExtendAlgorithm.cs
	作者：lenovo
    邮箱: 
    日期：2023/9/10 16:10:36
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;



#region interface
/// <summary>
/// 思想
/// 算法思想
/// </summary>
public enum EIdea
{
    /// <summary>
    /// 二分查找
    /// 算法查找
    /// </summary>
    BinarySearch,
}
public enum ETimeComplexity
{
    Log_n,
    n,
}
public interface IIdea { string Idea(); }
public interface IComplexity { }
public interface ITimeComplexity:IComplexity { string TimeComplexity() ; }
public interface ISpaceComplexity:IComplexity { string SpaceComplexity(); }
#endregion

#region 三大算法


#region 排序
public static partial class ExtendAlgorithm//排序
{
    static void Example_Algorithm_Sort()
    {
        InsertionSort();//插入排序
        RapidSort_Function();//快速排序
        SimpleSelectionSort();//简单选择排序

    }
    //插入排序
    //直接插入排序
    //折半插入排序
    //希尔排序
    //冒泡排序
    //快速排序
    //简单选择排序
    //堆排序
    //归并排序
    //基数排序

    static void InsertionSort()//插入排序
    {
        //数据
        int[] pokerArray = new int[] { 4, 3, 8, 6, 5, 7, 1, 10, 9, 7, 4 };
        //遍历
        for (int i = 1; i < pokerArray.Length; i++)//要被拿来比较的牌
        {
            if (pokerArray[i] < pokerArray[i - 1])//是小牌，拿起来插前面排好序的
            {
                int pokerInHand = pokerArray[i];//保存，因为后面后移会被覆盖

                for (int j = i - 1; j >= 0; j--)//跟前面排好序的牌比较
                {
                    if (pokerInHand < pokerArray[j])//比前面的牌小。下面进行后移，插入牌
                    {
                        pokerArray[j + 1] = pokerArray[j];//往后移留出空位
                        pokerArray[j] = pokerInHand;//空位插入前面保存的那张牌
                    }
                }
            }
        }
        //打印
        foreach (int i in pokerArray)
        {
            Console.Write(i + "\t");
        }
    }
    static void SimpleSelectionSort()//简单选择排序。大长腿比赛，逐渐淘汰掉小短身
    {
        int[] leggyContestantArray = new int[] { 162, 158, 163, 163, 160, 167, 170, 168, 168 };
        //排序
        for (int i = 0; i < leggyContestantArray.Length - 1; i++)//最后一名不用排，，所以i=1
        {
            int leader = leggyContestantArray[i];//第一名先先到先得
            int loser;
            for (int j = i + 1; j < leggyContestantArray.Length; j++)//i后面一堆竞选者
            {
                if (leggyContestantArray[j] > leader)//挑战成功的
                {
                    //比赛结果
                    loser = leader;//卫冕失败
                    leader = leggyContestantArray[j];//竞选对手上榜
                                                     //交换排位
                    leggyContestantArray[j] = loser;
                    leggyContestantArray[i] = leader;
                }
            }
        }

        //打印
        for (int i = 0; i < leggyContestantArray.Length; i++)
        {
            Console.WriteLine(leggyContestantArray[i]);

        }
    }
    static void RapidSort_Function()
    {
        int[] array = new int[] { 4, 3, 8, 6, 5, 7, 1, 10, 9, 7, 4 };
        int firstIndex = 0;
        int lastIndex = array.Length - 1;
        RapidSort(array, firstIndex, lastIndex);
        foreach (int i in array)
        {
            Console.Write(i + " ");
        }
    }
    static void RapidSort(int[] array, int left, int right)//快速排序
    {

        int i = left;
        int j = right;
        int pivot = array[i];

        while (i < j && true)
        {
            while (i < j && true)//右移
            {
                if (array[j] < pivot)//找到移值
                {
                    array[i] = array[j];
                    break;
                }
                else//没找到，移动另一边的索引
                {
                    j--;
                }
            }
            while (i < j && true)//左移
            {
                if (array[i] > pivot)
                {
                    array[j] = array[i];
                    break;
                }
                else
                {
                    i++;
                }
            }

            array[i] = pivot;

            RapidSort(array, left, i - 1);
            RapidSort(array, i + 1, right);
        }



    }
}
#endregion

public static partial class ExtendAlgorithm//搜索/查找
{
    //顺序查找-有序表
    //顺序查找-无序表
    //折半查找
    //B树
    //B+树
    //红黑树
    //哈希表-拉链法
    //哈希表-开放定址法
    //
    //平衡树是为了解决二叉查找树退化为链表的情况

    /// <summary>平衡二叉树</summary>
    public class BalancedTreeTreeAlgorithm : IAlgorithm, ITimeComplexity, IIdea
    {
        public string Idea()
        {
            throw new NotImplementedException();
        }

        //特点
        //具有二叉查找树的全部特性。
        //每个节点的左子树和右子树的高度差至多等于1。
        //
        //优点
        //可以保证不会出现大量节点偏向于一边的情况
        //平衡树解决了二叉查找树退化为近似链表的缺点
        //缺点
        //平衡树要求每个节点的左子树和右子树的高度差至多等于1，这个要求实在是太严了，导致每次进行插入/删除节点的时候，几乎都会破坏平衡树的第二个规则，进而我们都需要通过左旋和右旋来进行调整，使之再次成为一颗符合要求的平衡树。
        //在那种插入、删除很频繁的场景中，平衡树需要频繁着进行调整，这会使平衡树的性能大打折扣

        //
        //构建、插入、删除、左旋(右右型)、右旋(左左型)等操作    

        public string TimeComplexity()
        {
            return ETimeComplexity.n.Enum2String();
        }
    }


    public class BinarySearchTreeAlgorithm : IAlgorithm, ITimeComplexity ,IIdea
    {


        public string TimeComplexity()
        {
            return ETimeComplexity.Log_n.Enum2String();
        } 
        //
        //1、若它的左子树不为空，则左子树上所有的节点值都小于它的根节点值。
        //2、若它的右子树不为空，则右子树上所有的节点值均大于它的根节点值。
        //3、它的左右子树也分别可以充当为二叉查找树。
         
        
        
        public string Idea()
        {
            return EIdea.BinarySearch.Enum2String();
        }
    }
    public class RedBlackTreeAlgorithm : IAlgorithm, ITimeComplexity
    {
        //特点
        //1、具有二叉查找树的特点。
        //2、根节点是黑色的；
        //3、每个叶子节点都是黑色的空节点（NIL），也就是说，叶子节点不存数据。
        //4、任何相邻的节点都不能同时为红色，也就是说，红色节点是被黑色节点隔开的。
        //5、每个节点，从该节点到达其可达的叶子节点是所有路径，都包含相同数目的黑色节点。
        //
        //能够在最坏情况下，也能在 O(logn) 的时间复杂度查找到某个节点
        //红黑树是一种不大严格的平衡树。也可以说是一个折中发方案
        //
        //相对比AVL,优点是增删
        //解决平衡树在插入、删除等操作需要频繁调整的情况
        //不会像平衡树那样，频繁着破坏红黑树的规则，所以不需要频繁着调整，这也是我们为什么大多数情况下使用红黑树的原因。
        //单单在查找方面的效率的话，平衡树比红黑树快
        //
        //红黑树有哪些应用场景？向集合容器中 HashMap，TreeMap 等，内部结构就用到了红黑树了。
        //构建一棵节点个数为 n 的红黑树，时间复杂度是多少？
        //红黑树与哈希表在不同应该场景的选择？
        //红黑树有哪些性质？
        //红黑树各种操作的时间复杂度是多少？

        public string TimeComplexity()
        {
            return ETimeComplexity.Log_n.Enum2String();
        }
    }

}
public static partial class ExtendAlgorithm //图结构
{ 
   //存储结构-邻接矩阵
   //存储结构-邻接链表
   //广度优先搜索-BFS
   //深度度优先搜索-DFS   
   //Prim(普里姆)算法
   //Kruskal(克鲁斯卡尔)算法
  
   //Dijkstra(迪杰斯特拉)算法
   //Floyd(弗洛伊德)算法
   //拓扑排序-栈
   //关键路径
}
#endregion




public static partial class ExtendAlgorithm
{
    //遗传算法 决策树 下棋 搜索算法
    //生命:初始化 杂交 切割 组合 旋转 变异 选择
    //解决问题:旅行商路线
}
public static partial class ExtendAlgorithm
{
    //动态规划 有向无环图DAG(上下游,但是有很多小河)/递推关系/初始条件
    //
    //走楼梯,只能走1步或2步,求走n步楼梯有多少种解法        
    //Fm=Fm_1+Fm_2(Fm=F(m-1)+F(m-2))
    //F1=1(1个台阶只有不走这一种),F2=1(2个台阶只有走一步一种),F3=2(2个台阶有两个走一步,一个走两步两种)
    //
    //最小单词距离(编辑距离)
    // cats=>cat(删除sub) cat=>cats(增加add) cat =>cut(替换replace)   距离都是1
    // 
    //九宫棋 马尔科夫决策问题MDP

    //线性规划


}

public static partial class ExtendAlgorithm 
{
    //快速傅立叶变换（Fast Fourier Transform，FFT）
    //卷积运算
    //伯恩斯坦多项
    //矩阵运算规则
    //伯努利时间序列
    //马尔科夫序列
    //排序

    //对比学习
    //强化学习
    //深度学习
}


#region 有限状态机=>行为树



#region 行为树
abstract class BTNode { }
/// <summary>组合</summary>
abstract class BTComposite: BTNode { }
/// <summary>装饰</summary>
abstract class BTDecorator: BTNode { }
/// <summary>条件</summary>
abstract class BTCondition: BTNode { }
/// <summary>行为</summary>
abstract class BTAction: BTNode { }
/// <summary>序列</summary>
abstract class CompositeSequence : BTComposite { }
/// <summary>选择</summary>
abstract class CompositeSelector : BTNode { }
/// <summary>反转</summary>
abstract class DecoratorInverter: BTNode { }
/// <summary>重复</summary>
abstract class DecoratorRepeater : BTNode { }
#endregion
#endregion

#region 地图 路径 避障
public static partial class ExtendAlgorithm //地图
{
    static void Overview()
    { 
        new WaveFunctionCollapseAlgorithm();
        //柏林噪声
        new MitchellBestCandidateAlgorithm();
    }

}
public static partial class ExtendAlgorithm //路径规划
{
    //AStart/A*,启发式搜索,无法达到最短路径，只能计算次优路径
    //
    //JPS寻路算法,调点搜索算法,JumpPointSearch/邻居强迫,邻居裁剪/自然节点,劣性节点
    //https://www.bilibili.com/video/BV18z421i7s8/?spm_id_from=333.1007.tianma.1-1-1.click&vd_source=54db9dcba32c4988ccd3eddc7070f140
    //
    //HPA*寻路算法,使用分层策略优化过后的A星寻路算法，该算法与AStar存在相同的问题，无法达到最短路径，只能计算次优路径，但在效率上有非常大的优势。
}

public static partial class ExtendAlgorithm //动态避障算法
{
    //RVO,/
    //https://github.com/warmtrue/RVO2-Unity 
    //https://www.bilibili.com/video/BV1Nb421n7qJ/?spm_id_from=333.788.recommend_more_video.-1&vd_source=54db9dcba32c4988ccd3eddc7070f140

}
#endregion




#region 接口
public interface IAlgorithm
{

}
#endregion



/// <summary>柏林噪声</summary>
public class BerlinNoisyAlgorithm : IAlgorithm
{ 

}
/// <summary>米切尔最佳候选算法</summary>
public class MitchellBestCandidateAlgorithm: IAlgorithm
{

    
}

/// <summary>波函数坍缩算法
/// <see href="https://www.bilibili.com/video/BV1Br421M7Vm/?spm_id_from=333.788.recommend_more_video.2&vd_source=54db9dcba32c4988ccd3eddc7070f140" />
/// </summary>
public class WaveFunctionCollapseAlgorithm : IAlgorithm
{

}

#region 仿生学原理的群智能算法
public static partial class ExtendAlgorithm
{
    //鱼群算法
    //蚁群算法
    class FishSwarmAlgorithm : IAlgorithm
    {
       public  class Boid
        {
            /// <summary>位置</summary>
            public Vector3 position;
            /// <summary>速度</summary>
            public Vector3 velocity;
            /// <summary>加速度</summary>
            public Vector3 acceleration;


            /// <summary>
            /// <br/>聚合（Cohesion）：使Boid朝向群体中心移动。
            /// <br/>分离（Separation）：避免与邻近的Boid碰撞。
            /// <br/>对齐（Alignment）：使Boid的速度与邻近Boid的平均速度相匹配。
            /// </summary>
            public void Update(List<Boid> boids, float cohesion, float separation, float alignment)
            {
                // 这里实现Boid的更新逻辑
            }
        }


        public class BoidManager
        {
            public List<Boid> boids;
            public float cohesion;
            public float separation;
            public float alignment;
            void Start()
            {
                // 初始化Boid
            }
            void Update()
            {
                foreach (var boid in boids)
                {
                    boid.Update(boids, cohesion, separation, alignment);
                }
                // 更新Boid的位置
            }
        }
    }
}
#endregion

#region 十大人工智能算法
public static partial class ExtendAlgorithm
{
    //线性回归、逻辑回归、线性判别分析、分类和回归决策树、朴素贝叶斯、最近邻算法、学习向量量化、支持向量机、随机森林、神经网络。‌
    //https://mp.weixin.qq.com/s?__biz=MzU3NTQ4MDg0Nw==&mid=2247609419&idx=8&sn=5e71a1128f0ac908865d3e03536b36a0&chksm=fd21192bca56903d88bf7fb810efe8292439d96d205f0ef8c64360d79a7da0b208e17bd8da3e&scene=27
}
#endregion