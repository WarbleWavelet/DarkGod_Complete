/****************************************************
    文件：IPointerHandler.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/30 21:44:4
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


/// <summary>
/// <see cref=IEventSystemHandler/>
/// </summary>
public interface IPointerHandler :  
	IPointerDownHandler,IPointerUpHandler,
	IPointerEnterHandler,IPointerExitHandler,
	IPointerClickHandler
{


}



