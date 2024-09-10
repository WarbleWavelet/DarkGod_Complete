/****************************************************
    文件：ExtendHandler.EventSystemHandler.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/4 21:12:50
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
 

public static partial class ExtendEventSystemHandler 
{
    public  interface  IDragHandlers : IBeginDragHandler, IDragHandler, IEndDragHandler
    {

    }

    public interface ISelectHandlers : ISelectHandler, IDeselectHandler
    { 
    
    }




}



