/****************************************************
    文件：ExtendOthers.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/24 20:54:31
	功能：QF的
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendOthers
{
}


public static class FuncOrActionOrEventExtension
{
    private delegate void TestDelegate();

    public static void Example()
    {
        // action
        System.Action action = () => Log.I("action called");
        action.InvokeGracefully(); // if (action != null) action();

        // func
        Func<int> func = () => 1;
        func.InvokeGracefully();

        /*
        public static T InvokeGracefully<T>(this Func<T> selfFunc)
        {
            return null != selfFunc ? selfFunc() : default(T);
        }
        */

        // delegate
        TestDelegate testDelegate = () => { };
        testDelegate.InvokeGracefully();
    }


    #region Func Extension

    public static T InvokeGracefully<T>(this Func<T> selfFunc)
    {
        return null != selfFunc ? selfFunc() : default(T);
    }

    #endregion

    #region Action

    /// <summary>
    /// Call action
    /// 优雅地回调
    /// </summary>
    /// <param name="selfAction"></param>
    /// <returns> call succeed</returns>
    public static bool InvokeGracefully(this System.Action selfAction)
    {
        if (null != selfAction)
        {
            selfAction();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Call action
    /// </summary>
    /// <param name="selfAction"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool InvokeGracefully<T>(this Action<T> selfAction, T t)
    {
        if (null != selfAction)
        {
            selfAction(t);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Call action
    /// </summary>
    /// <param name="selfAction"></param>
    /// <returns> call succeed</returns>
    public static bool InvokeGracefully<T, K>(this Action<T, K> selfAction, T t, K k)
    {
        if (null != selfAction)
        {
            selfAction(t, k);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Call delegate
    /// </summary>
    /// <param name="selfAction"></param>
    /// <returns> call suceed </returns>
    public static bool InvokeGracefully(this Delegate selfAction, params object[] args)
    {
        if (null != selfAction)
        {
            selfAction.DynamicInvoke(args);
            return true;
        }
        return false;
    }

    #endregion
}

public static class GenericExtention
{
    public static void Example()
    {
        var typeName = GenericExtention.GetTypeName<string>();
        typeName.LogInfo();
    }

    public static string GetTypeName<T>()
    {
        return typeof(T).ToString();
    }
}

public static class IEnumerableExtension
{
    public static void Example()
    {
        // Array
        var testArray = new int[] { 1, 2, 3 };
        testArray.ForEach(number => number.LogInfo());

        // IEnumerable<T>
        IEnumerable<int> testIenumerable = new List<int> { 1, 2, 3 };
        testIenumerable.ForEach(number => number.LogInfo());
        new Dictionary<string, string>()
            .ForEach(keyValue => Log.I("key:{0},value:{1}", keyValue.Key, keyValue.Value));

        // testList
        var testList = new List<int> { 1, 2, 3 };
        testList.ForEach(number => number.LogInfo());
        testList.ForEachReverse(number => number.LogInfo());

        // merge
        var dictionary1 = new Dictionary<string, string> { { "1", "2" } };
        var dictionary2 = new Dictionary<string, string> { { "3", "4" } };
        var dictionary3 = dictionary1.Merge(dictionary2);
        dictionary3.ForEach(pair => Log.I("{0}:{1}", pair.Key, pair.Value));
    }

    #region Array Extension

    /// <summary>
    /// Fors the each.
    /// </summary>
    /// <returns>The each.</returns>
    /// <param name="selfArray">Self array.</param>
    /// <param name="action">Action.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T[] ForEach<T>(this T[] selfArray, Action<T> action)
    {
        if (selfArray == null || selfArray.Length == 0) //这时我加上去的
        {

            Debug.LogError("ForEach报错");
            return selfArray;
        }
        else
        { 
            Array.ForEach(selfArray, action);
        }
   
        return selfArray;
    }



    #region ForEach<T>

    #endregion  


    /// <summary>
    /// Fors the each.
    /// </summary>
    /// <returns>The each.</returns>
    /// <param name="selfArray">Self array.</param>
    /// <param name="action">Action.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> selfArray, Action<T> action)
    {
        if (action == null) throw new ArgumentException();
        foreach (var item in selfArray)
        {
            action(item);
        }

        return selfArray;
    }

    #endregion

    #region List Extension

    /// <summary>
    /// Fors the each reverse.
    /// </summary>
    /// <returns>The each reverse.</returns>
    /// <param name="selfList">Self list.</param>
    /// <param name="action">Action.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static List<T> ForEachReverse<T>(this List<T> selfList, Action<T> action)
    {
        if (action == null) throw new ArgumentException();

        for (var i = selfList.Count - 1; i >= 0; --i)
            action(selfList[i]);

        return selfList;
    }

    /// <summary>
    /// Fors the each reverse.
    /// </summary>
    /// <returns>The each reverse.</returns>
    /// <param name="selfList">Self list.</param>
    /// <param name="action">Action.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static List<T> ForEachReverse<T>(this List<T> selfList, Action<T, int> action)
    {
        if (action == null) throw new ArgumentException();

        for (var i = selfList.Count - 1; i >= 0; --i)
            action(selfList[i], i);

        return selfList;
    }

    /// <summary>
    /// 遍历列表
    /// </summary>
    /// <typeparam name="T">列表类型</typeparam>
    /// <param name="list">目标表</param>
    /// <param name="action">行为</param>
    public static void ForEach<T>(this List<T> list, Action<int, T> action)
    {
        for (var i = 0; i < list.Count; i++)
        {
            action(i, list[i]);
        }
    }

    /// <summary>
    /// 拷贝到
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    public static void CopyTo<T>(this List<T> from, List<T> to, int begin = 0, int end = -1)
    {
        if (begin < 0)
        {
            begin = 0;
        }

        var endIndex = Math.Min(from.Count, to.Count) - 1;

        if (end != -1 && end < endIndex)
        {
            endIndex = end;
        }

        for (var i = begin; i < end; i++)
        {
            to[i] = from[i];
        }
    }

    /// <summary>
    /// 将List转为Array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="selfList"></param>
    /// <returns></returns>
    public static T[] ToArraySavely<T>(this List<T> selfList)
    {
        var res = new T[selfList.Count];

        for (var i = 0; i < selfList.Count; i++)
        {
            res[i] = selfList[i];
        }

        return res;
    }

    /// <summary>
    /// 尝试获取，如果没有该数则返回null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="selfList"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static T TryGet<T>(this List<T> selfList, int index)
    {
        return selfList.Count > index ? selfList[index] : default(T);
    }

    #endregion

    #region Dictionary Extension

    /// <summary>
    /// Merge the specified dictionary and dictionaries.
    /// </summary>
    /// <returns>The merge.</returns>
    /// <param name="dictionary">Dictionary.</param>
    /// <param name="dictionaries">Dictionaries.</param>
    /// <typeparam name="TKey">The 1st type parameter.</typeparam>
    /// <typeparam name="TValue">The 2nd type parameter.</typeparam>
    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
        params Dictionary<TKey, TValue>[] dictionaries)
    {
        return dictionaries.Aggregate(dictionary,
            (current, dict) => current.Union(dict).ToDictionary(kv => kv.Key, kv => kv.Value));
    }

    /// <summary>
    /// 遍历
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="dict"></param>
    /// <param name="action"></param>
    public static void ForEach<K, V>(this Dictionary<K, V> dict, Action<K, V> action)
    {
        var dictE = dict.GetEnumerator();

        while (dictE.MoveNext())
        {
            var current = dictE.Current;
            action(current.Key, current.Value);
        }

        dictE.Dispose();
    }

    /// <summary>
    /// 向其中添加新的词典
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="dict"></param>
    /// <param name="addInDict"></param>
    /// <param name="isOverride"></param>
    public static void AddRange<K, V>(this Dictionary<K, V> dict, Dictionary<K, V> addInDict,
        bool isOverride = false)
    {
        var dictE = addInDict.GetEnumerator();

        while (dictE.MoveNext())
        {
            var current = dictE.Current;
            if (dict.ContainsKey(current.Key))
            {
                if (isOverride)
                    dict[current.Key] = current.Value;
                continue;
            }

            dict.Add(current.Key, current.Value);
        }

        dictE.Dispose();
    }

    #endregion
}



public static class OOPExtension
{
    interface ExampleInterface
    {

    }

    public static void Example()
    {
        if (typeof(OOPExtension).ImplementsInterface<ExampleInterface>())
        {
        }

        if (new object().ImplementsInterface<ExampleInterface>())
        {
        }
    }


    /// <summary>
    /// Determines whether the type implements the specified interface
    /// and is not an interface itself.
    /// </summary>
    /// <returns><n>true</n>, if interface was implementsed, <n>false</n> otherwise.</returns>
    /// <param name="type">Type.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static bool ImplementsInterface<T>(this Type type)
    {
        return !type.IsInterface && type.GetInterfaces().Contains(typeof(T));
    }

    /// <summary>
    /// Determines whether the type implements the specified interface
    /// and is not an interface itself.
    /// </summary>
    /// <returns><n>true</n>, if interface was implementsed, <n>false</n> otherwise.</returns>
    /// <param name="type">Type.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static bool ImplementsInterface<T>(this object obj)
    {
        var type = obj.GetType();
        return !type.IsInterface && type.GetInterfaces().Contains(typeof(T));
    }
}



public static class TypeExtension
{
    /// <summary>
    /// 获取默认值
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    public static object DefaultForType(this Type targetType)
    {
        return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
    }
}

/// <summary>
/// 通过编写方法并且添加属性可以做到转换至String 如：
/// 
/// <example>
/// [ToString]
/// public static string ConvertToString(TestObj obj)
/// </example>
///
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class ToString : Attribute { }

/// <summary>
/// 通过编写方法并且添加属性可以做到转换至String 如：
/// 
/// <example>
/// [FromString]
/// public static TestObj ConvertFromString(string str)
/// </example>
///
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class FromString : Attribute { }

public static class StringExtention
{
    public static void Example()
    {
        var emptyStr = string.Empty;
        emptyStr.IsNotNullAndEmpty().LogInfo();
        emptyStr.IsNullOrEmpty().LogInfo();
        emptyStr = emptyStr.Append("appended").Append("1").ToString();
        emptyStr.LogInfo();
        emptyStr.IsNullOrEmpty().LogInfo();
    }

    /// <summary>
    /// Check Whether string is null or empty
    /// </summary>
    /// <param name="selfStr"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string selfStr)
    {
        return string.IsNullOrEmpty(selfStr);
    }

    /// <summary>
    /// Check Whether string is null or empty
    /// </summary>
    /// <param name="selfStr"></param>
    /// <returns></returns>
    public static bool IsNotNullAndEmpty(this string selfStr)
    {
        return !string.IsNullOrEmpty(selfStr);
    }

    /// <summary>
    /// Check Whether string trim is null or empty
    /// </summary>
    /// <param name="selfStr"></param>
    /// <returns></returns>
    public static bool IsTrimNotNullAndEmpty(this string selfStr)
    {
        return !string.IsNullOrEmpty(selfStr.Trim());
    }

    /// <summary>
    /// 避免每次都用.
    /// </summary>
    private static readonly char[] mCachedSplitCharArray = { '.' };

    public static string[] Split(this string selfStr, char splitSymbol)
    {
        mCachedSplitCharArray[0] = splitSymbol;
        return selfStr.Split(mCachedSplitCharArray);
    }

    public static string UppercaseFirst(this string str)
    {
        return char.ToUpper(str[0]) + str.Substring(1);
    }

    public static string LowercaseFirst(this string str)
    {
        return char.ToLower(str[0]) + str.Substring(1);
    }

    public static string ToUnixLineEndings(this string str)
    {
        return str.Replace("\r\n", "\n").Replace("\r", "\n");
    }

    public static string ToCSV(this string[] values)
    {
        return string.Join(", ", values
            .Where(value => !string.IsNullOrEmpty(value))
            .Select(value => value.Trim())
            .ToArray()
        );
    }

    public static string[] ArrayFromCSV(this string values)
    {
        return values
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(value => value.Trim())
            .ToArray();
    }

    public static string ToSpacedCamelCase(this string text)
    {
        var sb = new StringBuilder(text.Length * 2);
        sb.Append(char.ToUpper(text[0]));
        for (var i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
            {
                sb.Append(' ');
            }

            sb.Append(text[i]);
        }

        return sb.ToString();
    }

    /// <summary>
    /// 有点不安全,编译器不会帮你排查错误。
    /// </summary>
    /// <param name="selfStr"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string FillFormat(this string selfStr, params object[] args)
    {
        return string.Format(selfStr, args);
    }

    public static StringBuilder Append(this string selfStr, string toAppend)
    {
        return new StringBuilder(selfStr).Append(toAppend);
    }

    public static string AddPrefix(this string selfStr, string toPrefix)
    {
        return new StringBuilder(toPrefix).Append(selfStr).ToString();
    }

    public static StringBuilder AppendFormat(this string selfStr, string toAppend, params object[] args)
    {
        return new StringBuilder(selfStr).AppendFormat(toAppend, args);
    }

    public static string LastWord(this string selfUrl)
    {
        return selfUrl.Split('/').Last();
    }

    public static int ToInt(this string selfStr, int defaulValue = 0)
    {
        var retValue = defaulValue;
        return int.TryParse(selfStr, out retValue) ? retValue : defaulValue;
    }

    public static DateTime ToDateTime(this string selfStr, DateTime defaultValue = default(DateTime))
    {
        var retValue = defaultValue;
        return DateTime.TryParse(selfStr, out retValue) ? retValue : defaultValue;
    }


    public static float ToFloat(this string selfStr, float defaulValue = 0)
    {
        var retValue = defaulValue;
        return float.TryParse(selfStr, out retValue) ? retValue : defaulValue;
    }

    /// <summary>
    /// 是否存在中文字符
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool HasChinese(this string input)
    {
        return Regex.IsMatch(input, @"[\u4e00-\u9fa5]");
    }

    /// <summary>
    /// 是否存在空格
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool HasSpace(this string input)
    {
        return input.Contains(" ");
    }

    /// <summary>
    /// 删除特定字符
    /// </summary>
    /// <param name="str"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static string RemoveString(this string str, params string[] targets)
    {
        return targets.Aggregate(str, (current, t) => current.Replace(t, string.Empty));
    }
}

public static partial class ExtendMeshRenderer
{

    /// <summary>
    /// //相当于将物体隐身，并不会影响物体的脚本运行，物体的碰撞体也依然存在。 
    /// </summary>
    /// <param name="mr"></param>
    /// <returns></returns>
    public static MeshRenderer HideMeshRenderer(this MeshRenderer mr)
    {
        mr.enabled = false;
        return mr;
    }

    public static MeshRenderer ShowMeshRenderer(this MeshRenderer mr)
    {
        mr.enabled = true;
        return mr;
    }
}








