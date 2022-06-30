/****************************************************
    文件：LoginWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/25 16:39:4
	功能：加载进度界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class LoadingWnd : WindowRoot
{

    [Header("进度条")]
    public Text textTips;
    public Text textPrg;
    public Image imageFG;
    public Image imagePoint;

    void Start()
    {
        //可能挂载快点，但想清晰点
        textTips = transform.Find("BottomPin/textTips").GetComponent<Text>();
        imageFG = transform.Find("BottomPin/loadingFg").GetComponent<Image>();
        imagePoint = transform.Find("BottomPin/loadingFg/imgPoint").GetComponent<Image>();
        textPrg = transform.Find("BottomPin/loadingFg/textPrg").GetComponent<Text>();

        InitWnd();
    }

    /// <summary>
    /// 初始进度窗口
    /// </summary>
    protected override void InitWnd()
    {
        base.InitWnd();
        SetText(textTips,"这是一条Tips");
        SetText(textPrg, "0.0%");
        imageFG.fillAmount=0;
        imagePoint.transform.localPosition=new Vector3(-607.488f,0,0);//value=0时

    }


    public void SetProgress(float prg)
    {
        //ForTooFastToSee(prg);
        //
        //textPrg.text = (int)prg*100+"%";//原来这样写，但是应该90%时显示为0%
        SetText(textPrg, (prg*100).ToString("0.00")+"%");
        imageFG.fillAmount = prg;
        imagePoint.transform.localPosition = new Vector3(-607.488f + prg*(607.488f*2), 0, 0);//value=0时
    }

    /// <summary>
    /// 太快看不到
    /// </summary>
    void ForTooFastToSee(float prg)
    {
        print(prg);
        print(prg * 100 + "%");
    }
}