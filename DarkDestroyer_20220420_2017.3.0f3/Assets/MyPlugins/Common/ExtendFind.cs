/****************************************************
    文件：ExtendFind.cs
	作者：lenovo
    邮箱: 
    日期：2024/2/16 20:20:30
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;



public static partial class ExtendFind 
{

    static Transform A1(this Transform t, string tarName)
    { 
        return GameObject.Find(tarName).transform;
    
    }
    static Transform A2(this Transform t, string tarName)
    {
        GameObject[] gos = GameObject.FindObjectsOfType<GameObject>();
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].name == tarName)
            {
                return gos[i].transform;
            }
        }

        return null;
    }
    static Transform A3(this Transform t,string tarName)
    {
        GameObject[] gos = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in gos)
        {
            if (go.name == tarName)
            {
                return go.transform;
            }
        }
        return null;
    }
}




