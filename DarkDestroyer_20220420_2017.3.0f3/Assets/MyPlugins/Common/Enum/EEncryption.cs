/****************************************************
    文件：EEncryption.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/6 21:19:9
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 加密算法
/// 
/// <para/>AB包从服务端下载到本地，中途因为某些原因导致传输的文件被损坏了，但损坏的文件，不能使用。因此，需要对文件进行完整性的校验。
/// 通过对数据进行计算，来生成一个校验值，我们可以根据该值来检查数据的完整性。   
/// 安全性是指检错的能力，即数据的错误能通过校验位检测出来
/// </summary>                                                                 
public enum EEncryption 
{
    /// <summary> 
    /// 多项式除法  
    /// 安全性跟多项式有很大关系，相对于MD5和SHA1要弱很多;CRC的计算效率很高
    /// 一般用作通信数据的校验
    /// </summary>
    CRC,


    /// <summary>   
    /// 替换、轮转 
    /// MD5的安全性很高 ;比较慢  
    /// 安全（Security）领域，比如文件校验、数字签名等。
    /// </summary>
    MD5,


    /// <summary>
    /// 替换、轮转
    /// SHA1的安全性最高;比较慢
    /// 安全（Security）领域，比如文件校验、数字签名等。
    /// </summary>
    SHA1

}



