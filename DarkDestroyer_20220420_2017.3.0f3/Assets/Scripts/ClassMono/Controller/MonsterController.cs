/****************************************************
    文件：MonsterController.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 11:39:10
	功能：MonsterController
*****************************************************/

using UnityEngine;

public class MonsterController : Controller 
{
    void Update()
    {
        if (isMove  )
        {
            UpdateDir();
            UpdateMove();
        }
      
      
    }

    public override void UpdateDir()
    {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1));
        Vector3 eulerAngles = new Vector3(0f, angle, 0f);
        transform.localEulerAngles = eulerAngles;
    }

    /// <summary>
    /// 移动
    /// </summary>
    public override void UpdateMove()
    {
        ctrl.Move( transform.forward * Time.deltaTime * Constants.MonsterMoveSpeed );
        ctrl.Move( Vector3.down * Time.deltaTime * Constants.MonsterMoveSpeed );//资源的问题，模型不能向下移动 && 不能通过Apply Root
        
    }
}