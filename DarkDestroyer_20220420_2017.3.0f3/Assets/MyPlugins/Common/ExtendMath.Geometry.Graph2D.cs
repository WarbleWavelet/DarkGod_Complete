/****************************************************
    文件：ExtendGeometry.cs
	作者：lenovo
    邮箱: 
    日期：2023/9/10 17:18:48
	功能：
*****************************************************/


using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.AccessControl;
using UnityEngine;
using static ExtendGeometry;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public static partial class ExtendGeometry
{
    static void Example()
    {
        Circle circle;
    }
}
public static partial class ExtendGeometry//角 角度 弧度
{
    /// <summary></summary>
    public enum EAngleType
    {
        NULL,
        /// <summary>锐角</summary>     
        SharpAngle,       
        /// <summary>直角</summary>
        RightAngle,
        /// <summary>钝角</summary>
        ObtuseAngle,

        /// <summary>平角</summary>
        StraightAngle,
        /// <summary>钝角</summary>
        RoundAngle,

    }

    /// <summary>弧度,或者角度</summary>
    public static EAngleType AngleType(this float angle, EAngleUnitType eAngleUnitType)
    {
        if (eAngleUnitType == EAngleUnitType.DEGREER)
        {
            return angle.AngleType();
        }
        else if (eAngleUnitType == EAngleUnitType.RADIAN)
        {
            float degree = angle.Radian2Degree();
            return degree.AngleType();
        }

        throw new System.Exception("异常:未定义");
    }

    /// <summary>向量先求角度</summary>
    public static EAngleType AngleType(this Vector2 v1, Vector2 v2)
    {
        float degree = Vector2.Angle(v1, v2);
        return degree.AngleType();
    }



    #region pri
    static EAngleType AngleType(this float degree)
    {
            if (0f < degree&& degree < 90f)
        { 
               return EAngleType.SharpAngle;
        }
        else if (90f == degree)
        {
            return EAngleType.RightAngle;
        }
        else if (90f > degree && degree<180f)
        {
            return EAngleType.ObtuseAngle;
        }
        else if (180f == degree)
        {
            return EAngleType.StraightAngle;
        }
        else if (360f == degree)
        {
            return EAngleType.RoundAngle;
        }
        return EAngleType.NULL;
    }

    #endregion
}
public static partial class ExtendGeometry//接口
{


    interface ISelfPosition_Vector2
    {
        Vector2 SelfPosition_Vector2();
    }

    /// <summary>
    /// 面积
    /// 可以区别开 正方形Square的 
    /// </summary>
    interface IArea
    {
         float Area();
    }

    /// <summary>
    /// 面积，接近平方之类
    /// 会首先用这个
    /// <para />但需要区别开 正方形Square的
    /// </summary>
    interface ISquare
    {
        /// <summary>面积</summary>
         float Square();
    }

    /// <summary>周长</summary>
    interface ICircumference
    {
        /// <summary>周长</summary>
         float Circumference();
    }
}
public static partial class ExtendGeometry  //总介绍
{
    //坐标对,比如圆的一个x对应两个y
    //直线,正比例,一次
    //方形Square
    //长方形(矩形) Rectangular
    //等边菱形Diamond
    //等腰菱形Kite
    //平行四边形Parallelogram

    //三角形 Triangle
    //等边三角 EquilateralTraiangle
    //等腰IsoscelesTraiangle
    //直角RightTraiangle
    //钝角ScaleneTraiangle
    //蛋形Oval
    //椭圆Ellipse 

    //圆锥Cone
    //圆柱Cylinder
    //正方Cube
    //长方体RectangularPrism
    //锥体Pyramid

    //等5边Pentagon
    //等6边Hexagon

    //等7边Heptagon
    //等8边Octagon
    //等9边Nonagon

