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
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;



public static partial class ExtendDataStructure//线性表
{
    public static void Example_DataStructure()
    {
        Example_LinearList();
    }
}


#region 大类
public static partial class ExtendDataStructure//线性表
{
    public static void Example_LinearList()
    {
        Example_SequentialList_Array();
        Example_SingleLinkedList_Head();
        //Example_SingleLinkedList_NoHead();
        //Example_DoublyLinkedList_Head();
        //Example_DoublyLinkedList_NoHead();
    }
    //顺序表-数组
    //单链表-不带头结点
    //单链表-带头结点
    //双链表-不带头结点
    //双链表-带头结点
    //循环单链表
    //循环双链表
    //静态链表
}

public static partial class ExtendDataStructure//栈
{      
    //栈-顺序表
    //栈-链表-不带头结点
    //栈-链表-带头结点
    //栈-括号匹配-顺序表
    //栈-表达式计算-顺序表
    public static void Example_Stack()
    {
        Example_LinkedStack_NoHead();
        Example_LinkedStack_Head();
        Example_SequentialStack_NoHead();
        Example_SequentialStack_Head();
        Example_SequentialStack_BracketMatching();
        Example_SequentialStack_Expression();

    }

    public static void Example_LinkedStack_NoHead()
    {


    }
    public static void Example_LinkedStack_Head()
    {
        LinkedStack<string> stringStack = new LinkedStack<string>();

        stringStack.Push(" 九阴白骨爪 ");
        stringStack.Push(" 泡椒鸡爪 ");
        stringStack.Push(" 蒜香鸡翅 ");
        stringStack.Push(" 酱香白酒 ");

        Console.WriteLine(stringStack.Peek());
        Console.WriteLine(stringStack.Count);
        Console.WriteLine(stringStack.Pop());
        Console.WriteLine(stringStack.Count);

    }
    public static void Example_SequentialStack_NoHead()
    {


    }
    public static void Example_SequentialStack_Head()
    {
        SequentialStack<string> stringStack = new SequentialStack<string>();

        stringStack.Push(" 九阴白骨爪 ");
        stringStack.Push(" 泡椒鸡爪 ");
        stringStack.Push(" 蒜香鸡翅 ");
        stringStack.Push(" 酱香白酒 ");

        Console.WriteLine(stringStack.Peek());
        Console.WriteLine(stringStack.Count);
        Console.WriteLine(stringStack.Pop());
        Console.WriteLine(stringStack.Count);

    }



    public static void Example_SequentialStack_BracketMatching()
    {


    }
    public static void Example_SequentialStack_Expression()
    {


    }

}
public static partial class ExtendDataStructure//队列
{


    //队列-顺序表
    //队列-链表-不带头结点
    //队列-链表-带头结点
    //队列-顺序表-循环队列
    //队列-双端队列
    //队列-层次遍历
}
public static partial class ExtendDataStructure//数组
{ 
    //数组-存储结构
    //压缩存储-对称矩阵
    //压缩存储-三角矩阵
    //压缩存储-三对角矩阵
    //系数矩阵-三元组
}

public static partial class ExtendDataStructure//串
{ 
    //朴素模式匹配
    //KMP算法
}

public static partial class ExtendDataStructure//树结构
{
    //二叉树BT. 存储顺序根左右. 五种基本形态. 先中后序/层次遍历
    //完全二叉树CBT-数组存储. 根左右
    //满二叉树. 就是很饱满，在不能增加深度的情况下，已经不能添加结点。 饱满的完全二叉树
    //二叉树-链式存储
    //二叉排序树-链式存储
    //哈夫曼树/霍夫曼树/最优树/带权路径长度最短的二叉树.-顺序存储. 小大排序,从左到右,从下到上
    //线索二叉树
    //平衡二叉树AVL/高度平衡树 .旋转避免BST的极端情况. 树的左右子树的高度差不超过1
    //BST/二叉搜索树/二叉排序树/二叉查找树 //O(n)查增删,有序时效率不行. 搜索树就是必须要有数值，左（右）子树上所有结点的值均小（大）于它的根结点的值。
    //平衡二叉搜索树：就是平衡二叉树和二叉搜索树的结合。
    //树存储结构-双亲
    //树存储结构-孩子
    //树存储结构-孩子双亲

