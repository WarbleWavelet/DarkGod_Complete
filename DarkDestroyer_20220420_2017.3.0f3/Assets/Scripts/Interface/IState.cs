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


/// <summary></summary>
public enum EAniState
{
    None=-1,
    Born=0,
    /// <summary>-1时为BlendTree</summary>
    Idle,
    Move,
    /// <summary>Atk12345 SkillAtk123</summary>
    Attack,
    Die=100,
    Hit=101,
}