    //箭头 Arrow
    //十字Cross
    //月形Crescent
    //爱心Heart
    //星形Star
    //水滴Teardrop
    //花瓣Petal       
    //6边等腰，像西式棺材Coffin
}
public static partial class ExtendGeometry  //辅助类,接口
    {
        //壳，像蘑菇Shell

        /// <summary>针对椭圆的长短轴长</summary>
        public class CoordinatePairs
    {
        public float Small; 
        public float Big;

        public CoordinatePairs(float num_1, float num_2)
        {
            if (num_1 <= num_2)
            {
                Small = num_1;
                Big = num_2;
            }
            else
            {
                Small = num_2;
                Big = num_1;
            }
        }
    }

    public enum FoucusAxisEnum
    { 
       X_AXIS,
       Y_AXIS,
    }


    /// <summary>角度类型</summary>
    public enum EAngleUnitType
    {
        /// <summary>角度</summary>
        DEGREER,
        /// <summary>弧度</summary>
        RADIAN    ,
        /// <summary>2维向量</summary>
        VECTOR2,
        /// <summary>3维向量</summary>
        VECTOR3,
        /// <summary>四元数</summary>
        QUATERION
    }  
}



public static partial class ExtendGeometry  //具体的图形
{

    /// <summary>直线 一次 正比例</summary>
    public class Straight
    {
        /**
         * y=kx+m
         * 
         */
        /// <summary>原点</summary>
        Vector2 _originPos;

        #region 数学常用字符
        /// <summary>斜率,Degrees的角度</summary>
        float _k;
        float _x0 { get { return _originPos.x; } }
        float _y0 { get { return _originPos.y; } }
        #endregion




        public Straight(Vector2 originPos, float k)
        {
            _originPos = originPos;
            _k = k;
        }
        public Straight(float angle,EAngleUnitType angleType)
        {
            float radian = angle.Angle2Radian(angleType);
            _k = radian;
        }



        public float GetX(float y)
        {
            return _x0 + (y - _y0) * _k;
        }

        public float GetY(float x)
        {
            return _y0 + (x - _x0) / _k;
        }

        #region 静态拓展

        /// <summary>
        /// (y-y0)=radian(x-x0)
        /// x=x0+(y-y0)/radian
        /// </summary>
        public static float GetX(float y, float k, Vector2 pos)
        {
            return pos.x + (y - pos.y) / k;
        }


        /// <summary>
        /// (y-y0)=radian(x-x0)
        ///  y=y0+(x-x0)*radian
        /// </summary>
        public static float GetY(float x, float k, Vector2 pos)
        {
            return pos.y + (x - pos.x) * k;
        }
        #endregion

    }

    /// <summary>正方形</summary>
    public class Square : ICircumference, IArea
    {
        public float SideLength;

        public Square(float sideLength)
        {
            SideLength = sideLength;
        }

        public float Circumference()
        {
            return SideLength * 4;
        }
        public float Area()
        {
            return SideLength.Pow2();
        }
    }

    /// <summary>长方形</summary>
    public class Rectangular : ICircumference, ISquare
    {
        public float Width;
        public float Height;



        public float Circumference()
        {
            return (Width + Height) * 2;
        }
        public float Square()
        {
            return Width * Height;
        }
    }

    /// <summary>三角形</summary>
    public class Triangle :ICircumference ,ISquare
    {
        float l;
        float m;
        float n;
        Vector2 O= Vector2.zero;
        Vector2 A ;
        Vector2 B ;

        #region 习惯
        float a { get { return A.x; } }
        float b{ get { return A.y; } }
        float c{ get { return B.x; } }
        float d { get { return B.y; } }
        #endregion  


        public Triangle(float a,float b,float c)
        { 
            if(   a+b<=c 
               || a+c<=b
               || b+c<=a)

                throw new System.Exception("异常");
        }

        public Triangle(Vector2 A,Vector2 B)
        { 
              this.A = A;
            this.B = B; 
        }
        public float Circumference()
        {
           return l + m + n;
        }

        /// <summary></summary>
        public float Square()
        {
            return A.CrossProduct(B,ExtendVector.EVectorCalcType.COORDINATE)/2.0f;
            //return (a*d-b*c).Abs()/2.0f; //一个三角形转为平行四边形的一般 => 平行四边形转化为两个平行四边形的相加
        }
    }


    /// <summary>圆形</summary>
    public  class Circle:ICircumference ,ISquare
    {

        #region 知识点
        /**
         * 圆心(l,m)
         * 标准方程 (x-l).Pow2()+(y-m).Pow2()=1
         * 参数方程 x = l + r * sin(Theta)
         *          y = m+ r * cos(Theta)
         */
        #endregion

