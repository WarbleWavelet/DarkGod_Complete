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
    public float steping = 0f;
    public float step = 0.1f;
    public float timing = 0f;
    public float time = 1f;

    [Header("外参")]
    public bool isCooled=false;
    [Header("UI")]
    public Image imgCD;
    public Text txtCD;
    public Button btn;
    /// <summary>被点击时设置为该按钮的selfID，结束后重置为-1</summary>
    int curID = -1;
    /// <summary>技能123分别是123</summary>
    int selfID = -1;
    public EntityPlayer player;
    #endregion


    #region Life


    public void Init(EntityPlayer player,float sec=5f )
    {
        time = sec;
        isCooled = true;
        curID = -1;
        selfID = GetSelfIDByName();
        this.player = player;
        BindUI();
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
            this.StepTimer(ref steping, step,
                ref timing,()=>
                { 
                    if (curID == selfID)//该按钮是点击的技能按钮,有点像绑定了timing,timing变化就执行
                    { 
                        txtCD.text = (time - timing).ToString("0.0");
                        imgCD.fillAmount = 1.0f - timing / time; 
                    }                
                },
                time,()=> 
                { 
                    isCooled = true;
                });
        }
        else
        {    
            btn.interactable = true;
            txtCD.text = "";
            imgCD.fillAmount = 0;
            curID = -1;
        }

    }
    #endregion



    #region pub


    public void ClickBtn()
    {
        
        timing = 0f;
        steping = 0f;
        step = 0.1f;
        curID = selfID;
        isCooled = false;
    }
    #endregion


    #region pri


    void BindUI()
    {
        btn = transform.Find("icon").GetComponent<Button>();
        imgCD = transform.Find("imgCD").GetComponent<Image>();
        txtCD = transform.Find("imgCD/txtCD").GetComponent<Text>();

        btn.onClick.AddListener(() => {
            ClickBtn();
        });

    }

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
    #endregion  

}