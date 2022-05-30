/****************************************************
    文件：Entity.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 2:23:10
	功能：实体基类
*****************************************************/



public class EntityBase
{
    public AniState curState=AniState.None;
    public StateMgr stateMgr = null;
    public PlayerController playerCtrl = null;

    public void Move()
    {
        stateMgr.ChaungeStaus(this, AniState.Move);
    }

    public void Idle()
    {
        stateMgr.ChaungeStaus(this, AniState.Idle);
    }
}