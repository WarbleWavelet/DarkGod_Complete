/****************************************************
    文件：ExtendSet.GreekAlphabet.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/21 15:5:6
	功能：  希腊字母
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static partial class ExtendGreekAlphabet
{
    // https://baike.baidu.com/item/%CF%89/7451083?fr=ge_ala
    public static void ExampleGreekAlphabet()
    {
        //Debug.Log(EGreekAlphabet.α);
        //Debug.Log(EGreekAlphabet.β);
        //Debug.Log(EGreekAlphabet.Δ);
        //Debug.Log(EGreekAlphabet.Π);

        string str="";
        for (int i = 0; i < EGreekAlphabet.COUNT.Enum2Int(); i++)
        {
            if (i % 4 == 0)
            {
                str += "\n";
            }
            str += i.Int2String<EGreekAlphabet>()+"\t";
        }

        Debug.Log(str);
    }



    /// <summary>希腊字母大小写,对应英文,汉语音译
    /// <br/>测试过VS+Unity可以打印</summary>
    public enum EGreekAlphabet
    {
        α, Α, alpha, 阿尔法,
        β, Β, beta, 贝塔,
        Γ, γ, gamma, 伽马,
        Δ, δ, delta,德尔塔 ,
        Ε, ε, epsilon, 伊普西龙,
        Ζ, ζ, zeta,捷塔 ,
        Η, η, eta, 艾塔 ,
        Θ, θ, theta,西塔,
        Ι, ι, iota,伊奥塔,
        Κ, κ, kappa,卡帕,
        Λ, λ, lambda, 兰姆达,
        Μ, μ, mu,缪,
        Ν, ν, nu, 纽,
        Ξ, ξ, xi,克西,
        Ο, ο, omicron,欧米克戎,
        Π, π, pi,派,
        Ρ, ρ, rho,柔,
        Σ, σ, sigma, 西格玛,
        Τ, τ, tau,陶,
        Υ, υ, upsilon,宇普西龙,
        Φ, φ, phi,发爱,
        Χ, χ, chi, 开,
        Ψ, ψ, psi, 普西,
        Ω, ω, omega ,欧米伽 ,
        COUNT
    }
}