        #region 数学公式常用
        public float r;
        public float d { get { return r * 2.0f; } }
        public Vector2 Pos;

        float a { get { return Pos.x; } }
        float b { get { return Pos.y; } }
        public float S { get { return Square(); } }
        public float L { get { return Circumference(); } }
        #endregion


        public Circle(float r, Vector2 pos)
        {
            this.r = r;
            Pos = pos;
        }

        /// <summary>直径</summary>
        public float Diameter()
        {
            return  d;
        }


        public float Square()
        {
            return Mathf.PI * (r.Pow2());
        }
      
        public float Circumference()
        {
            return 2 * Mathf.PI * r;
        }


        public float GetX_Theta(float theta)
        {
            return Pos.x + r * theta.Cos();
        }

        public float GetY_Theta(float theta)
        {
            return Pos.y + r * theta.Sin();
        }

        public CoordinatePairs GetXCoordinatePairs(float y)
        {
            float sqrt = (1 - (y - b).Pow2()).Sqrt();
            CoordinatePairs coordinatePairs = new CoordinatePairs(a-sqrt, a+sqrt);
            return coordinatePairs;
        }

        public CoordinatePairs GetYCoordinatePairs(float x)
        {
            float sqrt = (  1 - (x-a).Pow2()  ).Sqrt();
            CoordinatePairs coordinatePairs = new CoordinatePairs(b - sqrt, b + sqrt);
            return coordinatePairs;
        }

        #region 静态方法

        /// <summary>像时钟一样,知道12点的位置,输入角度,得到另外一个点的位置</summary>
        public static Vector3 SetLocalPositionAfterRotate(Transform lastNode, Transform circleCenter, float angle)
        {
            Vector3 vectorDirection = new Vector3(0f, 0f, angle);//向量方向
            Vector3 vectorLength = lastNode.LocalPos() - circleCenter.LocalPos();
            Vector3 vector = Quaternion.Euler(vectorDirection) * vectorLength;
            //
            Vector3 rotatedNodeLocalPos = vector + circleCenter.LocalPos();

            return rotatedNodeLocalPos;
        }
        #endregion

    }


    /// <summary>
    /// 椭圆 (x/a).Pow2()+(y/b).Pow2()=1 ,(k,h)=(0,0)时
    /// 椭圆 ((x-k)/a).Pow2()+((y-h)/b).Pow2()=1
    /// <para/>椭圆（Ellipse）是平面内到定点F1、F2的距离之和等于常数（大于|F1F2|）的动点P的轨迹，
    /// <br/>F1、F2称为椭圆的两个焦点。
    /// <br/>其数学表达式为：|PF1|+|PF2|=2a（2a>|F1F2|）。
    /// </summary>
    public class Ellipse : ICircumference, ISquare 
    {

        #region 说明
        /**
         *  标准方程
         *  参数方程 
         *  弦长公式  
         *  二级结论     
         *  焦点c，
            长轴a，短轴b，半短轴d，半长轴e，
            偏心率f，
            离心率g，
            整体椭圆系数h，
            偏心系数i，
            椭圆比例系数j，
            椭球系数k，
            焦距系数l，
            圆心距离系数m，
            偏心轴距离系数n，
            椭球的半短轴系数o，
            椭球的半长轴系数p，
            偏心轴系数q，
            椭球的比例系数r，
            椭球的离心率系数s，
            椭球的偏心率系数t，
            椭球的整体椭圆系数u，
            椭球的焦距系数v，
            椭球的离心轴距离系数w，
            椭球的圆心距离系数x，
            椭球的偏心轴距离系数y，
            椭球的偏心轴距离系数z。
        **/

        #endregion  

        #region 本质参数

