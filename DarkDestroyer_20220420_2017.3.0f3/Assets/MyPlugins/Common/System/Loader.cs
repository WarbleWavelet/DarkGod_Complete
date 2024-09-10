/****************************************************
	文件：Loader.cs
	作者：lenovo
	邮箱: 
	日期：2024/7/17 0:30:37
	功能：
*****************************************************/

using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Random = UnityEngine.Random;
using Excel;
using System.Runtime;
using UnityEngine.Networking;



#region 接口
/// <summary>加载器不同,对象类型,数量不同,回调不同,无法统一,写一个空的统一</summary>
public interface ILoader { }
public interface IWWWLoader:ILoader { }


/// <summary> UnityEngine.Object</summary>
public interface IUObjectLoader : ILoader
{
	//void LoadConfig(string path, Action<object> onCompleted); //拆分出去IConfigLoader
	T Load<T>(string path) where T : UnityEngine.Object;
	T[] LoadAll<T>(string path) where T : UnityEngine.Object;

}

public interface IUObjectAsyncLoaderByIEnumerator : ILoader
{
	IEnumerator LoadAsync<T>(string name, Action<T> cb) where T : UnityEngine.Object;

}


public interface IUObjectAsyncLoader : ILoader
{
	//void LoadConfigAsync(string path, Action<object> onCompleted); //拆分出去IConfigLoader
	T LoadAsync<T>(string path) where T : UnityEngine.Object;
	T[] LoadAllAsync<T>(string path) where T : UnityEngine.Object;

}

#region IIntLoader	IFloatLoader  IStringLoader
public interface IIntLoader : ILoader 
{
	int GetInt(string key);
	void SetInt(string key,int value);
}
public interface IFloatLoader : ILoader
{
    float GetFloat(string key);
    void SetFloat(string key, float value);
}
public interface IStringLoader : ILoader
{
    string GetString(string key);
    void SetString(string key, string value);
}
#endregion

#endregion







#region WWWLoader读取(Excel=>Bin=>Dic)的一个案例
//01 Excel+序列类=>Bin[ExcelTool.Excel2Bin]
//02 Bin=>Object(ExcelTool.Bin2Dic) 
public class EBDWWWLoader : IWWWLoader
{
	int _loadStep = 0;

	public EBDWWWLoader()
	{
		_loadStep = 0;
	}


	/// <summary>
	/// Bin的类转化为Dic
	/// 字典时引用类型,不用加ref
	/// </summary>
	public	IEnumerator Bin2Dic(string filename,Action cb,   Dictionary<string, List<ConfigClassBase>> Dic )
	{

		_loadStep++;
		string filepath = ExcelTool.GetConfigFilePath(filename);
		WWW www = new WWW(filepath);
		yield return www;


		while (www.isDone == false)
		{ 
			yield return null;
		} 


		if (www.error == null)
		{
			byte[] data = www.bytes;
			List<ConfigClassBase> datalist = (List<ConfigClassBase>)ExcelTool.Bin2Obj(data);
			Dic.Add(filename, datalist);
		}
		else
		{
			//GameLogTools.SetText("wwwError<<" + www.error + "<<" + filepath);
			Debug.Log("wwwError<<" + www.error + "<<" + filepath);
		}

		_loadStep--;
		if (_loadStep <= 0)
		{
			cb();
		}
	}

}


#region 配置类



[Serializable]
public abstract class ConfigClassBase//不能序列化接口
{
}

[Serializable]
public class ExcelATest1 : ConfigClassBase
{
	public string id;
	public string eat1_1;
	public string eat1_2;
	public int eat1_3;


}
[Serializable]
public class ExcelATest2 : ConfigClassBase
{

	public string id;
	public string eat2_1;
	public string eat2_2;
	public int eat2_3;

}
[Serializable]
public class ExcelBTest1 : ConfigClassBase
{

	public string id;
	public string ebt1_1;
	public string ebt1_2;
	public int ebt1_3;
}
#endregion


#region Tool


public static class ExcelTool //Excel.dll
{







