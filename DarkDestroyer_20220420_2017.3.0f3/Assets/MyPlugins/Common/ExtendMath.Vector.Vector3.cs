/****************************************************
	文件：EXtendVector3.cs
	作者：lenovo
	邮箱: 
	日期：2023/5/16 10:51:58
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static ExtendVector;
using Random = UnityEngine.Random;



public static partial class ExtendVector3 //Lst	 有的格式识别不了Vector3
{
	public static List<float> VectorLst2FloatLst(this List<Vector3> vlst)
	{ 
		 List<float> flst = new List<float>();
		for (int i = 0; i < vlst.Count; i++)
		{
			flst.Add(vlst[i].x);  
			flst.Add(vlst[i].y);  
			flst.Add(vlst[i].z);  
		}

		return flst;
	}

	public static List<Vector3> FloatLst2VectorLst(this List<float> flst)
	{
		List<Vector3> vlst = new List<Vector3>();
		for (int i = 0; i < flst.Count; i+=3)
		{
			vlst.Add(new Vector3(flst[i], flst[i+1], flst[i+2])) ;
		}

		return vlst;
	}
}

public static partial class ExtendVector3 //dir
{

}
public static partial class ExtendVector3
{
	public static void Example()
	{
		{
			#if NET_4_7_2_OR_NEWER
			Vector3 v1 = new Vector3(1, 1, 1);
			Vector3 v2 = new Vector3(2, 2, 2);
			Debug.Log($"{v1}+{v2}={v1.Vector3Add(v2)}");
			Debug.Log($"v1={v1},v2={v2}");
			Debug.Log($"{v1}-{v2}={v1.Vector3Sub(v2)}");
			Debug.Log($"v1={v1},v2={v2}");
#endif
		}
		{
			Vector3 v1 = new Vector3(1, 1, 1);
			Vector3 v2 = new Vector3(2, 2, 2);
			Debug.Log($"{v1}+{v2}={ExtendVector3.Vector3Add(v1, v2)}");
			Debug.Log($"v1={v1},v2={v2}");
			Debug.Log($"{v1}-{v2}={ExtendVector3.Vector3Sub(v1, v2)}");
			Debug.Log($"v1={v1},v2={v2}");
		}
	}
}
public static partial class ExtendVector3
{ 
	#region Add Sub （没有ref的）
	/// <summary>返回新的，不改变原来的两个</summary>
	public static Vector3 Vector3Add(  Vector3 v1, Vector3 v2)
	{
		return new Vector3(
			v1.x + v2.x,
			v1.y + v2.y,
			v1.z + v2.z
			);
	}

	public static Vector3 Vector3Sub( Vector3 v1, Vector3 v2)
	{
		return new Vector3(
			v1.x - v2.x,
			v1.y - v2.y,
			v1.z - v2.z
			);
	}
	#endregion

}


public static partial class ExtendVector3
{
#if NET_4_7_2_OR_NEWER

	#region Add Sub （ref的）
	public static Vector3 Vector3Add(ref this Vector3 v1,Vector3 v2)
	{
		v1= new Vector3(
			v1.x + v2.x,
			v1.y + v2.y,
			v1.z + v2.z
			);
		return v1;
	}

	public static Vector3 Vector3Sub(ref this Vector3 v1, Vector3 v2)
	{
		v1= new Vector3(
			v1.x - v2.x,
			v1.y - v2.y,
			v1.z - v2.z
			);
		return v1;
	}


	#endregion


	#region AddXYZ ref 

	public static Vector3 AddX(ref this Vector3 v, float x)
	{
		v = new Vector3(
			v.x+x,
			v.y ,
			v.z
			);
		return v;
	}
	public static Vector3 AddY(ref this Vector3 v, float y)
	{
		v = new Vector3(
			v.x ,
			v.y + y,
			v.z
			);
		return v;
	}

	public static Vector3 AddZ(ref this Vector3 v, float z)
	{
		v = new Vector3(
			v.x,
			v.y ,
			v.z+z
			);
		return v;
	}

	#endregion



	#region Set 不加ref不行


	public static Vector3 SetX(ref this Vector3 v, float value)
	{
		Vector3 pos = v;
		v = new Vector3(value, pos.y,pos.z);
		return v;
	}


	public static Vector3 SetY( ref this Vector3 v, float value)
	{
		Vector3 pos = v;
		v = new Vector3(pos.x, value, pos.z);
		return v;
	}


	public static Vector3 SetZ(ref this Vector3 v, float value)
	{
		Vector3 pos = v;
		v = new Vector3(pos.x, pos.y, value);
		return v;
	}

	public static Vector3 SetXY( this Vector3 v, float x,float y)
	{
		Vector3 pos = v;
		v = new Vector3(x, y,pos.z);
		return v;
	}



	/// <summary>ref直接动v的内存</summary>
	public static Vector3 Mult(ref this Vector3 v, float x, float y, float z)
	{
		v.x *= x;
		v.y *= y;
		v.z *= z;

		return v;
	}

	#endregion
#endif




}

public static partial class ExtendVector3
{
	  #if NET_4_7_2_OR_NEWER
  #region 取反 （ref的）
	public static Vector3 ReverseX(ref this Vector3 v)
	{
		float x = v.x;
		v.x = -x;
		return v;
	}
	public static Vector3 ReverseY(ref this Vector3 v)
	{
		float y = v.y;
		v.y = -y; 
		return v; 
	}
	public static Vector3 ReverseZ(ref this Vector3 v)
	{
		float z = v.z;
		v.z = -z;
		return v;
	}
	#endregion
#endif

}

public static partial class ExtendVector3     //角度 弧度
{

	/// <summary>无符号角度,角度不超过180</summary>
	public static float Degree(this Vector3 from, Vector3 to)
	{
		return Vector3.Angle(from,to);
	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="from"></param>
	/// <param name="to"></param>
	/// <param name="axis">旋转轴</param>
	/// <param name="dirOffset">在offset还是正时针</param>
	/// <returns></returns>
	public static EDir SignedDegree(this Vector3 from, Vector3 to,EAxis axis,float dirOffset=0)
	{
		Vector3 rotateVector=Vector3.zero;
		switch (axis)
		{
			case EAxis.X :rotateVector = Vector3.right;   break;
			case EAxis.Y :rotateVector = Vector3.up;      break;
			case EAxis.Z :rotateVector = Vector3.forward; break;
			default:  break;
		}

		float degree= Vector3.SignedAngle(from, to,rotateVector);
		if (degree < 0-dirOffset) return EDir.CONTRACLOCKWISE;
		if (degree >0+dirOffset ) return EDir.CLOCKWISE;
		return EDir.MIDDLECLOCKWISE;
	}



	public static EDir Dir_DotProduct(this Vector3 from, Vector3 to)
	{
		float radian = Vector3.Dot(from, to - from);
		if (radian == 1) return EDir.SAME;
		if (radian == -1) return EDir.OPPOSITE;
		if (radian == 0) return EDir.VERTICAL;
		if (radian > 0) return EDir.FRONT;
		if (radian < 0) return EDir.BEHIND;
		return EDir.NULL;
	}


	/// <summary></summary>
	public static float RadianOffset(this Vector3 from, Vector3 to)
	{
		float radian = Vector3.Dot(from, to - from);
		return radian;
	}

	public static float DegreeOffset(this Vector3 from, Vector3 to)
	{
		float radian = Vector3.Dot(from, to - from);
		float degree = (radian / from.Mod() * (to - from).Mod()).Acos().Radian2Degree(); //dot=|l||m|cos=
		return degree;
	}
}
public static partial class ExtendVector3  //Mod
{
	public static float ModPow2(this Vector3 v)
	{ 
		   return v.Mod().Pow2();
		//return v.sqrMagnitude;
	}


	public static float Mod(this Vector3 v)
	{
		return v.magnitude;
	}
}
public static partial class ExtendVector3//点击 叉积
{


	/// <summary>
	/// 两个向量的相似性
	/// </summary>
	public static float DotProduct(this Vector3 a, Vector3 b, EVectorCalcType vectorCalcType= EVectorCalcType.TRIANGLE)
	{

		switch (vectorCalcType)
		{
			case EVectorCalcType.COORDINATE :  return a.DotProduct_Coordinate(b);
			case EVectorCalcType.TRIANGLE :  return a.DotProduct_Triangle(b);
			default: throw new System.Exception("异常");
		}

	}

	/// <summary平行四边形</summary>
	/// <summary>Mod=平行四边形的面积</summary>
	public static float CorssProduct_Triangle(this Vector3 a, Vector3 b)
	{
		float degree = Vector3.Angle(a, b);
		float radian = degree.Degree2Radian();

		return a.Mod() * b.Mod() * (radian.Sin());
	}

	/// <summary>新的三维</summary>
	public static Vector3 CorssProduct_Coordinate(this Vector3 a, Vector3 b)
	{
		//axb=(l,m,n) x (o,p,q)=(mq-np,no-lq,lp-mo)
		float x = a.y * b.z - a.z * b.y;
		float y = a.z * b.x - a.x * b.z;
		float z = a.x * b.y - a.y * b.x;
		return new Vector3(x, y, z);
		// lhs.y * rhs.z - lhs.z * rhs.y,
		// lhs.z * rhs.x - lhs.x * rhs.z,
		// lhs.x * rhs.y - lhs.y * rhs.x;
		//  return Vector3.Cross(l,m);
	}


	#region pri
	static float DotProduct_Triangle(this Vector3 a, Vector3 b)
	{
		float degree = Vector3.Angle(a, b);
		float radian = degree.Degree2Radian();

		return a.Mod() * b.Mod() * (radian.Cos());
	}
	 static float DotProduct_Coordinate(this Vector3 a, Vector3 b)
	{
		return a.x * b.x + a.y * b.y + a.z * b.z;
		//return Vector3.Dot(l,m);
	}



	#endregion

}




