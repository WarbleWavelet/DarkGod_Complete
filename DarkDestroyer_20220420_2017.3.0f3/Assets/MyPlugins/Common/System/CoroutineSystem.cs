/****************************************************
    文件：CoroutineSystem.cs
	作者：#CreateAuthor#
    邮箱: 
    日期：#CreateTime#
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class CoroutineSystem : MonoSingleton<CoroutineSystem>
{
    public void  Add(IEnumerator ie)
    {
        this.StartCoroutine(ie);
    }
}



