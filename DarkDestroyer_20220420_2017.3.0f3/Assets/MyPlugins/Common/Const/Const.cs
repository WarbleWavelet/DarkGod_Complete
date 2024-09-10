using UnityEngine;
using static ExtendAudio;

public static partial class ConstAxes
{
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";
    public const string Mouse_X = "Mouse X";
    public const string Mouse_Y = "Mouse Y";
    public const string Mouse_ScrollWheel = "Mouse ScrollWheel";
}


public static partial class Const
{
    public const float Demo01_Offset = 10f;

    public const string Canvas = "Cnavas";



	#region Mgr      
	/// <summary>类对象池默认最大 Pool_MaxCnt </summary>
	public const int ClassObjectPool_MAXCNT = 500;
    public const int ClassObjectPool_AsyncLoadResPara_MAXCNT = 50;
    public const int ClassObjectPool_AsyncLoadResCallBack_MAXCNT = 100;
    public const int ClassObjectPool_RESOBJ_MAXCNT = 1000;

    /// <summary>卡着异步加载资源的最长时间</summary>
    public const int MAXASYNCLOADRESTIME = 200000;

    public const string FixPre = "";
    public const string FixSur_ResObject_m_Go = "(Recycle)";
    public const string FixSur_InstaniateGameObject = "(Clone)";


    //ResourceMgr
    public const int MaxCacheCnt = 500;

    #endregion




    #region 打安卓包
    public const string Android_keyaliasPass = "realframe";
    public const string Android_keystorePass = "realframe";
    /// <summary>密钥文件的名字</summary>
    public const string Android_keyaliasName = "android.keystore";
    /// <summary>keytool -list -keystore realframe.keystore  里面的名字</summary>
    public static string Android_keystoreName = Application.dataPath.Replace("Assets", "realframe.keystore");
    public static string Android_applicationIdentifierFix = "com.TTT.";

   

    #endregion


    #region AES加密
      public const string PrivateKey = "WWS";
    #endregion  

}

/// <summary>翻译</summary>
public class Const_Translate
{
    public const string Field = "字段";
    public const string Property = "属性";
}
public class Const_CHI
{
    public const string LeftHand = "左手";
    public const string RightHand = "右手";
    public const string You_Lose = "You Lose";
    public const string You_Win = "You Win";

}
public class Const_FolderName
{
    public const string AddresableAsset = "AddresableAsset";
    public const string android = "android";
}
public static partial class ConstPath//Path
{
    public const string StraightPath = "StraightPath";
    public const string WPath = "WPath";
    public const string VPath = "VPath";
    public const string EllipsePath = "EllipsePath";
    public const string StayOnTopPath = "StayOnTopPath";
    public const string EnterPath = "EnterPath";
    public const string Up2DownEnterPath = "Up2DownEnterPath";
    public const string Left2RightEnterPath = "Left2RightEnterPath";
    public const string Right2LeftEnterPath = "Right2LeftEnterPath";
}
public static partial class ConstPath//Path
{
    const string _userName = "lenovo";
    const string _projectName = "xxx";
    static string DefaultCachePath = $"C:/Users/{_userName}/AppData/LocalLow";
    /// <summary>unity自身默认Remote缓存路径</summary>
    public static string UnityDefaultCachePath = $"{DefaultCachePath}/Unity";
    /// <summary>Addressable默认Remote缓存路径</summary>
    public static string AddressableDefaultCachePath = $"{UnityDefaultCachePath}/{_projectName}_AdressableLoad";
}
public static partial class ConstAssetsPath//Assets/开头的
{ 
     const string AssetsFolder = "Assets/";
    public static string StreamingAssets = $"{AssetsFolder}StreamingAssets";
    public static string StreamingAssetsFolder = $"{AssetsFolder}StreamingAssets/";
    public static string Resources = $"{AssetsFolder}Resources";
    public static string ResourcesFolder = $"{AssetsFolder}Resources/";
}
public class Const_Geo
{
    /// <summary>对称</summary>
    public const string Symmetry = "Symmetry";
}
public static partial class Const
{
    /// <summary>放UI相关的资源与预制体</summary>
    public const string GUI = "GUI"; 
    /// <summary>放关卡地图相关资源与预制体</summary>
   public const string Maps = "Maps"; 
     /// <summary>放游戏角色等相关的资源与预制体</summary>
   public const string Charactors = "Charactors"; 
     /// <summary>放游戏音乐与音效</summary>
   public const string Sounds = "Sounds";
    /// <summary>游戏的特效</summary>
    public const string Effects = "Effects"; 
}
public static partial class Const
{
    /// <summary>模式</summary>
    public const string Pattern = "Pattern";
    /// <summary>机制</summary>
    public const string Mechanism = "Mechanism";
    /// <summary>策略</summary>
    public const string Strategy = "Strategy";

}
public static partial class Const
{
    public const string Bundles = "Bundles";
}
public static partial class Const
{

    public const int DelayBack = 2;
    /// <summary> view界面绑定脚本优先级 </summary>
    public const int BIND_PREFAB_PRIORITY_VIEW = 0;

    /// <summary> controller绑定脚本优先级 </summary>
    public const int BIND_PREFAB_PRIORITY_CONTROLLER = 1;


    //游戏中部分常量数据
    public const int LIFE_ITEM_NUM = 10;
    public const float CD_EFFECT_TIME = 2;

    /// <summary> 开火的基础时间 </summary>
    public const float FIRE_BASE_TIME = 1;

    /// <summary>  开火的CD时间 </summary>
    public const float FIRE_CD_TIME = 0.15f;

    /// <summary> 开火的持续时间 </summary>
    public const float FIRE_DURATION = 0.3f;

    /// <summary> 护盾持续时间 </summary>
    public const float SHIELD_TIME = 6;


    public const string MAP_PREFIX = "map_level_";

    public const string ENEMY_PREFIX = "Enemy_{0}_{1}";

    /// <summary> 敌方血条自适应宽度比例 </summary>
    public const float ENEMY_LIFE_BAR_WIDTH = 0.8f;

    public const float WAIT_BOSS_TIME = 5;

    /// <summary> 两关之间的等待时间 </summary>
    public const float WAIT_LEVEL_START_TIME = 2;//5;
}


