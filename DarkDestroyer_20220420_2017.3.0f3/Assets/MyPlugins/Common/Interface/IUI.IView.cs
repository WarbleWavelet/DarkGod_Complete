/****************************************************
    文件：IUI.IView.cs
	作者：lenovo
    邮箱: 
    日期：2024/3/30 11:56:47
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public interface IView : IViewInit, IViewShow, IViewHide, IViewUpdate, IReacquire
{
}

public interface IViewInit : IInit
{
}

public interface IViewShow : IShow
{
}

public interface IViewUpdate : IUpdate
{
}

public interface IViewHide : IHide
{
}



