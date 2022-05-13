/****************************************************
	文件：CfgSvc.cs
	作者：WWS
	邮箱: 
	日期：2022/05/13 18:25   	
	功能：配置数据服务
*****************************************************/
using System;
using System.Collections.Generic;
using System.Xml;
using PENet;
using PEProtocol;

class CfgSvc
{
    #region 单例
    private static CfgSvc _instance;
    public static CfgSvc Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CfgSvc();
            }
            return _instance;
        }
    }
    #endregion


    public void Init()
    {
        InitGuideCfg();
        PECommon.Log("CfgSvc Inited");

    }


    #region 任务引导
    public List<string> guideLst = new List<string>();
    Dictionary<int, GuideCfg> guideTaskDic = new Dictionary<int, GuideCfg>();

    private void InitGuideCfg()
    {
        XmlDocument doc = new XmlDocument();
        //doc.LoadXml(@"D:\Data\Projects\Unity\PESocketExample\DarkDestroyer_20220420_2017.3.0f3\Assets\Resources\ResCfgs\guide.xml");
        doc.Load(@"D:\Data\Projects\Unity\PESocketExample\DarkDestroyer_20220420_2017.3.0f3\Assets\Resources\ResCfgs\guide.xml");
        XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;
        //
        for (int i=0;i<nodLst.Count;i++)
        {
            XmlElement ele = nodLst[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)
                continue;
            int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
            //nodLst，ID
            GuideCfg c = new GuideCfg
            {
                ID = ID
            };
            foreach (XmlElement e in nodLst[i].ChildNodes)
            {
                switch (e.Name)
                {
                    case "coin":
                        {
                            c.coin= int.Parse(e.InnerText);
                        }
                        break;
                    case "exp":
                        {
                            c.exp = int.Parse(e.InnerText);
                        }
                        break;
                    default:
                        {

                        }
                        break;
                }

            }
            //
            guideTaskDic.Add(ID, c);
        }
    

    }
    public GuideCfg GetGuideCfg(int ID)
    {
        GuideCfg c = null;
        if (guideTaskDic.TryGetValue(ID, out c))
        {
            return c;
        }
        else
        {
            return null;
        }
    }

    #endregion

}


public class GuideCfg : BaseData<GuideCfg>
{
    public int coin;
    public int exp;
}

public class BaseData<T>
{
    public int ID;
}

