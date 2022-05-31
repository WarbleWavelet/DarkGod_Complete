/****************************************************
    文件：PlayerController.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/7 17:48:10
	功能：角色控制器
*****************************************************/

using System;
using UnityEngine;

public class PlayerController : Controller
{
    public  float targetBlend;
    public  float currentBlend;

    #region 生命


    void Update()
    {
        //InputByWSAD();
        if (currentBlend != targetBlend)
        {
            UpdateMixBlend();
        }
        if (isMove)
        {
            SetDir();
            SetMove();
            SetMainCamera();
        }

    }
    #endregion


    #region 动画
    public override void SetBlend(float blend)
    {

        targetBlend = blend;

    }

    /// <summary>
    /// 动画自然点
    /// </summary>
    public void UpdateMixBlend()
    {
        float incVel = Constants.AccelerSpeed * Time.deltaTime;
        if (Mathf.Abs(currentBlend - targetBlend) < incVel)
        {
            currentBlend = targetBlend;
        }
        else if (currentBlend > targetBlend)
        {
            currentBlend -= incVel;
        }
        else
        {
            currentBlend += incVel;
        }


        ani.SetFloat("Blend", currentBlend);

    }
    #endregion



    #region 方向

    #endregion


    #region 移动
    private void SetMove()
    {
        ctrl.Move(transform.forward * Time.deltaTime * Constants.PlayerMoveSpeed);
    }
    #endregion


    #region 相机

    #endregion




    #region 输入
    /// <summary>
    /// 键盘控制移动
    /// </summary>

    private void InputByWSAD()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 _dir = new Vector2(h, v).normalized;

        if (_dir != Vector2.zero)
        {
            Dir = _dir;
            SetBlend((float)Constants.BlendWalk);
        }
        else
        {
            Dir = Vector2.zero;
            SetBlend((float)Constants.BlendIdle);
        }
    }
    #endregion








}
