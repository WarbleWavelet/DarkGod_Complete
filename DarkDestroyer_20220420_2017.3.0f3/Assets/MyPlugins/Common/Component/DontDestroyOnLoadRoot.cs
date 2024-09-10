/****************************************************
    文件：DontDestroyOnLoadRoot.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/25 23:47:33
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public class DontDestroyOnLoadRoot : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.transform.root);
    }

}




