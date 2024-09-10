/****************************************************
    文件：ExtendButton.cs
	作者：lenovo
    邮箱: 
    日期：2023/8/6 20:22:49
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.Button;
using Random = UnityEngine.Random;


public static partial class ExtendButton
{
    public static ButtonClickedEvent AddListenerAfterRemoveAll(this ButtonClickedEvent onClick 
        , UnityAction action)
    {
        onClick.RemoveAllListeners();
        onClick.AddListener(action);
        return onClick;
    }
}




