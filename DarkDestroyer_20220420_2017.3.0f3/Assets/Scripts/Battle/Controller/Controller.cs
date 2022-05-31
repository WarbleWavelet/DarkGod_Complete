/****************************************************
    文件：Controller.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 11:24:25
	功能：抽取人和敌人的公共Ctrl
*****************************************************/

using UnityEngine;

public abstract class Controller :MonoBehaviour
{

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



    #region 生命

    public void Init()
    {
        cam = Camera.main;
        camTrans = cam.transform;
        camOffset = transform.position - camTrans.position;
        ctrl = transform.GetComponent<CharacterController>();
        ani = transform.GetComponent<Animator>();
    }
    void Update()
    {
        InputByWSAD();
        if (isMove)
        {
            SetDir();
            SetMove();
            SetMainCamera();
        }

    }
    #endregion


    #region 动画
    public virtual void SetBlend(float value)
    {

        ani.SetFloat("Blend", value);

    }
    public virtual void SetAction(int value)
    {

        ani.SetInteger("Action", value);

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
            SetBlend((float)Constants.BlendIdle);
        }
    }
    #endregion


}