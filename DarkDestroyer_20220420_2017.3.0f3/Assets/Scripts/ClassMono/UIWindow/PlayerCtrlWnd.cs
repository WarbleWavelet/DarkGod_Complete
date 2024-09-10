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

    [Header("左上")]
    public Image imgHP;
    public Text txtHP;
    [SerializeField] int _hpSum;
    public Button btnHead;
     

    [Header("右上")]
    public ItemBossHp itemBossHp;
    public Transform t;
    public GameObject go;


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
    Button btnAtk;


    [Header("下")]
    public Text txtExpPrg;
    public Transform expPrgTrans;


    [Header("总")]
    public  bool canMove = true;
    /// <summary>解决技能后需要动下摇杆才能移动</summary> 
    public Vector2 curDir;


    [Header("调试技能数据")]
    public Button btnTest;
    public int timePara;


    [Header("中间")]
    public Text txtName;
    bool isFirst = true;
    #endregion



    #region Life


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

           
            imgHP = transform.Find("LeftTopPin/prgHP/imgHP").GetComponent<Image>();
            txtHP = transform.Find("LeftTopPin/prgHP/txtHP").GetComponent<Text>();
            txtName = transform.Find("txtName").GetComponent<Text>();
            //
            btnHead = transform.Find("LeftTopPin/btnHead").GetComponent<Button>();
            btnHead.onClick.AddListener(ClickHeadBtn);
            //
            SetBossHPState(false);
                //
            isFirst =false;

           // btnTest.OnClick.AddListener(ClickTestBtn);
        }
        //
        InitPrgHP();
        SetName(GameRoot.Instance.PlayerData.name);
    }


    void Update()
    {
        TestCode();
    }
    #endregion



    #region internal

    internal void SetPrgHP(int val)
    {

        imgHP.fillAmount = (1.0f * val) / _hpSum;
        txtHP.text = val + "/" + _hpSum;
    }


    internal void SetName(string playerName)
    {
        txtName.text = playerName;
    }

    internal void RegisterTouchEvets()
    {

        OnClickDown(imgTouch.gameObject, (PointerEventData evt) => 
        {
            startPos = evt.position;
            SetActive(imgDirPoint);

            imgDirBg.transform.position = evt.position;
        });

        OnClickUp(imgTouch.gameObject, (PointerEventData evt) => 
        {
            imgDirBg.transform.position = defaultPos;
            imgDirPoint.transform.localPosition = Vector2.zero;
            SetActive(imgDirPoint, false);
            curDir = Vector2.zero;
            BattleSys.Instance.SetPlayerMoveDir(curDir);
        });

        OnDrag(imgTouch.gameObject, (PointerEventData evt) => 
        {
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
            BattleSys.Instance.SetPlayerMoveDir( curDir );
        });
    }


    internal bool GetCanRlsSkill()
    {
        return BattleSys.Instance.CanRlsSkill();
    }
    #region 右上
    internal void SetBossHPState(bool state=false,int hp=100)    {
        SetActive(t,state);
        itemBossHp.SetBossHPState(state,1f,hp);
        

    }

    internal void SetBossHPVal(int oldVal, int newVal)
    {
        itemBossHp.SetHPVal( oldVal,  newVal);
    }
    #endregion
    #endregion



    #region pri


    /// <summary>
    /// 适配摇杆点的半径
    /// </summary>
    /// <returns></returns>
    float AdaptDirPoint()
    {
        return 1.0f * Screen.height / Constants.ScreenStandardHeight * Constants.ScreenOPDis;
    }


    void InitPrgHP()
    {
        PlayerData pd = GameRoot.Instance.PlayerData;
        _hpSum = pd.hp;
        SetPrgHP(_hpSum);
    }
    private void TestCode()
    {
        if (Input.GetKeyDown(KeyCode.Q)) ClickNormalAtkBtn();
        if (Input.GetKeyDown(KeyCode.Alpha1)) ClickSkillBtnByKey(skill1Trans);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ClickSkillBtnByKey(skill2Trans);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ClickSkillBtnByKey(skill3Trans);
    }


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


    private void InitAtkBtn(Transform t)
    {
        btnAtk = t.Find("icon").GetComponent<Button>();
        btnAtk.onClick.AddListener(ClickNormalAtkBtn);
    }
    void SetAtkBtnInterable(bool state)
    {
        btnAtk.interactable = state;
    }

    private void InitSkillBtn(int skillID, Transform t)
    {

        SkillItem skill = t.GetOrAddComponent<SkillItem>();
        SkillCfg cfg = resSvc.GetSkillCfg(skillID);
        skill.Init(BattleSys.Instance.battleMgr.playerEntity, cfg.cdTime / 1000);

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


    void ClickNormalAtkBtn()
    {
        BattleSys.Instance.ReqReleaseSkill(0);
    }
    void ClickSkill1Btn()
    {
        BattleSys.Instance.ReqReleaseSkill(1);
    }
    void ClickSkill2Btn()
    {
        BattleSys.Instance.ReqReleaseSkill(2);
    }
    void ClickSkill3Btn()
    {
        BattleSys.Instance.ReqReleaseSkill(3);
    }

    void ClickSkillBtnByKey(Transform t)
    {
        SkillItem skillItem = t.GetOrAddComponent<SkillItem>();
        if (skillItem.btn.interactable == false)
        {
            return;
        }
        ClickSkill1Btn();
        skillItem.ClickBtn();
    }




    private void ClickTestBtn()
    {
        resSvc.ResetSkillCfgs();
    }    


    void ClickHeadBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIOpenPage);
        //MainCitySys.Instance.OpenInfoWnd();
        BattleSys.Instance.battleMgr.isPauseGame = true;//访问BattleSys下的EndBattleWnd
        BattleSys.Instance.SetEndBattleWndState(EndBattleType.Pause);
    }


    #endregion
}

