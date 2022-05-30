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

    #region 属性、字段
    Camera cam;
    
    public Animator animator;
    public CharacterController characterController;
    bool isMove = false;

    Vector2 dir = Vector2.zero;

    public Vector3 camOffset;
    public Transform camTrans;

    public float targetBlend;
    public float currentBlend;

    public Vector2 Dir
    {
        get
        {
            return dir;
        }

        set
        {
            if (value == Vector2.zero)
            {
                isMove = false;
            }
            else
            {
                isMove = true;
            }
            dir = value;
        }
    }
    #endregion



    #region 生命
    void Start()
    {
        Init();
    }
    public void Init()
    {
        cam = Camera.main;
        camTrans = cam.transform;
        camOffset = transform.position - camTrans.position;
    }
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


    private void SetDir()
    {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0,1))+camTrans.eulerAngles.y;
        Vector3 eulerAngles = new Vector3(0f, angle,0f);
        transform.localEulerAngles = eulerAngles;
    }

    public void SetMainCamera()
    {
        if (cam != null)
        { 
        camTrans.position=transform.position - camOffset;
        }

    }

    private void SetMove()
    {
        characterController.Move(transform.forward*Time.deltaTime*Constants.PlayerMoveSpeed);
    }

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
            SetBlend(Constants.BlendWalk);
        }
        else
        {
            Dir = Vector2.zero;
            SetBlend(Constants.BlendIdle);
        }
    }

   public void SetBlend(float blend)
    {
       
        targetBlend = blend;
        
    }

    /// <summary>
    /// 动画自然点
    /// </summary>

    public void UpdateMixBlend()
    {
       float incVel= Constants.AccelerSpeed* Time.deltaTime;
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


        animator.SetFloat("Blend", currentBlend);

    }

}
