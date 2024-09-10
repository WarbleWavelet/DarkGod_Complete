/****************************************************
    文件：ExtendVector.cs
	作者：lenovo
    邮箱: 
    日期：2023/9/8 9:43:7
	功能： 2/3交叉 适合放在一起比较
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendVector  //Log
{
    public static void Log(this Vector3[] posArr)
    {
        string str= "";
        foreach (Vector3 pos in posArr)
        {
            str += pos.x + "," + pos.y +"\n";
        }

        Debug.Log(str);
    }
}
public static partial class ExtendVector  //dir
{
    /// <summary>符合直觉点</summary>
    public static Vector2 Dir(this Vector2 fromDir, Vector2 toDir)
    {

        return toDir - fromDir;
    }


    /// <summary>符合直觉点</summary>
    public static Vector3 Dir(this Vector3 fromDir, Vector3 toDir)
    {
        return toDir - fromDir;
    }

    public static Vector2 Dir(this Vector2[] posArr,int fromIdx,int toIdx)
    {
        return posArr[toIdx] - posArr[fromIdx];
    }

    public static Vector3 Dir(this Vector3[] posArr, int fromIdx, int toIdx)
    {
        return posArr[toIdx] - posArr[fromIdx];
    }
}
public static partial class ExtendVector
{ 
    #region Face
    public static  Transform FaceTo(this Transform t, Vector2 dir, EDir eDir)
    {
        Vector3 e = t.rotation.eulerAngles;
        float radian = (dir.y / dir.x).Atan();//求弧度
        float degree = (radian).Radian2Degree();// 求角度    -80 (90) 80 
        float degreeOffset = 0; //朝向带来的偏移


        switch ( eDir )
        {                                                       
            case  EDir.UP :  degreeOffset = 90; break; //        -80 (90) 80     =>     10 0 -10  
            case  EDir.DOWN :  degreeOffset = -90; break; //    -80 (-90) -100   =>     0 -10   
            case  EDir.LEFT :  degreeOffset = 180; break; //    170 (180) 190    =>     10 0 -10  
            case  EDir.RIGHT :  degreeOffset = 0; break; //     -10 (0)   10     =>     10 0 -10  
            default: break;
        }


        if (degree < 0)
        {
            degree = degreeOffset + degree;//90+(-80)
        }
        else
        {
            degree = degree - degreeOffset;//80-90
        }
        //t.localEulerAngles = new Vector3(e.x, e.y, degree);
        t.rotation =  Quaternion.Euler(e.x, e.y, degree);

        return t;
    }

    #endregion  

}


public static partial class ExtendVector //枚举
{
    /// <summary>向量计算方式</summary>
    public enum EVectorCalcType
    {
        /// <summary>坐标</summary>
        COORDINATE,
        /// <summary>三角函数</summary>
        TRIANGLE,
        /// <summary>行列式</summary>
        DETERMINANT   ,
        /// <summary>矩阵</summary>
        MATRIX,

    }
}
public static partial class ExtendVector //2点击叉积
{ 
//二维只返回float
//三维有float和Vector3
}
public static partial class ExtendVector //2
{

    public static Vector2 V3ToV2(this Vector3 v)
    {
        float x = v.x;
        float y = v.y;
        return new Vector2(x, y);
    }
    public static Vector2Int V3ToV2Int(this Vector3 v)
    {
        int x= v.x.To<int>();
        int y= v.y.To<int>();
        return new Vector2Int(x,y); 
    }

    public static Vector3Int V3ToV3Int(this Vector3 v)
    {
        int x = v.x.To<int>();
        int y = v.y.To<int>();
        int z = v.z.To<int>();
        return new Vector3Int(x, y,z);
    }
}
public static partial class ExtendVector //加减
{

    public static Vector3 Add(Vector3 v, Vector2 v1)
    {
        Vector2 res=new Vector3( v.x + v1.x, v.y+v1.y,v.z);
        return res;
    }

    public static Vector3 Sub(Vector3 v, Vector2 v1)
    {
        return Add(v,-v1);
    }



}






