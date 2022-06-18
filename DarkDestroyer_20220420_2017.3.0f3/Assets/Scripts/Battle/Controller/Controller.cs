/****************************************************
    文件：Controller.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 11:24:25
	功能：抽取人和敌人的公共Ctrl
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller :MonoBehaviour
{

    [Header("Controller")]

    [Header("Svc")]
    public TimerSvc timerSvc;
    //


    //

    [Header("角色控制器和运动")]
    public CharacterController ctrl=null;
    public Animator ani;
    /// <summary>受Dir控制</summary>
    public bool isMove = false;
    Vector2 dir = Vector2.zero;

    #region 属性
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
   

    [Header("技能和特效")]
    public Dictionary<string, GameObject> skillDic = new Dictionary<string, GameObject>();


    [Header("技能速度")]
    public float skillMoveSpeed;
    public bool isSkillMove;


    [Header("Camera")]
    protected Transform camTrans;
    //[Header("UI")]
    //public Transform hpRoot;
    public AniState curState;
    #region 生命

    public virtual void Init()
    {
        if (transform.GetComponent<CharacterController>() == null)
        {
            ctrl = transform.gameObject.AddComponent<CharacterController>();
        }
        else
        {
            ctrl = transform.GetComponent<CharacterController>();
        }
        isMove = false;
        ani = transform.GetComponent<Animator>();
        timerSvc = TimerSvc.Instance;

    }
    void Update()
    {
        
        if (isMove)
        {
            SetDir();
            SetMove();    
        }

    }
    #endregion


    #region 动画 动画器




    public virtual void SetAniBlend(float value)
    {

        ani.SetFloat("Blend", value);

    }
    public virtual void SetAniAction(int value)
    {

        ani.SetInteger("Action", value);
        
    }


    #endregion



    #region 方向 移动
    /// <summary>
    /// 只摇杆在移动方向
    /// </summary>
    public virtual void SetDir()
    {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1)) ;
        Vector3 eulerAngles = new Vector3(0f, angle, 0f);
        transform.localEulerAngles = eulerAngles;
    }

    /// <summary>
    /// 移动
    /// </summary>
    public virtual void SetMove()
    {
        ctrl.Move(transform.forward * Time.deltaTime * Constants.DefaultMoveSpeed);
    }

    /// <summary>
    /// 连招时摇杆转向
    /// </summary>
    /// <param name="dir"></param>
    internal virtual void SetComboDir(Vector2 comboDir)
    {
        float angle = Vector2.SignedAngle(comboDir, new Vector2(0, 1)) + camTrans.eulerAngles.y;
        Vector3 eulerAngles = new Vector3(0f, angle, 0f);
        transform.localEulerAngles = eulerAngles;
    }

    internal virtual void SetNearTargetDir(Vector2 toDir)
    {
        float angle = Vector2.SignedAngle(toDir, new Vector2(0, 1));
        Vector3 eulerAngles = new Vector3(0f, angle, 0f);
        transform.localEulerAngles = eulerAngles;
    }



    #endregion

    #region 特效


    internal void SetSkillFbx(string skillName, float lifeTime)
    {
        GameObject go=null;
        if ( skillDic.TryGetValue(skillName, out go) )
        { 
            go.SetActive(true);
            timerSvc.AddTimerTask((int tid) => {
                go.SetActive(false); 
            }, lifeTime);
        }
    }




    /// <summary>
    /// 添加特效Go
    /// </summary>
    /// <param name="go"></param>
    internal void AddSkillFbx(GameObject go)
    {
        if (go != null)
        {
            skillDic.Add(go.name, go);
        }
    }
    #endregion

    /// <summary>
    /// 技能产生的移动
    /// </summary>
    /// <param name="move"></param>
    /// <param name="speed"></param>
    public void SetSkillMove(bool move, float speed)
    {
        isSkillMove = move;
        skillMoveSpeed = speed;


    }

}