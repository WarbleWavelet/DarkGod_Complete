/****************************************************
    文件：Entity.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 2:23:10
	功能：实体基类
*****************************************************/



using UnityEngine;

public class EntityBase
{
    public AniState curState = AniState.None;
    public StateMgr stateMgr = null;
    public Controller ctrl = null;

    public void Move()
    {
        stateMgr.ChangeStaus(this, AniState.Move);
       // SetBlend(Constants.BlendWalk);
    }

    public void Idle()
    {
        stateMgr.ChangeStaus(this, AniState.Idle);
      //  SetBlend(Constants.BlendIdle);
    }

    public virtual void SetBlend(float blend)
    {
        if (ctrl != null)
        {
            ctrl.SetBlend(blend);
        }

    }

    public virtual void SetDir(Vector2 dir)
    {
        if (ctrl != null)
        {
            ctrl.Dir = dir;
        }

    }
}