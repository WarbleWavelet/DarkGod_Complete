/****************************************************
    文件：IConfigPath.cs
	作者：lenovo
    邮箱: 
    日期：2023/12/12 13:49:49
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



#region IConfig
/// <summary>
/// 啥都没有，表明这有关配置文件，不要随便改字段名
/// <br/>或者可以加config路径方便查找。但我暂不需要
/// </summary>
public interface IConfig
{
}
public interface IJson : IConfig
{
}
#endregion



#region IConfigPath
public interface IConfigPath
{
    string ConfigPath();
}
public interface IJsonPath  : IConfigPath
{
}

public interface IXml : IConfigPath
{
}

public interface ITxt : IConfigPath
{
}


/// <summary>AssetBundle</summary>
public interface IAB : IConfigPath
{
}

/// <summary>ScriptableObject</summary>
public interface ISO : IConfigPath
{
}

public interface IBinary : IConfigPath
{
}

public interface IExcel : IConfigPath
{
}




#endregion  
