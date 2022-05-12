/****************************************************
    文件：EnableMeshRender.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/12 1:4:46
	功能：
*****************************************************/

using UnityEditor;
using UnityEngine;

   
public class EnableMeshRender : MonoBehaviour 
{



    [MenuItem("Tools/通用工具/切换物体显隐状态 %e")]
    void A()
    {

        print("1");
        MeshRenderer[] mrs = transform.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer mr in mrs)
        {
            mr.enabled = !mr.isVisible;
        }
    }



}