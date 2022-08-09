/****************************************************
    文件：SkillItem.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/9 20:12:49
	功能：只处理按钮的冷却
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour 
{

    #region 简单的计时例子

    [Header("SkillItem")]
    public float timer = 0f;
    public float delta = 0.1f;
    public float deltaSum = 0f;

    [Header("外参")]
    public bool isCooled=false;
    public float time = 1f;
    [Header("UI")]
    public Image imgCD;
    public Text txtCD;
    public Button btn;
    /// <summary>被点击时设置为该按钮的selfID，结束后重置为-1</summary>
    int curID = -1;
    /// <summary>技能123分别是123</summary>
    int selfID = -1;
    public EntityPlayer player;

    void BindUI()
    {
        btn=transform.Find("icon").GetComponent<Button>();
        imgCD=transform.Find("imgCD").GetComponent<Image>();
        txtCD=transform.Find("imgCD/txtCD").GetComponent<Text>();

        btn.onClick.AddListener(() => {
            ClickBtn();
        });

    }

  public  void Init(EntityPlayer player,float sec=5f )
    {
        time = sec;
        isCooled = true;
        curID = -1;
        selfID = GetSelfIDByName();
        this.player = player;
        BindUI();
    }

    public void ClickBtn()
    {
        
        deltaSum = 0f;
        timer = 0f;
        delta = 0.1f;
        curID = selfID;
        isCooled = false;
    }

    void Update()
    {
        if (player.isDead)
        {
            btn.interactable = false;
        }
        if (isCooled == false || PlayerCtrlWnd.Instance.GetCanRlsSkill()==false )
        {
            btn.interactable = false;
            //
            timer += Time.deltaTime;
            if (timer > delta)
            {
                deltaSum += delta;

                timer = 0f;
            }

            if (deltaSum > time)
            {
                isCooled = true;
                deltaSum = 0f;
                timer = 0f;

            }
            if (curID == selfID)//该按钮是点击的技能按钮
            { 
                txtCD.text = (time - deltaSum).ToString("0.0");
                imgCD.fillAmount = 1.0f - deltaSum / time; 
            }

        }
        else
        {
            txtCD.text = "";
            imgCD.fillAmount = 0;
            btn.interactable = true;
            curID = -1;
        }

    }

    #endregion

    /// <summary>
    /// 该按钮是哪个技能
    /// </summary>
    /// <returns></returns>
    int GetSelfIDByName()
    {
        if (gameObject.name.Equals("1")) return 1;
        if (gameObject.name.Equals("2")) return 2;
        if (gameObject.name.Equals("3")) return 3;
        return 0;
    }
}