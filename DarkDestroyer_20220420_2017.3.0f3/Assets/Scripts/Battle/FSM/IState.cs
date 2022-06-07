/****************************************************
    文件：IState.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 1:56:21
	功能：状态接口
*****************************************************/


public interface IState
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="args">可变参数</param>
    void Enter(EntityBase entity, params object[] args);

    void Process(EntityBase entity, params object[] args);

    void Exit(EntityBase entity, params object[] args);
}

public enum AniState
{
    None=-1,
    Born=0,
    Idle,
    Move,
    Attack,
    Die=100,
    Hit=101,
}