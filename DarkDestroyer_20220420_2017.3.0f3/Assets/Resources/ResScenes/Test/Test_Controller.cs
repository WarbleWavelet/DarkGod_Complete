/****************************************************
    文件：Test_Controller.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/31 16:46:33
	功能：测试
*****************************************************/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Test_Controller : MonoBehaviour 
{

    [Header("Test_Controller")]
    public Animator ani;
    //
    public Camera cam;
      public Vector3 camOffset;
    public Transform camTrans;
    //
    public CharacterController ctrl;
    public bool isMove = false;
    //
    Vector2 dir = Vector2.zero;
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

//
        public float targetBlend;
    public float currentBlend;
    //
    bool isLock=false;
    //


    [Header("Test")]
    public Button btnAtk;
    public Button btnSkill1;
    public Button btnSkill2;
    public Button btnSkill3;
    public GameObject skill1Fbx;
    public GameObject skill2Fbx;
    public GameObject skill3Fbx;
    public GameObject curSkillFbx;
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
        ctrl = transform.GetComponent<CharacterController>();
        ani = transform.GetComponent<Animator>();
        isLock = false;
        //
        btnAtk.onClick.AddListener(    () => ClickSkillBtn(0)   );
        btnSkill1.onClick.AddListener( () => ClickSkillBtn(1)   );
        btnSkill2.onClick.AddListener( () => ClickSkillBtn(2)   );
        btnSkill3.onClick.AddListener( () => ClickSkillBtn(3)   );

    }


    void ClickSkillBtn(int index)
    {
       
        switch (index)
        {
            case 0 :
                {
                    curSkillFbx = null;
                }
                break;
            case 1:
                {
                    curSkillFbx = skill1Fbx;
                }
                break;
            case 2:
                {
                    curSkillFbx = skill2Fbx;
                }
                break;
            case 3:
                {
                     curSkillFbx = skill3Fbx;
                }
                break;
            default: break;
        }
        SetAnimator("Action", index);
        if (curSkillFbx != null)
        { 
         curSkillFbx.SetActive(true);
        }
       
        StartCoroutine(Delay());

    }


    void Update()
    {
        InputByWSAD();
        if (currentBlend != targetBlend)
        {
            UpdateMixBlend();
        }
        //
        //
        if (isMove)
        {
            SetDir();
            SetMove();
            SetMainCamera();
        }

    }
    #endregion


    #region 动画

    public  void SetBlend(float blend)
    {
        targetBlend = blend;
    }
    public void SetAnimator(string key,int value)
    {
        ani.SetInteger(key, value);
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


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        SetAnimator("Action",-1);
        if (curSkillFbx != null)
        { 
            curSkillFbx.SetActive(false);
            curSkillFbx = null;
        }

    }
    #endregion



    #region 方向
    public virtual void SetDir()
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
            SetBlend( (float)Constants.BlendIdle);
        }
    }
    #endregion

}