    //
}
public static partial class ExtendDataStructure //顺序表-数组
{
    /**顺序表-数组
     * 线性表的顺序存储结构，必须占用一片连续的存储空间。
     * 数据访问量比较大，且新增、删除操作不频繁的数据。
     * 数组是顺序表的一种实现
     * 
     * 优点：
        存储密度大，即存储结构中(_array)，100%的空间都给到元素使用了。
        可随机存取表中任意元素，时间复杂度为O(1)。
        缺点：
        插入、删除、查找的时间复杂度为O(n)；
        浪费存储空间，且静态存储（数组大小固定了，无法扩容）
    */

}
#endregion



#region 详细类
public static partial class ExtendDataStructure//顺序表-数组
{
    static void Example_SequentialList_Array()
    {
        SequentialList_Array<int> list = new SequentialList_Array<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);

        // 输出第二个元素
        Console.WriteLine(list[1]); // 输出 2

        // 移除第二个元素
        list.RemoveAt(1);

        // 检查是否包含数字 2
        Console.WriteLine(list.Contains(2)); // 输出 False

        // 遍历顺序表
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine(list[i]);
        }
    }
    public class SequentialList_Array<T>
    {
        private T[] items;
        private int count;
        private const int defaultCapacity = 4;

        public SequentialList_Array() : this(defaultCapacity) { }

        public SequentialList_Array(int capacity)
        {
            items = new T[capacity];
            count = 0;
        }

        public int Count => count;

        public void Add(T item)
        {
            if (count == items.Length)
            {
                // 如果数组满了，则需要扩容
                Array.Resize(ref items, items.Length * 2);
            }
            items[count++] = item;
        }

        public T this[int index]
        {
            get
            {
                if (index >= count || index < 0)
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                return items[index];
            }
            set
            {
                if (index >= count || index < 0)
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                items[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            if (index >= count || index < 0)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
            // 将指定位置后面的元素前移
            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }
            count--;
        }

        public bool Contains(T item)
        {
            return Array.IndexOf(items, item, 0, count) >= 0;
        }
    }
}
public static partial class ExtendDataStructure //带头结点的单链表
{
    interface ILinearList<T>//线性表
    {
        //增删查改
        void Add(T item);
        void Insert(int index, T item);
        //删
        void Remove(T item);
        void Clear();
        //查
        int IndexOf(T item);
        T GetItem(int index);
        int Count();
        bool IsEmpty();
        T this[int index] { get; }
        //改
        void Replace(T from, T to);




    }

    public static void Example_SingleLinkedList_Head()
    {

        Console.WriteLine("呵呵·");
        SequentialList_Head<string> groupList = new SequentialList_Head<string>();
        Console.WriteLine("Count" + groupList.Count());

        groupList.Add("AOA");
        groupList.Add("Tara");
        groupList.Add("F(x)");
        groupList.Add("少女时代");

        for (int i = 0; i < groupList.Count(); i++)
        {
            Console.WriteLine(groupList.IndexOf(groupList[i]) + groupList[i]);
        }

        groupList.Insert(0, "Kara");
        groupList.Remove("Tara");
        groupList.RemoveAt(2);

        Console.WriteLine("Count" + groupList.Count());


        for (int i = 0; i < groupList.Count(); i++)
        {
            Console.WriteLine(groupList.IndexOf(groupList[i]) + groupList[i]);
        }
    }

    class SequentialList_Head<T> : ILinearList<T> //顺序表；alt回车，实现方法
    {

        //头结点
        private Node<T> head;


