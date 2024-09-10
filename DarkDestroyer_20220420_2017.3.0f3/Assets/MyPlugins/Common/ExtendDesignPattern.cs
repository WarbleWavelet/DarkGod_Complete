/****************************************************
    文件：ExtendDesignPattern.cs
	作者：lenovo
    邮箱: 
    日期：2024/6/26 11:59:40
	功能： 设计模式
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static partial class ExtendDesignPattern//中介者 Mediator
{ }
public static partial class ExtendDesignPattern//外观模式  Facade
{ }
public static partial class ExtendDesignPattern
{ 

}
public static partial class ExtendDesignPattern //六原则
{
    //    六大设计模式
    //开闭原则：一个软件实体如类、模块和函数应该对修改封闭，对扩展开放。
    //单一职责原则：一个类只做一件事，一个类应该只有一个引起它修改的原因。
    //里氏替换原则：子类应该可以完全替换父类。也就是说在使用继承时，只扩展新功能，而不要破坏父类原有的功能。
    //依赖倒置原则：细节应该依赖于抽象，抽象不应依赖于细节。把抽象层放在程序设计的高层，并保持稳定，程序的细节变化由低层的实现层来完成。
    //迪米特法则：又名「最少知道原则」，一个类不应知道自己操作的类的细节，换言之，只和朋友谈话，不和朋友的朋友谈话。
    //接口隔离原则：客户端不应依赖它不需要的接口。如果一个接口在实现时，部分方法由于冗余被客户端空实现，则应该将接口拆分，让实现类只需依赖自己需要的接口方法。
}
public static partial class ExtendDesignPattern//工厂
{

    #region 简单工厂  缺点,需要进行修改
    public interface IProduct
    {
        void DoSth();
    }

    public class ProductFirst : IProduct
    {
        public virtual void DoSth()
        {
            Debug.Log("ProductFirst DoSth");
        }
    }
    public class ProductSecond : IProduct
    {
        public virtual void DoSth()
        {
            Debug.Log("ProductFirst DoSth");
        }
    }

    public class SimpleFactory
    {
        public static IProduct Create(int id)
        {
            switch (id)
            {
                case 1:
                    return new ProductFirst();
                    break;
                case 2:
                    return new ProductSecond();
                    break;
                default:
                    return null;
                    break;
            }
        }
    }
    #endregion



    #region 工厂 优点,用实现继承替代修改,将逻辑放在工厂子类;缺点:继承工厂需要额外的代码

    public interface IWalker
    {
        void Walk(Transform target);
    }

    public class LeftWalker : MonoBehaviour, IWalker
    {
        Transform _target;
        public virtual void Walk(Transform target)
        {
            _target = target;
            StartCoroutine(WalkLeft());
        }

        IEnumerator WalkLeft()
        {
            while (true)
            {
                _target.Translate(Vector3.left * Time.deltaTime);
                Debug.Log("WalkLeft " + _target.localPosition);
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public class RightWalker : MonoBehaviour, IWalker
    {
        Transform _target;
        public virtual void Walk(Transform target)
        {
            _target = target;
            StartCoroutine(WalkRight());
        }

        IEnumerator WalkRight()
        {
            while (true)
            {
                _target.Translate(Vector3.right * Time.deltaTime);
                Debug.Log("WalkRight " + _target.localPosition);
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public interface IWalkerFactory
    {
        IWalker Create();
    }

    public class LeftWalkerFactory : IWalkerFactory
    {
        public virtual IWalker Create()
        {
            return new GameObject().AddComponent<LeftWalker>();
        }
    }

    public class RightWalkerFactory : IWalkerFactory
    {
        public virtual IWalker Create()
        {
            return new GameObject().AddComponent<RightWalker>();
        }
    }
    #endregion


    #region 抽象工厂 产品族
    public interface IActorFactory
    {
        IFlyer CreateFlyer(GameObject go);
        IWalker CreateWalker(GameObject go);
    }

    public interface IFlyer
    {
        void Fly(Transform target);
    }

    public class LeftFlyer :Component, IFlyer
    {
        public void Fly(Transform target)
        {
            throw new NotImplementedException();
        }
    }

    public class RightFlyer : Component, IFlyer
    {
        public void Fly(Transform target)
        {
            throw new NotImplementedException();
        }
    }

    public class LeftActorFactory : IActorFactory
    {
        public virtual IFlyer CreateFlyer(GameObject go)
        {
            return go.AddComponent<LeftFlyer>();
        }

        public virtual IWalker CreateWalker(GameObject go)
        {
            return go.AddComponent<LeftWalker>();
        }

    }

    public class RightActorFactory : IActorFactory
    {
        public virtual IFlyer CreateFlyer(GameObject go)
        {
            return go.AddComponent<RightFlyer>();
        }

        public virtual IWalker CreateWalker(GameObject go)
        {
            return go.AddComponent<RightWalker>();
        }

    }
    #endregion

}
public static partial class ExtendDesignPattern//适配器 ,把自己交给适配器.翻译器
{
    #region 示例
    class Test
    {
        void Main()
        {     
            NormalPeople normal=new NormalPeople();
            PeopleCantHear peopleCantHear = new PeopleCantHear();
            Voice2WordApp app=  new Voice2WordApp(peopleCantHear);
            Voice voice0;
            Voice voice1;
            //
            voice0 = normal.Speak();
            voice1 = app.Speak(voice0);
            voice0 = normal.Speak(voice1);
            voice1 = app.Speak(voice0);
            voice0 = normal.Speak(voice1);
            voice1 = app.Speak(voice0);
            voice0 = normal.Speak(voice1);
            voice1 = app.Speak(voice0);
            voice0 = normal.Speak(voice1);
            voice1 = app.Speak(voice0);
            voice0 = normal.Speak(voice1);

            normal.Speak();
            //现在两人都可以Speak  ,解决了PeopleCantHear的耳聋残疾带来的沟通问题
        }

        void Main1()
        {
            NormalPeople normal = new NormalPeople();
            PeopleCantSee peopleCantSee   = new PeopleCantSee();
            Word2VoiceApp app = new Word2VoiceApp(peopleCantSee);
            Word word0;
            Voice voice1;
            //
            word0 = normal.Write();
            voice1 = app.Speak(word0);
            word0 = normal.Write(voice1);
            voice1 = app.Speak(word0);
            word0 = normal.Write(voice1);
            voice1 = app.Speak(word0);
            word0 = normal.Write(voice1);
            voice1 = app.Speak(word0);
            word0 = normal.Write(voice1);
            voice1 = app.Speak(word0);
            word0 = normal.Write(voice1);
            voice1 = app.Speak(word0);
            //现在两人都可以Speak  ,解决了PeopleCantHear的耳聋残疾带来的沟通问题
        }
    }

    #endregion


    #region 沟通单位
    class Voice
    { 
    
    }

    class Word
    {
        Voice _voice;

        public Word(Voice voice)
        {
#if NET_4_7_OR_NEWER
            _voice = voice ?? throw new ArgumentNullException(nameof(voice));

#endif
        }
    }
    #endregion


    #region 能力interface
    interface ICanSpeak
    {
        /// <summary>开头发起说话</summary>
        Voice Speak();
    }
    interface ICanWrite
    {
        /// <summary>开头发起说话</summary>
        Word Write();
    }
    interface ICanWriteAfterVoice
    {
        /// <summary>听到别人的声音后书写</summary>
        Word Write(Voice voice);
    }

    interface ICanSpeakAfterVoice
    {
        /// <summary>听到别人的声音,说话</summary>
        Voice Speak(Voice voice);
    }
    interface ICanSpeakAfterWord
    { 
        /// <summary>看到别人的文字后,说话</summary>
        Voice Speak(Word word);    
    }
    #endregion


    #region 不同能力的人
    class NormalPeople : ICanSpeak,ICanWrite, ICanSpeakAfterVoice, ICanSpeakAfterWord ,ICanWriteAfterVoice
    {
        public Word Write()
        {
            throw new NotImplementedException();
        }

        public Voice Speak(Voice voice)
        {
            throw new NotImplementedException();
        }

        public Voice Speak()
        {
            throw new NotImplementedException();
        }

        public Voice Speak(Word word)
        {
            throw new NotImplementedException();
        }

        public Word Write(Voice voice)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>眼瞎的人</summary>
    class PeopleCantSee : ICanSpeak, ICanSpeakAfterVoice
    {
        public Voice Speak()
        {
            throw new NotImplementedException();
        }

        public Voice Speak(Voice voice)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>耳聋的人</summary>
    class PeopleCantHear : ICanSpeak, ICanSpeakAfterWord
    {
        public Voice Speak(Word word)
        {
            throw new NotImplementedException();
        }

        public Voice Speak()
        {
            throw new NotImplementedException();
        }
    }
    #endregion


    /// <summary>适配器</summary>
    interface IAdaptor
    {

    }


    #region 耳聋

    /// <summary>语音转文字的功能</summary>
    interface IVoice2WordFunction
    {
        Word  Voice2Word(Voice voice);
    }


    class Voice2WordApp : IAdaptor, IVoice2WordFunction,ICanSpeakAfterVoice
    {
        PeopleCantHear _peopleCantHear;
        Voice _voice;
        Word _word;

        public Voice2WordApp(PeopleCantHear peopleCantHear)
        {
            _peopleCantHear = peopleCantHear;
        }

        public Voice Speak(Voice voice)
        {
            _voice = voice;
            _word = Voice2Word(_voice);
            return _peopleCantHear.Speak(_word);
      }

        public Word Voice2Word(Voice voice)
        {
            throw new NotImplementedException();
        }
    }


    #endregion

    #region 眼瞎
    /// <summary>语音转文字的功能</summary>
    interface IWord2VoiceFunction
    {
        Voice Word2Voice(Word word);
    }


    /// <summary>眼瞎辅助软件</summary>
    class Word2VoiceApp :IAdaptor, IWord2VoiceFunction, ICanSpeakAfterWord
    {
        PeopleCantSee _peopleCantSee;
        Voice _voice;
        Word _word;

        public Word2VoiceApp(PeopleCantSee peopleCantSee)
        {
            _peopleCantSee = peopleCantSee;
        }

        public Voice Speak(Word word)
        {
            _word = word;
            _voice = Word2Voice(_word);
            return  _peopleCantSee.Speak(_voice);
        }

        public Voice Word2Voice(Word word)
        {
            throw new NotImplementedException();
        }
    }
    #endregion



}


#region 单例模式
public static partial class ExtendDesignPattern//    单例模式
{
    #region 文字描述
    //Instance通常指的是单例对象的实例，‌即整个应用程序中唯一的一个实例。
    //‌Singleton则通常指的是实现单例模式的类名
    //意思是类名用‌Singleton,实例对象后用Instance
    //
    //单例模式实现方式 优点	缺点	结论
    //饿汉式（静态变量）	写法简单，避免了同步问题，适用于单线程	没有懒加载，可能造成内存浪费	可用，适用于单线程
    //饿汉式（静态代码块）	写法简单，避免了同步问题，适用于单线程	没有懒加载，可能造成内存浪费	可用，适用于单线程
    //懒汉式（线程不安全）	实现了懒加载	线程不安全	不可用
    //懒汉式（线程安全，同步方法）	实现了懒加载，解决了线程安全问题	效率低	不可用
    //懒汉式（线程安全，同步代码块）	提升了效率	线程不同步	不可用
    //双重检查	实现了懒加载，解决了线程安全问题，效率高	无法防止利用反射来重复构建对象	推荐使用
    //静态内部类	避免了线程不安全，效率高，实现了懒加载	无法防止利用反射来重复构建对象	推荐使用
    //枚举	避免了线程同步问题，还能防止反序列化需要重新创建新的对象	没有实现懒加载	推荐使用
    //
    //1）饿汉式；2）懒汉式；3）双重检查锁定（Double-Checked Locking）；4）静态构造函数；5）使用.NET 4 的Lazy 类型
    #endregion


    #region Summary的cref来源
    const string SingletonHungry_Good = "";
    const string Singleton_PriveteConstruct = "私有的构造函数，防止用new关键字声明对象";
    #endregion
}

#region 非泛型(不能实际用(谁想重复写)) 推出来的 泛型
                                                  



#region 饿汉式
    /// <summary>
    /// 优点：实例在类加载的时候就已经创建好了，并且线程安全。
    /// 缺点：实例在类加载的时候就已经创建好了，如果不使用造成资源浪费。
    /// </summary>
     sealed class SingletonHungry_NonGeneric
{
    /// <summary><see cref=Singleton_PriveteConstruct></summary>
    private SingletonHungry_NonGeneric() { }
    private static SingletonHungry_NonGeneric _instance = new SingletonHungry_NonGeneric();
    public static SingletonHungry_NonGeneric Instance
    {
        get
        {
            return _instance;
        }
    }
}

public abstract class SingletonHungry<T> where T : new()
{
    /// <summary><see cref=Singleton_PriveteConstruct></summary>
    private SingletonHungry() { }
    private static T _instance = new T();
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }
}
# endregion

#region 懒汉式 
/// <summary>
/// 优点：实例在需要的时候才会创建，不会造成内存浪费。
/// 缺点：多个线程同时访问获取实例时，可能会创建多个实例，这不是线程安全的。
/// </summary>
 sealed class SingletonLazy_NonGeneric
{
    /// <summary><see cref=Singleton_PriveteConstruct></summary>
    private SingletonLazy_NonGeneric() { }
    private static SingletonLazy_NonGeneric _instance = null;
    public static SingletonLazy_NonGeneric Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SingletonLazy_NonGeneric();
            }
            return _instance;
        }
    }
}

public abstract class SingletonLazy<T> where T : new()
{
    /// <summary><see cref=Singleton_PriveteConstruct></summary>
    private SingletonLazy() { }
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }
}
#endregion

#region 双重检查锁定（Double-Checked Locking
/// <summary>
/// 优点：可以保证线程安全，并且实例在需要时才会创建，不会造成内存浪费。
/// 缺点：由于使用了锁，会引入额外的性能开销。
/// </summary>
 sealed class SingletonDoubleLock_NonGeneric
{
    /// <summary><see cref=Singleton_PriveteConstruct></summary>
    private SingletonDoubleLock_NonGeneric() { }
    /// <summary>加锁因子</summary>
    private readonly static object _lock = new object();
    private static SingletonDoubleLock_NonGeneric _instance = null;
    public static SingletonDoubleLock_NonGeneric Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SingletonDoubleLock_NonGeneric();
                    }
                }
            }
            return _instance;
        }
    }
}

public abstract class SingletonDoubleCheckedLocking<T> where T : new()
{
    /// <summary><see cref=Singleton_PriveteConstruct></summary>
    private SingletonDoubleCheckedLocking() { }
    /// <summary>加锁因子</summary>
    private readonly static object _lock = new object();
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
            }
            return _instance;
        }
    }
}
#endregion

#region  静态构造函数
/// <summary>
/// 优点：不是提前创建对象，节省资源，线程安全。
/// 缺点：完全懒惰的，在执行Instance调用时，才会执行嵌套类的创建单例语句
/// </summary>
 sealed class SingletonStaticConstruct_NonGeneric
{
    /// <summary><see cref=Singleton_PriveteConstruct></summary>
    private SingletonStaticConstruct_NonGeneric() { }
    private static class Inner
    {
        internal static SingletonStaticConstruct_NonGeneric _instance = new SingletonStaticConstruct_NonGeneric();
    }
    public static SingletonStaticConstruct_NonGeneric Instance
    {
        get
        {
            return Inner._instance;
        }
    }


}

public abstract class SingletonStaticConstruct<T> where T : new()
{
    /// <summary><see cref=Singleton_PriveteConstruct></summary>
    private SingletonStaticConstruct() { }
    private static class Inner
    {
        internal static T _instance = new T();
    }
    public static T Instance
    {
        get
        {
            return Inner._instance;
        }
    }


}
#endregion

#region 使用.NET 4 的Lazy 类型
/// <summary>
///  优点：线程安全的单例，同时他的性能非常好。
///  缺点：这里使用.NET4 或者更高版本,可以使用System.Lazy 这个类型声明懒惰的
/// </summary>
 sealed class SingletonLazyNET4_NonGeneric
{
    /// <summary><see cref=Singleton_PriveteConstruct></summary>
    private SingletonLazyNET4_NonGeneric() { }

    private static readonly Lazy<SingletonLazyNET4_NonGeneric> _instance = new Lazy<SingletonLazyNET4_NonGeneric>(() =>
    {
        return new SingletonLazyNET4_NonGeneric();
    });

    public static SingletonLazyNET4_NonGeneric Instance
    {
        get { return _instance.Value; }
    }
}
#endregion

#endregion



#region 泛型 见名知意


/// <summary>找GetConstructors</summary>
public abstract class Singleton<T> where T : Singleton<T>
{
    private static T mInstance;

    public static T Instance
    {
        get
        {
            if (mInstance == null)
            {
                var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

                if (ctor == null)
                    throw new Exception("Non-public ctor() not found!");

                mInstance = ctor.Invoke(null) as T;
            }

            return mInstance;
        }
    }

    protected Singleton()
    {

    }
}

/// <summary>只排除不是MonoBehaviour
///  懒汉借一个判断
/// </summary> 
public abstract class SingletonLazyNotMono<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)//不存在就是实例
            {
                var t = new T();
                if (t is MonoBehaviour)
                {
                    Debug.LogError("Mono类请使用MonoSingleton");
                    return null;
                }

                _instance = t;
            }

            return _instance;
        }
    }
}



/// <summary>
/// 为空就会New()
///  也就是懒汉模式
/// </summary> 
public abstract class SingletonWillNew<T> where T : new()
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var t = new T();
                _instance = t;
            }

            return _instance;
        }
    }
}
#endregion




#region Mono

#region MonoSingleton
/// <summary>
/// 01 与普通单例相比，new时是先new GameObject，然后AddComponent
/// <br />  02 与MonoSingletonSimple相比，用GameObject.Find检查保证唯一性
/// </summary>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T mInstance = null;

    public static T Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<T>();

                if (FindObjectsOfType<T>().Length > 1)
                {
                    Debug.LogWarning("More than 1");
                    return mInstance;
                }

                if (mInstance == null)
                {
                    var instanceName = typeof(T).Name;
                    Debug.LogFormat("Instance Name: {0}", instanceName);
                    var instanceObj = GameObject.Find(instanceName);

                    if (!instanceObj)
                        instanceObj = new GameObject(instanceName);

                    mInstance = instanceObj.AddComponent<T>();
                    DontDestroyOnLoad(instanceObj); //保证实例不会被释放

                    Debug.LogFormat("Add New Singleton {0} in Game!", instanceName);
                }
                else
                {
                    Debug.LogFormat("Already exist: {0}", mInstance.name);
                }
            }

            return mInstance;
        }
    }

    protected virtual void OnDestroy()
    {
        mInstance = null;
    }
}
#endregion


#region MonoSingletonSimple



/// <summary>
/// 可能是想用MonoBehaviour的方法
/// <br/>与MonoSingleton相比，没有用GameObject.Find检查保证唯一性
/// </summary>
public class MonoSingletonGetOrAdd<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _single;

    public static T Single
    {
        get
        {
            if (_single == null)
            {
                var go = new GameObject(typeof(T).Name);
                DontDestroyOnLoad(go);
                _single = go.AddComponent<T>();
            }

            return _single;
        }
    }

    protected virtual void OnDestroy()
    {
        _single = null;
    }
}

#endregion


#region MonoSingletonGet
public class MonoSingletonGet<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public static T Instance => instance;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            instance = GetComponent<T>();
        }
        else
        {
            Debug.LogError("Something went wrong.  There should never be more than one instance of " + typeof(T));
        }
    }

}
#endregion


#endregion

#region 辅助


/// <summary>
/// 通过Lazy
/// 访问实例才会创建（）
///  创建的线程安全（防止两个发起者同时创建）
/// </summary>
public class SingletonByLazy
{
    //01 私有构造，防止被new
    private SingletonByLazy() { }
    public static readonly Lazy<SingletonByLazy> _instance = new Lazy<SingletonByLazy>(
        () => new SingletonByLazy());

    public static SingletonByLazy Instance
    {
        get { return _instance.Value; }
    }

}
#endregion

#endregion










