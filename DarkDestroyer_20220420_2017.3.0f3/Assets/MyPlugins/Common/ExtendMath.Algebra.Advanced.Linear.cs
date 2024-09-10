/****************************************************
	文件：ExtendMath.Algebra.Advanced.Linear.cs
	作者：lenovo
	邮箱: 
	日期：2024/5/21 13:49:26
	功能：线性代数
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Animations;
using static ExtendLinearAlgebra;
using static ExtendLinearAlgebra.Matrix;
using static ExtendMath_Unit;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;




#region 其它
public static partial class ExtendLinearAlgebra
{
	//高斯-赛德尔迭代（Gauss–Seidel method）,Liebmann方法 ,连续位移方法
	//数值线性代数中的一个迭代法，可用来求出线性方程组解的近似值
	//该方法以卡尔·弗里德里希·高斯和路德维希·赛德尔命名。
	//
	//同雅可比法一样，高斯-赛德尔迭代是基于矩阵分解原理。
	//在数值线性代数中，Gauss-Seidel方法也称为Liebmann方法或连续位移方法，是用于求解线性方程组的迭代方法。
		//虽然它可以应用于对角线上具有非零元素的任何矩阵，但只能在矩阵是对角线主导的或对称的和正定的情况下，保证收敛。 
		//在1823年，只在高斯给他的学生Gerling的私人信中提到。1874年之前由塞德尔自行出版。
}
public static partial class ExtendLinearAlgebra
{

		//线性代数是数学的一个分支
		// 研究对象是向量，向量空间（或称线性空间），线性变换和有限维的线性方程组。
		// 向量空间是现代数学的一个重要课题
		//
		// 抽象代数
		// 泛函分析中；通过解析几何，线性代数得以被具体表示。
		// 线性代数的理论已被泛化为算子理论。
		// 由于科学研究中的非线性模型通常可以被近似为线性模型，使得线性代数被广泛地应用于自然科学和社会科学中。

		//研究对象向量、矩阵、行列式
		//抽象代数、泛函分析

}


#endregion

#region 线性方程组
public static partial class ExtendLinearAlgebra//线性方程组
{
	//可以处理矩阵运算
	public class LinearEquations
	{

	}
}
#endregion



#region 行列式
public static partial class ExtendLinearAlgebra//行列式
{
#region 初始化,取值设值方式
/// <summary>初始化,取值设值方式</summary>
public enum EDetValueType
{
	/// <summary>行</summary>
	ROW,
	/// <summary>列</summary>
	COLUM,
}

#endregion

	#region 性质行列式性质
	//行列式转置后值不变
	//互换行列式中两行，值变为相反数
	//行列式中两行成比例，行列式为0
	//行列式中一行所有元素乘以一个数后加到另一行，行列式值不变
	//
	//|a|代表矩阵 A的行列式
	//十字乘法
	//
	//矩阵的行列式是一个可以从方形矩阵（方阵）计算出来的特别的数。
	//矩阵一定要是方形矩阵（就是，行和列的数目相同）
	//
	//将矩阵化简成为三角矩阵：
	//代数余子式：
	//范德蒙行列式
	//克莱姆法则,条件：方程的个数等于未知数的个数
	//
	//det(col(1,2,3)) det(row(4,5,6))=det(row(4,5,6),row(8,10,12),row(12,15,18))
	//det(row(1,2)) + det(row(3,4))=det(row(4,6))
	//det(row(1,2)) - det(row(3,4))=det(row(-2,-2))
	//
	//方法一：化上三角行列式
	//这是求行列式的最基础的方法，一般就是一列（行）乘上一个数加到某一列（行），使其转化为上（下）三角形行列式
	//
	//    方法二：连加法
	//
	//特征：当你发现行列式每一行（列）的值加起来都相等且不等于0时，试试把他们其余行（列）全部加到第一行（列）去，然后再把这个和提出来，从而第一行（列）就全是1了，从而简化行列式。
	//        方法三：滚动消去法
	//
	//特征：当你发现，相邻的行（列）长得比较相似，很多项长得一样时。不妨试试滚动相减。即：最后一行（列）开始的每一行（列）都减去上一行（列）。
	//        四：逐行（列）相加减法
	//
	//该方法是将第一行（列）加（减）到第二行，获得的新的第二行再拿去加（减）第三行。
	//特征：发现前（后）一行（列）中的元素如果去掉“某个元素”后，再和下一行（列）相加减，就能把下一行（列）的某些元素消去，而不带来新的元素。并且前一行（列）中的那个想要去掉的 “某个元素” 能用同样的方法事先先消掉。
	//当然值得注意的是：从最后一行开始和从第一行开始，结果往往会不一样，需要读者在做题的时候，选择好到底应该从哪开始。
	//        五：拆分行列式
	//
	//把一个行列式拆成几个好算的行列式之和
	//
	//
	//        直接按一行（列）展开
	//        七：按拉普拉斯公式，多行展开
	//
	//在算矩阵时，可挖洞后再算，以简化计算。
	//
	//
	//
	//八：加边法
	//
	//当每一行有较多相同元素时，可考虑按一行展开的反向操作，加多一行，然后用新加的行去减其他的行，来简化行列式
	//        九：加边法和范德蒙德行列式一起用
	//        方法十：归纳法
	//
	//该方法多用于证明行列式的值等于某个式子，或对于已经知道结果的行列式使用。同数学归纳法。先证明阶为2 时成立，再从 n-1成立推出n阶也成立。

	#endregion
	public static void Example_Det()
	{
		Det2x2 det2 = new Det2x2(new Vector2(1, 2), new Vector2(4, 3));
		Det3x3 det3 = new Det3x3(new Vector3(1, 2, 3), new Vector3(4, 3, 2), new Vector3(5, 7, 8));
		//
		//13,4,5,6,1
		//13,5,5,78,6
		//13,2,5,6,1
		//13,4,8,6,1
		//1,4,5,6,7
		//Det det = new Det(new List<float> { 13,4,5,6,1});
		//det.AddRow(new List<float> { 13,5,5,78,6});
		//det.AddRow(new List<float> { 13,2,5,6,1});
		//det.AddRow(new List<float> { 13,4,8,6,1});
		//det.AddRow(new List<float> { 1,4,5,6,7});
		Det det =
		   new Det(new List<float> { 12, 3, 3, 1 },EDetValueType.ROW);
		det.AddRow(new List<float> { 1, 2, 4, 3 });
		det.AddRow(new List<float> { 6, 9, 5, 4 });
		det.AddRow(new List<float> { 2, 3, 4, 5 });
		Debug.Log(det2.Value());
		Debug.Log(det3.Value());
		Debug.Log(det.ToString());
		Debug.Log(det[4, 4]);
	}



	#region 基底的Det,只能求值
	#region Det2x2
	public class Det2x2
	{
		float a;
		float b;
		float c;
		float d;



		public Det2x2(Vector2 v1, Vector2 v2, EDetValueType dEetValueType= EDetValueType.COLUM)
		{
			a = v1.x;
			b = v1.y;
			c = v2.x;
			d = v2.y;
		}


		public float Value()
		{
			//   a  c
			//   b  d
			return a * d - b * c;
		}
	}

	#endregion


	#region Det3x2
	public class Det3x2
	{
		float a11;
		float a21;
		float a31;
		float a12;
		float a22;
		float a32;

		public Det3x2(Vector3 v1, Vector3 v2, EDetValueType dEetValueType = EDetValueType.COLUM)
		{

			throw new System.Exception("异常:不是方阵,不能计算");
			a11 = v1.x;
			a21 = v1.y;
			a31 = v1.z;
			a12 = v2.x;
			a22 = v2.y;
			a32 = v2.z;
		}

		//public float Value()
		//{
		//    return 0;
		//}

		public Vector3 Value()
		{

			throw new System.Exception("异常");
			//  return Vector3.Cross(new Vector3(a11, a21, a31), new Vector3(a12, a22, a32));
		}
	}

	#endregion


	#region Det3x3
	public class Det3x3
	{
		float a11;
		float a21;
		float a31;
		float a12;
		float a22;
		float a32;
		float a13;
		float a23;
		float a33;

		public Det3x3(Vector3 v1, Vector3 v2, Vector3 v3, EDetValueType dEetValueType = EDetValueType.COLUM)
		{
			a11 = v1.x;
			a21 = v1.y;
			a31 = v1.z;
			a12 = v2.x;
			a22 = v2.y;
			a32 = v2.z;
			a13 = v3.x;
			a23 = v3.y;
			a33 = v3.z;
		}



		public float Value()
		{
			//   a11   a12   a13
			//   a21   a22   a23
			//   a31   a32   a33
			float value = 0;
			value += a11 * new Det2x2(new Vector2(a22,a32),new Vector2(a23,a33)).Value();
			value -= a12 * new Det2x2(new Vector2(a21, a31), new Vector2(a23, a33)).Value();
			value += a13 * new Det2x2(new Vector2(a21, a31), new Vector2(a22, a32)).Value();


			return value;
		}
	}

	#endregion


	#region Det4x4
	public class Det4x4
	{
		float a11;
		float a21;
		float a31;
		float a41;
		float a12;
		float a22;
		float a32;
		float a42;
		float a13;
		float a23;
		float a33;
		float a43;
		float a14;
		float a24;
		float a34;
		float a44;

		public Det4x4(Vector4 v1, Vector4 v2, Vector4 v3,Vector4 v4, EDetValueType dEetValueType = EDetValueType.COLUM)
		{
			a11 = v1.x;
			a21 = v1.y;
			a31 = v1.z;
			a41 = v1.w;
			//
			a12 = v2.x;
			a22 = v2.y;
			a32 = v2.z;
			a42 = v2.z;
			//
			a13 = v3.x;
			a23 = v3.y;
			a33 = v3.z;
			a43 = v3.z;
			//
			a14 = v4.x;
			a24 = v4.y;
			a34 = v4.z;
			a44 = v4.w;
		}



		public float Value()
		{
			//   a11   a12   a13   a14
			//   a21   a22   a23   a24
			//   a31   a32   a33   a34
			//   a41   a42   a43   a44
			float value = 0;
			value += a11 * new Det3x3(new Vector3(a22, a32, a42), new Vector3(a23, a33, a43), new Vector3(a24, a34, a44)).Value();
			value -= a12 * new Det3x3(new Vector3(a21, a31, a41), new Vector3(a23, a33, a43), new Vector3(a24, a34, a44)).Value();
			value += a13 * new Det3x3(new Vector3(a21, a31, a41), new Vector3(a22, a32, a42), new Vector3(a24, a34, a44)).Value();
			value -= a14 * new Det3x3(new Vector3(a21, a31, a41), new Vector3(a22, a32, a42), new Vector3(a23, a33, a43)).Value();


			return value;
		}
	}

	#endregion
	#endregion




	#region Det
	/// <summary>
	/// 行列式 四阶开始有问题.四阶不是一样的计算方式.先用上面的num x num
	/// <br/>内部是先行后列,方便索引
	/// <br/>二维行列式由三角形面积引出(已知点的坐标)
	/// <br/>https://www.shuxuele.com/algebra/a-calculator.html
	/// </summary>
	public class Det
	{


		#region 前置知识
		//行列式可以看做是有向面积或体积的概念在一般的欧几里得空间中的推广。
		//或者说，在 n 维欧几里得空间中，行列式描述的是一个线性变换对“体积”所造成的影响。
		//
		//上三角,下三角,对角,反对角
		//转置
		//按行展开,按列展开
		//
		//性质1　行列式与它的转置行列式相等。
		//性质2 互换行列式的两行(列)，行列式变号。
		//推论 如果行列式有两行(列)完全相同，则此行列式为零。
		//性质3 行列式的某一行(列)中所有的元素都乘以同一数k，等于用数k乘此行列式。
		//推论 行列式中某一行(列)的所有元素的公因子可以提到行列式符号的外面。
		//性质4 行列式中如果有两行(列)元素成比例，则此行列式等于零。
		//性质5 把行列式的某一列(行)的各元素乘以同一数然后加到另一列(行)对应的元素上去，行列式不变
		// 行列互换，行列式不变；
		// 一行的公因子可以提出去，或者以一数乘行列式的一行就相当于用这个数乘此行列式；
		// 如果行列式中一行为0，那么行列式为0；
		// 某一行是两组数的和，则此行列式等于两个行列式的和，而这两个行列式除此行外与原来的行列式对应的行相同；
		// 如果行列式中有两行相同，那么行列式为0；
		// 行列式中有成比例的两行，则行列式为0；
		// 把一行的倍数加到另一行，行列式值不变；
		// 对换行列式中两行的位置，行列式反号。
		//
		// a11  a12  a13  a14
		// a21  a22  a23  a24
		// a31  a32  a33  a34
		// a41  a42  a43  a44
		//对角线
		// a=a11*a22*a33*a44     right=1
		// a=a21*a32*a43         right=a14
		// a=a31*a42             right=a13*a24
		// a=a41                 right=a12*a23*a34
		//  a[i,x]                  a[i+x,rightStart+1]
		//反对角线
		// a=a11                 right=a24*a33*a42
		// a=a12*a21             right=a34*a43
		// a=a13*a22*a31         right=a44
		// a=a14*a23*a32*a41     right=1
		#endregion


		#region 字属
		List<List<float>> _det;


		int _colCnt { get { return _det[0].Count; } }
		int _rowCnt { get { return _det.Count; } }
		public float this[int row, int col]
		{
			get
			{
				return _det[row - 1][col - 1];
			}
			set
			{
				_det[row - 1][col - 1] = value;
			}
		}


		public Det Copy()
		{
			Det det = new Det(_det[0],EDetValueType.ROW);
			for (int i = 0; i < _rowCnt; i++)
			{
				List<float> colLst = new List<float>();
				for (int j = 0; j < _colCnt; j++)
				{
					colLst.Add(_det[i][j]);
				}
				det.AddRow(colLst);
			}

			return det;
		}
		public Det Transposition()
		{
			float tmp = 0;
			Det det = Copy();
			for (int i = 1; i < _rowCnt; i++)
			{
				for (int j = 0; j < i + 1; j++) //到对角线就行
				{
					tmp = det[i, j];
					det[i, j] = det[j, i];
					det[j, i] = tmp;
				}
			}
			return det;
		}



		#endregion


		#region 构造
		public Det()
		{

		}
		public Det(List<float> row,EDetValueType detValueType)
		{
			if (detValueType == EDetValueType.ROW)
			{ 
				_det = new List<List<float>>();
				_det.Add(row);            
			}

			throw new System.Exception("异常:未定义");

		}
		public Det(int r, int c)
		{
			_det = new List<List<float>>();
			for (int i = 0; i < r; i++)
			{
				List<float> row = new List<float>();
				for (int j = 0; j < c; j++)
				{
					row.Add(0);
				}
				_det.Add(row);
			}
		}


		/// <summary>3行一列</summary>
		public Det(Vector3 v,EDetValueType detValueType=EDetValueType.COLUM)
		{

			switch (detValueType)
			{
				case EDetValueType.COLUM:
					{
						_det = new List<List<float>>();
						_det.Add( new List<float> { v.x});
						_det.Add( new List<float> { v.y});
						_det.Add( new List<float> { v.z});
					}
					break;
				case EDetValueType.ROW:
					{
						_det = new List<List<float>>();
						_det.Add(new List<float> { v.x,v.y,v.z  });
					}
					break;
				default:  throw new System.Exception("异常");
			}
		}


		/// <summary>3行3列</summary>
		public Det(Vector3 v1, Vector3 v2, Vector3 v3, EDetValueType detValueType = EDetValueType.COLUM)
		{

			switch (detValueType)
			{
				case EDetValueType.COLUM:
					{
						_det = new List<List<float>>();
						_det.Add(new List<float> { v1.x, v2.x, v3.x });
						_det.Add(new List<float> { v1.y, v2.y, v3.y});
						_det.Add(new List<float> { v1.z, v2.z, v3.z});
					}
					break;
				case EDetValueType.ROW:
					{
						_det = new List<List<float>>();
						_det.Add(new List<float> { v1.x, v2.x, v3.x });
						_det.Add(new List<float> { v1.y, v2.y, v3.y });
						_det.Add(new List<float> { v1.z, v2.z, v3.z });
					}
					break;
				default: throw new System.Exception("异常");
			}
		}
		#endregion


		#region pub   增删查改
		public void AddLst(List<float> lst, EDetValueType detValueType)
		{

			switch (detValueType)
			{
				case EDetValueType.COLUM: AddCol(lst); break;
				case EDetValueType.ROW:AddRow(lst); break;
				default:break;
			}
		}


		public void AddRow(List<float> row)
		{

			if (row.Count == _colCnt)
			{
				_det.Add(row);
			}
			else if (row.Count < _colCnt) //补全
			{

				int sub = _colCnt - row.Count;
				for (int i = 1; i < sub + 1; i++)
				{
					row.Add(0);
				}
				_det.Add(row);
			}
			else if (row.Count > _colCnt)
			{
				int sub = row.Count - _colCnt;
				int curCols = _colCnt;
				int afterCols = row.Count;
				for (int i = 0; i < _colCnt; i++)
				{
					for (int j = 0; j < sub + 1; j++)
					{
						_det[i].Add(0);
					}
				}
				_det.Add(row);

			}

		}


		public void AddCol(List<float> col)
		{

			throw new System.Exception("异常");
			if (col.Count == _det[1].Count)
			{
				for (int i = 1; i < _rowCnt; i++)
				{
					_det[i].Add(col[i + 1]);
				}
			}
			else if (col.Count < _colCnt) //补全
			{

				int sub = _colCnt - col.Count;
				int curCol = _colCnt;
				for (int i = 1; i < _colCnt + 1; i++) //原行
				{
					for (int j = curCol + 1; j < 1; j++)
					{

					}

				}

			}
			else if (col.Count > _det.Count)
			{
				int sub = col.Count - _det.Count;
				_det.Add(col);
				for (int j = 1; j < sub; j++)
				{
					_det[_rowCnt][j] = 0;
				}
			}

			throw new System.Exception("异常");
		}




		public float Value()
		{
			if (_rowCnt != _colCnt)
			{
				throw new System.Exception("异常:非方阵不能计算");
			}
			if (_rowCnt == 1 && _colCnt == 1)
			{
				return this[0,0];
			}
			if (_rowCnt == 2 && _colCnt == 2)
			{
				return this[0,0] * this[1,1] - this[1,0] * this[0,1];   //ad-bc
			}
			if (_rowCnt > 3) //4阶及以上  因为返回的是行列式
			{

				throw new System.Exception("异常,未开发");
			}

			int rows = 0;
			int cols = 0;
			float sum = 0;
			float tmp;
			float leftTmp = 1;
			float rightTmp = 1;
			int r = 0; int c = 0;
			float diagonal = 0;
			float reverseDiagonal = 0;
			int leftCnt = 0;
			int rightCnt = 0;
			int rightStart = 0;
			float tmpDet = 0;

			#region 对角线
			//对角线
			// a=a11*a22*a33*a44     right=1
			// a=a21*a32*a43         right=a14
			// a=a31*a42             right=a13*a24
			// a=a41                 right=a12*a23*a34
			for (int i = 0; i < _rowCnt; i++)
			{
				leftCnt = _colCnt - i;
				rightCnt = i;
				leftTmp = 1;
				rightTmp = 1;
				for (int j = 0; j < leftCnt; j++)
				{

					r = i + 1 + j;
					c = j + 1;
					leftTmp *= _det[r - 1][c - 1];
				}
				rightStart = c + 1;
				for (int j = 0; j < rightCnt; j++)
				{
					r = 1 + j;
					c = rightStart + j;
					rightTmp *= _det[r - 1][c - 1];
				}
				tmp = leftTmp * rightTmp;
				diagonal += tmp;
			}
			#endregion


			#region 反对角线
			//反对角线
			// a=a11                 right=a24*a33*a42
			// a=a12*a21             right=a34*a43
			// a=a13*a22*a31         right=a44
			// a=a14*a23*a32*a41     right=1
			for (int i = 0; i < _rowCnt; i++)
			{
				leftCnt = i + 1;
				rightCnt = _colCnt - leftCnt;
				leftTmp = 1;
				rightTmp = 1;
				for (int j = 0; j < leftCnt; j++)
				{
					r = j + 1;
					c = leftCnt - j;
					leftTmp *= _det[r - 1][c - 1];
				}
				rightStart = r + 1;
				for (int j = 0; j < rightCnt; j++)
				{
					r = rightStart + j;
					c = _colCnt - j;
					rightTmp *= _det[r - 1][c - 1];
				}
				tmp = leftTmp * rightTmp;
				reverseDiagonal -= tmp;
			}
			#endregion

			return diagonal + reverseDiagonal;
		}
		#endregion

		#region 运算





		public Det Add(Det det)
		{
			if (!EqualRowAndCol(det))
			{
				return null;
			}
			Det res= new Det(_rowCnt,_colCnt);
			for (int i = 0; i < _rowCnt; i++)
			{
				for (int j = 0; j < _colCnt; j++)
				{
					det[i, j] = this[i, j] + det[i, j];
				}
			}
			return res;
		}

		public Det Sub(Det det)
		{
			if (!EqualRowAndCol(det))
			{
				return null;
			}
			Det res = new Det(_rowCnt, _colCnt);
			for (int i = 0; i < _rowCnt; i++)
			{
				for (int j = 0; j < _colCnt; j++)
				{
					det[i, j] = this[i, j] - det[i, j];
				}
			}
			return res;
		}


		public float Mult(float para)
		{
			return para * Value();
		}



		public Det Mult( Det det)
		{

			throw new System.Exception("异常:未定义");
			//det(col(1,2,3)) det(row(4,5,6))=det(row(4,5,6),row(8,10,12),row(12,15,18))
			if (this._rowCnt != det._colCnt)
			{
				Debug.LogFormat($"行列不匹配:{this._rowCnt}!={det._colCnt}");
			}
			
			Det res = new Det(this._rowCnt ,det._colCnt);
			int order = this._rowCnt; //或者 det._colCnt
			for (int k = 0; k < order; k++) //新行列式的第几行//尝试从一行或一列扩充到多行多列
			{
				for (int i = 0; i < this._rowCnt; i++) 
				{
					for (int j = 0; j < det._colCnt; j++)
					{
						res[i, j] = this[i, k] * det[k, j];
					}
				}                
			}

			return res;
		}
		#endregion


		#region 打印
		public override string ToString()
		{
			string str = "";
			str += $"Det[{_rowCnt},{_colCnt}]={Value()}\n";
			for (int i = 0; i < _rowCnt; i++)
			{
				for (int j = 0; j < _colCnt; j++)
				{
					str += _det[i][j] + "\t";
				}
				str += "\n";
			}

			return str;
		}

		public static string Show()
		{
			return "Determinant |A| det(A)";
		}
		#endregion

		#region pri
		bool EqualRow(Det det)
		{
			return _rowCnt == det._rowCnt;
		}
		bool EqualCol(Det det)
		{
			return _colCnt == det._colCnt;
		}
		bool EqualRowAndCol(Det det)
		{
			return _rowCnt == det._rowCnt && _colCnt == det._colCnt;
		}
		#endregion  

		#region 内部类
		public class DetUnit
		{
			public int RowIdx;
			public int ColIdx;
			/// <summary>浮点</summary>
			public float value;
			/// <summary>复数</summary>
			public ComplexNumber complexNumbersVal;

			public DetUnit(int rowIdx, int colIdx, float value)
			{
				RowIdx = rowIdx;
				ColIdx = colIdx;
				this.value = value;
			}

			public DetUnit(int rowIdx, int colIdx, ComplexNumber complexNumbers)
			{
				RowIdx = rowIdx;
				ColIdx = colIdx;
				this.complexNumbersVal = complexNumbers;
			}
		}

		#endregion  
	}
	#endregion



}

#endregion  


#region 矩阵                                  
public static partial class ExtendLinearAlgebra  //矩阵
{

	public enum EMatrixValueType
	{ 
		ROW,
		COLUM,
	}


	#region 未能完整处理好Matrix,写一些基础的来使用



	#region Matrix1x1
	public class Matrix1x1
	{

		#region 字属
		public float[,] matrix;
		public const int rowCnt = 1;
		public const int colCnt = 1;
		public float this[int row, int col]
		{
			get
			{
				return matrix[row - 1, col - 1];
			}
			set
			{
				matrix[row - 1, col - 1] = value;
			}
		}
		#endregion


		#region 构造
		public Matrix1x1()
		{
			matrix = new float[rowCnt, colCnt];
		}

		public Matrix1x1(float a11)
		{
			matrix = new float[rowCnt, colCnt];
			matrix[0, 0] = a11;

		}
		#endregion


	}

	#endregion




	#region Matrix1x3
	   public class Matrix1x3
	{

		#region 字属
		public float[,] matrix;
		public const int rowCnt = 1;
		public const int colCnt = 3;
		public float this[int row, int col]
		{
			get
			{
				return matrix[row - 1, col - 1];
			}
			set
			{
				matrix[row - 1, col - 1] = value;
			}
		}
		#endregion


		#region 构造
		public Matrix1x3()
		{
			matrix = new float[rowCnt, colCnt];
		}

		public Matrix1x3(float a11,float a12, float a13)
		{
			matrix = new float[rowCnt, colCnt];
			matrix[0, 0] = a11;
			matrix[0, 1] = a12;
			matrix[0, 2] = a13;

		}
		#endregion


	}

	#endregion


	#region Matrix3x1
	public class Matrix3x1
	{

		#region 字属
		public float[,] matrix;
		public const int rowCnt=3;
		public const int colCnt=1;
		public float this[int row, int col]
		{
			get
			{
				return matrix[row - 1, col - 1];
			}
			set
			{
				matrix[row - 1, col - 1] = value;
			}
		}
		#endregion


		#region 构造
		public Matrix3x1()
		{
			matrix = new float[rowCnt, colCnt];
		}
		public Matrix3x1(Vector3 v, EMatrixValueType eMatrixValueType = EMatrixValueType.COLUM)
		{
			if (eMatrixValueType != EMatrixValueType.COLUM)
			{
				throw new System.Exception("异常:未定义");
			}
			matrix = new float[rowCnt,colCnt];
			matrix[0,0] = v.x;
			matrix[1,0] = v.y;
			matrix[2,0] = v.z;
		}

		public Matrix3x1(float m11,float m21,float m31, EMatrixValueType eMatrixValueType = EMatrixValueType.COLUM)
		{
			if (eMatrixValueType != EMatrixValueType.COLUM)
			{
				throw new System.Exception("异常:未定义");
			}
			matrix = new float[rowCnt, colCnt];
			matrix[0, 0] = m11;
			matrix[1, 0] = m21;
			matrix[2, 0] = m31;
		}
		#endregion

		#region pub 乘法

		public Matrix3x3 Dual()
		{
			Vector3 a = new Vector3(matrix[0, 0], matrix[1, 0], matrix[2, 0]);
			Vector3 v1 = new Vector3(0f, a.z, -a.y);
			Vector3 v2 = new Vector3(-a.z, 0f, a.x);
			Vector3 v3 = new Vector3(a.y, -a.x, 0f);
			Matrix3x3 dual = new Matrix3x3(new Vector3[3] { v1, v2, v3 });
			return dual;
		}


		public Matrix3x1 CrossProduct(Matrix3x1 mb)
		{
			//  用矩阵axb=a(3x3)xb(3x1)

		
			Matrix a = this.Matrix3x1ToMatrix(); 		
			Matrix b = mb.Matrix3x1ToMatrix(); 

			Matrix res = a.CrossProduct(b);
			Matrix3x1 mRes = res.MatrixToMatrix3x1();
			return mRes;
		}



		public Matrix1x1 DotProduct(Matrix3x1 mb)
		{
			Matrix a = this.Matrix3x1ToMatrix();
			Matrix b = mb.Matrix3x1ToMatrix();
			Matrix res = a.DotProduct(b);
			Matrix1x1 mRes = res.MatrixToMatrix1x1();

			return mRes;

		}


		#endregion


	}



	#endregion




	#region Matrix3x3
	public class Matrix3x3
	{

		#region 字属
		public float[,] matrix;
		public const int rowCnt = 3;
		public const int colCnt = 3;
		public float this[int row, int col]
		{
			get
			{
				return matrix[row - 1, col - 1];
			}
			set
			{
				matrix[row - 1, col - 1] = value;
			}
		}
		#endregion


		#region 构造
		public Matrix3x3()
		{ 
		
		}
		
		public Matrix3x3(Vector3[] arr, EMatrixValueType eMatrixValueType = EMatrixValueType.COLUM)
		{
			if (eMatrixValueType != EMatrixValueType.COLUM)
			{
				throw new System.Exception("异常:未定义");
			}
			matrix = new float[rowCnt, colCnt];
			Vector3 v;
			for (int j = 0; j < colCnt; j++)
			{
				v = arr[j];
				for (int i = 0; i < rowCnt; i++)
				{
					matrix[i, j] = v[i]; //Vector3自己带了
				}
			}
		}
		#endregion


		#region 乘法
	



		public Matrix3x1 DotProduct(Matrix3x1 m2)
		{
			if (colCnt != Matrix3x1.rowCnt)//b.rowCnt取不了
			{
				throw new System.Exception($"异常:矩阵列行不匹配{colCnt}!={Matrix3x1.rowCnt}");   
			}
			float a11 = matrix[0, 0];
			float a21 = matrix[1, 0];
			float a31 = matrix[2, 0];
			float a12 = matrix[0, 1];
			float a22 = matrix[1, 1];
			float a32 = matrix[2, 1];
			float a13 = matrix[0, 2];
			float a23 = matrix[1, 2];
			float a33 = matrix[2, 2];
			float b11 = m2[0, 0];
			float b21 = m2[1, 0];
			float b31 = m2[2, 0];
			float res11 = a11 * b11 + a12 * b21 + a13 * b31;
			float res21 = a21 * b11 + a22 * b21 + a23 * b31;
			float res31 = a31 * b11 + a32 * b21 + a33 * b31;
			Matrix3x1 res = new Matrix3x1(new Vector3(res11,res21,res31));
			return res;
		}

		#endregion  
	}

	#endregion

	#endregion


	#region interface
	/// <summary>方便看方法</summary>
	public interface IMatrix
	{


		#region 基础的
		void SetRow(int idx, float[] row);
		 void SetRow(int idx, ComplexNumber[] row);

		 void SetCol(int idx, float[] col);
		 void SetCol(int idx, ComplexNumber[] col);
		void SetVal(int rowIdx, int colIdx, float value);
		 void SetUnit(MatrixUnit unit);
		 MatrixUnit[] GetRow(int idx);
		 MatrixUnit[] GetCol(int idx);
		#endregion
	}
	#endregion








	#region Matrix
	/// <summary> 
	/// 注意开始索引为1,不为0
	/// <br/>a(注意不是Martix矩阵)：矩阵=>方阵=>对角矩阵=>单位矩阵
	/// <br/>行向量（转置）列向量
	/// <br/>向量a * 矩阵 = 向量b（矩阵就a变为b）=>矩阵是对向量的一种变换
	/// <br/>关键词，将矩阵拆成基向量，再乘以变换矩阵 ,具体看Common_Matrix.Rotate_RoundX，Matrix.Rotate_RoundY，Matrix.Rotate_RoundZ
	/// <br/> 符号大括号(),方括号[]
	/// <para/>
	/// <br/>AB 一般不等于 BA .要点就是顺序敏感	(本质是向量/点/矩阵的变换是以原点为中心,旋转后移动,移动后旋转时不同的)
	/// <br/>(AB)C=dualA(BC)
	/// <br/>dualA(B+C)=AB+AC
	/// <br/>(dualA+B)C=AC+BC
	/// </summary>
	public class Matrix    : IMatrix
	{
		#region TODO
		//逆矩阵
		//      转置矩阵
		//      单位矩阵
		//增广矩阵
		//符号矩阵
		//伴随矩阵
		//满秩矩阵
		//      对角矩阵
		//初等矩阵
		//雅可比矩阵
		//共轭矩阵
		//过渡矩阵
		#endregion



		#region 字属
		public int rowCnt;
		public int colCnt;
		public MatrixUnit[,] matrix;//[,] 矩阵  [][]拼图阵（缺这缺那）	//
		/// <summary>注意开始索引为1,不为0</summary>	
		public MatrixUnit this[int row, int col]
		{
			get
			{
				return matrix[row - 1,col - 1];
			}
			set
			{
				matrix[row - 1,col - 1] = value;
			}
		}
		#endregion


		#region 构造
		/// <summary>一定要给个定义时使用的,不是这种情况不要用</summary>
		public Matrix() { }
		public Matrix(int rowCnt, int colCnt, float p = 0)
		{
			//这种构造不需要给出EMatrixValueType
			//if (matrixValueType != EMatrixValueType.ROW)
			//{
			//	Debug.LogErrorFormat($"错误:矩阵构造方式{matrixValueType}");
			//	return;
			//}
			this.rowCnt = rowCnt;
			this.colCnt = colCnt;
			matrix = new MatrixUnit[rowCnt, colCnt];
			for (int i = 0; i < rowCnt; i++)
			{
				for (int j = 0; j < colCnt; j++)
				{
					MatrixUnit unit = new MatrixUnit(i, j, p);
					matrix[i, j] = unit;
				}
			}
		}

		public Matrix(int rowCnt, int colCnt, float[,] arr, EMatrixValueType matrixValueType = EMatrixValueType.ROW)
		{
			if (matrixValueType != EMatrixValueType.ROW)
			{
				Debug.LogErrorFormat($"错误:矩阵构造方式{matrixValueType}");
				return;
			}
			this.rowCnt = rowCnt;
			this.colCnt = colCnt;
			matrix = new MatrixUnit[rowCnt, colCnt];
			MatrixUnit unit;

			if (matrixValueType == EMatrixValueType.ROW)
			{
				for (int i = 0; i < rowCnt; i++)
				{
					for (int j = 0; j < colCnt; j++)
					{
						unit = new MatrixUnit(i, j, arr[i, j]);
						matrix[i, j] = unit;
					}
				}
			}
			else
			{
				for (int i = 0; i < rowCnt; i++)
				{
					for (int j = 0; j < colCnt; j++)
					{
						unit = new MatrixUnit(j, i, arr[i, j]);
						matrix[j, i] = unit;
					}
				}
			}

		}


		#region Vector2
		public Matrix(Vector2 v, EMatrixValueType matrixValueType = EMatrixValueType.COLUM)
		{
			if (matrixValueType != EMatrixValueType.COLUM)
			{
				Debug.LogErrorFormat($"错误:矩阵构造方式{matrixValueType}");
				return;
			}
			this.rowCnt = 2;
			this.colCnt = 1;
			matrix = new MatrixUnit[rowCnt, colCnt];
			SetCol(0, new float[2] { v.x, v.y });
		}

		#endregion


		#region Vector3




		public Matrix(Vector3 v,bool homogeneous=false, EMatrixValueType matrixValueType = EMatrixValueType.COLUM)
		{
			if (matrixValueType != EMatrixValueType.COLUM)
			{
				Debug.LogErrorFormat($"错误:矩阵构造方式{matrixValueType}");
				return;
			}
			if (homogeneous)
			{
				this.rowCnt = 4;
				this.colCnt = 1;
				matrix = new MatrixUnit[rowCnt, colCnt];
				SetCol(0, new float[4] { v.x, v.y, v.z,0f });
			}
			else
			{
				this.rowCnt = 3;
				this.colCnt = 1;
				matrix = new MatrixUnit[rowCnt, colCnt];
				SetCol(0, new float[3] { v.x, v.y, v.z });
			}
	
		}

		public Matrix(Vector3 v1, Vector3 v2, Vector3 v3,EMatrixValueType matrixValueType = EMatrixValueType.COLUM)
		{
			if (matrixValueType != EMatrixValueType.COLUM)
			{
				Debug.LogErrorFormat($"错误:矩阵构造方式{matrixValueType}");
				return;
			}
			this.rowCnt = 3;
			this.colCnt = 3;
			matrix = new MatrixUnit[rowCnt, colCnt];
			SetCol(0, v1);
			SetCol(0, v2);
			SetCol(0, v3);
		}
		#endregion

		#endregion



		#region pub	 IMatrix

		public void SetRow(int idx, float[] row)
		{
			MatrixUnit unit;
			for (int i = 0; i < row.Length; i++)
			{
				unit = new MatrixUnit(idx, i, row[i]);
				matrix[idx, i] = unit;
			}
		}

		public void SetRow(int idx, ComplexNumber[] row)
		{
			MatrixUnit unit;
			for (int i = 0; i < row.Length; i++)
			{
				unit = new MatrixUnit(idx, i, row[i]);
				matrix[idx, i] = unit;
			}
		}

		/// <summary>
		/// 设置列
		/// </summary>
		/// <param name="idx"></param>
		/// <param name="col"></param>
		public void SetCol(int idx, float[] col)
		{
			MatrixUnit unit;
			for (int i = 0; i < col.Length; i++)
			{
				unit = new MatrixUnit(i, idx, col[i]);
				matrix[i, idx] = unit;
			}
		}

		public void ExchangeCol(int idx1,int idx2)
		{
			MatrixUnit unit;
			MatrixUnit[] tmp = GetRow(idx1);
			for (int i = 0; i < colCnt; i++)
			{
				matrix[i, idx1] = matrix[i, idx2];
				matrix[i, idx2] = tmp[i];
			}
		}

		#region 拓展Vector
		public void SetCol(int idx, Vector2 v)
		{
			MatrixUnit unit;
			for (int i = 0; i < 2; i++)
			{
				unit = new MatrixUnit(i, idx, v[i]);
				matrix[i, idx] = unit;
			}
		}
		public void SetCol(int idx, Vector3 v)
		{
			MatrixUnit unit;
			for (int i = 0; i < 3; i++)
			{
				unit = new MatrixUnit(i, idx, v[i]);
				matrix[i, idx] = unit;
			}
		}
		#endregion


		public void SetCol(int idx, ComplexNumber[] col)
		{
			MatrixUnit unit;
			for (int i = 0; i < col.Length; i++)
			{
				unit = new MatrixUnit(i, idx, col[i]);
				matrix[i, idx] = unit;
			}
		}


		public void SetVal(int rowIdx, int colIdx, float value)
		{
			MatrixUnit unit = new MatrixUnit(rowIdx, colIdx, value);
			matrix[rowIdx, colIdx] = unit;
		}


		public void SetUnit(MatrixUnit unit)
		{
			matrix[unit.RowIdx, unit.ColIdx] = unit;
		}


		public MatrixUnit[] GetRow(int idx)
		{
			if (idx >= rowCnt)
			{
				return null;
			}

			MatrixUnit[] row = new MatrixUnit[colCnt];
			for (int i = 0; i < colCnt; i++)
			{
				MatrixUnit unit = new MatrixUnit(idx, i, matrix[idx, i].value);
				row[i] = unit;
			}
			return row;
		}
		public MatrixUnit[] GetCol(int idx)
		{
			if (idx >= colCnt)
			{
				return null;
			}

			MatrixUnit[] col = new MatrixUnit[rowCnt];
			for (int i = 0; i < rowCnt; i++)
			{
				MatrixUnit unit = new MatrixUnit(i, idx, matrix[i, idx].value);
				col[i] = unit;
			}
			return col;
		}

		/// <summary>
		/// 转置  .这种方便接口看方法汇总
		/// </summary>
		/// <param name="O_Matrix"></param>
		/// <returns></returns>
		public  Matrix Transpose()
		{
			Matrix res = new Matrix(colCnt, rowCnt);
			for (int i = 0; i < rowCnt; i++)
			{
				for (int j = 0; j < colCnt; j++)
				{
					res.matrix[j, i] = new MatrixUnit(i, j, matrix[i, j].value);
				}
			}
			return res;
		}

		public Matrix Unit()
		{
			return ExtendMatrix.UnitMatrix(rowCnt, colCnt);

		}

		public Matrix Inverse()
		{
			throw new System.NotImplementedException();
		}

		public Matrix Dual()
		{
			if (rowCnt!=3 || colCnt!=1)//不是3x1
			{
				throw new System.Exception("异常:未定义");
			}
			Matrix3x1 m = new Matrix3x1(this[0, 0].value, this[1, 0].value, this[2, 0].value, EMatrixValueType.COLUM);
			Matrix3x3 dual3x3 = m.Dual();
			Matrix res = dual3x3.Matrix3x3ToMatrix();


			return res;
		}
		#endregion




		#region 内部类
		public class MatrixUnit
		{
			public int RowIdx;
			public int ColIdx;
			/// <summary>浮点</summary>
			public float value;
			/// <summary>复数</summary>
			public ComplexNumber complexNumbersVal;

			public MatrixUnit(int rowIdx, int colIdx, float value=0)
			{
				RowIdx = rowIdx;
				ColIdx = colIdx;
				this.value = value;
			}

			public MatrixUnit(int rowIdx, int colIdx, ComplexNumber complexNumbers)
			{
				RowIdx = rowIdx;
				ColIdx = colIdx;
				this.complexNumbersVal = complexNumbers;
			}
		}

		#endregion


		#region ToString
		public override string ToString()
		{
			string str = string.Format("矩阵[{0},{1}]\n", rowCnt, colCnt);
			for (int i = 0; i < rowCnt; i++)
			{
				for (int j = 0; j < colCnt; j++)
				{
					str += matrix[i, j].value + "\t";
				}
				str += "\n";
			}

			return str;
		}


		#endregion

	}
	#endregion


}

#region 拓展
/// <summary本来写在IMatrix,拆出来好点></summary>
interface IExtendMatrix
{
	#region 变化
	/// <summary>
	/// 转置矩阵
	/// [dualA.DotProduct(B)].Transpose()=[B.Transpose()].DotProduct( dualA.Transpose())
	/// </summary>
	Matrix Transpose();


	/// <summary>
	/// 逆矩阵
	/// 逆向操作</summary>
	Matrix Inverse();


	/// <summary>
	/// 单位矩阵
	/// </summary>
	Matrix Unit();


	/// <summary>对偶矩阵,CrossProduct需要</summary>
	Matrix Dual();
	#endregion



	#region 操作
	/// <summary>乘法,符号*</summary>
	Matrix Multiply( Matrix m2);

	/// <summary>符号·,  a·b=a.Transpose() * b</summary>
	Matrix DotProduct(Matrix m2);
	/// <summary>符号x,  a x b=a.Transpose() * b</summary>
	Matrix CrossProduct(Matrix m2);
	#endregion
}
#endregion


public static partial class ExtendMatrix//点乘 叉乘
{

	public static Matrix DotProduct(this Matrix a, Matrix b)
	{
		Matrix aT = a.Transpose();
		Matrix res = aT.Multiply(b);//相乘

		return res;
	}


	public static Matrix CrossProduct(this Matrix m1, Matrix m2)
	{
		if (m1.rowCnt != 3 || m1.colCnt != 1)//Vector3形式,目前只做了3x3的对偶矩阵
		{
			throw new System.Exception("异常:未定义");
		}

		Matrix3x1 a = m1.MatrixToMatrix3x1();
		Matrix3x3 dual = a.Dual();  //对偶矩阵
		Matrix dualA = dual.Matrix3x3ToMatrix();
		Matrix res = dualA.Multiply(m2);//相乘

		return res;
	}
}


public static partial class ExtendMatrix//不基础的
{

	#region pub	static 提供支持



	#region 单位矩阵
	/// <summary>
	/// 求单位矩阵
	/// </summary>
	/// <param name="matrix"></param>
	/// <returns></returns>
	public static Matrix UnitMatrix(this Matrix matrix)
	{

		return UnitMatrix(matrix.rowCnt, matrix.colCnt);
	}


	public static Matrix UnitMatrix(int rowCnt, int colCnt)
	{

		Matrix matrix = new Matrix(rowCnt, colCnt, 0);
		SetDiagonal(matrix);

		return matrix;
	}
	#endregion



	/// <summary>
	/// 转置
	/// </summary>
	/// <param name="O_Matrix"></param>
	/// <returns></returns>
	public static Matrix Transpose(Matrix m)
	{
		Matrix res = new Matrix(m.colCnt, m.rowCnt);
		for (int i = 0; i < m.rowCnt; i++)
		{
			for (int j = 0; j < m.colCnt; j++)
			{
				res.matrix[j, i] = new MatrixUnit(i, j, m.matrix[i, j].value);
			}
		}
		// a = leftTmp;
		return res;
	}

	public static Matrix Dual(this Matrix m)
	{
		if (m.rowCnt != 3 || m.colCnt != 1)//不是3x1
		{
			throw new System.Exception("异常:未定义");
		}
		Matrix3x1 a = new Matrix3x1(m[0, 0].value, m[1, 0].value, m[2, 0].value, EMatrixValueType.COLUM);
		Matrix3x3 dualA = a.Dual();
		Matrix res = dualA.Matrix3x3ToMatrix();


		return res;
	}


	/// <summary>
	/// 设置对角线
	/// </summary>
	/// <returns></returns>
	public static void SetDiagonal(this Matrix matrix, float value = 1)
	{
		for (int i = 0; i < matrix.rowCnt; i++)
		{
			for (int j = 0; j < matrix.colCnt; j++)
			{
				if (i == j)
				{
					matrix.SetVal(i, j, value);
				}
			}
		}
	}


	#endregion
}
public static partial class ExtendMatrix//基础的Matrix
{

}
public static partial class ExtendMatrix // 加减乘除 浮点
{
	public static Matrix Add(this Matrix matrix, float p)
	{

		for (int i = 0; i < matrix.rowCnt; i++)
		{
			for (int j = 0; j < matrix.colCnt; j++)
			{
				matrix.matrix[i, j].value += p;
			}
		}
		return matrix;
	}
	public static Matrix Sub(this Matrix matrix, float p)
	{

		for (int i = 0; i < matrix.rowCnt; i++)
		{
			for (int j = 0; j < matrix.colCnt; j++)
			{
				matrix.matrix[i, j].value -= p;
			}
		}
		return matrix;
	}


	public static Matrix Multiply(this Matrix matrix, float p)
	{

		for (int i = 0; i < matrix.rowCnt; i++)
		{
			for (int j = 0; j < matrix.colCnt; j++)
			{
				matrix.matrix[i, j].value *= p;
			}
		}
		return matrix;
	}

	public static Matrix Divide(this Matrix matrix, float p)
	{

		for (int i = 0; i < matrix.rowCnt; i++)
		{
			for (int j = 0; j < matrix.colCnt; j++)
			{
				matrix.matrix[i, j].value /= p;
			}
		}
		return matrix;
	}
}

public static partial class ExtendMatrix// 转化
{


	/// <summary>注意时按列</summary>
	public static Vector3[] MatrixToV3(this Matrix m)
	{
		Vector3[] res =new Vector3[m.colCnt];
		for (int i = 0; i < m.colCnt; i++)
		{
			//报错,分开写
			float x = m[1, i+1].value;
			float y = m[2, i+1].value;
			float z = m[3, i+1].value;

			res[i] = new Vector3(x,y,z);

   //         res[i].x= m[0,i].value;
			//res[i].y= m[1,i].value;
			//res[i].z= m[2,i].value;
		}

		return res;
	}
	public static Matrix3x3 MatrixToMatrix3x3(this Matrix m)
	{
		Matrix3x3 res = new Matrix3x3();
		float[,] a = new float[m.rowCnt, m.colCnt];
		for (int i = 0; i < m.rowCnt; i++)
		{
			for (int j = 0; j < m.colCnt; j++)
			{
				res[i, j] = m[i, j].value;
			}
		}
		return res;
	}

	public static Matrix3x1 MatrixToMatrix3x1(this Matrix m)
	{
		Matrix3x1 res = new Matrix3x1();
		float[,] a = new float[m.rowCnt, m.colCnt];
		for (int i = 0; i < m.rowCnt; i++)
		{
			for (int j = 0; j < m.colCnt; j++)
			{
				res[i, j] = m[i, j].value;
			}
		}

		return res;
	}

	public static Matrix1x1 MatrixToMatrix1x1(this Matrix m)
	{
		Matrix1x1 res = new Matrix1x1(m[0, 0].value);
		return res;
	}

	public static Matrix Matrix3x3ToMatrix(this Matrix3x3 m)
	{
		float[,] a = new float[3, 3];
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				a[i, j] = m[i, j];
			}
		}
		return new Matrix(3, 3, a, EMatrixValueType.ROW);
	}

	public static Matrix Matrix1x1ToMatrix(this Matrix1x1 m)
	{
		Matrix res = new Matrix(1, 1);
		res[0, 0].value = m[0, 0];
		return res;
	}
	public static Matrix Matrix3x1ToMatrix(this Matrix3x1 m)
	{
		Matrix res = new Matrix(3, 1);
		res[0, 0].value = m[0, 0];
		res[0, 1].value = m[0, 1];
		res[0, 2].value = m[0, 2];
		return res;

	}

	public static Vector3 Matrix3x1ToV3(this Matrix3x1 m)
	{
		float x = m[0, 0];
		float y = m[1, 0];
		float z = m[2, 0];
		Vector3 res = new Vector3(x, y, z);
		return res;
	}
}

public static partial class ExtendMatrix //MatrixUnit
{
	public static MatrixUnit Inverse(this MatrixUnit matrixUnit)
	{
		matrixUnit.value = -matrixUnit.value;
		return matrixUnit;

	}
}
public static partial class ExtendMatrix //缩放 矩阵 变换 2D 3D
{
	/// <summary>变换类型</summary>
	public enum ETransformation
	{
		/// <summary>缩放,
		/// <br/>AddCol[n,0],AddCol[0,1]	,缩放x轴
		/// <br/>AddCol[1,0],AddCol[0,m]	,缩放y轴
		/// </summary>
		Scale,
		/// <summary>翻转,
		/// <br/>AddCol[-m,0],AddCol[0,1],沿着x轴翻转
		/// <br/>AddCol[1,0],AddCol[0,-n],沿着y轴翻转
		/// </summary>
		Flip,
		/// <summary>切变,拉扯,
		/// <br/>AddCol[m,n],AddCol[0,1]=>拉扯x轴
		/// <br/>AddCol[1,0],AddCol[m,n]=>拉扯y轴
		/// </summary>
		Shear,
		/// <summary>
		///  旋转
		///  <br/>结合正方形(1x1)旋转后的两个点的变化,组成方程组,可以求出旋转矩阵
		///  <br/>AddCol[cos,sin],AddCol[-sin,cos]
		/// </summary>
		Rotate,
		/// <summary>平移
		/// 仿射坐标,包括,平移,齐次坐标
		/// </summary>
		Transiation,
	}

	//public static Matrix Transformation(this Matrix m,float x,float i,float z=1f)

	[Obsolete("已过时")]
	public static Matrix Transformation(this Matrix m, ETransformation t)
	{
		Matrix tM=new Matrix();

		switch ( t )
		{
			case ETransformation.Scale:
				{
					//tM = ScaleMatrix(x,i,z);  //适合单独用

				}
				break;
			case ETransformation.Flip:
				{
					//tM = FlipMatrix (x,i, z);	//适合单独用
				}
				break;
			case ETransformation.Rotate:
				{
					//tM = RotateMatrix(x, i, z); //适合单独用
				}
				break;
			case ETransformation.Transiation:
				{
				   // tM = TransiationMatrix(x, i, z); //适合单独用
				}
				break;
			default:
				{

				}
				break;
		}

		return tM.Multiply(m);


		#region 看样子不用分别
		//switch ( t )
		//{
		//	case ETransformation.Scale :
		//		{
  //                  tM = new Matrix(3, 3);
  //                  tM[0, 0].value = x;
  //                  tM[1, 1].value = i;
  //                  tM[2, 2].value = z;
  //                  return tM.Multiply(m);
		//		}
  //          case ETransformation.Flip:
  //              {

  //              }
  //              break;
  //          case ETransformation.Shear:
  //              {

  //              }
  //              break;
  //          default:
		//		{

		//		}
		//		break;
		//}

		#endregion

	}

	/// <summary>操作方向</summary>
	public enum EVector
	{ 
		X,Y,Z ,XY,XZ,YZ,XYZ
	}


	#region 2D
	public	static Matrix ScaleMatrix2D(float scaleX,float scaleY)
	{
		Matrix tM = new Matrix(3, 3);//不初始化后面用不了
		tM[0, 0].value = scaleX;
		tM[1, 1].value = scaleY;
		tM[2, 2].value = 1;	 //齐次

		return tM;

	}


	/// <summary>m=Add(-1,0).Add(0,1) 或者 -m 或者	  Add(-1,0).Add(0,-1)</summary>
	public static Matrix FlipMatrix2D(EVector eVector)
	{
		Matrix tM = new Matrix(3, 3);//不初始化后面用不了
		tM = tM.Unit();

		int a = 1;
	
		switch (eVector)
		{
			case EVector.X :tM[0, 0].value= tM[0, 0].value.Inverse(); break;
			case EVector.Y :tM[1, 1].value= tM[1, 1].value.Inverse(); break;
			case EVector.XY :
				{ 
					tM[0, 0].value = tM[0, 0].value.Inverse();
					tM[1, 1].value= tM[1, 1].value.Inverse();				
				}break;
			default:throw new System.Exception("异常:未定义");
		}
			
		return tM;

	}


	/// <summary>
	/// 旋转
	/// m=Add(cos,sin).Add(sin,-cos)
	/// </summary>

	public static Matrix RotateMatrix2D(float rotateRadian)
	{
		//m=AddCol(a,b)  AddCol(c,d) .Mutipl i AddCol(x,i)
		//afetrX=  ax+by
		//afetrY=  bx+dy
		//
		//

		Matrix tM = new Matrix(3, 3);//不初始化后面用不了
		tM.SetCol(0,new float[3] {rotateRadian.Cos(),rotateRadian.Sin(),0 });
		tM.SetCol(1,new float[3] {-rotateRadian.Sin(),rotateRadian.Cos(),0 });
		tM.SetCol(2,new float[3] {0,0,1 });

		return tM;

	}


	/// <summary>
	/// 平移 
	/// 齐次坐标:统一变换,尤其是平移
	/// 
	/// </summary>
	public static Matrix TransiationMatrix2D(this Vector2 v, float tx,float ty)
	{
		//m=AddCol(a,b)  AddCol(c,d) .Mutipl i AddCol(x,i) +AddCol(tx,ty)
		//afetrX=  ax+by +t1
		//afetrY=  bx+dy	+t2
		//
		// point(x,i)=>(x,i,1).Transpose()
		// Vector(x,i)=>(x,i,0).Transpose()
		//
		//v+v=v	(?,?,0)
		//p-p=v(?,?,1-1=0)
		//p+v=p(?,?,1-1=0)
		//p+p= (?,?,1+1=2)(x,i,w)=>(x/w,i/w,1)当w=2,(x/2,i/2,1)为重点
		//
		//	最终提出了仿射坐标AddCol(a,b,0) AddCol(c,d,0) AddCol(tx,ty,1)
		Matrix b =new Matrix(3,1);
		b.SetCol(0,new float[] { v.x,v.y,1});
		//
		Matrix tM = new Matrix(3, 3);//不初始化后面用不了
		tM.SetCol(0, new float[] { 1, 0, 0 });
		tM.SetCol(1, new float[] { 0, 1, 0 });
		tM.SetCol(2, new float[] { tx, ty, 1 });
		//
		Matrix res = tM.Multiply(b);

		return res;

	}
	#endregion


	#region 3D


	#region 缩放
	public static Matrix ScaleMatrix(float scaleX, float scaleY, float scaleZ)
	{
		Matrix tM = new Matrix(4, 4);//不初始化后面用不了
		tM[1, 1].value = scaleX;
		tM[2, 2].value = scaleY;
		tM[3, 3].value = scaleZ;
		tM[4, 4].value = 1;  //齐次

		return tM;

	}

	public static Vector3 ScaleVector3(this Vector3 v,float scaleX, float scaleY, float scaleZ)
	{
		Matrix tM = ScaleMatrix( scaleX,  scaleY,  scaleZ);
		Matrix vM = new Matrix(v,true);
		Matrix afterVM = tM.Multiply(vM);
		Vector3 afterV = afterVM.MatrixToV3()[0];


		return afterV;

	}
	#endregion



	/// <summary>m=Add(-1,0).Add(0,1) 或者 -m 或者	  Add(-1,0).Add(0,-1)</summary>
	public static Matrix FlipMatrix(EVector eVector)
	{
		Matrix tM = new Matrix(4, 4);//不初始化后面用不了
		tM = tM.Unit();

		switch (eVector)
		{
			case EVector.X: tM[0, 0].Inverse(); break;
			case EVector.Y: tM[1, 1].Inverse(); break;
			case EVector.Z: tM[2, 2].Inverse(); break;
			case EVector.XY: 
				tM[0, 0].Inverse(); 
				tM[1, 1].Inverse();
				break;
			case EVector.YZ: 
				tM[1, 1].Inverse(); 
				tM[2, 2].Inverse();
				break;
			case EVector.XZ: 
				tM[0, 0].Inverse(); 
				tM[2, 2].Inverse();
				break;
			case EVector.XYZ: 
				tM[0, 0].Inverse(); 
				tM[0, 0].Inverse();  
				tM[2, 2].Inverse();
				break;
			default: throw new System.Exception("异常:未定义");
		}

		return tM;

	}




	#region 旋转


	/// <summary>
	/// 旋转
	/// axis旋转轴
	/// m=Add(cos,sin).Add(sin,-cos)
	/// </summary>

	public static Matrix RotateAxisMatrix(float rotateRadian,EAxis axis )
	{
		//m=AddCol(a,b,, 0)  AddCol(d,e,f,0) AddCol(g,h,i,0) AddCol(tx,ty,tz,1)
		//afetrX=  ax+by+cz 
		//afetrY=  dx+ey+fz	
		//afetrZ=  gx+hy+iz	
		//
		//(默认逆时针维正方向)	 EAxis.X	:i->z  =>  i.CrossProduct(z)=x
		//(默认逆时针维正方向)	 EAxis.Y	:z->x  =>  z.CrossProduct(x)=i (这个的旋转矩阵样式不一样,-sin在左下)
		//(默认逆时针维正方向)	 EAxis.Z	:x->i  =>  x.CrossProduct(i)=z

		Matrix tM = new Matrix(4, 4);//不初始化后面用不了
		tM = tM.Unit();
		float sin= rotateRadian.Sin();
		float cos= rotateRadian.Cos();
		switch (axis)
		{
			case EAxis.X :
				{
					tM.SetCol(1, new float[4] {cos,-sin, 0,0 });
					tM.SetCol(2, new float[4] {sin,cos, 0,0 });
				}
				break;
			case EAxis.Y:
				{
					tM.SetCol(0, new float[4] {cos,sin, 0, 0 });
					tM.SetCol(2, new float[4] {-sin,cos, 0, 0 });
				}
				break;
			case EAxis.Z:
				{
					tM.SetCol(0, new float[4] {cos,-sin, 0, 0 });
					tM.SetCol(1, new float[4] {sin,cos, 0, 0 });
				}
				break;
			default:throw new System.Exception("异常:未定义"); 
		}


		return tM;

	}


	public static Vector3 RotateVector3(this Vector3 v, float rotateRadianX, float rotateRadianY, float rotateRadianZ, bool direct = true)
	{
		Matrix tM = RotateMatrix(rotateRadianX, rotateRadianY, rotateRadianZ);
		Matrix vM = new Matrix(v,true);
		Matrix afterVM = tM.Multiply(vM);
		Vector3 afterV = afterVM.MatrixToV3()[0];
		return afterV;

	}
	public static Matrix RotateMatrix(float rotateRadianX, float rotateRadianY, float rotateRadianZ,bool direct=true)
	{
		if (direct)
		{ 
		return RotateMatrix_Directly(rotateRadianX, rotateRadianY, rotateRadianZ);
		
		}
		return RotateMatrix_AxisByAxis(rotateRadianX, rotateRadianY, rotateRadianZ);
	}


	public static Matrix RotateMatrix_Directly(float rotateRadianX, float rotateRadianY, float rotateRadianZ)
	{
		float x = rotateRadianX;
		float y = rotateRadianY;
		float z = rotateRadianZ;
		float sinX = x.Sin();
		float cosX = x.Cos();
		float sinY = y.Sin();
		float cosY = y.Cos();
		float sinZ = z.Sin();
		float cosZ = z.Cos();


		Matrix tM = new Matrix(4, 4);//不初始化后面用不了
		tM = tM.Unit();
		tM.SetCol(0,new float[] {cosY*cosZ,		cosX*sinZ+sinY*sinY*cosZ,	sinX*sinZ-cosX*sinY*cosZ,	0 } );
		tM.SetCol(1,new float[] {-cosY*sinZ,	cosX*cosZ-sinX*sinY*sinZ,	sinX*cosZ+cosX*sinY*sinZ,	0 } );
		tM.SetCol(2,new float[] {sinY,			-sinX*cosY,					cosX*cosY,					0 } );


		return tM;

	}

	public static Matrix RotateMatrix_AxisByAxis(float rotateRadianX, float rotateRadianY, float rotateRadianZ)
	{
		Matrix xTm = new Matrix(4, 4);//不初始化后面用不了
		Matrix yTm = new Matrix(4, 4);
		Matrix zTm = new Matrix(4, 4);
		xTm = RotateAxisMatrix(rotateRadianX,EAxis.X);
		yTm = RotateAxisMatrix(rotateRadianY,EAxis.Y);
		zTm = RotateAxisMatrix(rotateRadianZ,EAxis.Z);
		Matrix res= new Matrix();
		res = (xTm.CrossProduct(yTm)).CrossProduct(zTm); ;

		return res;

	}
	#endregion



	#region 平移
	/// <summary>
	/// 返回平移矩阵 
	/// 齐次坐标:统一变换,尤其是平移
	/// 
	/// </summary>
	public static Matrix TransiationMatrix(float tx, float ty, float tz)
	{
		//m=AddCol(a,b,, 0)  AddCol(d,e,f,0) AddCol(g,h,i,0) AddCol(tx,ty,tz,1)
		//afetrX=  ax+by+cz +tx
		//afetrY=  dx+ey+fz	+ty
		//afetrZ=  gx+hy+iz	+tz
		//
		// point(x,i)=>(x,i,z,1).Transpose()
		// Vector(x,i)=>(x,i,z,0).Transpose()
		//
		//v+v=v	(?,?,?,0)
		//p-p=v(?,?,?,1-1=0)
		//p+v=p(?,?,?,1-1=0)
		//p+p= (?,?,?,1+1=2)(x,i,z,w)=>(x/w,i/w,z/w,1)当w=2,(x/2,i/2,z/2,1)为中点
		//
		//	最终提出了仿射坐标AddCol(a,b,0) AddCol(c,d,0) AddCol(tx,ty,1)
		Matrix tM = new Matrix(4, 4);//不初始化后面用不了
		tM.SetCol(0, new float[] { 1, 0, 0, 0 });
		tM.SetCol(1, new float[] { 0, 1, 0, 0 });
		tM.SetCol(2, new float[] { 0, 0, 1, 0 });
		tM.SetCol(3, new float[] { tx, ty, tz, 1 });
		//
		return tM;
	}

	/// <summary>
	/// 平移 
	/// 齐次坐标:统一变换,尤其是平移
	/// 
	/// </summary>
	public static Vector3 TransiationVector3(this Vector3 v, float tx, float ty, float tz)
	{
		Matrix b = new Matrix(4, 1);
		b.SetCol(0, new float[] { v.x, v.y, v.z, 1 });
		//
		Matrix tM = TransiationMatrix( tx,  ty,  tz);
		Matrix resM = tM.Multiply(b);
		Vector3 resV = resM.MatrixToV3()[0];
		return resV;
	}


	#endregion


	#endregion


	#region 正交投影

	/// <summary>正交投影</summary>
	public static Vector3 OrthogonalProjection(this Vector3 v , EAxis axis=EAxis.Z)
	{
		Matrix tM = new Matrix(4,4);
		tM=tM.Unit();
		int idx = 0;
		switch (axis)
		{
			case EAxis.X: idx = 0; break;
			case EAxis.Y: idx = 1; break;
			case EAxis.Z: idx = 2; break;
			default: break;
		}

		tM.SetCol(idx,new float[4] { 0f,0f,0f,0f});
		//
		Matrix vM = new Matrix(v,true);
		Matrix afterVM=tM.Multiply(vM);
		Vector3 afterV = afterVM.MatrixToV3()[0];

		return afterV;
	}






	#endregion
	#region 透视投影


	public static Vector3 PerspectiveProjection(this Vector3 v, float focalLength)
	{
		//n000
		//0n00
		//0000
		//0010
		Matrix tM = PerspectiveMatrix();
        tM.SetCol(0, new float[] { focalLength, 0, 0, 0 });
        tM.SetCol(1, new float[] { 0, focalLength, 0, 0 });
        //
        Matrix vM = new Matrix(v, true);
        Matrix afterVM = tM.Multiply(vM);
        Vector3 afterV = afterVM.MatrixToV3()[0];

        return afterV;
    }
	public static Matrix PerspectiveMatrix()
	{
		//1000
		//0100
		//0000
		//0010
        Matrix tM = new Matrix(4, 4);
        tM = tM.Unit();
        tM.ExchangeCol(2, 3);
        tM.SetCol(2, new float[] { 0, 0, 0, 0 });
		return tM;

    }
	public static Vector3 PerspectiveProjection(this Vector3 v)
	{
		Matrix tM = PerspectiveMatrix();
        //
        Matrix vM = new Matrix(v,true);
		Matrix afterVM = tM.Multiply(vM);
		Vector3 afterV = afterVM.MatrixToV3()[0];

		return afterV;
	}

	#endregion


}
public static partial class ExtendMatrix //加减乘除 矩阵
{
	public static Matrix Add(this Matrix m1, Matrix m2)
	{
		if (m1.rowCnt != m2.rowCnt || m1.colCnt != m2.colCnt)
		{
			Debug.LogErrorFormat("矩阵相加吗，行列不一致：matrix1：matrix2=（{0},{1}）：（{2},{3}）",
				m1.rowCnt, m2.rowCnt,
				m1.colCnt, m2.colCnt);
			return null;
		}

		int rowCnt = m1.rowCnt;
		int colCnt = m1.colCnt;
		Matrix matrix = new Matrix(rowCnt, colCnt);
		for (int i = 0; i < rowCnt; i++)
		{
			for (int j = 0; j < colCnt; j++)
			{
				float value = m1.matrix[i, j].value + m2.matrix[i, j].value;
				MatrixUnit unit = new MatrixUnit(i, j, value);
				matrix.matrix[i, j] = unit;
			}
		}

		return matrix;
	}




	/// <summary>
	/// (MxN)矩阵乘(NxP)矩阵=(MxP)矩阵,内标同,抵消
	/// <para/>测试过3x2乘以2x4,正确
	/// </summary>	
	public static Matrix Multiply(this Matrix m1, Matrix m2)
	{
		// if (a.列数 != b.行数)
		if (m1.colCnt != m2.rowCnt)
		{
			Debug.LogErrorFormat("矩阵1的行数{0}!=矩阵2的列数{1}", m1.colCnt, m2.rowCnt);
			return null;
		}
		int N = m1.colCnt;//或 b.rowCnt ,	(MxN)矩阵乘(NxP)矩阵=(MxP)矩阵中的N
		Matrix res = new Matrix(m1.rowCnt, m2.colCnt);//(MxN)矩阵乘(NxP)矩阵=(MxP)矩阵
																//
		MatrixUnit unit;//临时变量
		for (int i = 0; i < res.rowCnt; i++) //	前行
		{
			for (int j = 0; j < res.colCnt; j++) //后列
			{
				//GAMES101强调的技巧就是这里.res[i,x]就取matrix1的第i行, matrix2的第i列
				MatrixUnit[] row = m1.GetRow(i); //取前行
				MatrixUnit[] col = m2.GetCol(j); //取后列
													  //
				float value = 0;
				for (int n = 0; n < N; n++)
				{
					value += row[n].value * col[n].value;//按索引相乘后相加
				}
				unit = new MatrixUnit(i, j, value);
				res.matrix[i, j] = unit; //(i,x)
			}
		}

		return res;
	}


	public static Matrix Multiply(this Matrix m1, Vector3 v)
	{
		// if (a.列数 != b.行数)
		Matrix matrix2 = new Matrix(v, EMatrixValueType.COLUM);
		return m1.Multiply(matrix2);
	}

	public static Matrix Multiply(Matrix m1, Vector2 v)
	{
		// if (a.列数 != b.行数)
		Matrix matrix2 = new Matrix(v, EMatrixValueType.COLUM);
		return m1.Multiply(matrix2);
	}

	public static Matrix1x1 Multiply(this Matrix1x3 m1, Matrix3x1 m2)
	{
		float a11 = m1[0, 0] * m2[0, 0] + m1[0, 0] * m2[0, 0] + m1[0, 0] * m2[0, 0];
		return new Matrix1x1(a11);

	}

}

#endregion  



