/****************************************************
    文件：Instants.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/25 16:26:27
	功能：常量配置
*****************************************************/

using UnityEngine;

public enum TxtColor
{
    Red,
    Green,
    Blue,
    Yellow

}
public class Constants
{

    #region 颜色
    private const string ColorRed = "<color=#FF0000FF>";
    private const string ColorGreen = "<color=#00FF00FF>";
    private const string ColorBlue = "<color=#00B4FFFF>";
    private const string ColorYellow = "<color=#FFFF00FF>";
    /// <summary>结束的标签</summary>
    private const string ColorEnd = "</color>";

    public static string Color(string str, TxtColor c)
    {
        string result = "";
        switch (c)
        {
            case TxtColor.Red:
                result = ColorRed + str + ColorEnd;
                break;
            case TxtColor.Green:
                result = ColorGreen + str + ColorEnd;
                break;
            case TxtColor.Blue:
                result = ColorBlue + str + ColorEnd;
                break;
            case TxtColor.Yellow:
                result = ColorYellow + str + ColorEnd;
                break;
        }
        return result;
    }
    #endregion

    #region Scene
    public const string sceneLogin = "SceneLogin";
   // public const string sceneMainCity = "SceneMainCity";
    public const int MainCityMapID = 10000;//map.xml
    #endregion


    #region Audio
    public const string BGLogin = "bgLogin";
    /// <summary>进入主城的Bgm</summary>
    public const string BGMainCity = "bgMainCity";
    public const string UILoginBtn = "uiLoginBtn";
    public const string UIClickBtn = "uiClickBtn";
    public const string UIOpenPage = "uiOpenPage"; 
    /// <summary>点击侧边栏的Bgm</summary>
    public const string UIExtenBtn = "uiExtenBtn";
    /// <summary>强化</summary>
    public const string FBItemEnter = "fbitem";
    #endregion


    #region Screen

    public const int ScreenStandardWidth = 1334;
    public const int ScreenStandardHeight = 750;
    /// <summary>摇杆点标准距离</summary>
    public const int ScreenOPDis = 90;
    #endregion


    #region Ani
    public const int PlayerMoveSpeed = 8;
    public const int MonsterMoveSpeed = 4;

    /// <summary>加速度，过渡动画</summary>
    public const float AccelerSpeed = 5f;
    public const int BlendIdle = 0;
    public const int BlendWalk = 1;
    #endregion 


    #region AutoGuide
    public const int NPCTask = -1;
    public const int NPCWiseMan = 0;
    public const int NPCGeneral = 1;
    public const int NPCArtisan = 2;
    public const int NPCTrader = 3;



    #endregion
    /// <summary>最高星级</summary>
    public const int MaxStarLv = 10;


}