/****************************************************
    文件：GameObjectName.cs
	作者：lenovo
    邮箱: 
    日期：2022/7/15 12:57:58
	功能：
*****************************************************/



public static partial class GameObjectName 
{

	public const string ContentBGImg = "ContentBGImg";
	public const string BG1 = "BG1";
	public const string BG2 = "BG2";
	public const string RoleImg = "RoleImg";
	public const string DialogText = "DialogText";
	public const string RoleText = "RoleText";
	public const string BtnDesText = "BtnDesText";

}
public static partial class GameObjectName  //组件
{
    /// <summary>半径</summary>
    public const string Collider = "Collider";
    public const string Collider2D = "Collider2D";
    public const string Trigger = "Trigger";
    public const string Trigger2D = "Trigger2D";
}
public static partial class GameObjectName  //图形
{
    /// <summary>半径</summary>
    public const string Radius = "Radius";
    /// <summary>弧度</summary>
    public const string Radian = "Radian";
    /// <summary>弧度</summary>
    public const string Angle = "Angle";
    /// <summary>天使</summary>
    public const string Angel = "Angel";
    /// <summary>欧拉角,用V3来表示的角度</summary>
    public const string EulerAngle = "EulerAngle";
    /// <summary>数学上的向量</summary>
    public const string Vector = "Vector";
    /// <summary>物理学上的向量</summary>
    public const string Magnitude = "Magnitude";
    /// <summary>向量长度</summary>
    public const string VectorLength = "VectorLength";
    /// <summary>向量方向</summary>
    public const string VectorDirection = "VectorDirection";
}
public static partial class GameObjectName  //关系图
{
    public const string Node = "Node";
    public const string Line = "Line";

}
public static partial class GameObjectName
{
    public const string LevelText = "LevelText";
    public const string LevelsTrans = "LevelsTrans";
    public const string EnterBtn = "EnterBtn";
    public const string BackBtn = "BackBtn";
    public const string LeftBtn = "LeftBtn";
    public const string RightBtn = "RightBtn";
    public const string MaskBtn = "MaskBtn";
    public const string StartBtn = "StartBtn";
    public const string ExitBtn = "ExitBtn";
    public const string StrengthenBtn = "StrengthenBtn";
    public const string UpgradesBtn = "UpgradesBtn";
    public const string HerosTrans = "HerosTrans";
    //
    public const string PropertyItem_Attack = "PropertyItem_Attack";
    public const string PropertyItem_FireRate = "PropertyItem_FireRate";
    public const string PropertyItem_Life = "PropertyItem_Life";
    //
    public const string IconImg = "IconImg";
    public const string StarText = "StarText";
    public const string DiamondText = "DiamondText";
}
public static partial class GameObjectName //QF的
{
    public const string UIRoot = "UIRoot";
    public const string Common = "Common";
    /// <summary>弹出框</summary>
    public const string PopUI = "PopUI";
    public const string Bg = "Bg";
    public const string BackGround = "BackGround";
    //
    /// <summary>面板</summary>
    public const string Panel = "Panel";
    /// <summary>视图</summary>
    public const string View = "View";
    /// <summary>窗口</summary>
    public const string Window = "Window";
    public const string Wnd = "Wnd";
    /// <summary>条</summary>
    public const string Bar = "Bar";
}
public static partial class GameObjectName
{
    public const string Canvas = "Canvas";