        public Vector2 Pos { get; }
        public Vector2 Center { get { return Pos; } }
        /// <summary>焦点1 小的</summary>
        public Vector2 Focus1 { 
            get 
            {
                if (XHalfAxis > YHalfAxis) //焦点x轴
                {
                    return new Vector2(Center.x - (float)c, Center.y);
                }
                else if (XHalfAxis < YHalfAxis) //焦点y轴
                {
                    return new Vector2(Center.x  , Center.y-(float)c);
                }
                else//圆
                {
                    return Center;
                }
            } 
        }
        /// <summary>焦点2 大的</summary>
        public Vector2 Focus2 { get { return -Focus1; } }
        /// <summary>焦半径1</summary>
        public float FocusRadius1 { get; }
        /// <summary>焦半径2</summary>
        public float FocusRadius2 { get; }
        /// <summary>焦距 |F1-F2| = 2c</summary>
        public float FocusDistance { get { return Vector2.Distance(Focus1, Focus2); } }
        /// <summary>X半轴长.构造得来</summary>
        public float XHalfAxis { get; }
        /// <summary>Y半轴长.构造得来</summary>
        public float YHalfAxis { get; }
        #endregion


        #region 次生参数

        /// <summary>X轴长</summary>
        public double XAxis { get { return XHalfAxis * 2; } }
        /// <summary>Y轴长</summary>
        public double YAxis { get { return YHalfAxis * 2; } }

        /// <summary>短半轴长</summary>
        public double ShortHalfAxis { get 
            {
                if (XHalfAxis >= YHalfAxis)
                {
                   return YHalfAxis;
                }
                else if (XHalfAxis < YHalfAxis)
                {
                    return XHalfAxis;
                }

                throw new System.Exception("空值异常");
            } }
        /// <summary>长半轴长</summary>
        public double LongHalfAxis
        {
            get
            {
                if (XHalfAxis >= YHalfAxis)
                {
                    return XHalfAxis;
                }
                else if (XHalfAxis < YHalfAxis)
                {
                    return YHalfAxis;
                }
                throw new System.Exception("空值异常");
            }
        }
        /// <summary>短轴长</summary>
        public double ShortAxis { get { return ShortHalfAxis * 2; } }
        /// <summary>长轴长</summary>
        public double LongAxis { get { return LongHalfAxis * 2; } }


        #region 四个端点
       const  int _pointCnt = 4;
        public Vector2 TopPoint { get { return _pointPosArr[0]; } }
        public Vector2 BottomPoint { get { return _pointPosArr[1]; } }
        public Vector2 LeftPoint { get { return _pointPosArr[2]; } }
        public Vector2 RightPoint { get { return _pointPosArr[3]; } }
        
        /// <summary>4个端点0123 上下左右</summary>
        /// 
         Vector2[] _pointPosArr = new Vector2[_pointCnt];
         Vector2[] PointPosArr 
        { 
            get
            {
                if (_pointPosArr == null)
                {
                    _pointPosArr[0] = new Vector2(Pos.x - 0f, (float)(Pos.y + YHalfAxis));
                    _pointPosArr[1] = new Vector2(Pos.x - 0f, (float)(Pos.y- YHalfAxis));
                    _pointPosArr[2] = new Vector2((float)(Pos.x-XHalfAxis),    Pos.y-0f);
                    _pointPosArr[3] = new Vector2((float)(Pos.x+ XHalfAxis),    Pos.y - 0f);
                }
                return _pointPosArr;
            } 
        }
        #endregion  
        //

        #endregion


        #region 数学公式常用
        /// <summary>x半轴长(x-radius)/l</summary>
        public double a { get { return XHalfAxis; } }
        /// <summary>y半轴长(y-h)/m</summary>
        public double b { get { return YHalfAxis; } }
        /// <summary>中心x坐标,x-radian</summary>
        public double k { get { return Pos.x; } }
        /// <summary>中心y坐标,y-h</summary>
        public double h { get { return Pos.y; } }
        /// <summary></summary>
        public double c { get { return (a.Pow2() - b.Pow2().Abs().Sqrt()); } }
        /// <summary>离心率 焦距与长轴比例 2c/2a</summary>
        public double e { get { return FocusDistance / LongAxis; } }
        #endregion

        #region 四个端点
        public Vector3 Top { get { return new Vector3(Center.x, Center.y+YHalfAxis); } }
        public Vector3 Bottom { get { return new Vector3(Center.x,Center.y - YHalfAxis); } }
        public Vector3 Left { get { return new Vector3(Center.x - XHalfAxis,Center.y); } }
        public Vector3 Right { get { return new Vector3(Center.x + XHalfAxis, Center.y); } }
        #endregion


