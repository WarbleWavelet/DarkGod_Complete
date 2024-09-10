/****************************************************
    文件：Instants.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/25 16:26:27
	功能：常量配置
*****************************************************/

using UnityEngine;







public static class Constants
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
    /// <summary>副本</summary>
    public const string InstanceHuangYe = "bgHuangYe";
    public const string InstanceWin = "fbwin";
    public const string InstanceLose = "fblose";
    //
    public const string UILoginBtn = "uiLoginBtn";
    public const string UIClickBtn = "uiClickBtn";
    public const string UIOpenPage = "uiOpenPage"; 
    /// <summary>点击侧边栏的Bgm</summary>
    public const string UIExtenBtn = "uiExtenBtn";
    /// <summary>强化</summary>
    public const string FBItemEnter = "fbitem";
    public const string AssassinHit = "assassin_Hit";
    #endregion


    #region Screen

    public const int ScreenStandardWidth = 1334;
    public const int ScreenStandardHeight = 750;
    /// <summary>摇杆点标准距离</summary>
    public const int ScreenOPDis = 90;
    #endregion


    #region Ani
    public const int DefaultMoveSpeed = 8;
    public const int PlayerRunSpeed = 8;
    public const int MonsterMoveSpeed = 3;

    /// <summary>加速度，过渡动画</summary>
    public const float AccelerSpeed = 5f;

    public const int BlendIdle = 0;
    public const int BlendRun = 1;
    #endregion


    #region ctrl Action 动画器的参数
    /// <summary>Idle和Move的BlendTree</summary>
    public const int ActionDefault = -1;
    public const int ActionBorn = 0;
    public const int ActionIdle = 1;
    public const int ActionDie = 100;
    public const int ActionHit = 101;
    public const int ActionAtk1 = 11;
    public const int ActionAtk2 = 12;
    public const int ActionAtk3 = 13;
    public const int ActionAtk4 = 14;
    public const int ActionAtk5 = 15;
    public const int ActionSkill1 = 1;
    public const int ActionSkill2 = 2;
    public const int ActionSkill3 = 3;

    /// <summary>skill cfg里的ID</summary>
    public const int AttackID1 = 111;
    public const int AttackID2 = 112;
    public const int AttackID3 = 113;
    public const int AttackID4 = 114;
    public const int AttackID5 = 115;
    public const int SkillID1 = 101;
    public const int SkillID2 = 102;
    public const int SkillID3 = 103;
    public const int SkillIDDefault = 0;
    #region 延时播放动画
    public const int DelayDefault = 500;//1s
    public const int DelayBorn = 500;
    /// <summary>Born后延时</summary>
    public const int BornDelay = 1500;
    public const int DelayIdle = 1000;
    public const int DelayActive = 500;
    public const int DelayDieAniLength = 5000;

    #endregion

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



    #region 聊天
    /// <summary>每句字数</summary>
    public const int MaxChatLenth = 12;
    /// <summary>同时多少句</summary>
    public const int MaxChatCount= 12;
    #endregion

    /// <summary>头上血条内层的变化速度</summary>
    public const float ItemHpPrgSpeed = 0.05f;

    /// <summary> 毫米秒内再按生效，连招</summary>
    public const int ComboSpace = 500;
    public const int NoComboNextSkillID = 0;

    //Layer
    public const int LayerPlayer = 9;
    public const int LayerMonster = 10;
}