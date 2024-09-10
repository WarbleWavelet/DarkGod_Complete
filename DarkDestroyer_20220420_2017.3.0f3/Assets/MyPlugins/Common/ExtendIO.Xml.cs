/****************************************************
    文件：ExtendIO.Xml.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/15 12:45:26
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;


public static partial class ExtendXml //自定义,写死的
{
    [Serializable]
    public  class SaveGame
    {
        public List<int> enemyTypeList = new List<int>();
        public List<double> enemyXYZList = new List<double>();
        public int score;
        public int killCount;
    }


    #region xml文件
//   <save name = "SaveFile1" >
//  < target >
//    < type > 1 </ type >
//    < x > 0 </ x >
//    < y > 10.7857751846313 </ y >
//    < z > -41.3900909423828 </ z >
//  </ target >
//  < target >
//    < type > 3 </ type >
//    < x > -6.40850430499995E-08 </ x >
//    < y > 0.412000447511673 </ y >
//    < z > -41.3800392150879 </ z >
//  </ target >
//  < target >
//    < type > 3 </ type >
//    < x > 1.2379999247969E-09 </ x >
//    < y > 0.828986525535584 </ y >
//    < z > -41.3806991577148 </ z >
//  </ target >
//  < target >
//    < type > 0 </ type >
//    < x > 0 </ x >
//    < y > 14.8823690414429 </ y >
//    < z > -41.2791557312012 </ z >
//  </ target >
//  < target >
//    < type > 2 </ type >
//    < x > -12.6999998092651 </ x >
//    < y > 14.9000015258789 </ y >
//    < z > 0.792234003543854 </ z >
//  </ target >
//  < target >
//    < type > 2 </ type >
//    < x > -0.400000005960464 </ x >
//    < y > 1.40000081062317 </ y >
//    < z > 9.88948631286621 </ z >
//  </ target >
//  < target >
//    < type > 3 </ type >
//    < x > 12.1999998092651 </ x >
//    < y > 29 </ y >
//    < z > 19.0389919281006 </ z >
//  </ target >
//  < score > 40 </ score >
//  < killCount > 40 </ killCount >
//</ save >
    #endregion
    #region Xml
    public static  void Object2Xml<T>(T save, string path) where T: SaveGame
    {

        XmlDocument xmlDoc = new XmlDocument();
        XmlElement root = xmlDoc.CreateElement("save");
        root.SetAttribute("name", "SaveFile1");


        for (int i = 0; i < save.enemyTypeList.Count; i++)
        {
            XmlElement target = xmlDoc.CreateElement("target");

            //类型和位置
            XmlElement type = xmlDoc.CreateElement("type");
            XmlElement x = xmlDoc.CreateElement("x");
            XmlElement y = xmlDoc.CreateElement("y");
            XmlElement z = xmlDoc.CreateElement("z");
            type.InnerText = save.enemyTypeList[i].ToString();
            int j = i * 3;
            x.InnerText = save.enemyXYZList[j].ToString();
            y.InnerText = save.enemyXYZList[j + 1].ToString();
            z.InnerText = save.enemyXYZList[j + 2].ToString();

            //print(type.InnerText+","+x.InnerText+ "," + y.InnerText+ "," + z.InnerText);

            target.AppendChild(type);
            target.AppendChild(x);
            target.AppendChild(y);
            target.AppendChild(z);

            root.AppendChild(target);
        }
        //分数
        XmlElement score = xmlDoc.CreateElement("score");
        score.InnerText = save.score.ToString();
        root.AppendChild(score);
        //击杀数
        XmlElement killCount = xmlDoc.CreateElement("killCount");
        killCount.InnerText = save.killCount.ToString();
        root.AppendChild(killCount);


        //
        xmlDoc.AppendChild(root);
        xmlDoc.Save(path);
    }


    public static SaveGame LoadByXml(string path)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(path);
        SaveGame save = new SaveGame();
        //
        XmlNodeList targetList = xmlDoc.GetElementsByTagName("target");
        for (int i = 0; i < targetList.Count; i++)
        {
            int type = int.Parse(targetList[i].ChildNodes[0].InnerText);
            double x = double.Parse(targetList[i].ChildNodes[1].InnerText);
            double y = double.Parse(targetList[i].ChildNodes[2].InnerText);
            double z = double.Parse(targetList[i].ChildNodes[3].InnerText);

            save.enemyTypeList.Add(type);
            save.enemyXYZList.Add(x);
            save.enemyXYZList.Add(y);
            save.enemyXYZList.Add(z);
        }
        //
        int score = int.Parse(xmlDoc.GetElementsByTagName("score")[0].InnerText);
        int killCount = int.Parse(xmlDoc.GetElementsByTagName("killCount")[0].InnerText);
        save.score = score;
        save.killCount = killCount;

        return save;
    }
    #endregion
}
public static partial class ExtendXml 
{  
    //Application.dataPath + "/" Demo02_test.xml";
    //需要对类和字段做标签


    static void Example()
    { 
    
    }

    #region Class2Xml

    /// <summary>
    /// xml序列化
    /// </summary>
    /// <param name="cfg"></param>
    /// <param name="path"></param>
  public static  void Class2Xml<T>(T cfg, string path)
    {
        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        XmlSerializer xml = new XmlSerializer(cfg.GetType());
        xml.Serialize(sw, cfg);
        sw.Close();
        fs.Close();

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif


    }
    #endregion



    #region Xml2Class


    /// <summary>
    /// Xm反序列化
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static T Xml2Class<T>(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        XmlSerializer xs = new XmlSerializer(typeof(T));
        T cfg = (T)xs.Deserialize(fs);
        fs.Close();
        return cfg;
    }
    #endregion
}

