/****************************************************
    文件：SkillItem.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/9 20:12:49
	功能：
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
    void BindUI()
    {
        btn=transform.Find("icon").GetComponent<Button>();
        imgCD=transform.Find("imgCD").GetComponent<Image>();
        txtCD=transform.Find("imgCD/txtCD").GetComponent<Text>();

        btn.onClick.AddListener(() => {
            ClickBtn();
        });

    }

  public  void Init(float sec=5f)
    {
        time = sec;
        isCooled = true;
        BindUI();
    }

    public void ClickBtn()
    {
        deltaSum = 0f;
        timer = 0f;
        delta = 0.1f;
        
        isCooled = false;
    }

    void Update()
    {
        if (isCooled == false)
        {
            btn.interactable = false;
            //
            timer += Time.deltaTime;
            if (timer > delta)
            {
                deltaSum += 0.1f;

                timer = 0f;
            }

            if (deltaSum > time)
            {
                isCooled = true;
                deltaSum = 0f;
                timer = 0f;

            }

            txtCD.text = (time - deltaSum).ToString("0.0");
            imgCD.fillAmount = 1.0f - deltaSum / time;
        }
        else
        {
            txtCD.text ="";
            imgCD.fillAmount = 0;
            btn.interactable = true;
        }

    }

#endregion

}