	#region pub
	/// <summary>
	/// 反序列化
	/// </summary>
	/// <param name="bytes"></param>
	/// <returns></returns>
	public static object Bin2Obj(byte[] bytes)
	{
		object dic = null;
		if (bytes == null)
			return dic;
		//利用传来的byte[]创建一个内存流
		MemoryStream ms = new MemoryStream(bytes);
		BinaryFormatter formatter = new BinaryFormatter();
		//把流中转换为Dictionary
		dic = (List<ConfigClassBase>)formatter.Deserialize(ms);
		return dic;
	}

	public static string GetConfigFilePath(string tablename)
	{
		string src = "";

		if (Application.platform == RuntimePlatform.Android)
		{
			src = "jar:file://" + Application.dataPath + "!/assets/Config/" + tablename;
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			src = "file://" + Application.dataPath + "/Raw/Config/" + tablename;
		}
		else
		{
			src = "file://" + Application.streamingAssetsPath + "/Config/Xls/" + tablename;
		}
		return src;
	}
	#endregion


#region pri

#if !NET_4_6

	/// <summary>
	/// 载入一个excel文件 Loads the data. xls或xlsx
	/// </summary>
	/// <param name="completePath">Filename.</param>
	public static string Excel2Bin(string completePath)
	{
		FileStream stream = File.Open(completePath, FileMode.Open, FileAccess.Read);
		IExcelDataReader excelReader = completePath.Contains(".xlsx")
			? ExcelReaderFactory.CreateOpenXmlReader(stream)
			: ExcelReaderFactory.CreateBinaryReader(stream);

		DataSet result = excelReader.AsDataSet();

		string res = "";
		//处理所有的子表
		for (int i = 0; i < result.Tables.Count; i++)
		{
		   // Debug.Log(result.Tables[i].TableName);
			bool issuccess = HandleATable(result.Tables[i],  completePath);
			if (issuccess)
				res += result.Tables[i].TableName + "\n";
		}
		return res;
	}

	 static string GetClassNameByName(string tablename)
	{

		if (tablename.Substring(0, 6) == "string")
		{
			return "GameStringConf";
		}
		return tablename;
	}

	/// <summary>
	/// 处理一张表 Handle A table.
	/// </summary>
	/// <param name="result">Result.</param>
	 static bool HandleATable(DataTable result, string completePath)
	{
		Debug.Log(result.TableName);

		//创建这个类
		Type t = Type.GetType(GetClassNameByName(result.TableName));
		if (t == null)
		{
			Debug.Log("the type is null  : " + result.TableName);
			return false;
		}

		int columns = result.Columns.Count;
		int rows = result.Rows.Count;

		//行数从0开始  第0行为注释
		int fieldsRow = 1;//字段名所在行数
		int contentStarRow = 2;//内容起始行数

		//获取所有字段
		string[] tableFields = new string[columns];

		for (int j = 0; j < columns; j++)
		{
			tableFields[j] = result.Rows[fieldsRow][j].ToString();
			//Debuger.Log(tableFields[j]);
		}

		//存储表内容的字典
		List<ConfigClassBase> datalist = new List<ConfigClassBase>();

		//遍历所有内容
		for (int i = contentStarRow; i < rows; i++)
		{
			ConfigClassBase o = Activator.CreateInstance(t) as ConfigClassBase;

			for (int j = 0; j < columns; j++)
			{
				System.Reflection.FieldInfo info = o.GetType().GetField(tableFields[j]);

				if (info == null)
				{
					continue;
				}

				string val = result.Rows[i][j].ToString();

				if (info.FieldType == typeof(int))
				{
					info.SetValue(o, int.Parse(val));
				}
				else if (info.FieldType == typeof(float))
				{
					info.SetValue(o, float.Parse(val));
				}
				else
				{
					info.SetValue(o, val);
				}
				//Debuger.Log(val);
			}

			datalist.Add(o);

		}

		SaveTableData(datalist, result.TableName + ".msconfig",  completePath);
		return true;
	}
#endif
	/// <summary>
	/// 把Dictionary序列化为byte数据
	/// Saves the table data.
	/// </summary>
	/// <param name="dic">Dic.</param>
	/// <param name="tablename">Tablename.</param>
	 static void SaveTableData(List<ConfigClassBase> datalist, string tablename, string completePath)
	{

		byte[] dicdata = SerializeObj(datalist);
		//WriteByteToFile(gzipData,tablename);
		WriteByteToFile(dicdata, SaveConfigFilePath(tablename, completePath));
	}

