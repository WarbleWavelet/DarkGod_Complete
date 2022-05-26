/****************************************************
    文件：ResSvc.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/23 17:27:0
	功能：资源服务
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResSvc : MonoBehaviour 
{
    public static ResSvc Instance;

    /// <summary>
    /// 初始化服务
    /// </summary>
    public void InitSvc()
    {
        Instance = this;
        InitRDNameCfg(PathDefine.RDNameCfg);
        InitMapCfg(PathDefine.MapCfg);
        InitGuideCfg(PathDefine.GuideCfg);
        InitStrongCfg(PathDefine.StrongCfg);
        InitTaskRewardCfg(PathDefine.TaskRewardCfg);
        PECommon.Log("Init ResSvc",LogType.Log);
    }

    void Update()
    {
        if (prgCB != null)
        {
            prgCB();
        }
    }





    #region Scene
    /// <summary>不停查询进度</summary>
    Action prgCB;

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void AsyncLoadScene(string sceneName,Action loaded)
    {
        GameRoot.Instance.loadingWnd.SetWndState();

        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName);

        prgCB = () =>
        {
            float val = sceneAsync.progress;
            LoadingWnd loadingWnd = GameRoot.Instance.loadingWnd;
            loadingWnd.SetProgress(val);

            if (val == 1)
            {
                if (loaded != null)
                { 
                    loaded();
                }

                sceneAsync = null;
                prgCB = null;
                loadingWnd.gameObject.SetActive(false);
            }

        };        
    }
    #endregion


    #region Audio
    Dictionary<string, AudioClip> adDict = new Dictionary<string, AudioClip>();
    /// <summary>
    /// 加载声音
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cache">缓存不？</param>
    /// <returns></returns>

    public AudioClip LoadAudio(string path, bool cache = false)
    {
        AudioClip au = null;
        if (adDict.TryGetValue(path, out au) == false)
        {
            au = Resources.Load<AudioClip>(path);
            if (cache)
            {
                adDict.Add(path, au);
            }
        }



        return au;
    }
    #endregion


    #region Sprite
    Dictionary<string, Sprite> spDict = new Dictionary<string, Sprite>();
    /// <summary>
    /// 加载图片
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cache">缓存不？</param>
    /// <returns></returns>

    public Sprite LoadSprite(string path, bool cache = false)
    {
        Sprite sp = null;
        if (spDict.TryGetValue(path, out sp) == false)
        {
           sp = Resources.Load<Sprite>(path);
            if (cache)
            {
                spDict.Add(path, sp);
            }
        }

        return sp;
    }
    #endregion

    #region 随机名字
    public List<string> surnameLst = new List<string>();
    public List<string> manLst = new List<string>();
    public List<string> womanLst = new List<string>();
    /// <summary>
    /// 初始化随机名字的配置文件
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void InitRDNameCfg(string path)
    {
        TextAsset xml = Resources.Load<TextAsset>(path);
        XmlNodeList nodLst = GetListFromTextAsset(xml);
        if (nodLst != null)
        {
            for (int i = 0; i < nodLst.Count; i++)
            {
                XmlElement ele = nodLst[i] as XmlElement;

                if (ele.GetAttributeNode("ID") == null) continue;

                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                foreach (XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                        case "surname":
                            {
                                surnameLst.Add(e.InnerText);
                            }
                            break;
                        case "man":
                            {
                                manLst.Add(e.InnerText);
                            }
                            break;
                        case "woman":
                            {
                                womanLst.Add(e.InnerText);
                            }
                            break;
                        default:
                            {

                            }
                            break;
                    }
                }
            }
        }
    }
    public string GetRDName(bool man = true)
    {
        string rdName = surnameLst[PETools.RDInt(0, surnameLst.Count - 1)];
        if (man)
        {
            rdName += manLst[PETools.RDInt(0, manLst.Count - 1)];
        }
        else
        {
            rdName += womanLst[PETools.RDInt(0, womanLst.Count - 1)];
        }

        return rdName;
    }
    #endregion


    #region Common
    /// <summary>
    /// 字符串数组转Vector3
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    Vector3 ParseVector3ByXmlElement(XmlElement e)
    {
        string[] valArr = e.InnerText.Split(',');
        return new Vector3(float.Parse(valArr[0]), float.Parse(valArr[1]), float.Parse(valArr[2]));
    }
    XmlNodeList GetListFromTextAsset(TextAsset xml)
    {
        if (!xml)
        {
            PECommon.Log("xml file:" + PathDefine.RDNameCfg + "not exist",LogType.Error);
            return null;
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);

            XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;


            return nodLst;
          
        }
    }
    #endregion


    #region 预制体
    Dictionary<string,GameObject> goDic=new Dictionary<string,GameObject>();
    public GameObject LoadPrefab(string path, bool cache = false)
    { 
        GameObject prefab=null;
        if (!goDic.TryGetValue(path, out prefab))
        {
            prefab = Resources.Load<GameObject>(path);
            if (cache)
            {
                goDic.Add(path,prefab);
            }
        }
        //
        GameObject go = null;
        if (prefab != null)
        {
            go = Instantiate(prefab);
            return go;
        }
        return null;
    }

    #endregion

    #region map
    public List<string> mapLst = new List<string>();
    Dictionary<int, MapCfg> mapCfgDataDic=new Dictionary<int, MapCfg> ();
    /// <summary>
    /// 初始化随机名字的配置文件
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void InitMapCfg(string path)
    {
        TextAsset xml = Resources.Load<TextAsset>(path);
        XmlNodeList nodLst = GetListFromTextAsset(xml);
        if (nodLst != null)
        {
            for (int i = 0; i < nodLst.Count; i++)
            {
                XmlElement ele = nodLst[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null) 
                    continue;
                //
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                MapCfg c=new MapCfg 
                { 
                    ID = ID
                };
                foreach (XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                        case "mapName":
                            {
                                c.mapName= e.InnerText;
                            }
                            break;
                        case "sceneName":
                            {
                                c.sceneName = e.InnerText;
                            }
                            break;
                        case "mainCamPos":
                            {
                                c.mainCamPos = ParseVector3ByXmlElement(e);
                            }
                            break;
                        case "mainCamRote":
                            {
                                c.mainCamRote = ParseVector3ByXmlElement(e);
                            }
                            break;
                        case "playerBornPos":
                            {
                                c.playerBornPos = ParseVector3ByXmlElement(e);
                            }
                            break;
                        case "playerBornRote":
                            {
                                c.playerBornRote = ParseVector3ByXmlElement(e);
                            }
                            break;
                        default:
                            {

                            }
                            break;
                    }
                    
                }
                //
                mapCfgDataDic.Add(ID, c);

            }
        }
        //

    }

    public MapCfg GetMapDataCfg(int ID)
    {
        MapCfg c = null;
        if (mapCfgDataDic.TryGetValue(ID, out c))
        {
            return c;
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region 任务引导
    public List<string> guideLst = new List<string>();
    Dictionary<int, AutoGuideCfg> guideTaskDic = new Dictionary<int, AutoGuideCfg>();

    private void InitGuideCfg(string path)
    {
        TextAsset xml = Resources.Load<TextAsset>(path);
        XmlNodeList nodLst = GetListFromTextAsset(xml);
        if (nodLst != null)
        {
            for (int i = 0; i < nodLst.Count; i++)
            {
                XmlElement ele = nodLst[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)
                    continue;
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                //nodLst，ID
                AutoGuideCfg c = new AutoGuideCfg
                {
                    ID = ID
                };
                foreach (XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                        case "npcID":
                            {
                                c.npcID = int.Parse(e.InnerText);
                            }
                            break;
                        case "dilogArr":
                            {
                                c.dilogArr = e.InnerText;
                            }
                            break;
                        case "actID":
                            {
                                c.actID = int.Parse(e.InnerText);
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
    }
    public AutoGuideCfg GetGuideCfg(int ID)
    {
        AutoGuideCfg c = null;
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
    /// <summary>pos+starLv</summary>
    Dictionary<int, Dictionary<int, StrongCfg>> strongDic = new Dictionary<int, Dictionary<int, StrongCfg>>();

    private void InitStrongCfg(string path)
    {
        TextAsset xml = Resources.Load<TextAsset>(path);
        XmlNodeList nodLst = GetListFromTextAsset(xml);
        //
        for (int i = 0; i < nodLst.Count; i++)
        {
            XmlElement ele = nodLst[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)
                continue;
            int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
            //
            StrongCfg sd = new StrongCfg
            {
                ID = ID
            };
            foreach (XmlElement e in nodLst[i].ChildNodes)
            {
                int val = int.Parse(e.InnerText);
                switch (e.Name)
                {
                    case "pos":
                        {
                            sd.pos = val;
                        }
                        break;
                    case "starlv":
                        {
                            sd.starlv = val;
                        }
                        break;
                    case "addhp":
                        {
                            sd.addhp = val;
                        }
                        break;
                    case "addhurt":
                        {
                            sd.addhurt = val;
                        }
                        break;
                    case "minlv":
                        {
                            sd.minlv = val;
                        }
                        break;
                    case "coin":
                        {
                            sd.coin = val;
                        }
                        break;
                    case "crystal":
                        {
                            sd.crystal = val;
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
            if (strongDic.TryGetValue(sd.pos, out dic))
            {
                dic.Add(sd.starlv, sd);
            }
            else
            {
                dic = new Dictionary<int, StrongCfg>();
                dic.Add(sd.starlv, sd);
                strongDic.Add(sd.pos, dic);
            }
        }
    }


    public StrongCfg GetStrongCfg(int pos,int starLv)
    {
        StrongCfg c = null;
        Dictionary<int, StrongCfg> dic = null;
        if (strongDic.TryGetValue(pos, out dic))
        {
            if (dic.ContainsKey(starLv))
            {
                c = dic[starLv];
            }
        }
        return c;
    }

    public int GetPropAddPreLv(PosType posType, int starLv, PropType propType)
    {

        int pos = (int)posType;
        int prop = (int)propType;
        Dictionary<int, StrongCfg> cfgDic=null;
        int propSum = 0;
        if (strongDic.TryGetValue(pos, out cfgDic))

        {
            for (int i = 0; i < starLv; i++)
            {
                if (i < starLv)
                {
                    StrongCfg cfg = null;
                    if (cfgDic.TryGetValue(i, out cfg))
                    {
                        switch (propType)
                        {
                            case PropType.Hp: propSum += cfg.addhp; break;
                            case PropType.Hurt: propSum += cfg.addhurt; break;
                            case PropType.Def: propSum += cfg.adddef; break;
                            default: break;
                        }
                    }

                }
            }
        }


        return propSum;
    }



    #endregion

    #region taskReward


    public List<TaskRewardData> taskRewardLst = new List<TaskRewardData>();
    Dictionary<int, TaskRewardCfg> taskRewardDic = new Dictionary<int, TaskRewardCfg>();
    
    private void InitTaskRewardCfg(string path)
    {
        TextAsset xml = Resources.Load<TextAsset>(path);
        XmlNodeList nodLst = GetListFromTextAsset(xml);
        if (nodLst != null)
        {
            // <taskName>智者点拨</taskName>
            // < count > 1 </ count >
            // < exp > 1130 </ exp >
            // < coin > 1280 </ coin >
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
                        case "taskName":
                            {
                                c.taskName =e.InnerText;
                            }
                            break;
                        case "count":
                            {
                                c.count = int.Parse(e.InnerText);
                            }
                            break;
                        case "coin":
                            {
                                c.coin =  int.Parse(e.InnerText);
                            }
                            break;
                        case "exp":
                            {
                                c.exp = int.Parse(e.InnerText);
                            }
                            break;

                        default:break;
                    }

                }
                //
                taskRewardDic.Add(ID, c);

            }
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

}


#region 强化
public enum PosType
{
    Head,
    Body,
    Waist,
    Hands,
    Leg,
    Feet
}

public enum PropType
{
    Hurt,
    Hp,
    Def,
}
#endregion
