/****************************************************

	文件：
	作者：WWS
	日期：2022/10/31 15:25:09
	功能：追要对Unity的Componetn组件的拓展方法(this大法)


*****************************************************/

//using Codice.Client.BaseCommands.Merge;

using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
//using Unity.Plastic.Antlr3.Runtime;


public static partial class ExtendReflection
{
    public static string Method2String(this object o)
    {
      return  CommonClass.Log_ClassFunction(1);
    }
}

public static partial class ExtendReflection
{
   // Conditional：起条件编译的作用，只有满足条件，才允许编译器对它的代码进行编译。一般在程序调试的时候使用。
   //DllImport：用来标记非.NET的函数，表明该方法在一个外部的DLL中定义。
   //Obsolete：这个属性用来标记当前的方法已经被废弃，不再使用了。
}
public static partial class ExtendReflection    //CustomAttributeExtensions
{

    public static void Example_CustomAttributeExtensions( )
    {
        //var customerInfoName = typeof(CustomerInfo).GetCustomAttributeValue<NameAttribute, string>(x => x.Name);
        //var customerAddressName = typeof(CustomerInfo).GetCustomAttributeValue<NameAttribute, string>(x => x.Name, "Address");
        //var customerInfoDesc = typeof(CustomerInfo).GetCustomAttributeValue<DescriptionAttribute, string>(x => x.Description);
        //Console.WriteLine("CustomerInfo Name:" + customerInfoName);
        //Console.WriteLine("customerInfo >Address Name:" + customerAddressName);
        //Console.WriteLine("customerInfo Desc:" + customerInfoDesc);
    }
    /// <summary>
    /// Cache Data
    /// </summary>
    private static readonly ConcurrentDictionary<string, object> Cache = new ConcurrentDictionary<string, object>();
    /// <summary>
    /// 获取CustomAttribute Value
    /// </summary>
    /// <typeparam name="TAttribute">Attribute的子类型</typeparam>
    /// <typeparam name="TReturn">TReturn的子类型</typeparam>
    /// <param name="sourceType">头部标有CustomAttribute类的类型</param>
    /// <param name="attributeValueAction">取Attribute具体哪个属性值的匿名函数</param>
    /// <returns>返回Attribute的值，没有则返回null</returns>
    public static TReturn GetCustomAttributeValue<TAttribute, TReturn>(this Type sourceType, Func<TAttribute, TReturn> attributeValueAction) where TAttribute : Attribute
    {
        return _getAttributeValue(sourceType, attributeValueAction, null);
    }
    /// <summary>
    /// 获取CustomAttribute Value
    /// </summary>
    /// <typeparam name="TAttribute">Attribute的子类型</typeparam>
    /// <typeparam name="TReturn">TReturn的子类型</typeparam>
    /// <param name="sourceType">头部标有CustomAttribute类的类型</param>
    /// <param name="attributeValueAction">取Attribute具体哪个属性值的匿名函数</param>
    /// <param name="propertyName">field name或property name</param>
    /// <returns>返回Attribute的值，没有则返回null</returns>
    public static TReturn GetCustomAttributeValue<TAttribute, TReturn>(this Type sourceType, Func<TAttribute, TReturn> attributeValueAction, string propertyName) where TAttribute : Attribute
    {
        return _getAttributeValue(sourceType, attributeValueAction, propertyName);
    }
    #region pri
    private static TReturn _getAttributeValue<TAttribute, TReturn>(Type sourceType, Func<TAttribute, TReturn> attributeFunc, string propertyName) where TAttribute : Attribute
    {
        var cacheKey = BuildKey<TAttribute>(sourceType, propertyName);
        var value = Cache.GetOrAdd(cacheKey, k => GetValue(sourceType, attributeFunc, propertyName));
        if (value is TReturn) return (TReturn)Cache[cacheKey];
        return default(TReturn);
    }


