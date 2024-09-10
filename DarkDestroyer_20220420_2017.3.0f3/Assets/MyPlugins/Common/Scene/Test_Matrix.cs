/****************************************************
    文件：Test_Matrix.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/17 15:11:11
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static ExtendLinearAlgebra;
using static ResourcesName;
using Random = UnityEngine.Random;


public class Test_Matrix : MonoBehaviour
{
    bool change = false;
    [Header("正交投影(默认Z)")]
    public EAxis E_Axis = EAxis.Z;

    [Header("透视投影(焦距)")]
    public float FocalLength = 1f;
    //

    [Header("变换矩阵")]
    /// <summary>变换矩阵对应的Vector3</summary>
    public Vector3 tV = Vector3.one;
    public GameObject Prefab;
    //

    [Header("计时")]
    Transform[] grid;
    Vector3[] pos;
    [SerializeField] float timer = 0f;
    [SerializeField] float time = 5f;


    void Start()
    {
        grid = ExtendShader.CreateMatrix(Prefab);
        pos=new Vector3[grid.Length];   
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = grid[i].localPosition;
        }
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.S))
        {
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i].localPosition = grid[i].localPosition.ScaleVector3(tV.x, tV.y, tV.z);//缩放
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i].localPosition = grid[i].localPosition.TransiationVector3(tV.x, tV.y, tV.z);//平移            }
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i].localPosition = grid[i].localPosition.OrthogonalProjection(E_Axis);//
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i].localPosition = grid[i].localPosition.PerspectiveProjection(FocalLength);//
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            for (int i = 0; i < grid.Length; i++)
            {                                                             
                if (grid[i].position.z <= 0)//相机默认
                {
                    grid[i].Hide();
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i].localPosition = grid[i].localPosition.RotateVector3(tV.x, tV.y, tV.z);//旋转
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i].Show();
                grid[i].localPosition = pos[i];
            }
        }
        return;
        timer = this.Timer(timer, time, () =>
        {
            // if (change) return;
            for (int i = 0; i < grid.Length; i++)
            {
                //grid[i].localPosition = grid[i].localPosition.TransiationVector3(tV.x,tV.y,tV.z);//平移
                //grid[i].localPosition = grid[i].localPosition.ScaleVector3(tV.x,tV.y,tV.z);//缩放
                //grid[i].localPosition = grid[i].localPosition.RotateVector3(tV.x,tV.y,tV.z);//旋转
                //grid[i].localPosition = grid[i].localPosition.OrthogonalProjection(E_Axis);//
                //grid[i].localPosition = grid[i].localPosition.PerspectiveProjection();//
                // grid[i].localPosition = grid[i].localPosition.PerspectiveProjection(FocalLength);//
            }
            //  change = true ;
        });
    }
}
