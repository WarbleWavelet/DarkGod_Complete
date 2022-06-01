/****************************************************
    文件：PlayerController.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/7 17:48:10
	功能：角色控制器
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{


    [Header("动画融合")]    
    public  float targetBlend;
    public  float currentBlend;

    [Header("相机")]
    public Camera cam;
    public Vector3 camOffset;
    public Transform camTrans;

    [Header("技能和特效")]
    public GameObject draggeratk1fx;
    public override void Init()
    {
        base.Init();
        cam = Camera.main;
        camTrans = cam.transform;
        camOffset = transform.position - camTrans.position;

        AddSkillFbx(draggeratk1fx);

    }



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
    public override void SetDir()
    {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1)) + camTrans.eulerAngles.y;
        Vector3 eulerAngles = new Vector3(0f, angle, 0f);
        transform.localEulerAngles = eulerAngles;
    }
    #endregion


    #region 移动
    private void SetMove()
    {
        ctrl.Move(transform.forward * Time.deltaTime * Constants.PlayerMoveSpeed);
    }
    #endregion


    #region 相机
    public void SetMainCamera()
    {
        if (cam != null)
        {
            camTrans.position = transform.position - camOffset;
        }

    }
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