    private static string BuildKey<TAttribute>(Type type, string propertyName) where TAttribute : Attribute
    {
        var attributeName = typeof(TAttribute).FullName;
        if (string.IsNullOrEmpty(propertyName))
        {
            return type.FullName + "." + attributeName;
        }
        return type.FullName + "." + propertyName + "." + attributeName;
    }
    private static TReturn GetValue<TAttribute, TReturn>(this Type type, Func<TAttribute, TReturn> attributeValueAction, string name)
        where TAttribute : Attribute
    {
        TAttribute attribute = default(TAttribute);
        if (string.IsNullOrEmpty(name))
        {
            attribute = type.GetCustomAttribute<TAttribute>(false);
        }
        else
        {
            var propertyInfo = type.GetProperty(name);
            if (propertyInfo != null)
            {
                attribute = propertyInfo.GetCustomAttribute<TAttribute>(false);
            }
            else
            {
                var fieldInfo = type.GetField(name);
                if (fieldInfo != null)
                {
                    attribute = fieldInfo.GetCustomAttribute<TAttribute>(false);
                }
            }
        }
        return attribute == null ? default(TReturn) : attributeValueAction(attribute);
    }

    #endregion  
}
public static partial class ExtendReflection
{
    public static void Example()
    {
       typeof(ExtendReflection).LogInfo();
    }


    /// <summary>
    /// 得到在类前面用以下的方式用Type的所有类，达到预制体路径与脚本的绑定
    /// <para />[BindPrefab(DefinePath_AirCombat.PREFAB_DIALOG,Constants_AirCombat.BIND_PREFAB_PRIORITY_VIEW)]
    /// </summary>
    public static Type[] GetTypes(this Type type)//type=typeof(T)
    {
        Assembly assembly = Assembly.GetAssembly(type);
        Type[] types = assembly.GetExportedTypes();
        return types;
    }

    public static Type[] GetTypes<T>()//type=typeof(T)
    {
        Type type = typeof(T);
        Assembly assembly = Assembly.GetAssembly(type);
        Type[] types = assembly.GetExportedTypes();
        return types;
    }
}

public class Manager
{
    /// <summary>
    /// 编辑器默认的程序集Assembly-CSharp.dll
    /// </summary>
    private static Assembly defaultCSharpAssembly;

#if UNITY_ANDROID
    /// <summary>
    /// 程序集缓存
    /// </summary>
    private static Dictionary<string, Assembly> assemblyCache = new Dictionary<string, Assembly>();
#endif

    /// <summary>
    /// 获取编辑器默认的程序集Assembly-CSharp.dll
    /// </summary>
    public static Assembly DefaultCSharpAssembly
    {
        get
        {
            //如果已经找到，直接返回
            if (defaultCSharpAssembly != null)
                return defaultCSharpAssembly;

            //从当前加载的程序包中寻找，如果找到，则直接记录并返回
            var assems = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assem in assems)
            {
                //所有本地代码都编译到Assembly-CSharp中
                if (assem.GetName().Name == "Assembly-CSharp")
                {
                    //保存到列表并返回
                    defaultCSharpAssembly = assem;
                    break;
                }
            }

            return defaultCSharpAssembly;
        }
    }

    /// <summary>
    /// 获取Assembly
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Assembly GetAssembly(string name)
    {
#if UNITY_ANDROID
        if (!assemblyCache.ContainsKey(name))
            return null;

        return assemblyCache[name];
#else
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {

            if (assembly.GetName().Name == name)
            {
                return assembly;
            }
        }

        return null;
#endif
    }



    /// <summary>
    /// 获取默认的程序集
    /// </summary>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public static Type GetDefaultAssemblyType(string typeName)
    {
        return DefaultCSharpAssembly.GetType(typeName);
    }


    public static Type[] GetTypeList(string assemblyName)
    {
        return GetAssembly(assemblyName).GetTypes();
    }
}



public static partial class ExtendReflection//_Common
{

    /// <summary>命名空间.类名</summary>
    public static  string Example_Common()
    {
        Assembly assembly = ExtendReflection.GetCommon();
        Type selfType = assembly.GetType("Common.ExtendReflection");
        selfType.LogInfo();
        return selfType.ToString();
    }

    /// <summary>CommonClass,
    /// <para/> UnityEngine.</summary> 
    public static  Assembly GetCommon()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in assemblies)
            {
                if (a.FullName.StartsWith("Common,"))
                {
                    return a;
                }
            }

            Log.E(">>>>>>>Error: Can\'t find Common.dll");
            return null;
        }

    }


public static partial class ExtendReflection
{


