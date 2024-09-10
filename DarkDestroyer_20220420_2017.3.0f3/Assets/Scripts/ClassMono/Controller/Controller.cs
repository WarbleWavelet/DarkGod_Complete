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





public abstract class Controller :MonoBehaviour , ISkillEffectProcessor
{

    #region FPC
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
                SetAniBlend((float)Constants.BlendIdle);
            }
            else
            {

                isMove = true;
                SetAniBlend((float)Constants.BlendRun);

            }
            dir = value;
        }
    }

    public bool runAI;
    #endregion
   

    [Header("技能和特效")]
     Dictionary<string, GameObject> _skillDic = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> SkillDic { get { return _skillDic; }set { _skillDic = value; } }


    [Header("技能速度")]
    public float skillMoveSpeed;
    public bool isSkillMove;


    [Header("Camera")]
    protected Transform camTrans;
    //[Header("UI")]
    //public Transform hpRoot;
    public EAniState curState;
    #endregion  







    #region 动画 动画器

    #region 生命

    public virtual void Init()
    {
        ctrl = gameObject.GetOrAddComponent<CharacterController>();
        isMove = false;
        ani = gameObject.GetComponentOrLogError<Animator>();
        timerSvc = TimerSvc.Instance;

    }
    void Update()
    {
        
        if (isMove)
        {
            UpdateDir();
            UpdateMove();    
        }

      
    }
    #endregion


    public virtual void SetAniBlend(float value)
    {

        ani.SetFloat(ConstAnimator.Blend, value);

    }
    public virtual void SetAniAction(int value)
    {

        ani.SetInteger(ConstAnimator.Action, value);
        
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

    #region 方向 移动
    /// <summary>
    /// 只摇杆在移动方向
    /// </summary>
    public virtual void UpdateDir()
    {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1)) ;
        Vector3 eulerAngles = new Vector3(0f, angle, 0f);
        transform.localEulerAngles = eulerAngles;
    }

    /// <summary>
    /// 移动
    /// </summary>
    public virtual void UpdateMove()
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

    #region ISkillEffectProcessor


    public void SetSkillFbx(string skillName, float lifeTime)
    {
        GameObject go=null;
        if ( _skillDic.TryGetValue(skillName, out go) )
        {
            go.Show();
            timerSvc.AddTimerTask((int tid) => 
            {
                go.Hide();
            }, lifeTime);
        }
    }





    public void AddSkillFbx(GameObject go)
    {
        if (go != null && _skillDic.ContainsKey(go.name)==false)
        {
            _skillDic.Add(go.name, go);
        }
    }
    #endregion



}