using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public static class CommonClass
{

    #region 对比
    /// <summary>"abc".CompareTo("aca")返回-1 </summary>
    public static int Compare_CompareTo(string a,string b)
    {
        return a.CompareTo(b);
    }



    /// <summary>
    /// 值类型，引用类型
    /// </summary>
    public static bool Compare_Equals(object a, object b)
    {
        return a.Equals(b);

    }
    /// <summary> ==,值类型；所以当对比的是值类型，却用一用类型object l=1;object m=1; =>l==b出问题</summary>
    public static bool Compare_TwoEqual(object a,object b)
    {
        return a == b;

    }


    #endregion

    #region Enum enum 枚举


    public static void Enum_Foreach<T>()
    {
        foreach (T p in Enum.GetValues(typeof(T)))
        {
            Debug.Log(p);
        }
    }


    private enum Property
    {
        attack = 0,
        fireRate,
        life,
        COUNT //遍历枚举用到
    }
    private static void Enum_Foreach(Property _enum)
    {
        for (Property i = 0; i < Property.COUNT; i++)
        {
            Debug.Log(i.ToString());
        }
    }
    #endregion  

    #region Type


    public static string Type_Name<T>()
    {
        return typeof(T).Name;
    }

    public static TypeConverter Type_Converter_Get(Type type)
    {
      return  TypeDescriptor.GetConverter(type);
    }


    public static TypeConverter Type_Converter_Get<T>()
    {
        Type type = typeof(T);
        return TypeDescriptor.GetConverter(type);
    }
    #endregion

    #region Find


    /// <summary></summary>
    public static GameObject  Find(this  GameObject go,  string goName)
    {
        //  return  GameObject.Find(goName); 
        return go.transform.Find(goName).gameObject;
    }

    /// <summary>搜索子树 </summary>
    public static Transform Find(this Transform t, string path)
    {
        return t.Find(path);  //局部搜索
    }

    #endregion

    #region Canvas快捷操作


    public static Transform Canvas_Get()
    {
        Transform canvas= GameObject.FindGameObjectWithTag(Tags.Canvas).transform;
        if (canvas == null)
        {
            canvas = UnityEngine.Object.FindObjectOfType<Canvas>().transform;
        }

        return canvas;
    }
    public static Text Canvas_Text_Get()
    {

        GameObject canvas = GameObject.FindGameObjectWithTag(Tags.Canvas);
        return canvas.transform.Find( GameObjectName.Text).GetComponent<Text>();
    }

    public static Button Canvas_Button_AddListener(Action action)
    {

        return Canvas_Button_AddListener(GameObjectName.Button,  action);
    }

    public static Button Canvas_Button_AddListener(string btnName, Action action)
    {

        GameObject canvas = GameObject.FindGameObjectWithTag( Tags.Canvas );
        Button button = canvas.transform.Find(btnName).GetComponent<Button>();
        button.onClick.AddListener(() => action());
        return button;
    }

    public static Button Button_AddListener(Button button,Action action)
    {

        button.onClick.AddListener(() => action());
        return button;
    }

    #endregion


    #region 枚举 布尔

    /// <summary>
    /// 字符串转枚举
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_string"></param>
    /// <returns></returns>
    public static T String2Enum<T>(string _string)
    {
        if (_string == null) return default(T);
        return (T)Enum.Parse(typeof(T), _string);
    }

    public static object String2Enum<T>(string _string, bool _bool = true)
    {
        if (_string == null) return null;
        return (T)Enum.Parse(typeof(T), _string, _bool);
    }


    /// <summary>
    /// 字符串转布尔
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_string"></param>
    /// <returns></returns>
    public static bool String2Bool(string _string)
    {
        return _string == "true" ? true : false;
    }


    public static bool Try_String2Bool(string _string)
    {
        bool _bool;
        bool.TryParse(_string, out _bool);
        return _bool;
    }

    #endregion




    #region Guid
    /// <summary>异步的Guid，为了可以取消该异步</summary> 
    static long m_asyncGuid = 0;
    /// <summary>异步的Guid，为了可以取消该异步</summary>
    public static long CreateGuid()
    {
        return m_asyncGuid++;
    }
    #endregion



    #region Log

    /// <summary>这种不理想，各种系统命名</summary>
    public static void Log(object obj)
    {
        Debug.Log(obj.GetType().ToString() + "." + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().ToString());//类名.方法名
    }




    /// <summary>
    ///  本来想在ILRunTime案例中使用，但是跨域时打印不满意，都是系统函数
    ///  <para/>缺点，有继承抽象类的好像不行，会去打印抽象类的命名空间
    /// </summary>
    /// <param name="frame"> 1:第一层，也就是当前类；2:第二层，也就是调用类；3:第三层，多层调用类；n：以此类推</param>
    /// <returns></returns>
    public static string Log_ClassFunction(int frame = 1)
    {
        StackTrace trace = new StackTrace();
        var sb = new StringBuilder();

        Type type = trace.GetFrame(frame).GetMethod().DeclaringType;// GetFrame()获取是哪个类来调用的
        string method = trace.GetFrame(frame).GetMethod().ToString();// 获取是类中的那个方法调用的

        if (type != null)
        {
            sb.Append(type);
        }

        if (method != null)
        {
            sb.Append(".");
            sb.Append(method);
        }

       // Debug.LogFormat("{0}（命名空间.类.方法，frame={1}）", sb, frame);//未修改前
        string str = sb.ToString().TrimName(TrimNameType.PointPre_SpacingAfter); //
        Debug.LogFormat("{0}（命名空间.类.方法，frame={1}）", str, frame);
        return str;
    }

    public static string Log_ClassFunction(string content,int frame = 1)
    {
        StackTrace trace = new StackTrace();
        var sb = new StringBuilder();

        Type type = trace.GetFrame(frame).GetMethod().DeclaringType;// GetFrame()获取是哪个类来调用的
        string method = trace.GetFrame(frame).GetMethod().ToString();// 获取是类中的那个方法调用的

        if (type != null)
        {
            sb.Append(type);
        }

        if (method != null)
        {
            sb.Append(".");
            sb.Append(method);
        }

        // Debug.LogFormat("{0}（命名空间.类.方法，frame={1}）", sb, frame);//未修改前
        string str = sb.ToString().TrimName(TrimNameType.PointPre_SpacingAfter); //
        Debug.LogFormat("{0}（命名空间.类.方法，frame={1}）", str, frame);
        return str;
    }
    public static string Log_Method(int frame = 1)
    {
        StackTrace trace = new StackTrace();
        var sb = new StringBuilder(); Type type = trace.GetFrame(frame).GetMethod().DeclaringType;// GetFrame()获取是哪个类来调用的
        string method = trace.GetFrame(frame).GetMethod().ToString();// 获取是类中的那个方法调用的

        if (type != null)
        {
            sb.Append(type);
        }

        if (method != null)
        {
            sb.Append(".");
            sb.Append(method);
        }

        // Debug.LogFormat("{0}（命名空间.类.方法，frame={1}）", sb, frame);//未修改前
        string str = sb.ToString().TrimName(TrimNameType.PointPre_SpacingAfter); //
                                                                                 //Debug.LogFormat("{0}（命名空间.类.方法，frame={1}）", str, frame);
        str = str.TrimName(TrimNameType.PointAfter);
        str = str.TrimName(TrimNameType.BracketsAfter);

        return str;
    }

    public static string Log_NamespaceClassFunction(string content,int frame = 1)
    {
        StackTrace trace = new StackTrace();
        var sb = new StringBuilder();

        Type type = trace.GetFrame(frame).GetMethod().DeclaringType;// GetFrame()获取是哪个类来调用的
        string nameSpace = trace.GetFrame(frame).GetMethod().DeclaringType.Namespace;// GetFrame()获取是哪个类来调用的
        string method = trace.GetFrame(frame).GetMethod().ToString();// 获取是类中的那个方法调用的

        if (nameSpace != null)
        {
            sb.Append(nameSpace);
        }
        if (type != null)
        {
            sb.Append(".");
            sb.Append(type);
        }

        if (method != null)
        {
            sb.Append(".");
            sb.Append(method);
        }

        Debug.LogFormat("{0}{1}（命名空间.类.方法，frame={1}）", sb,content, frame);

        return sb.ToString();
    }
    public static string Log_NamespaceClassFunction(int frame = 1)
    {
        StackTrace trace = new StackTrace();
        var sb = new StringBuilder();

        Type type = trace.GetFrame(frame).GetMethod().DeclaringType;// GetFrame()获取是哪个类来调用的
        string nameSpace = trace.GetFrame(frame).GetMethod().DeclaringType.Namespace;// GetFrame()获取是哪个类来调用的
        string method = trace.GetFrame(frame).GetMethod().ToString();// 获取是类中的那个方法调用的

         if(nameSpace!=null)
{
            sb.Append(nameSpace);
        }
        if (type != null)
        {
            sb.Append(".");
            sb.Append(type);
        }

        if (method != null)
        {
            sb.Append(".");
            sb.Append(method);
        }

        Debug.LogFormat("{0}（命名空间.类.方法，frame={1}）",sb,  frame);

        return sb.ToString();
    }
    #endregion


    #region 字符串处理


    /// <summary>
    ///  取枚举表述的值<para />
    ///  懒得改以前的<para />
    /// </summary>
    /// <param name="path"></param>
    /// <param name="type"></param>
    /// <returns></returns>

    [Obsolete("已过时，建议使用=>ExtendUtil.TrimName(string path, TrimNameType type)")]
    public static string TrimName(string path, TrimNameType type)
    {
        switch (type)
        {
            case TrimNameType.SlashFirst:
                {
                    return path.Substring(0,path.IndexOf('/')); //sdcvghasvdj/gdhsag/l.prefab => sdcvghasvdj
                }
            case TrimNameType.SlashAfter:
                {
                    return path.Substring(path.LastIndexOf('/') + 1);//sdcvghasvdj/gdhsag/l.prefab => l.prefab
                }
            //break;             
            case TrimNameType.SlashPre:
                {
                    return path.Substring(0, path.LastIndexOf('/'));//sdcvghasvdj/gdhsag/l.prefab =>sdcvghasvdj/gdhsag
                }
            //break;
            case TrimNameType.DashAfter:
                {
                    return path.Substring(path.LastIndexOf('_') + 1);// A_B=>B
                }
            //break;             
            case TrimNameType.DashPre:
                {
                    return path.Substring(0, path.LastIndexOf('_'));// A_B=>IgnoreLayerCollision
                }
            //break;
            case TrimNameType.SlashAndPoint:
                {
                    string name = path.Substring(path.LastIndexOf('/') + 1);// plane.unity3d
                    name = name.Substring(0, name.LastIndexOf('.'));// plane
                    return name;
                }
            case TrimNameType.PointPre:
                {
                    string name = path.Substring(0, path.LastIndexOf('.'));// plane.unity3d=> plane
                    return name;
                }
            case TrimNameType.PointAfter:
                {
                    string name = path.Substring( path.LastIndexOf('.')+1);// plane.unity3d=> unity3d
                    return name;
                }
            case TrimNameType.BracketsAfter:
                { 
                    string name=path.Substring(0, path.LastIndexOf('(') );//Andy(Clone)=>Andy
                    return name;
                }
            case TrimNameType.SpacingAfter:
                {
                    string name = path.Substring(path.LastIndexOf('.') + 1);// plane.unity3d=> unity3d
                    return name;
                }
            default:
                {
                    return path;
                }
        }


    }



    public static bool NotEndsWith(string str, params string[] lst)
    {
        foreach (var item in lst)
        {
            if (str.EndsWith(item))
            {
                return false;
            }
        }

        return true;
    }


    public static bool EndsWith(string str, params string[] lst)
    {
        foreach (var item in lst)
        {
            if (str.EndsWith(item))
            {
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// 去除所有空格
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string TrimAllSpace(string val)
    {
        return val.Trim().Replace(" ", "");
    }
    #endregion




    #region 辅助
    /// <summary>
    /// AB加载，实例时Shader丢失，修复该问题
    /// </summary>
    /// <param name="go"></param>
    public static void FixShader(GameObject go, string shaderName)
    {
        MeshRenderer[] mr = go.GetComponentsInChildren<MeshRenderer>();
        List<Material> lst = new List<Material>();
        for (int i = 0; i < mr.Length; i++)
        {
            lst.AddRange(mr[i].materials);
        }
        SkinnedMeshRenderer[] smr = go.GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < smr.Length; i++)
        {
            lst.AddRange(smr[i].materials);
        }

        for (int i = 0; i < lst.Count; i++)
        {
            lst[i].shader = Shader.Find(shaderName);
        }
    }
    #endregion


    public static void PlayBGMusic(AudioSource source, AudioClip clip)

    {

        source.clip = clip;
        source.Play();

    }


    public static void BindBtn( Button btn, Action action)
    {
        btn.onClick.AddListener(() =>
        {
            action();

        });
    }








    #region 转格式
    /// <summary>
    /// 为了可视化
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public static void Class2Xml<T>(T cfg, string outputPath)
    {
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }
        FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        XmlSerializer xml = new XmlSerializer(cfg.GetType());
        xml.Serialize(sw, cfg);
        sw.Close();
        fs.Close();


    }


    /// <summary>
    /// 将类对象cfg转Bin，放在path下
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public static void Class2Bin<T>(T cfg, string path)
    {
        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        fs.Seek(0, SeekOrigin.Begin);//清空
        fs.SetLength(0);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, cfg);
        fs.Close();


    }
    #endregion






    #region  App   网络

    public static bool IsAndroidOrIOS()
    {

        return Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android;
    }


    /// <summary>
    /// 网络状态
    /// </summary>
    /// <returns></returns>
    public static NetworkStatusType NetworkStatus()
    {

        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                {
                    return NetworkStatusType.Traffic;
                }

            case NetworkReachability.ReachableViaLocalAreaNetwork:

                {
                    return NetworkStatusType.Wifi;
                }
            case NetworkReachability.NotReachable:
            default:
                {
                    return NetworkStatusType.NotReachable;
                }
        }
    }


    #endregion


    #region BuildTarget
    public static string BuildTarget = "StandaloneWindows64";
    public static void SetBuildTarget(string str)
    {
        BuildTarget = str;
    }
    #endregion



 




    public static void Refresh()
    {
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

	/// <summary>
	/// 名字形象
	/// </summary>
	/// <param name="path"></param>

    public static void ShootAt(this string path)
    {
#if UNITY_EDITOR
        AssetDatabase.Refresh(); //有的是新建，Refresh之前是检测不到的的
        Selection_ActiveObject(path);
#endif
    }


    public static void Selection_ActiveObject(string path)
    {
#if UNITY_EDITOR
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);//"Assets/Config/ABCfg.asset";
#endif
    }

    public static void Selection_OpenPath(string path)
    {
#if UNITY_EDITOR
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);//"Assets/Config/ABCfg.asset";
#endif
    }



    #region  Time DateTime


    public static long Time_Now()
    {
        return System.DateTime.Now.Ticks; 
    }

    /// <summary>数字：年月日时分</summary>
    public static void LogNowTime()
    {
         System.DateTime.Now.ToString("yyyyMMddHHmm");
    }

    /// <summary>
    /// action执行的时间。秒 毫微 纳
    /// </summary>
    /// <param name="action"></param>              \
    public static void Time_During(Action action,TimeUnit timeUnit=TimeUnit.nSeconds)
    {
        long curTime = System.DateTime.Now.Ticks;
        action();
        long during = System.DateTime.Now.Ticks - curTime;

        switch (timeUnit)
        {
            case TimeUnit.Seconds: Debug.LogFormat("运行时间：{0}秒", (during / 1000000000.0f).ToString("0.00")); break;
            case TimeUnit.mSeconds: Debug.LogFormat("运行时间：{0}毫秒", (during / 1000000.0f).ToString("0.00")); break;
            case TimeUnit.uSeconds: Debug.LogFormat("运行时间：{0}微秒", (during / 1000.0f).ToString("0.00")); break;
            case TimeUnit.nSeconds: Debug.LogFormat("运行时间：{0}纳秒", (during).ToString("0.00")); break;
            case TimeUnit.pSeconds: Debug.LogFormat("运行时间：{0}皮秒", (during * 1000.0f).ToString("0.00")); break;
            default:  break;
        }
    }


    /// <summary>
    ///  C#自带的异步   <para />
    ///  每过awaitTime，就会问距离timer过了多久<para />
    ///  如果超过time，isActing==false（没有正在执行action） ，就执行action<para />
    ///  bug 问题是需要异步内部改变isActing需要被返回，但又不被允许
    /// </summary>
    /// <param name="timer"></param>
    /// <param name="time"></param>
    /// <param name="isActing"></param>
    /// <param name="action"></param>
    /// <param name="awaitTime"></param>
    public static async void Timer(DateTime timer, float time, bool isActing, Action action, float awaitTime = 1f)
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(awaitTime)); //等1秒
            var during = (timer - DateTime.Now).Seconds; //时长
            if (during >= time && !isActing)//防止Pool正在销毁，事件叠加卡帧
            {
                isActing = true;
                action();
                /** action
                    while (条件)//不用这么多
                    {
                        await _task.Delay(100);//0.01s，大约一帧销毁一个
                    }
                    isActing = false;
                **/
            }
        }
    }
    #endregion



    #region Reflection


    /// <summary>
    /// 获取程序集下的类
    /// 热更DLL
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="classPath"></param>
    /// <returns></returns>
    public static Type Reflection_Class_Get(string typeName)
    {
        return Type.GetType(typeName);
    }

    /// <summary>
    /// 热更DLL
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Type Reflection_Class_Get<T>()
    {
        return typeof(T);
    }

    /// <summary>
    /// 热更DLL当中，可以直接通过Activator来创建实例
    /// </summary>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public static object Reflection_Object_Get(string typeName)
    {
        Type t = Type.GetType(typeName);//或者typeof(TypeName)

        return Activator.CreateInstance(t);
       // return Activator.CreateInstance<TypeName>();//也行
    }

    /// <summary>
    /// 获取有这个类的程序集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Assembly Reflection_Assembly_Get<T>() 
    { 
         return Assembly.GetAssembly(typeof(T));
    }





    /// <summary>
    /// 通过反射调用方法
    /// 在热更DLL当中，通过反射调用方法跟通常C#用法没有任何区别
    /// </summary>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public static void Reflection_Invoke<T>(string methodName)
    {
        Type type = typeof(T);

        object instance = Activator.CreateInstance(type);
        MethodInfo mi = type.GetMethod(methodName);

        mi.Invoke(instance, null);
    }


    #endregion



    #region Resources


    /// <summary>
    /// 加载并且实例了一个预制体
    /// </summary>
    /// <param name="type"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject Resources_LoadPrefab(string path,Transform parent=null)//type=typeof(T)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        if (prefab == null)
        {
            Debug.LogErrorFormat("加载不到预制体，路径：Resources/{0}",path);    
        }
        GameObject go = GameObject.Instantiate(prefab);
        if (parent != null)
        {
            go.SetParent(parent);
        }
        return go;
    }

    public static void DownKeyCode(KeyCode keyCode, UnityAction unityAction)
    {
        if (Input.GetKeyDown(keyCode))
        {
            unityAction();
        }
    }
    #endregion

}