    public static void Example_QFramework()//QF的
    {
        var selfType = ExtendReflection.GetAssemblyCSharp().GetType("QFramework.ReflectionExtension");
        selfType.LogInfo();
    }
    public static Assembly GetAssemblyCSharp()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var a in assemblies)
        {
            if (a.FullName.StartsWith("Assembly-CSharp,"))
            {
                return a;
            }
        }

        Log.E(">>>>>>>Error: Can\'t find Assembly-CSharp.dll");
        return null;
    }

    public static Assembly GetAssemblyCSharpEditor()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var a in assemblies)
        {
            if (a.FullName.StartsWith("Assembly-CSharp-Editor,"))
            {
                return a;
            }
        }

        Log.E(">>>>>>>Error: Can\'t find Assembly-CSharp-Editor.dll");
        return null;
    }

    /// <summary>
    /// 通过反射方式调用函数
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="methodName">方法名</param>
    /// <param name="args">参数</param>
    /// <returns></returns>
    public static object InvokeByReflect(this object obj, string methodName, params object[] args)
    {
        var methodInfo = obj.GetType().GetMethod(methodName);
        return methodInfo == null ? null : methodInfo.Invoke(obj, args);
    }

    /// <summary>
    /// 通过反射方式获取域值
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="fieldName">域名</param>
    /// <returns></returns>
    public static object GetFieldByReflect(this object obj, string fieldName)
    {
        var fieldInfo = obj.GetType().GetField(fieldName);
        return fieldInfo == null ? null : fieldInfo.GetValue(obj);
    }

    /// <summary>
    /// 通过反射方式获取属性
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="fieldName">属性名</param>
    /// <returns></returns>
    public static object GetPropertyByReflect(this object obj, string propertyName, object[] index = null)
    {
        var propertyInfo = obj.GetType().GetProperty(propertyName);
        return propertyInfo == null ? null : propertyInfo.GetValue(obj, index);
    }

    /// <summary>
    /// 拥有特性
    /// </summary>
    /// <returns></returns>
    public static bool HasAttribute(this PropertyInfo prop, Type attributeType, bool inherit)
    {
        return prop.GetCustomAttributes(attributeType, inherit).Length > 0;
    }

    /// <summary>
    /// 拥有特性
    /// </summary>
    /// <returns></returns>
    public static bool HasAttribute(this FieldInfo field, Type attributeType, bool inherit)
    {
        return field.GetCustomAttributes(attributeType, inherit).Length > 0;
    }

    /// <summary>
    /// 拥有特性
    /// </summary>
    /// <returns></returns>
    public static bool HasAttribute(this Type type, Type attributeType, bool inherit)
    {
        return type.GetCustomAttributes(attributeType, inherit).Length > 0;
    }

    /// <summary>
    /// 拥有特性
    /// </summary>
    /// <returns></returns>
    public static bool HasAttribute(this MethodInfo method, Type attributeType, bool inherit)
    {
        return method.GetCustomAttributes(attributeType, inherit).Length > 0;
    }


    /// <summary>
    /// 获取第一个特性
    /// </summary>
    public static T GetFirstAttribute<T>(this MethodInfo method, bool inherit) where T : Attribute
    {
        var attrs = (T[])method.GetCustomAttributes(typeof(T), inherit);
        if (attrs != null && attrs.Length > 0)
            return attrs[0];
        return null;
    }

    /// <summary>
    /// 获取第一个特性
    /// </summary>
    public static T GetFirstAttribute<T>(this FieldInfo field, bool inherit) where T : Attribute
    {
        var attrs = (T[])field.GetCustomAttributes(typeof(T), inherit);
        if (attrs != null && attrs.Length > 0)
            return attrs[0];
        return null;
    }

    /// <summary>
    /// 获取第一个特性
    /// </summary>
    public static T GetFirstAttribute<T>(this PropertyInfo prop, bool inherit) where T : Attribute
    {
        var attrs = (T[])prop.GetCustomAttributes(typeof(T), inherit);
        if (attrs != null && attrs.Length > 0)
            return attrs[0];
        return null;
    }

    /// <summary>
    /// 获取第一个特性
    /// </summary>
    public static T GetFirstAttribute<T>(this Type type, bool inherit) where T : Attribute
    {
        var attrs = (T[])type.GetCustomAttributes(typeof(T), inherit);
        if (attrs != null && attrs.Length > 0)
            return attrs[0];
        return null;
    }
}
