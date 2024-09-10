/****************************************************
    文件：IUpdate.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/25 20:52:18
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public interface IUpdate<T> where T : class
{
    void Update(List<T> lst);

}



