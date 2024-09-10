/****************************************************
    文件：ExtendDotNet.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/29 1:34:5
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExtendLinearAlgebra;
using Random = UnityEngine.Random;

public static partial class ExtendDotNet
{
    //没能成功将版本.NET Standard	2.0改成支持 File.ReadAllTextAsync(filename).NET Standard	2.1
    //这需要unity2021,可选Standard	2.1
    //string a = await File.ReadAllTextAsync(filename);
//
    //net2.0 standard包小
    //.netFramework4.x
    //?切换后,看反编译的api,可以看到dll位置的不同
}
public static partial class ExtendDotNet
{
    //.net target和sdk的区别
    //高版本的sdk能替代低版本的sdk,兼容支持低版本的target
}
public static partial class ExtendDotNet
{
    //    C#
    //IL
    //CLR翻译
    //JIT编译
    //编译结果，机器指令，运行时追加到内存，会释放
    //CPU

    //对抗linux不跨平台
    //.net framework
    //此时linux开发者开发了mono平台，让用户在linux中使用C#，这和日后的.net standard有异曲同工之妙
    //mono touch开发触屏，发展为Xamarin，能开发IOS，安卓，后被收编在.net5中

    //FCL，framework class library，快速开发方案
    //桌面开发，WPF,WINFORM,UWP
    //企业应用，WCF,WF
    //ASP，http/web
    //数据库，ADO

    //跨平台，打不过linux
    //.net core
    //先改ASP，轰动，
    //然后ADO，桌面开发，花了三四年

    //.net standard新老共存的暂时标准

    //最终合并成.net5
    //现在.net6



    /// <summary></summary>
    public enum EDotNet
    {
        /// <summary></summary>
        DotNet_Core_3_RC=201907,
        /// <summary></summary>
        DotNet_Core_3_Release=201909,
        /// <summary></summary>
        DotNet_Core_3_GA=201909,
        /// <summary> LTS=Long Term Support </summary>
        DotNet_Core_3_1_LTS=201911,
        /// <summary></summary>
        DotNet_5_GA=202011,
        /// <summary></summary>
        DotNet_5_Release=202011,
        /// <summary></summary>
        DotNet_6_LTS=202111,
        /// <summary></summary>
        DotNet_7_GA=202211,
        /// <summary></summary>
        DotNet_8_LTS=202311,
    }

}



