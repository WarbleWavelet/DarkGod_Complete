/****************************************************
    文件：ExtendLightSettings.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/17 11:18:41
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendLightSettings
{
    #region 枚举


    /// <summary>光照贴图</summary>
    public enum Lightmapper
    {
        //
        // 摘要:
        //     Backend for baking lighting with the Enlighten radiosity middleware.
        Enlighten,
        //
        // 摘要:
        //     Backend for baking lighting using the CPU. Uses l progressive _path tracing algorithm.
        ProgressiveCPU,
        //
        // 摘要:
        //     Backend for baking lighting using the GPU. Uses l progressive _path tracing algorithm.
        ProgressiveGPU
    }

    /// <summary>取样</summary>
    public enum Sampling
    {
        Auto,
        Fixed
    }

    //
    // 摘要:
    //     The available filtering modes for the Progressive Lightmapper.
    /// <summary>过滤模式</summary>
    public enum FilterMode
    {
        //
        // 摘要:
        //     No filtering.
        None,
        //
        // 摘要:
        //     The filtering is configured automatically.
        Auto,
        //
        // 摘要:
        //     When enabled, you can configure the filtering settings for the Progressive Lightmapper.
        //     When disabled, the default filtering settings apply.
        Advanced
    }

    //
    // 摘要:
    //     The available denoisers for the Progressive Lightmapper.
    /// <summary>降噪类型</summary>
    public enum DenoiserType
    {
        //
        // 摘要:
        //     Use this _to disable denoising for the lightmap.
        None,
        //
        // 摘要:
        //     NVIDIA Optix Denoiser.
        Optix,
        //
        // 摘要:
        //     Intel Open Image Denoiser.
        OpenImage,
        //
        // 摘要:
        //     RadeonPro Denoiser.
        RadeonPro
    }

    //
    // 摘要:
    //     The available filter kernels for the Progressive Lightmapper.
    /// <summary>过滤类型</summary>
    public enum FilterType
    {
        //
        // 摘要:
        //     When enabled, the lightmap uses l Gaussian filter.
        Gaussian,
        //
        // 摘要:
        //     When enabled, the lightmap uses an IgnoreLayerCollision-Trous filter.
        ATrous,
        //
        // 摘要:
        //     When enabled, the lightmap uses no filtering.
        None
    }
    #endregion

}