    public const string MainCamera = "Main Camera";
    public const string Text = "Text";
    public const string Button = "Button";
    public const string Slider = "Slider";
    public const string Mask = "Mask";
}
public static partial class GameObjectName
{ 
    public const string BGButton = "BGButton";
}
public static partial class GameObjectName
{
    public const string RecyclePoolTrans = "RecyclePoolTrans";//ObjectMgr用到
    public const string NormalNavMesh = "NormalNavMesh";
    public const string FightNavMesh = "FightNavMesh";
    public const string TargetPos = "TargetPos";
    public const string FightBG = "FightBG";
    public const string Player = "Player";
    public const string PlayerPos = "PlayerPos";
    public const string EnemyPos = "EnemyPos";
    /// <summary>无时间信息的路径</summary>
    public const string Path = "Path";
    /// <summary>轨迹,路径(Path)和轨迹(Trajectory)的区别就在于,轨迹还包含了时间信息</summary>
    public const string Trajectory = "Trajectory";
    /// <summary>补丁</summary>
    public const string Patch = "Patch";
    public const string PlayerDieStartMovePath = "PlayerDieStartMovePath";
    public const string PlayerDieEndMovePath = "PlayerDieEndMovePath";
    public const string EnemyDieStartMovePath = "EnemyDieStartMovePath";
    public const string EnemyDieEndMovePath = "EnemyDieEndMovePath";
    public const string BgTrans = "BgTrans";
    public const string ClickEffects = "ClickEffects";
    public const string AudioSourceManager = "AudioSourceManager";
    public const string GameStart = "GameStart";
    public const string GameRoot = "GameRoot";
    //
    public const string System = "System";
    public const string LifeCycleSystem = "LifeCycleSystem";
    public const string CoroutineSystem = "CoroutineSystem";
    public const string AudioSystem = "AudioSystem";
    public const string Sys = "Sys";
    public const string Mgr = "Mgr";
    public const string Mgr_bracketGame = "Mgr(Game)";
    public const string Mgr_bracketMain = "Mgr(Main)";
    public const string GameLayerMgr = "GameLayerMgr";
    public const string PoolMgr = "PoolMgr";
    public const string GameProcessMgr = "GameProcessMgr";
    public const string LifeCycleMgr = "LifeCycleMgr";
    public const string GameMgr = "GameMgr";
    public const string GameManager = "GameManager";
    //
    public const string BulletRoot = "BulletRoot";
    public const string Shoot = "Shoot";
    public const string Muzzle = "Muzzle";
    /// <summary>多个枪口时父节点</summary>
    public const string Muzzles = "Muzzles";
    public const string MuzzleTrans = "MuzzleTrans";
    public const string PlaneEnemyCreator = "PlaneEnemyCreator";
    public const string map_0 = "map_0";
    public const string map_1 = "map_1";
    public const string MapMgr = "MapMgr";
    public const string MapCtrl = "MapCtrl";
    public const string MissileCreaterMgr = "MissileCreaterMgr";
    public const string SpawnPlaneMgr = "SpawnPlaneMgr(自己加的)";
    public const string CreatorMgr = "CreatorMgr";
    public const string Creator = "Creator";
    /// <summary>管出不管进,只负责new或生成</summary>
    public const string Factory = "Factory";
    /// <summary>有进有出,生成回收</summary>
    public const string Pool = "Pool";
    public const string BulletPool = "BulletPool";
    public const string Value = "Value";
    public const string GuideUiRoot = "GuideUiRoot";
    public const string PlaneEnemyView = "PlaneEnemyView";
    public const string PlayerView = "PlayerView";
    public const string SlowSpeedEffect = "SlowSpeedEffect";

}
public static partial class GameObjectName
{ 

}
public static partial class GameObjectName
{ 
    public const string Heros = "Heros";
    public const string Hand = "Hand";
    public const string Enter = "Enter";
    public const string Continue = "Continue";
    public const string Exit = "Exit";

    public const string Life = "Life";
    public const string Progress = "Progress";
    public const string Preload = "Preload";
    public const string Switchplayer = "Switchplayer";
    public const string Property = "Property";

    public const string Number = "Number";
    public const string Num = "Num";
    public const string Result = "Result";
    public const string Frame = "Frame";
    public const string Content = "Content";
    public const string Buttons = "Buttons";
    public const string Icon = "Icon";
    public const string Item = "Item";
    /// <summary>载体</summary>
    public const string Carrier = "Carrier";
    //
    public const string Levels = "Levels";
    public const string LevelClickable = "LevelClickable";
    public const string LevelSelectable = "LevelSelectable";
    public const string LevelItem = "LevelItem";
    //
    public const string Start = "Start";
    public const string Begin = "Begin";
    public const string End = "End";
    public const string GameOver = "GameOver";
    public const string Back = "Back";
    public const string Move = "Move";


}

public static partial class GameObjectName
{
    public const string Plane = "Plane";
    public const string Star = "Star";
    public const string Money = "Money";
    public const string Gold = "Gold";
    public const string Diamond = "Diamond";
    public const string Strengthen = "Strengthen";
    public const string Shield = "Shield";
    public const string Bullet = "Bullet";
    public const string Bomb = "Bomb";
    public const string Boss = "Boss";
    public const string Score = "Score";
}
public static partial class GameObjectName
{
    public const string Left = "Left";
    public const string Right = "Right";
     //
    public const string Earn = "Earn";
    public const string Cost = "Cost";
    public const string Spend = "Spend";
    //
    public const string Add = "Add";
    /// <summary>服从控制，减法</summary>
    public const string Sub = "Sub";
    /// <summary>控制</summary>
    public const string Dom = "Dom";
    /// <summary>代理</summary>
    public const string Proxy = "Proxy";

}
