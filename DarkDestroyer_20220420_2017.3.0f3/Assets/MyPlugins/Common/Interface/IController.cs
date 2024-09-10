/****************************************************
    文件：I.cs
	作者：lenovo
    邮箱: 
    日期：2024/3/30 22:13:37
	功能： 一般与IView搭配成为MVC
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public interface IController : IControllerInit, IControllerShow, IControllerHide, IControllerUpdate ,IControllerReacquire ,IAddUpdateListener
{
    
}

public interface IControllerInit : IInit
{
}

public interface IControllerShow : IShow
{
}

public interface IControllerUpdate : IUpdate
{
}

public interface IControllerHide : IHide
{
}

/// <summary>重新获取</summary>
public interface IControllerReacquire : IReacquire
{
}


