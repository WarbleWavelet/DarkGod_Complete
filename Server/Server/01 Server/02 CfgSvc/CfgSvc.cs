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
        InitStrongCfg();

        PECommon.Log("CfgSvc Inited");


    }
    #region 引导

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

    #region 玩家突破
    public List<string> strongLst = new List<string>();
    Dictionary<int, Dictionary<int, StrongCfg>> strongDic = new Dictionary<int, Dictionary<int, StrongCfg>>();

    private void InitStrongCfg()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(@"D:\Data\Projects\Unity\PESocketExample\DarkDestroyer_20220420_2017.3.0f3\Assets\Resources\ResCfgs\strong.xml");
        XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;
        //
        for (int i = 0; i < nodLst.Count; i++)
        {
            XmlElement ele = nodLst[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)
                continue;
            int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
            //
            StrongCfg cfg = new StrongCfg
            {
                ID = ID
            };
            foreach (XmlElement e in nodLst[i].ChildNodes)
            {
                switch (e.Name)
                {
                    case "pos":
                        {
                            cfg.pos = int.Parse(e.InnerText);
                        }
                        break;
                    case "starlv":
                        {
                            cfg.startlv = int.Parse(e.InnerText);
                        }
                        break;
                    case "addhp":
                        {
                            cfg.addhp = int.Parse(e.InnerText);
                        }
                        break;
                    case "addhurt":
                        {
                            cfg.addhurt = int.Parse(e.InnerText);
                        }
                        break;
                    case "minlv":
                        {
                            cfg.minlv = int.Parse(e.InnerText);
                        }
                        break;
                    case "coin":
                        {
                            cfg.coin = int.Parse(e.InnerText);
                        }
                        break;
                    case "crystal":
                        {
                            cfg.crystal = int.Parse(e.InnerText);
                        }
                        break;

                    default:
                        {

                        }
                        break;
                }

            }
            //

            Dictionary<int, StrongCfg> dic = null;
            if (strongDic.TryGetValue(cfg.pos, out dic))
            {
               dic.Add(cfg.startlv ,cfg);
            }
            else
            {
                dic = new Dictionary<int, StrongCfg>();
                dic.Add(cfg.startlv, cfg);
                strongDic.Add(cfg.pos,dic);

            }
        }
    }
    public StrongCfg GetStrongCfg(int pos,int starLv)
    {
        StrongCfg c = null;
        Dictionary<int,StrongCfg> dic= null;
        if (strongDic.TryGetValue(pos, out dic))
        {
            if (dic.ContainsKey(starLv))
            {
                c = dic[starLv];
            }
            return c;
        }
        else
        {
            return null;
        }
    }


    #endregion

}
#region 引导
public class GuideCfg : BaseData<GuideCfg>
{
    public int coin;
    public int exp;
}

#endregion


#region  玩家突破
/**
	<item ID = "1" >

        < pos > 0 </ pos >
        < starlv > 1 </ starlv >
        < addhp > 20 </ addhp >
        < addhurt > 25 </ addhurt >
        < adddef > 18 </ adddef >
        < minlv > 1 </ minlv >
        < coin > 150 </ coin >
        < crystal > 5 </ crystal >
    </ item >
    **/
public class StrongCfg : BaseData<StrongCfg>
{
    public int pos;
    public int startlv;
    public int addhp;
    public int addhurt;
    public int adddef;
    public int minlv;
    public int coin;
    public int crystal;
}


#endregion


public class BaseData<T>
{
    public int ID;
}