        //构造
        public SequentialList_Head()
        {
            head = null;

        }


        //增
        public void Add(T item)
        {
            Node<T> newNode = new Node<T>(item);//

            if (this.head == null)//空表
            {
                this.head = newNode;
            }

            else
            {
                //找到最后节点
                Node<T> lastNode = this.head;

                while (true)
                {
                    if (lastNode.Next != null)
                    {
                        lastNode = lastNode.Next;//一直往下找
                    }
                    else//找到最后节点
                    {
                        break;
                    }

                }
                lastNode.Next = newNode;
            }

        }
        public void Insert(int index, T item)
        {
            Node<T> insertNode = new Node<T>(item);



            if (index == 0)//插入头，1个变两个
            {
                insertNode.Next = this.head;//包含了没有头结点的情况
                this.head = insertNode;
            }
            else//插入中间
            {
                Node<T> preNode = this.head;
                for (int i = 1; i < index; i++)//得到插入位置前面的节点
                {
                    preNode = preNode.Next;//遍历，比如inedx=3，得到了前面的2
                }
                //NodeStack<T> preNode = tmpNode;
                //NodeStack<T> currentNode = preNode.Next;

                //preNode.Next = insertNode;
                insertNode.Next = preNode.Next;
                preNode.Next = insertNode;
            }
        }
        //删
        public void RemoveAt(int index)
        {
            Node<T> preNode = this.head;
            if (index == 0)//只有一个节点
            {

                this.head = null;
                //等价于this.head = (head.Next == null)?null:head.Next;
            }
            else//
            {
                for (int i = 1; i < index; i++)
                {
                    preNode = preNode.Next;
                }

                preNode.Next = ((preNode.Next).Next == null) ? null : (preNode.Next).Next;//移除的是最后一个节点           
            }

        }
        public void Remove(T item)
        {
            Node<T> removeNode = new Node<T>(item);
            Node<T> preNode = this.head;

            if (removeNode.Value.Equals(this.head.Value))
            {
                this.head = this.head.Next;//老二篡位
            }
            else
            {
                while (true)
                {
                    if (preNode.Next == null)//最后一个
                    {
                        preNode = null;
                        break;
                    }
                    if (removeNode.Value.Equals(preNode.Next.Value))//找到
                    {
                        preNode.Next = (preNode.Next).Next;//删掉Next
                        break;
                    }
                    else//遍历
                    {
                        preNode = preNode.Next;
                    }
                }
            }


        }
        public void Clear()
        {
            this.head = null;
        }
        //查
        public int Count()//outside检测
        {
            int count = 0;
            Node<T> nextNode = this.head;

            if (this.head == null) return count;
            else count++;//有头

            while (true)//遍历头的后面
            {


                if (nextNode.Next == null)
                {
                    //Console.WriteLine("index outside");//test，打印的话，每次执行Count都会outside打印
                    break;
                }
                else
                {
                    count++;//进来count=1
                    nextNode = nextNode.Next;
                }





            }



            return count;
        }
        public bool IsEmpty()
        {
            if (this.head == null) return true;

            return false;
        }
        public T this[int index]
        {

            get
            {
                Node<T> tmpNode = this.head;
                for (int i = 1; i <= index; i++)//0直接 return head
                {
                    if (tmpNode.Next == null)
                    {
                        Console.WriteLine("index outside");
                        tmpNode = null;
                        break;
                    }

                    tmpNode = tmpNode.Next;
                }

                return tmpNode.Value;
            }
            set
            {

            }
        }
        public T GetItem(int index)
        {
            return this[index];

        }
        public int IndexOf(T value)
        {
            Node<T> tmpNode = this.head;


            if (tmpNode == null) return -1;


            int index = 0;
            while (true)
            {
                if (tmpNode.Value.Equals(value))//找到
                    return index;

                //没找到
                tmpNode = tmpNode.Next;

                if (tmpNode == null) //到尾没找到
                    return -1;
                else
                    index++;

            }
        }
        //改
        public void Replace(T from, T to)
        {
            throw new NotImplementedException();
        }