	/// <summary>
	/// 序列化
	/// </summary>
	/// <param name="dic"></param>
	/// <returns></returns>
	 static byte[] SerializeObj(object obj)
	{
		MemoryStream ms = new MemoryStream();
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(ms, obj);//把字典序列化成流
		byte[] bytes = ms.GetBuffer();

		return bytes;
	}



	/// <summary>
	/// 二进制数据写入文件 Writes the byte to file.
	/// </summary>
	/// <param name="data">Data.</param>
	/// <param name="tablename">path.</param>
	 static void WriteByteToFile(byte[] data, string path)
	{

		FileStream fs = new FileStream(path, FileMode.Create);
		fs.Write(data, 0, data.Length);
		fs.Close();
	}

	/// <summary>
	/// 读取文件二进制数据 Reads the byte to file.
	/// </summary>
	/// <returns>The byte to file.</returns>
	/// <param name="path">Path.</param>
	 static byte[] ReadByteToFile(string path)
	{
		//Debug.Log(path);
		//如果文件不存在，就提示错误
		if (!File.Exists(path))
		{
			Debug.Log("读取失败！不存在此文件");
			Debug.Log(path);
			return null;
		}
		FileStream fs = new FileStream(path, FileMode.Open);
		BinaryReader br = new BinaryReader(fs);
		byte[] data = br.ReadBytes((int)br.BaseStream.Length);

		fs.Close();
		return data;
	}

	 static string SaveConfigFilePath(string tablename,string completePath)
	{
		return completePath.TrimName(TrimNameType.SlashPre)+"/"+ tablename;
	}
#endregion

}
#endregion

#endregion



#region IConfigLoader
public interface IConfigLoader
{
	/// <summary>常见</summary>
	IEnumerator LoadConfig(string pathOrFileName, Action<object> onCompleted);
	string LoadConfig(string path);
}
[Obsolete("已过时")]
public class WWWConfigLoader : IConfigLoader
{



	public string LoadConfig(string path)
	{
		throw new NotImplementedException();
	}

	#region pri
    public IEnumerator LoadConfig(string path, Action<object> onCompleted)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			path = $"file://{path}";
		}
		var www = new WWW(path);
		yield return www;


		if (www.error != null)
		{
			Debug.LogError("WWWConfigLoader加载配置错误，路径为：" + path);
			yield break;
		}


		onCompleted(www.text);
		//Debug.Log("文件加载成功，路径为：" + path);
	}
	#endregion



}



#endregion


#region WebRequestConfigLoader
public class WebRequestConfigLoader : IConfigLoader
{
	public IEnumerator LoadConfig(string fileName, Action<object> onCompleted)
	{

		throw new System.Exception("异常:未定义");
	}

	public string LoadConfig(string fileName)
	{
		//通过文档的名字生成路径并寻找
		string url = new System.Uri(System.IO.Path.Combine(Application.streamingAssetsPath, fileName)).ToString();
		UnityWebRequest request = UnityWebRequest.Get(url);
		request.SendWebRequest();
		if (request.error == null)
		{
			while (true)
			{
				//返回文档中的记录
				if (request.downloadHandler.isDone)
				{
					return request.downloadHandler.text;
				}
			}
		}
		else
		{

			Debug.LogError("读取Config文件失败");
			return String.Empty;
		}
	}
}

#endregion


#region PlayerPrefsLoader


public class PlayerPrefsLoader : IIntLoader, IFloatLoader, IStringLoader
{
    public float GetFloat(string key)
    {
		return PlayerPrefs.GetFloat(key);
    }

    public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key,value);
    }

    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
}
#endregion