#region 枚举

public enum TrimNameType
{
    None,
    /// <summary>IgnoreLayerCollision/B/C.prefab => IgnoreLayerCollision</summary>
    SlashFirst,
    /// <summary>IgnoreLayerCollision/B/C.prefab => C.prefab</summary>
    SlashAfter,
    /// <summary>IgnoreLayerCollision/B/C.prefab => IgnoreLayerCollision/B</summary>
    SlashPre,
    /// <summary>A_B => B</summary>
    DashAfter,
    /// <summary>A_B => IgnoreLayerCollision</summary>
    DashPre,
    /// <summary>IgnoreLayerCollision/B/C.prefab => C</summary>
    SlashAndPoint,
    /// <summary>C.prefab => C</summary>
    PointPre,
    /// <summary>IgnoreLayerCollision.Void B() => IgnoreLayerCollision</summary>
    PointAfter,
    /// <summary>IgnoreLayerCollision(B) => IgnoreLayerCollision</summary>
    BracketsAfter  ,
    SpacingPre,
    /// <summary>IgnoreLayerCollision.Void B() => B()</summary>
    SpacingAfter,
    /// <summary>IgnoreLayerCollision.Void B() => IgnoreLayerCollision.B()</summary>
    PointPre_SpacingAfter
}


/// <summary>
///网络状态
/// </summary>
public enum NetworkStatusType
{
    NotReachable,
    Traffic,
    Wifi

}

/// <summary>
/// 时间单位 s(秒),ms(毫秒),μs(微秒),ns(纳秒),ps(皮秒) 
/// </summary>
public enum TimeUnit
{
    /// <summary> 秒 </summary>
    Seconds,
    /// <summary> 毫秒 </summary>
    mSeconds,
    /// <summary> 微秒 </summary>
    uSeconds,
    /// <summary> 纳秒 </summary>
    nSeconds,
    /// <summary> 皮秒 </summary>
    pSeconds ,
    /// <summary> 1帧（0.016777.....7s） </summary>
    pFrame
}




#endregion

