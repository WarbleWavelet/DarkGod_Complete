/****************************************************
    文件：ExtendInterfaceCoding.cs
	作者：lenovo
    邮箱: 
    日期：2023/9/10 14:34:3
	功能：接口编程的收集
01 接口编程面向的是方法和功能实现，而不是类
02 接口编程将行为方法集成到一个方法
03 有人说类似于协议

01 接口编程的使用场景：野怪、敌人、防御塔都会被玩家攻击，这是可以进行接口编程
02 接口编程+继承的联合使用：但是不同职业的敌人有着不同的攻击类型，有着相同的手上方法，
这时可以让不同职业的敌人继承于同一个玩家角色
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendInterfaceCoding
{ 


}
public static partial class ExtendInterfaceCoding
{




    interface SaveAndLoad
    {
        bool NeedSave{ get; set; }
        bool Save();
        bool Load();
        string ToString();
    }

    interface DecryptionAndEncryption
    {
        bool Decryption();
        bool Encryption();
         string ToString();
    }
#if NET_4_7
    /// <summary>不同的接口有相同方法名时</summary>
    static void Example()
    { 
        Demo demo = new Demo();
         SaveAndLoad i1= demo as SaveAndLoad;
        DecryptionAndEncryption i2 = demo as DecryptionAndEncryption;
        i1.ToString();
        i2.ToString();
    }
    class Demo : SaveAndLoad, DecryptionAndEncryption
    {


        bool SaveAndLoad.NeedSave 
        {
            get => throw new System.NotImplementedException(); 
            set => throw new System.NotImplementedException(); 
        }



        bool DecryptionAndEncryption.Decryption()
        {
            throw new System.NotImplementedException();
        }

        bool DecryptionAndEncryption.Encryption()
        {
            throw new System.NotImplementedException();
        }

        bool SaveAndLoad.Load()
        {
            throw new System.NotImplementedException();
        }

        bool SaveAndLoad.Save()
        {
            throw new System.NotImplementedException();
        }

        string SaveAndLoad.ToString()
        {
            throw new System.NotImplementedException();
        }

        string DecryptionAndEncryption.ToString()
        {
            throw new System.NotImplementedException();
        }
    }
#endif
}




