/****************************************************
    文件：PlayerCtrlWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 17:56:0
	功能：战斗界面UI
*****************************************************/

using PEProtocol;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerCtrlWnd : WindowRoot 
{


    #region 属性
   #region 单例
    private static PlayerCtrlWnd _instance;      

    public static PlayerCtrlWnd Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerCtrlWnd();
            }
            return _instance;
        }
    }
    #endregion

    [Header("右下")]
    public Transform normalAtkTrans;
    public Transform skill1Trans;
    public Transform skill2Trans;
    public Transform skill3Trans;
    [Header("左下")]
    public Image imgTouch;
    public Image imgDirBg;
    public Image imgDirPoint;
    /// <summary>BG位置确定后Point的位置</summary>
    public Vector2 startPos = Vector2.zero;
    /// <summary>BG默认的位置</summary>
    public Vector2 defaultPos = Vector2.zero;
    /// <summary>摇杆点的运动半径</summary>
    float pointDis;

    [Header("下")]
    public Text txtExpPrg;
    public Transform expPrgTrans;

    [Header("总")]
    public bool isFirst=true;
    public bool canMove = true;
    /// <summary>解决技能后需要动下摇杆才能移动</summary> 
    public Vector2 curDir;


    [Header("调试技能数据")]
    public Button btnTest;
    #endregion

    protected override void InitWnd()
    {
        base.InitWnd();
        //
        defaultPos = imgDirBg.transform.position;
        pointDis = AdaptDirPoint();//放这里，运行时改变无效
        SetActive(imgDirPoint, false);
        RegisterTouchEvets();
        //
        if (isFirst)
        { 
            InitAtkBtn(normalAtkTrans);
            InitSkillBtn(101,skill1Trans);
            InitSkillBtn(102,skill2Trans);
            InitSkillBtn(103,skill3Trans);
            btnTest.onClick.AddListener(ClickTestBtn);
            isFirst =false;
        }
        //


    }



   


    
    void Update()
    {
        TestCode();


        
    }

    private void TestCode()
    {
        if (Input.GetKeyDown(KeyCode.Q)) ClickNormalAtkBtn();
        if (Input.GetKeyDown(KeyCode.Alpha1)) ClickSkill1Btn();
        if (Input.GetKeyDown(KeyCode.Alpha2)) ClickSkill2Btn();
        if (Input.GetKeyDown(KeyCode.Alpha3)) ClickSkill3Btn();
    }




    #region 摇杆
    /// <summary>
    /// 适配摇杆点的半径
    /// </summary>
    /// <returns></returns>
    float AdaptDirPoint()
    {
        return 1.0f * Screen.height / Constants.ScreenStandardHeight * Constants.ScreenOPDis;
    }
    public void RegisterTouchEvets()
    {

        OnClickDown(imgTouch.gameObject, (PointerEventData evt) => {
            startPos = evt.position;
            SetActive(imgDirPoint);

            imgDirBg.transform.position = evt.position;
        });

        OnClickUp(imgTouch.gameObject, (PointerEventData evt) => {
            imgDirBg.transform.position = defaultPos;
            imgDirPoint.transform.localPosition = Vector2.zero;
            SetActive(imgDirPoint, false);
            curDir = Vector2.zero;
            BattleSys.Instance.SetMoveDir(curDir);
        });

        OnDrag(imgTouch.gameObject, (PointerEventData evt) => {
            SetActive(imgDirPoint);
            //
            Vector2 dir = evt.position - startPos;
            float len = dir.magnitude;
            if (len > pointDis)
            {
                Vector2 clampDir = Vector2.ClampMagnitude(dir, pointDis);
                imgDirPoint.transform.position = clampDir + startPos;
            }
            else
            {
                imgDirPoint.transform.position = evt.position;
            }

            curDir = dir.normalized;
                BattleSys.Instance.SetMoveDir( curDir );
            
           

        });


    }
    #endregion

    #region 经验条


    /// <summary>
    /// 适配经验条
    /// </summary>
    void AdaptExpPrg(PlayerData pd)
    {
        GridLayoutGroup grid = expPrgTrans.GetComponent<GridLayoutGroup>();
        float rate = 1f * Constants.ScreenStandardHeight / Screen.height;
        float width = rate * Screen.width;
        float itemWidth = (width - 78 - 5.83f - 6.5f - 9 * 3.8f) / 10;
        grid.cellSize = new Vector2(itemWidth, 8.9f);
        //
        int expValPrg = (int)(1f * pd.exp / PECommon.GetExpUpValByLV(pd.lv) * 100);
        SetText(txtExpPrg, expValPrg + "%");
        int index = expValPrg / 10;

        for (int i = 0; i < expPrgTrans.childCount; i++)
        {
            Image expItem = expPrgTrans.GetChild(i).GetComponent<Image>();
            if (i < index)
            {
                expItem.fillAmount = 1;
            }
            else
            {
                if (i == index)
                {
                    expItem.fillAmount = (expValPrg % 10 * 1f) / 10;
                }
                else
                    expItem.fillAmount = 0;
                {
                }
            }
        }

    }
    #endregion





    #region 技能
    private void InitAtkBtn(Transform t)
    {
        Button btn = t.Find("icon").GetComponent<Button>();
        btn.onClick.AddListener(ClickNormalAtkBtn);
    }


    private void InitSkillBtn(int skillID, Transform t)
    {

        SkillItem skill = GetOrAddSkillItem(t);
        SkillCfg cfg = resSvc.GetSkillCfg(skillID);
        skill.Init(cfg.cdTime / 1000);

        Button btn = t.Find("icon").GetComponent<Button>();
        switch (t.gameObject.name)
        {
            case "btnSkill1":
                {
                    btn.onClick.AddListener(ClickSkill1Btn);
                }
                break;
            case "btnSkill2":
                {
                    btn.onClick.AddListener(ClickSkill2Btn);
                }
                break;
            case "btnSkill3":
                {
                    btn.onClick.AddListener(ClickSkill3Btn);
                }
                break;
            default: break;
        }
    }
    public void ClickNormalAtkBtn()
    {
        BattleSys.Instance.ReqReleaseSkill(0);
    }
    public void ClickSkill1Btn()
    {
        BattleSys.Instance.ReqReleaseSkill(1);
    }
    public void ClickSkill2Btn()
    {
        BattleSys.Instance.ReqReleaseSkill(2);
    }
    public void ClickSkill3Btn()
    {
        BattleSys.Instance.ReqReleaseSkill(3);
    }

    /// <summary>
    /// 计时组件
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    SkillItem GetOrAddSkillItem(Transform t)
    {
        SkillItem skill = t.gameObject.GetComponent<SkillItem>();

        if (skill == null)
        {
            skill = t.gameObject.AddComponent<SkillItem>();
        }

        return skill;
    }
    #endregion



    #region 测试
    private void ClickTestBtn()
    {
        resSvc.ResetSkillCfgs();
    }
    #endregion




    #region 左上头像
    public void ClickHeadBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIOpenPage);
        MainCitySys.Instance.OpenInfoWnd();
    }
    #endregion
}

enum SkillType
{
    Attack,
    Skill1,
    Skill2,
    Skill3

}