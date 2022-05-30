/****************************************************
    文件：BtnAtk.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/30 1:41:14
	功能：暂且放着，在PlayerCtrlWnd运行了一个，但4个麻烦，所以先拆出来
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class BtnAtk 
{

    [Range(0f, 1f)]
    public float prg;

    public float skill1Timer = 0f;
    public float skill1Time = 5f;

    public float speed = 10f;
    public bool isCool;
    public Text txtCD;
    public Button btn;
    public Image imgCD;

    void InitAtkBtn()
    {

        txtCD.text = "";
        btn.interactable = true;
        btn.onClick.AddListener(() => { btn.interactable = true; isCool = false; });
        imgCD.fillAmount = 0f;
        isCool = true;
    }
    void Update()
    {
        if (!isCool)
        {
            prg = skill1Timer / skill1Time;
            txtCD.text = (skill1Time - skill1Timer).ToString("0.0") + "s";
            imgCD.fillAmount = 1f - prg;
            skill1Timer = Timer(() =>
            {
                btn.interactable = false;
                skill1Timer += Time.deltaTime * speed;
            },
            () =>
            {
                GameRoot.AddTips("冷却完成");
                btn.interactable = true;
                isCool = true;
                txtCD.text = "";
            }, skill1Timer, skill1Time);
        }
    }

    #region 委托复用形式
    public delegate void Delegate();//定义委托类型
    /// <summary>超出时间执行超时函数overTimeFunc，否则计时函数timingFunc</summary>
    public float Timer(Delegate timingFunc, Delegate overTimeFunc, float timer, float time)//定时执行函数
    {
        timer += Time.deltaTime;

        if (timer > (time + 0.001f) || timer > (time - 0.001f))//观察到面板上slider不是总恰好==0
        {
            timer = 0f;
            overTimeFunc();
        }
        else
        {
            timingFunc();
        }
        return timer;
    }

    #endregion
}