        #region 内部类
        class Node<T>//单链表
        {
            private Node<T> next;//下个节点
            private T value;
            //构造
            public Node()
            {
                this.value = default(T);
                this.next = null;
            }
            public Node(T value)
            {
                this.value = value;
                this.next = null;
            }
            public Node(T value, Node<T> next)
            {
                this.value = value;
                this.next = next;
            }
            public Node(Node<T> next)
            {
                this.next = next;
            }
            //读取
            public Node<T> Next
            {
                get { return this.next; }
                set { this.next = value; }//value好像是预定义的
            }
            public T Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }


        #endregion
    }

}

public static partial class ExtendDataStructure //栈
{

    interface IStack<T>//栈接口
    {
        //增
        void Push(T item);
        //删
        T Pop();
        void Clear();
        //查
        int Count { get; }
        int GetLength();
        bool IsEmpty();
        T Peek();
        //改


    }
    class SequentialStack<T> : IStack<T>//顺序栈
    {
        private T[] value;
        private int top;//索引

        //构造
        public SequentialStack(int size)
        {
            this.value = new T[size];
            this.top = -1;
        }
        public SequentialStack() : this(10)
        {
        }
        //增
        public void Push(T item)
        {
            this.top++;
            this.value[this.top] = item;
        }
        //删
        public T Pop()
        {
            T preTopValue = this.value[this.top];
            this.top--;

            return preTopValue;
        }
        public void Clear()
        {
            this.top = -1;
        }
        //查
        public int Count { get { return this.top + 1; } }
        public int GetLength()
        {
            return this.Count;
        }
        public bool IsEmpty()
        {
            if (this.Count == 0)
                return true;

            return false;
        }
        public T Peek()
        {
            return this.value[top - 1];
        }

    }

    class LinkedStack<T> : IStack<T>//链栈
    {
        private NodeStack<T> top;//栈顶结点
        private int count = 0;//元素个数

        //构造

        //public LinkedStack() : this(10)
        //{
        //}

        //public LinkedStack(NodeStack<T> top, int count)
        //{
        //    this.top = top;
        //    this.count = count;
        //}

        //增
        public void Push(T item)
        {
            NodeStack<T> newNode = new NodeStack<T>(item);
            newNode.Next = this.top;//指向现在老二
            this.top = newNode;//更新老大
            this.count++;
        }
        //删
        public T Pop()
        {
            T preTopValue = this.top.Value;//送走原来的老大
            this.top = this.top.Next;//老二上位
            this.count--;

            return preTopValue;
        }
        public void Clear()
        {
            this.count = 0;
            this.top = null;
        }
        //查
        public int Count { get { return this.count; } }
        public int GetLength()
        {
            return this.count;
        }
        public bool IsEmpty()
        {
            if (this.count == 0)
                return true;

            return false;
        }
        public T Peek()
        {
            return this.top.Value;
        }
    }

    class NodeStack<T>
    {
        private T value;
        private NodeStack<T> next;
        //构造
        public NodeStack(T value, NodeStack<T> next)//赋值赋值
        {
            this.value = value;
            this.next = next;
        }
        public NodeStack(T value)//赋值
        {
            this.value = value;
            this.next = null;
        }
        public NodeStack(NodeStack<T> next)//赋值
        {
            this.value = default(T);
            this.next = next;
        }
        public NodeStack()//都不赋值
        {
            this.value = default(T);
            this.next = null;
        }

