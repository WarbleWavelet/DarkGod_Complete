/****************************************************
	文件：CfgSvc.cs
	作者：WWS
	邮箱: 
	日期：2022/05/13 18:25   	
	功能：配置数据服务
*****************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        InitGuideCfg(PathDefine.Guide);
        InitStrongCfg(PathDefine.Strong);
        InitTaskRewardCfg(PathDefine.TaskReward);
        InitMapCfg(PathDefine.Map);

        PECommon.Log("CfgSvc Init");


    }
    #region 引导

    public List<string> guideLst = new List<string>();
    Dictionary<int, GuideCfg> guideTaskDic = new Dictionary<int, GuideCfg>();

    private void InitGuideCfg(string path)
    {
        XmlDocument doc = new XmlDocument();
        //doc.LoadXml(@"");失败
        doc.Load(path);
        XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;
        //
        for (int i = 0; i < nodLst.Count; i++)
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
                            c.coin = int.Parse(e.InnerText);
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

    private void InitStrongCfg(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
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

    #region 任务奖励

    public List<string> taskRewardLst = new List<string>();
    Dictionary<int, TaskRewardCfg> taskRewardDic = new Dictionary<int, TaskRewardCfg>();

    private void InitTaskRewardCfg(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;
        //
        for (int i = 0; i < nodLst.Count; i++)
        {
            XmlElement ele = nodLst[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)
                continue;
            int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
            //nodLst，ID
            TaskRewardCfg c = new TaskRewardCfg
            {
                ID = ID
            };
            foreach (XmlElement e in nodLst[i].ChildNodes)
            {
                switch (e.Name)
                {
                    case "count":
                        {
                            c.count = int.Parse(e.InnerText);
                        }
                        break;
                    case "coin":
                        {
                            c.coin = int.Parse(e.InnerText);
                        }
                        break;
                    case "exp":
                        {
                            c.exp = int.Parse(e.InnerText);
                        }
                        break;

                    default: break;
                }

            }
            //
            taskRewardDic.Add(ID, c);
        }


    }
    public TaskRewardCfg GetTaskRewardCfg(int ID)
    {
        TaskRewardCfg c = null;
        if (taskRewardDic.TryGetValue(ID, out c))
        {
            return c;
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region 副本

    //public List<string> instanceLst = new List<string>();
    //Dictionary<int, InstanceCfg> instanceDic = new Dictionary<int, InstanceCfg>();

    //private void InitInstanceCfg(string path)
    //{
    //    XmlDocument doc = new XmlDocument();
    //    doc.Load(path);
    //    XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;
    //    //
    //    for (int i = 0; i < nodLst.Count; i++)
    //    {
    //        XmlElement ele = nodLst[i] as XmlElement;
    //        if (ele.GetAttributeNode("ID") == null)
    //            continue;
    //        int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
    //        //nodLst，ID
    //        InstanceCfg c = new InstanceCfg
    //        {
    //            ID = ID
    //        };
    //        foreach (XmlElement e in nodLst[i].ChildNodes)
    //        {
    //            switch (e.Name)
    //            {
    //                case "power":
    //                    {
    //                        c.power = int.Parse(e.InnerText);
    //                    }
    //                    break;
    //                case "exp":
    //                    {
    //                        c.exp = int.Parse(e.InnerText);
    //                    }
    //                    break;
    //                case "coin":
    //                    {
    //                        c.coin = int.Parse(e.InnerText);
    //                    }
    //                    break;
    //                case "crystal":
    //                    {
    //                        c.crystal = int.Parse(e.InnerText);
    //                    }
    //                    break;
    //                default: break;
    //            }

    //        }
    //        //
    //        instanceDic.Add(ID, c);
    //    }


    //}
    //public InstanceCfg GetInstanceCfg(int ID)
    //{
    //    InstanceCfg c = null;
    //    if (instanceDic.TryGetValue(ID, out c))
    //    {
    //        return c;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}
    public List<string> mapLst = new List<string>();
    Dictionary<int, MapCfg> mapDic = new Dictionary<int, MapCfg>();

    private void InitMapCfg(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;
        //
        for (int i = 0; i < nodLst.Count; i++)
        {
            XmlElement ele = nodLst[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)
                continue;
            int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
            //nodLst，ID
            MapCfg c = new MapCfg
            {
                ID = ID
            };
            foreach (XmlElement e in nodLst[i].ChildNodes)
            {
                switch (e.Name)
                {
                    case "power":
                        {
                            c.power = int.Parse(e.InnerText);
                        }
                        break;
                    case "exp":
                        {
                            c.exp = int.Parse(e.InnerText);
                        }
                        break;
                    case "coin":
                        {
                            c.coin = int.Parse(e.InnerText);
                        }
                        break;
                    case "crystal":
                        {
                            c.crystal = int.Parse(e.InnerText);
                        }
                        break;
                    default: break;
                }

            }
            //
            mapDic.Add(ID, c);
        }


    }
    public MapCfg GetMapCfg(int ID)
    {
        MapCfg c = null;
        if (mapDic.TryGetValue(ID, out c))
        {
            return c;
        }
        else
        {
            return null;
        }
    }

    public int Get_MaxID_MapCfg()
    {
        int max = 10002;
        foreach (int key in mapDic.Keys)
        {
            if (max < key)
            {
                max = key;
            }
        }
        return max;
    }
    #endregion
}

#region 数据配置类
/// <summary>
/// 配置类
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseData<T>
{
    public int ID;
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


#region 任务奖励
/**
	<item ID="1">
		<taskName>智者点拨</taskName>
		<count>1</count>
		<exp>1130</exp>
		<coin>1280</coin>
	</item>
 * **/
public class TaskRewardCfg : BaseData<TaskRewardCfg>
{
    /// <summary>任务名</summary>
    //public string taskName;//不需要
    /// <summary>金币</summary>
    public int coin;
    /// <summary>经验</summary>
    public int exp;
    /// <summary>需要完成的次数</summary>
    public int count;


}

public class TaskRewardData : BaseData<TaskRewardData>
{
    /// <summary>是否已经完成</summary>
    public TaskState state;
    /// <summary>已经完成的次数</summary>
    public int prgs;
}


#endregion

#region  副本
/**
	<item ID="10000">
		<mapName>圣光主城</mapName>
		<sceneName>SceneMainCity</sceneName>
		<power>0</power>
		<mainCamPos>17.4,7,50</mainCamPos>
		<mainCamRote>45,135,0</mainCamRote>
		<playerBornPos>22,-1.1,45</playerBornPos>
		<playerBornRote>0,0,0</playerBornRote>
	</item>
    **/
public class InstanceCfg : BaseData<InstanceCfg>
{
    public string mapName;
    public string sceneName;
    public int power;
    public float[] mainCamPos;
    public float[] mainCamRote;
    public float[] playerBornPos;
    public float[] playerBornRote;

}


public class MapCfg : BaseData<MapCfg>
{
    //通关奖励
    public int power;
    public int exp;
    public int coin;
    public int crystal;
}

#endregion
#endregion


