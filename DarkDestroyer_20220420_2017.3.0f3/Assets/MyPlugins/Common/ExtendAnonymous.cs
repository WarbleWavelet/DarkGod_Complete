/****************************************************
    文件：ExtendAnonymous.cs
	作者：lenovo
    邮箱: 
    日期：2023/8/28 13:17:9
	功能： 匿名 Linq 反射
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendAnonymous
{
    static bool _unity = false;

    public static void Example()
    {
        _unity = true;

       // Debug.Log("ExtendAnonymous"); //啥都没有加个标志打印
        if (true) 使用匿名();
        if (true) 动态类型处理匿名();
        if (true) 反射处理匿名();
        if (true) Linq查询匿名集合();
        if (true) Linq的Join();
        if (true) Linq的部分字段();
    }


    #region 辅助
    static void 使用匿名()
    {
        var person = new { Name = "岳飞", Age = "18" };
        Log($"姓名：{person.Name}，年龄：{person.Age}");
    }


    /// <summary>缺点Name等没有提示</summary>
    static void 动态类型处理匿名()
    {
        dynamic person = PersonInfo();
        //Unityerror CS0656: Missing compiler required member 'Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create'
        //Net 4x + IL2CPP   //QF在这种情况下打包报错，相爱会 Net2.0+Mono打包先。所以先注释下面
        //https://blog.csdn.net/VAR_720/article/details/132085575
       // Log($"姓名：{person.Name},年龄：{person.Age}");
    }

    static void 反射处理匿名()
    {
        #if NET_4_8_OR_NEWER
        CharacterInfo(new { Name = "张宪", Age = "18" });
        #endif
    }

    static void Linq查询匿名集合()
    {
        var persons = new[]
        {
            new { Name="高宠",Age=18} ,
            new { Name="陆文龙",Age=19} ,
            new { Name="余化龙",Age=20} ,
            new { Name="张宪",Age=20} 
        };
        var query= from p in persons
                 where p.Age>19
                 select p;
        foreach (var person in query)
        {
            Log($"姓名：{person.Name},年龄：{person.Age}");
        }
    }


    static void Linq的Join()
    { 
          var countryLst=new List<王朝>
          {
             new 王朝{ CountryID=0, CountryName="宋"},
             new 王朝{ CountryID=1, CountryName="金"},
          };

        var personLst = new List<将领>
        { 
           new 将领{ CountryID=0,PersonName="岳雷"} ,
           new 将领{ CountryID=0,PersonName="岳霆"} ,
           new 将领{ CountryID=1,PersonName="完颜宗弼"} ,
           new 将领{ CountryID=1,PersonName="完颜娄室"} ,
        
        };

        var query = from c in countryLst
                    join p in personLst
                    on c.CountryID equals p.CountryID
                    select new
                    {
                        王朝 = c.CountryName,
                        将领名 = p.PersonName
                    };

        foreach (var q in query)
        {
            Log($"姓名：{q.王朝},将领：{q.将领名}");
        }


    }

    static void Linq的部分字段()
    { 
        var personLst = new List<将领>
        {
           new 将领{ CountryID=0,PersonName="岳雷"} ,
           new 将领{ CountryID=0,PersonName="岳霆"} ,
           new 将领{ CountryID=1,PersonName="完颜宗弼"} ,
           new 将领{ CountryID=1,PersonName="完颜娄室"} ,

        };
        var query = from p in personLst
                    select new
                    {
                        p.PersonName
                    };

        foreach (var p in personLst)
        {
            Log($"将领：{p.PersonName}");
        }    
    }
#endregion


#region 辅助的辅助
#if NET_4_8_OR_NEWER
    static void CharacterInfo(object obj)
    {
        Type type = obj.GetType();
        PropertyInfo? pName = type.GetProperty("Name");
        PropertyInfo? pAge = type.GetProperty("Age");
        Log($"类型：{type.Name}，姓名：{pName?.GetValue(obj)}，年龄：{pAge?.GetValue(obj)}");
    }
#endif
    static dynamic PersonInfo()
    {
        return new { Name = "牛皋", Age = "18" }; 
    }

    static void Log(string str)
    {
        if (_unity)
        {

            Debug.LogFormat(str);//支持$
        }
        else
        {
            Console.WriteLine(str);
        }
    }

     class 将领
    { 
          public int CountryID { get; set; }
          public string PersonName { get; set; }
    }

    class 王朝
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
#endregion


}