        public Ellipse(float xHalfAxis, float yHalfAxis, Vector2 pos)
        {

            XHalfAxis = xHalfAxis;
            YHalfAxis = yHalfAxis;
            Pos = pos;
        }

        public Ellipse(double xHalfAxis, double yHalfAxis, Vector2 pos)
        {

            XHalfAxis = (float)xHalfAxis;
            YHalfAxis = (float)yHalfAxis;
            Pos = pos;
        }

        /// <summary>2PIb+4*(l-m)</summary>
        public double Circumference()
        {
            return 2 * Mathf.PI * b + 4*(a - b);
        }

        /// <summary>面积 PI*l*b或PI*A*B/4</summary>
        public double Square()
        {
            return Mathf.PI * a * b;
        }




        #region IGetX,IGetY
        /// <summary>    
        /// 根据y求两个x ,先小后大
        /// <br/>v1 = a.Pow2();
        /// <br/>v2 = 1 - ((y-h) / b).Pow2();
        /// <br/>v3 = (v1 * v2).Sqrt();
        /// </summary>
        public double[] GetXArr(double y)
        {
            double[] xArr= new double[2];
            double v1 = a.Pow2();
            double v2 = 1 - ((y-h) / b).Pow2();
            double v3 = (v1 * v2).Sqrt();
            //
            xArr[0] = k - v3;
            xArr[1] = k + v3;
           return xArr;
        }

        /// <summary>
        /// 根据x求两个y  ,先小后大
        /// <br/>v1 =  b.Pow2();
        /// <br/>v2 =   1 - (( x -  k) / a).Pow2();
        /// <br/>v3 = (v1*v2).Sqrt();
        /// </summary>
        public double[] GetYArr(double x)
        {
            double[] yArr = new double[2];
            double v1 =  b.Pow2();
            double v2 =   1 - (( x -  k) / a).Pow2();
            double v3 = (v1*v2).Sqrt();
            //double sqrt = h+/- (b.Pow2-(x-k/a).Pow2).sqrt
            yArr[0] = h - v3 ;
            yArr[1] = h + v3 ;
            return yArr;
        }


        #region 辅助类型转换

        /// <summary>以centerPos为新的中心将坐标进行移动计算</summary>

        public float[] GetXArr(float y, Vector2 centerOffset)
        {
            float[] pre = GetXArr((double)y).ToFloatArray();
            float[] after = pre.ForeachAdd(centerOffset.x);
            return after;
        }

        /// <summary>以centerPos为新的中心将坐标进行移动计算</summary>
        public float[] GetYArr(float x,Vector2 centerOffset)
        {
            float[] pre= GetYArr((double)x).ToFloatArray();
            float[] after = pre.ForeachAdd(centerOffset.y);
            return after;
        }


        public float GetYBottom(float x)
        {
            return GetYArr((double)x).ToFloatArray()[0];
        }


        public float GetYTop(float x)
        {
            return GetYArr((double)x).ToFloatArray()[1];
        }


        public float GetXLeft(float y)
        {
            return GetXArr((double)y).ToFloatArray()[0];
        }


        public float GetXRight(float y)
        {
            return GetXArr((double)y).ToFloatArray()[1];
        }
        #endregion




        float ICircumference.Circumference()
        {
            throw new System.NotImplementedException();
        }

        float ISquare.Square()
        {
            throw new System.NotImplementedException();
        }
        #endregion

    }


    /// <summary>双曲线</summary>
    public class Hyperbola 
    {

