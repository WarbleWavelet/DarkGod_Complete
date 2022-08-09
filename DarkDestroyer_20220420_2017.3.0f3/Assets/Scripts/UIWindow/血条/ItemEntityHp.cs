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

public class ItemEntityHp : MonoBehaviour
{


    #region 字段 属性
    [Header("ItemEntityHp")]
    /// <summary>最上面一层</summary> 
    public Image imgPgLayer1;
    public Image imgPgLayer2;
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
    int hpVal;
    /// <summary>z做血条两层PreG需要的数据</summary>
    public float curPrg;
    public float tarPrg;

    [Header("血条位置的两种做法")]
    public Vector3 offsetPos = new Vector3(0, 20f, 0);
    public Transform hpRoot;
    #endregion


    public void BindUI()
    {
        imgPgLayer1 = transform.Find("imgPgLayer1").GetComponent<Image>();
        imgPgLayer2 = transform.Find("imgPgLayer2").GetComponent<Image>();

        txtCritical = transform.Find("txtCritical").GetComponent<Text>();
        aniCritical = transform.Find("txtCritical").GetComponent<Animation>();
        txtDodge = transform.Find("txtDodge").GetComponent<Text>();
        aniDodge = transform.Find("txtDodge").GetComponent<Animation>();
        txtHp = transform.Find("txtHp").GetComponent<Text>();
        aniHp = transform.Find("txtHp").GetComponent<Animation>();


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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetHurt(10);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCritical(10);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetDodge();
        }

        SetHpItemPos_1();
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

 


    #region 血条放头上
    /// <summary>
    /// 血条位置
    /// </summary>
    void SetHpItemPos_1()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(hpRoot.position);
        rect.anchoredPosition = screenPos * scaleRate;
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

  

public enum TipsType
{
    HP,
    CRITICAL,
    DODGE
}