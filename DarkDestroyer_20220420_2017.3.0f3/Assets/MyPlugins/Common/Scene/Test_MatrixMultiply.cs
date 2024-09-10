/****************************************************
    文件：Test_MatrixMultiply.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/12 20:34:22
	功能：
*****************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExtendLinearAlgebra;
using Random = UnityEngine.Random;
 

public class Test_MatrixMultiply : MonoBehaviour
{
    [SerializeField] Matrix m1;
    [SerializeField] Matrix m2;
    [SerializeField] Matrix m3;
    [SerializeField] float[,] arr1;
    [SerializeField] float[,] arr2;
    [Header("3x2")]
    [SerializeField] float a11;
    [SerializeField] float a12;
    [SerializeField] float a21;
    [SerializeField] float a22;
    [SerializeField] float a31;
    [SerializeField] float a32;

    [Header("2x4")]
    [SerializeField] float b11;
    [SerializeField] float b12;
    [SerializeField] float b13;
    [SerializeField] float b14;
    [SerializeField] float b21;
    [SerializeField] float b22;
    [SerializeField] float b23;
    [SerializeField] float b24;

    [SerializeField] float timer = 0;
    [SerializeField] float time = 2;


    private void OnEnable()
    {
        arr1 = new float[,] { { a11, a12 }, { a21, a22 }, { a31, a32 } };
        arr2 = new float[,] { { b11, b12, b13 ,b14}, { b21, b22, b23,b24 }};

    }
    private void Update()
    {
        timer = this.Timer(timer,time,()=>
        { 
            m1 = new Matrix(3,2,arr1,EMatrixValueType.ROW);
            m2 = new Matrix(2,4,arr2,EMatrixValueType.ROW);
            m3 = m1.Multiply(m2);

            Debug.Log(m1.ToString());
            Debug.Log(m2.ToString());
            Debug.Log(m3.ToString());        
        });

    }

}



