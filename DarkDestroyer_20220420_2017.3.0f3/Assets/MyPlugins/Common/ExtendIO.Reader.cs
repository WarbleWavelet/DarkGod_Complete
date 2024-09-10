/****************************************************
    文件：ExtendIO.Reader.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/21 17:44:24
	功能：Config 配置 数据
*****************************************************/

using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Random = UnityEngine.Random;
 
public static partial class ExtendReader 
{


}



#region Reader
public interface IReader
{ 

}

//public class JsonReader: IReader
//{

//}

public class XmlReader : IReader
{

}

public class BinReader : IReader
{

}

public class ExcelReader : IReader
{

}

public class TxtReader : IReader
{

}
#endregion





#region Json

public interface IJsonReader : IReader
{
    IJsonReader this[string key] { get; }
    IJsonReader this[int key] { get; }
    void Count(Action<int> callBack);
    void Get<T>(Action<T> callBack);
    void SetData(object data);
    ICollection<string> Keys();
}


//jsonReader["planes"][0]["planeId"].GetOut<int>(()=>)
//jsonReader["planes"][0]["planeId"]
public class JsonReader : IJsonReader
{
    private readonly Queue<KeyQueue> _keyQueues = new Queue<KeyQueue>();
    private KeyQueue _keyQueue;
    private JsonData _jsonData;
    private JsonData _tmpJsonData;

    public IJsonReader this[int key]
    {
        get
        {
            SetReader(key, () => _tmpJsonData = _tmpJsonData[key]);
            return this;
        }
    }

    public IJsonReader this[string key]
    {
        get
        {
            SetReader(key, () => _tmpJsonData = _tmpJsonData[key]);
            return this;
        }
    }


    #region  public


    public void Count(Action<int> cb)
    {
        var success = SetKey<Action>(() =>
        {
            cb.DoIfNotNull(GetCount());
        });

        if (!success)
        {
            cb(GetCount());
        }
        else
        {
            _keyQueues.Enqueue(_keyQueue);
            _keyQueue = null;
        }
    }

    public void Get<T>(Action<T> callBack)
    {
        if (_keyQueue != null)
        {
            _keyQueue.OnComplete(dataTemp =>
            {
                var value = GetValue<T>(dataTemp);
                ResetData();
                callBack(value);
            });

            _keyQueues.Enqueue(_keyQueue);
            _keyQueue = null;
            ExecuteKeysQueue();
            return;
        }

        if (callBack == null)
        {
            Debug.LogWarning("当前回调方法为空，不返回数据");
            ResetData();
            return;
        }

        var data = GetValue<T>(_tmpJsonData);
        ResetData();
        callBack(data);
    }

    /// <summary>load之后的数据填充</summary>
    public void SetData(object data)
    {
        if (data is string)
        {
            _jsonData = JsonMapper.ToObject(data as string);
            ResetData();
            ExecuteKeysQueue();
        }
        else
        {
            Debug.LogError("当前传入数据类型错误，当前类只能解析json");
        }
    }

    public ICollection<string> Keys()
    {
        if (_tmpJsonData == null)
            return new string[0];

        return _tmpJsonData.Keys;
    }
    #endregion


    #region  pri

    void SetReader<T>(T key, Action action)
    {
        if (!SetKey(key))
        {
            try
            {
                action.DoIfNotNull();
            }
            catch (Exception e)
            {
                Debug.LogError($"在数据中无法找到对应键值，数据：" +
                    $"({key.GetType().Name}){_tmpJsonData.ToJson()}" +
                    $"\t 键值：{key}");
            }
        }
    }



    /// <summary>GetOrAdd</summary>
    private bool SetKey<T>(T key)
    {
        if (_jsonData == null || _keyQueue != null)
        {

            IKey keyData = new Key(key);

            if (_keyQueue == null)
            {
                _keyQueue = new KeyQueue();
            }
            _keyQueue.Enqueue(keyData);


            return true;
        }

        return false;
    }

    private int GetCount()
    {
        return _tmpJsonData.IsArray ? _tmpJsonData.Count : 0;
    }

    private void ExecuteKeysQueue()
    {
        if (_jsonData == null)
            return;

        IJsonReader reader = null;
        foreach (var keyQueue in _keyQueues)
        {
            foreach (var value in keyQueue)
            {
                if (value is string)
                    reader = this[(string)value];
                else if (value is int)
                    reader = this[(int)value];
                else if (value is Action)
                    ((Action)value)();
                else
                    Debug.LogError("当前键值类型错误");
            }


            keyQueue.Complete(_tmpJsonData);
        }

        _keyQueues.Clear();
    }

    private T GetValue<T>(JsonData data)
    {
        var converter = TypeDescriptor.GetConverter(typeof(T));

        try
        {
            if (converter.CanConvertTo(typeof(T)))
                return (T)converter.ConvertTo(data.ToString(), typeof(T));
            return (T)(object)data;
        }
        catch (Exception e)
        {
            Debug.LogError("当前类型转换出现问题，目标类型为：" + typeof(T).Name + "  data:" + data);
            return default(T);
        }
    }

    private void ResetData()
    {
        _tmpJsonData = _jsonData;
    }
    #endregion

}

#endregion 



#region Key


public class KeyQueue : IEnumerable
{
    private Action<JsonData> _complete;
    private readonly Queue<IKey> _keyQueue = new Queue<IKey>();



    public IEnumerator GetEnumerator()
    {
        foreach (var key in _keyQueue)
        {
            yield return key.Get();
        }
    }



    #region 辅助


    public void Enqueue(IKey key)
    {
        _keyQueue.Enqueue(key);
    }

    public void Dequeue()
    {
        _keyQueue.Dequeue();
    }

    public void Clear()
    {
        _keyQueue.Clear();
    }

    public void Complete(JsonData data)
    {
        _complete.DoIfNotNull(data);
    }

    public void OnComplete(Action<JsonData> complete)
    {
        _complete = complete;
    }
    #endregion

}

public interface IKey
{
    Type KeyType { get; }
    void Set<T>(T key);
    object Get();
}

public class Key : IKey
{
    private object _key;
    public Type KeyType { get; private set; }

    public Key(object key)
    {
        _key = key;
    }

    public Key()
    {

    }


    public void Set<T1>(T1 key)
    {
        _key = key;
    }

    public object Get()
    {
        return _key;
    }
}
#endregion



