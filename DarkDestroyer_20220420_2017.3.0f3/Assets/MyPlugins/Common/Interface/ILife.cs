/****************************************************
    文件：ILife.cs
	作者：lenovo
    邮箱: 
    日期：2024/3/30 11:58:0
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



#region Init


#region  或，只能选一个的那种
public interface IInit
{
    void Init();
}
/// <summary>慎用，拆箱使用经常报错</summary>
public interface IInitParas
{
    void InitParas(params object[] os);
}


/// <summary>参数para化,返回组件</summary>
public interface IInitParas<T> where T : Component
{
    T InitParas(params object[] os);
}

public interface IInitComponent<T> where T : Component
{
    T InitComponent();
}

public interface IInitTransform
{
    void Init(Transform transform);
}

public interface IInitTransform<T> where T : Component
{
    T Init(Transform transform);
}
#endregion


/// <summary>函数顺序严格，不要随便动</summary>
public interface IInitStrictly
{
    void InitStrictly();
}

/// <summary>自己加的。给需要设置父节点的，防止都在顶节点太乱</summary>
public interface IInitParent
{
    void InitParent(Transform parent);
}

public interface IInitEntity<T> where T : Component
{
    T InitEntity(Transform entity);
}
#endregion



#region Show  Hide

/// <summary>
/// 表示可以被调用,比如父节点,或者一个单例类,系统
/// <br/>不像mono的生命是不可被Coder调用的</summary>
public interface IFowller
{ }     

public interface IShow : IFowller
{
    void Show();
}

/// <summary>不是SetActive(false),是用来配合LifeCycle</summary>
public interface IHide : IFowller
{
    void Hide();
}
#endregion




#region Update
public interface IUpdate :IFowller
{
    /// <summary>在跑的计时</summary>
    int Framing { get; set; }
    /// <summary>不设置默认为0,就是每个逻辑帧都会跑
    /// <br/>多少帧跑一次逻辑
    /// <br/>会与UnityEngine.Time冲突，冲突时需要写UnityEngine.Frame</summary>
    int Frame { get; }
    /// <summary>不要改成Update，避免与MonoBehaviour的冲突</summary>
    void FrameUpdate();
}


/// <summary>一般定义一个Action，在方法里面+=</summary>
public interface IAddUpdateListener
{
    void AddUpdateListener(System.Action update);
}
#endregion




public interface IDestroy:IFowller
{

    void DestroyFunc();
}


#region Open  Close Clear   IReacquire
public interface IOpen
{
    void Open();
}

/// <summary>重新获取</summary>
public interface IReacquire
{
    /// <summary>重新获取</summary>
    void Reacquire();
}

/// <summary>面板的Destroy，Close贴切</summary>
public interface IClose
{
    void Close();
}

public interface IClear
{
    void Clear();
}
#endregion



#region Start  Stop  Begin    End


#region Start  Stop 

public interface IStart
{
    void StartFunc();
}

public interface IStop
{
    void Stop();
}
#endregion


#region Begin    End
/// <summary>偏向特效</summary>
public interface IBegin
{
    void Begin();
}

public interface IEnd
{
    void End();
}
#endregion



/// <summary>特效停止后的回调</summary>
public interface IStopAction
{
    void Stop(Action cb);
}



#region 暂停继续
public interface IPause
{
    void Pause();
}

public interface IContinue
{
    void Continue();
}

public interface IResume
{
    void Resume();
}
#endregion

#endregion


