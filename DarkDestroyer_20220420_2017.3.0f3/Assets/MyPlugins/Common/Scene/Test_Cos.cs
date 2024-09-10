/****************************************************
    文件：Test_Cos.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/11 21:41:19
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public class Test_Cos : MonoBehaviour
{
    public float degree=30;
    [SerializeField]  float radian;
     [SerializeField] float sin;
     [SerializeField] float cos;
    [SerializeField] float tan;
    private void Update()
    {
        radian = degree.Degree2Radian().TendTo();
        sin = radian.Sin().TendTo();
        cos = radian.Cos().TendTo();
        tan = radian.Tan().TendTo();
    }

}