        #region 相关知识
        /**
         *   (  (PF1)-(PF2)  ).Abs() = 2a
         * 
         *  (x/l).Pow2()-(y/m).Pow2()=1   （l>0，m>0）  (Focus在x轴)
         *  (y/l).Pow2()-(x/m).Pow2()=1   （l>0，m>0）  (Focus在y轴)
         *  
         *  ((x-m)/l).Pow2()-((y-n)/m).Pow2()=1   （l>0，m>0） (Focus在x轴)
         *  ((y-n)/l).Pow2()-((x-m)/m).Pow2()=1   （l>0，m>0） (Focus在y轴)         
         *  
         *  渐近线方程Y=±(m/l)X(焦x轴)
         *  渐近线方程Y=±(l/m)X(焦y轴)
         *  离心率e=n/l（l²+m²=n²)
         *  参数关系c²=l²+m²
         *  准线方程x=±l²/n [1]
         *  
         *  最短距离 l
         *  实轴长 2a
         *  虚轴长 2b
         *  离心率 n=(l.Pow2()+m.Pow2()).Sqrt()
         *  通径长度(垂直于焦轴经过焦点的与双曲线的两个交点之间的距离) 焦轴坐标=一c一0 +>2(m.Pow2())/l
         *  焦点到渐近线距离 m
         *  
         *  内准圆问题
         *  蒙日圆问题
         *  焦点三角形面积公式
         */
        #endregion


        public Vector2 Pos;
        public FoucusAxisEnum E_FoucusAxisEnum;
        /// <summary>焦点</summary>
        public  CoordinatePairs FoucusParis { get { return new CoordinatePairs(-c,c); } }


        #region 衍生概念
        public float RealAxisLength { get { return 2 * a; } }
        public float VirtualAxisLength { get { return 2 * b; } }

        #endregion

        #region 数学公式常用
        /// <summary></summary>
        public float a;
        public float b;
        /// <summary></summary>
        public float c { get { return (a.Pow2() + b.Pow2()).Sqrt(); } }
        public float e { get { return c/a; } }
        #endregion

        public Hyperbola(FoucusAxisEnum foucusAxisEnum ,float a, float b)
        {
            E_FoucusAxisEnum = foucusAxisEnum;
            this.a = a;
            this.b = b;
        }


        public CoordinatePairs GetXCoordinatePairs(float y)
        {
            if (E_FoucusAxisEnum == FoucusAxisEnum.X_AXIS)
            {
                float sqrt = (  a.Pow2()  *  (  1+(y / b).Pow2()  )).Sqrt();
                return new CoordinatePairs(-sqrt, sqrt);
            }
            else if( E_FoucusAxisEnum == FoucusAxisEnum.Y_AXIS)
            {
                float sqrt = (  b.Pow2()   *   (  -1 + (y / b).Pow2())  ).Sqrt();
                return new CoordinatePairs(-sqrt, sqrt);
            }


            throw new System.Exception("异常");
        }
    }
}
public static partial class ExtendGeometry  //
{
  public  class Line  : MonoBehaviour
    {
      public  Node from;
      public  Node to;
        public int index;

        public Line(int index, Node from, Node to)
        {
            this.index = index;
            this.from = from;
            this.to = to;
        }
    }

    public class Node :MonoBehaviour
    {
        public int index;
        List<Node> fromNodeLst = new List<Node>();
        List<Node> toNodeLst = new List<Node>();

        public Node InitComponent(int index)
        {
            this.index = index;
            return this;
        }
    }

    public static class NodeProxy
    { 
      static  Dictionary<int,Node> nodeDic=new Dictionary<int, Node> ();
        static int index=0;
        private static GameObject _prefab;

        public static void Init(GameObject prefab)
        {
            _prefab = prefab;
        }
        public static Node AddNode()
        { 
            index++;
            GameObject go = GameObject.Instantiate(_prefab);
            go.name = GameObjectName.Node + index;
            Node node = go.GetOrAddComponent<Node>();
            node.InitComponent(index) ;
            nodeDic.Add(index,node);
            return node;
        }
    }
   public static class LineProxy
    {
        static Dictionary<int, Line> lineLst = new Dictionary<int, Line>();
        static int index=0;
        private static GameObject _prefab;

        public static void Init(GameObject prefab)
        {
            _prefab = prefab;
        }
        public static void DrawLine(Node from, Node to)
        {

            index++;
            drawLine( from,  to);
            lineLst.Add(index,new Line(index,from,to));
        }
        static void drawLine(Node from, Node to)
        {
            GameObject go = GameObject.Instantiate(_prefab);
            LineRenderer lr = go.AddComponent<LineRenderer>();
            lr.Draw(from, to);
        }

    }
}




