/****************************************************
    文件：ExtendShader.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/24 19:58:37
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResourcesName;
using Random = UnityEngine.Random;
using static ExtendLinearAlgebra;


public static partial class ExtendShader//例子
{
   public  static Transform[] CreateMatrix(GameObject prefab, int cnt=10)
    {
        prefab.GetOrAddComponent<RectTransform>().localScale = Vector3.one * 0.5f;//符合矩阵大小需要
        Transform[] grid=new Transform[cnt.Pow3()];
        int i = 0;
        for (int x = 0; x < cnt; x++)
        {
            for (int y = 0; y < cnt; y++)
            {
                for (int z = 0; z < cnt; z++,i++)
                {
                    grid[i]= CreateGridPoint(prefab,cnt,x, y,z);
                }


            }
        }

        return grid;
    }

    private static Transform CreateGridPoint(GameObject prefab, int cnt, int x, int y, int z)
    {
        Transform point = GameObject.Instantiate(prefab).transform;
        point.gameObject.name = $"{x},{y},{z}";
        point.localPosition = GetCoorPos(cnt,x, y, z);
        point.GetOrAddComponent<MeshRenderer>().material.color = new Color(
            (float)x/cnt,
            (float)y/cnt,
            (float)z/cnt
            );
        return point;
    }

    private static Vector3 GetCoorPos(int cnt,int x, int y, int z)
    {
        float after = (cnt - 1)*0.5f;//  一半边长的平移量
       return new Vector3(
            x - after ,
            y - after ,
            z-after
            ); 
    }
}
public static partial class ExtendShader 
{


    #region 枚举
    /// <summary>着色器单位</summary>
    public enum EShaderUnit
    { 
        顶点,
        片元,
        色块,

    }
    /// <summary>着色器语言</summary>
    public enum EShaderLanguage
    {
        /// <summary>实时着色器语言</summary>
        RealTimeSL,
        /// <summary>离线着色器语言</summary>
        OutLineSL,
        //
        /// <summary>老大哥</summary>
        OpenGL, GLSL,
        /// <summary>平台多</summary>
        CG, CForGraphic,
        /// <summary>平台多余OpenGL,效果好</summary>
        DirectX_HighLevel, HLSL,
        //
        /// <summary>汇编</summary>
        DirectX_Assembler,

        //

    }

    public enum EUnityShaderLanguage
    {
        /// <summary>
        /// Unity本地化(可以方便地引用unity的东西) ,容器(还可以杂糅支持的语言).
        /// 支持的语言(比如CG)都可以,加标志区别
        /// </summary>
        ShaderLab,
        /// <summary>老版本</summary>
        CG,
        HLSL,

    }

    /// <summary>管线</summary>
    public enum ERenderPipeline
    {
        /// <summary>内置</summary>
        BRP,
        /// <summary>可编程</summary>
        SRPs,
        /// <summary>自定义</summary>
        CRP,
        /// <summary>通用</summary>
        URP,
        /// <summary>HighDefinition</summary>
        HDRP,
    }
    #endregion


    public interface IBRP2URP {}
    public interface IBRP2HDRP{ }
    public interface IURP2HDRP{ }
    public interface IHDRP2URP{ }
}
public static partial class ExtendShader { }
public static partial class ExtendShader { }
public static partial class ExtendShader_Noise 
{
    public enum NoiseFunction
    {
        柏林噪声,PerlinNoise
    }

}




