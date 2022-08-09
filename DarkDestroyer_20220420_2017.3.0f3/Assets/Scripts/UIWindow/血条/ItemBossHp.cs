/****************************************************
    文件：ItemEntityHp.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/8 13:25:42
	功能：人物头上的血条和相关文字
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemBossHp : MonoBehaviour
{


    #region 字段 属性

    [Header("暴击")]
    public Text txtCritical;
    public Animation aniCritical;
    [Header("闪避")]
    public Text txtDodge;
    public Animation aniDodge;
    [Header("血量")]
    public Text txtHp;
    public Animation aniHp;

    [Header("血条跟随")]
    /// <summary>主人位置rect</summary>
    public RectTransform rect;
    /// <summary>主人位置</summary>
    public Transform rootTrans;
    /// <summary>缩放比例</summary>
    public float scaleRate = 1.0f * Constants.ScreenStandardWidth / Screen.width;


    [Header("血条位置的两种做法")]
    public Vector3 offsetPos = new Vector3(0, 20f, 0);
    public Transform hpRoot;
    [Header("右上")]
    /// <summary>越大越底部，这样写，以后加层不用重复修改</summary>
    public Image imgPgLayer2;
    public Image imgPgLayer1;
    public Text txtBossName;
    public float curPrg;
    public float tarPrg;
    public int hpVal;
    #endregion


    public void BindUI()
    {


        txtCritical = transform.Find("txtCritical").GetComponent<Text>();
        aniCritical = transform.Find("txtCritical").GetComponent<Animation>();
        txtDodge = transform.Find("txtDodge").GetComponent<Text>();
        aniDodge = transform.Find("txtDodge").GetComponent<Animation>();
        txtHp = transform.Find("txtHp").GetComponent<Text>();
        aniHp = transform.Find("txtHp").GetComponent<Animation>();

        imgPgLayer2 = transform.Find("bgHP/imgPgLayer2").GetComponent<Image>();
        imgPgLayer1 = transform.Find("bgHP/imgPgLayer1").GetComponent<Image>();
        txtBossName = transform.Find("txtBossName").GetComponent<Text>();

    }

    void Start()
    {
        BindUI();
    }

    void Update()
    {
        if (curPrg != tarPrg)
        {
            UpdateMixPrg();
        }
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SetHurt(10);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    SetCritical(10);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    SetDodge();
        //}

        //SetHpItemPos_1();
    }
    #region 血条
    /// <summary>
    /// 血条Prg
    /// </summary>
    /// <param name="oldVal"></param>
    /// <param name="newVal"></param>
    internal void SetHPVal(int oldVal, int newVal)
    {
        curPrg = (1.0f * oldVal) / this.hpVal;
        tarPrg = (1.0f * newVal) / this.hpVal;
        imgPgLayer1.fillAmount = tarPrg;
       
    }


    /// <summary>
    /// 血条内层Prg
    /// </summary>
    public void UpdateMixPrg()
    {
        float incVel = Constants.ItemHpPrgSpeed * Time.deltaTime;
        if (Mathf.Abs(curPrg - tarPrg) < incVel)
        {
            curPrg = tarPrg;
        }
        else if (curPrg > tarPrg)
        {
            curPrg -= incVel;
        }
        else
        {
            curPrg += incVel;
        }

        imgPgLayer2.fillAmount = curPrg;
    }

    /// <summary>
    /// 初始化血条
    /// </summary>
    /// <param name="t"></param>
    /// <param name="hpRoot"></param>
    /// <param name="hp"></param>
    public void InitItemHpInfo(Transform t, Transform hpRoot, int hp)
    {
        rootTrans = t;
        this.hpRoot = hpRoot;
        rect = transform.GetComponent<RectTransform>();
        hpVal = hp;
        imgPgLayer1.fillAmount = 1;
        imgPgLayer2.fillAmount = 1;
        offsetPos = new Vector3(0, 130f, 0);
    }
    #endregion

    #region 右上BossHP


    public void SetBossHPState(bool state = false, float prg = 1f,int hp=100)
    {
        gameObject.SetActive(state);
        imgPgLayer2.fillAmount = prg;
        imgPgLayer1.fillAmount = prg;
        hpVal = hp;
    }


    #endregion


    #region 血条放头上
    /// <summary>
    /// 血条位置
    /// </summary>
    void SetHpItemPos_1()
    {
        //Vector3 screenPos = Camera.main.WorldToScreenPoint(hpRoot.position);
        //rect.anchoredPosition = screenPos * scaleRate;
    }
    void SetHpItemPos_2()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(rootTrans.position);
        rect.anchoredPosition = screenPos * scaleRate + offsetPos;
    }
    #endregion

    #region 飘字
    public void SetHurt(int num)
    {
        SetTips(TipsType.HP, "-", num);
    }

    public void SetCritical(int num)
    {
        SetTips(TipsType.CRITICAL, "暴击 ", num);
    }

    public void SetDodge()
    {
        SetTips(TipsType.DODGE, "闪避", -1);
    }
    void SetTips(TipsType type, string des, int num)
    {
        Animation ani = null;
        Text text = null;
        switch (type)
        {

            case TipsType.HP:
                {
                    ani = aniHp;
                    text = txtHp;
                }
                break;
            case TipsType.CRITICAL:
                {
                    ani = aniCritical;
                    text = txtCritical;
                }
                break;
            case TipsType.DODGE:
                {
                    ani = aniDodge;
                    text = txtDodge;
                }
                break;
            default: break;
        }
        //
        ani.Stop();
        if (num == -1)
        {
            text.text = des;
        }
        else
        {
            text.text = des + num.ToString();
        }

        ani.Play();
    }
    #endregion
}

  
