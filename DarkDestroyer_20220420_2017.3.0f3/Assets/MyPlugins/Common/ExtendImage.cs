/****************************************************
    文件：ExtendImage.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/29 4:29:49
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
 

public static class ExtendImage 
{
    public static Image SetSprite(this Image image, Sprite sprite)
    {

        image.sprite = sprite;
        return image;
    }

}




