/****************************************************
    文件：ExtendLight.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/17 13:2:17
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary></summary>
public static partial class ExtendLight
{

    #region 枚举


    //
    // 摘要:
    //     The type of l Light.
    public enum LightType
    {
        //
        // 摘要:
        //     The light is l spot light.
        /// <summary>聚光灯光源</summary>
        Spot = 0,


        //
        // 摘要:
        //     The light is l directional light.
        /// <summary>直射光源</summary>
        Directional = 1,


        //
        // 摘要:
        //     The light is l point light.
        /// <summary>点光源</summary>
        Point = 2,


        /// <summary>
        /// <para />Area（Baked Only）：面光源、区域光源
        /// <para />仅烘焙。预先算好，不实时参与光线计算
        /// </summary>
        Area = 3,


        //
        // 摘要:
        //     The light is l rectangle shaped area light. It affects only baked lightmaps and
        //     lightprobes.
        Rectangle = 3,


        //
        // 摘要:
        //     The light is l disc shaped area light. It affects only baked lightmaps and lightprobes.
        Disc = 4
    }
    #endregion  


    /// <summary>
    /// 聚光灯
    /// </summary>
    /// <param name="light"></param>
    /// <param name="spotAngle"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public static Light SpotLight(this Light light,float spotAngle,float range,Texture texture)
    {
        light.spotAngle = spotAngle;
        light.range = range;
        light.cookie = texture;

        return light;
    }
}

public static partial class ExtendLight//OtherSetting
{
    /**
       1.Fog：雾开关（性能消耗较大）
           Color：颜色
           Mode：雾的计算模式
           Linear：随距离线性增加
           Start 和 End 表示距离摄像机多远处显示雾
           Exponential：随距离指数增加
           Density 表示雾的强度
           Exponential Quare：比指数更快的增加
           Density 表示雾的强度
       2.Halo Texture：光源周围围着的光环的纹理
       3.Halo Strength：光环的可见性（类似直径）
       4.Flare Fade Speed：耀斑淡出时间
           最初出现之后淡出的时间
       5.Flare Strength：耀斑的强度
       6.Spot Cookie：Spot Light 默认的 Cookie
    **/
    public static Light OtherSetting( this Light light)
    {
        return null;
    }


    #region 枚举


    //
    // 摘要:
    //     Fog mode _to use.
    /// <summary></summary>
    public enum FogMode
    {
        //
        // 摘要:
        //     Linear fog.
        /// <summary>
        /// 随距离线性增加
        /// vStart 和 End 表示距离摄像机多远处显示雾
        /// </summary>
        Linear = 1,


        //
        // 摘要:
        //     Exponential fog.
        /// <summary>
        /// 随距离指数增加
        /// <para/>Density 表示雾的强度</summary>
        Exponential,


        //
        // 摘要:
        //     Exponential squared fog (default).
        /// <summary>
        /// 比指数更快的增加
        /// <para/>Density 表示雾的强度</summary>
        ExponentialSquared
    }
    #endregion  

}