        //开放属性
        public T Value { get { return this.value; } set { this.value = value; } }
        public NodeStack<T> Next { get { return this.next; } set { this.next = value; } }
    }
}
public static partial class ExtendDataStructure //HuffmanTree
{
   public static void Example_HuffmanTree()
    {
        HuffmanTree tree = new HuffmanTree(2, 1, 4, 3);
        tree.Create();

        Debug.Log(tree.ToString());
        //index: 0，weight: 1，lChild_index: -1，rChild_index: -1
        //index: 1，weight: 2，lChild_index: -1，rChild_index: -1
        //index: 2，weight: 3，lChild_index: -1，rChild_index: -1
        //index: 3，weight: 4，lChild_index: -1，rChild_index: -1
        //index: 4，weight: 3，lChild_index: 0，rChild_index: 1
        //index: 5，weight: 6，lChild_index: 2，rChild_index: 4
        //index: 6，weight: 10，lChild_index: 3，rChild_index: 5
    }
    public class HuffmanTree
    {

        #region 内部类
         class Node
        {
            private int weight;//权重值
            private int lChild;//左子节点的序号
            private int rChild;//右子节点的序号
            private int index;//本节点的序号

            public int Weight
            {
                get { return weight; }
                set { weight = value; }
            }

            public int LChild
            {
                get { return this.lChild; }
                set { lChild = value; }
            }

            public int RChild
            {
                get { return this.rChild; }
                set { rChild = value; }
            }

            public int Index
            {
                get { return this.index; }
                set { index = value; }
            }

            public Node()
            {
                weight = 0;
                lChild = -1;
                rChild = -1;
                index = -1;
            }

            public Node(int w, int lc, int rc, int p)
            {
                weight = w;
                lChild = lc;
                rChild = rc;
                index = p;
            }
        }

        #endregion  

        private List<Node> _tmp;
        private List<Node> _nodes;

        public HuffmanTree(params int[] weights)
        {
            if (weights.Length < 2)
            {
                throw new Exception("叶节点不能少于2个!");
            }

            int n = weights.Length;

            Array.Sort(weights);

            //先生成叶子节点，并按weight从小到大排序
            List<Node> lstLeafs = new List<Node>(n);
            for (int i = 0; i < n; i++)
            {
                var node = new Node();
                node.Weight = weights[i];
                node.Index = i;
                lstLeafs.Add(node);
            }


            //创建临时节点容器
            _tmp = new List<Node>(2 * n - 1);

            //真正存放所有节点的容器
            _nodes = new List<Node>(_tmp.Capacity);

            _tmp.AddRange(lstLeafs);
            _nodes.AddRange(_tmp);
        }

        /// <summary>
        /// 构造Huffman树
        /// </summary>
        public void Create()
        {
            while (this._tmp.Count > 1)
            {
                var tmp = new Node(this._tmp[0].Weight + this._tmp[1].Weight, _tmp[0].Index, _tmp[1].Index, this._tmp.Max(c => c.Index) + 1);
                this._tmp.Add(tmp);
                this._nodes.Add(tmp);

                //删除已经处理过的二个节点
                this._tmp.RemoveAt(0);
                this._tmp.RemoveAt(0);


                //重新按权重值从小到大排序
                this._tmp = this._tmp.OrderBy(c => c.Weight).ToList();
            }
        }

        /// <summary>
        /// 测试输出各节点的关键值(调试用)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _nodes.Count; i++)
            {
                var n = _nodes[i];
                sb.AppendLine("index:" + i + "，weight:" + n.Weight.ToString().PadLeft(2, ' ') + "，lChild_index:" + n.LChild.ToString().PadLeft(2, ' ') + "，rChild_index:" + n.RChild.ToString().PadLeft(2, ' '));
            }
            return sb.ToString();
        }
    }
}
public static partial class ExtendDataStructure//排序 搜索 图
{
    //听说是三大算法类型,所以扔到ExtendAlgorithm
}
#endregion

public interface ICreate { }
public interface ICreateByHead: ICreate { }
public interface ICreateByTrail: ICreate { }
public interface IFind{ }
public interface IFindByValue:IFind{ }
public interface IInsert{ }
public interface IDelete { }






