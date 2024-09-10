/****************************************************
    文件：IStateProcessor.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/8 23:21:49
	功能：
*****************************************************/

using UnityEngine;

public interface IStateProcessor
{
    StateMgr StateMgr { get; set; }
    void StateMove();
    void StateIdle();
    void StateBorn();
    void StateAttack(int skillID);
    void StateDie();
}