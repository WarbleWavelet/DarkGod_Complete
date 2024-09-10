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
    [SerializeField]  float _tarBlend;
    [SerializeField] float _curBlend;

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
            UpdateADWS();

        }
        if (_curBlend != _tarBlend)
        {
            UpdateMixBlend();
        }

        if (isMove)
        {
            UpdateDir();        
            UpdateMainCamera(); 
            UpdateMove();
        }
        //
       if (isSkillMove)
        {
            UpdateMainCamera();
            UpdateSkillMove();
        }

     
    }



    #endregion


    #region pub



    public override void SetAniBlend(float blend)
    {

        _tarBlend = blend;
    }


    /// <summary>
    /// 动画自然点
    /// </summary>
    public void UpdateMixBlend()
    {
        float incVel = Constants.AccelerSpeed * Time.deltaTime;
        if (Mathf.Abs(_curBlend - _tarBlend) < incVel)
        {
            _curBlend = _tarBlend;
        }
        else if (_curBlend > _tarBlend)
        {
            _curBlend -= incVel;
        }
        else
        {
            _curBlend += incVel;
        }


        ani.SetFloat("Blend", _curBlend);

    }



    public override void UpdateDir()
    {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1)) + camTrans.eulerAngles.y;
        Vector3 eulerAngles = new Vector3(0f, angle, 0f);
        transform.localEulerAngles = eulerAngles;
    }

    public override void UpdateMove()
    {
        ctrl.Move(transform.forward * Time.deltaTime * Constants.PlayerRunSpeed);
    }


    public void UpdateMainCamera()
    {
        if (cam != null)
        {
            camTrans.position = transform.position - camOffset;
        }

    }
    #endregion


    #region pri 
    private void UpdateSkillMove(bool isMove = true)
    {
        if (isMove)
            ctrl.Move(transform.forward * Time.deltaTime * this.skillMoveSpeed);
        else
            this.skillMoveSpeed = 0f;


    }


    /// <summary>
    /// 键盘控制移动
    /// </summary>

    private void UpdateADWS()
    {
        float h = Input.GetAxisRaw( ConstAxes.Horizontal);
        float v = Input.GetAxisRaw(ConstAxes.Vertical);
        Vector2 dir = new Vector2(h, v).normalized;

        if (dir != Vector2.zero)
        {
            Dir = dir;
        }
        else
        {
            Dir = Vector2.zero;
        }
    }
    #endregion


}
