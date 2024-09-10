/****************************************************
    文件：GameObjectPath.cs
	作者：lenovo
    邮箱: 
    日期：2023/8/29 15:0:57
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class GameObjectPath
{
    public const string GameRoot_PLANE = "GameRoot/PLANE";
    public const string GameRoot_PLANE_Plane = "GameRoot/PLANE/Plane";
    public const string Pool_BulletPool = "Pool/BulletPool";
    public const string Pool_PlanePool = "Pool/PlanePool";
    public const string Pool_LifePool = "Pool/LifePool";
    public const string RightTopPin_Star_Value = "RightTopPin/Star/Value";
    public const string Star_Value = "Star/Value";
    public const string OK_Value = "OK/Start";
    public const string Star_BG_Text = "Star/BG/Text";
    public const string Diamond_BG_Text = "Diamond/BG/Text";
    public const string Creator_EnemyCreator = "Creator/EnemyCreator";
    public const string Upgrades_Text = "Upgrades/Text";
    public const string Upgrades_Upgrades_Text = "Upgrades/Upgrades/Text";
    public const string Frame_Content = "Frame/Content";
    public const string Frame_Buttons = "Frame/Buttons";
    public const string Enter_Text = "Enter/Text";
    public const string Hand_Text = "Hand_Text";
    public const string Mgr_CreatorMgr = "Mgr/CreatorMgr";
    public static string System_LifeCycleSystem =  $"{ GameObjectName.System}/{ GameObjectName.LifeCycleSystem}";
    public static string System_CoroutineSystem =  $"{ GameObjectName.System}/{ GameObjectName.CoroutineSystem }";

}




