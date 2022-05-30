/****************************************************
    文件：IState.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 1:56:21
	功能：状态接口
*****************************************************/


public interface IState
{
    void Enter(EntityBase entity);

    void Process(EntityBase entity);

    void Exit(EntityBase entity);
}

public enum AniState
{
    None,
    Idle,
    Move,
}