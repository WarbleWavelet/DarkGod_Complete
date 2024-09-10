/****************************************************
    文件：ExtendIO.Bin.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/20 14:48:55
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendBin //说明介绍
{
   // 二进制文件使用起来比较方便，体积小
   //常搭配序列类
}
public static partial class ExtendBin
{
    #region Bin
    public static  void Object2Bin<T>(T t, string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(path);


        bf.Serialize(fs, t);

        fs.Close();
    }

    public static T Bin2Object<T>(string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(path, FileMode.Open);

        T save = (T)bf.Deserialize(fs);

        fs.Close();
        return save;
    }
    #endregion


}



