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


    [Header("技能特效,平A特效")]
    public GameObject dagger_skill1;
    public GameObject dagger_skill2;
    public GameObject dagger_skill3;
    public GameObject dagger_atk1;
    public GameObject dagger_atk2;
    public GameObject dagger_atk3;
    public GameObject dagger_atk4;
    public GameObject dagger_atk5;
    public bool CanMove;


    void Awake()
    {
        CanMove = true;
        Init();
    }

    public override void Init( )
    {
        base.Init();
        cam = Camera.main;
        camTrans = cam.transform;
        camOffset = transform.position - camTrans.position;
        //
        AddSkillFbx(dagger_skill1);
        AddSkillFbx(dagger_skill2);
        AddSkillFbx(dagger_skill3);
        AddSkillFbx(dagger_atk1);
        AddSkillFbx(dagger_atk2);
        AddSkillFbx(dagger_atk3);
        AddSkillFbx(dagger_atk4);
        AddSkillFbx(dagger_atk5);
        //
        isSkillMove = false;

    }



    #region 生命


    void Update()
    {
        if (CanMove)
        {
            InputByWSAD();

        }
        if (currentBlend != targetBlend)
        {
            UpdateMixBlend();
        }

        if (isMove)
        {
            SetDir();        
            SetMainCamera(); 
            SetMove();
        }
        //
       if (isSkillMove)
        {
            SetMainCamera();
            SetSkillMove();
        }

     
    }


    #endregion


    #region 动画
    public override void SetAniBlend(float blend)
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



    #region 方向 移动
    public override void SetDir()
    {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1)) + camTrans.eulerAngles.y;
        Vector3 eulerAngles = new Vector3(0f, angle, 0f);
        transform.localEulerAngles = eulerAngles;
    }

    public override void SetMove( )
    {
        ctrl.Move(transform.forward * Time.deltaTime * Constants.PlayerMoveSpeed);
    }


    #endregion


    #region 技能
    private void SetSkillMove(bool isMove=true)
    {
        if (isMove)
            ctrl.Move(transform.forward * Time.deltaTime * this.skillMoveSpeed);
        else
            this.skillMoveSpeed = 0f;
        
       
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
            SetAniBlend((float)Constants.BlendWalk);
        }
        else
        {
            Dir = Vector2.zero;
            SetAniBlend((float)Constants.BlendIdle);
        }
    }
    #endregion








}
