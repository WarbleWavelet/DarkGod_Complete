/****************************************************
    文件：Test_ResourcesFiolder2Txt.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/15 14:33:15
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public class Test_ResourcesFiolder2Txt : MonoBehaviour
{
    private void Start()
    {

#if UNITY_EDITOR
        ExtendIO.ResourcesFolder2Txt(TColone.Texture);
       // ExtendIO.StreamingAssetsFolder2Txt("txt");//仿造上面的
#endif


    }

}



