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
        InitMap();
        InitRDNameCfg(PathDefine.RDNameCfg);

        InitGuideCfg(PathDefine.GuideCfg);
        InitStrongCfg(PathDefine.StrongCfg);
        InitTaskRewardCfg(PathDefine.TaskRewardCfg);
        //
        InitSkill();
        
        //

        //
        PECommon.Log("ResSvc Init");
    }

    void Update()
    {
        if (prgCB != null)
        {
            prgCB();
        }
    }

    /// <summary>
    /// 控制顺序
    /// </summary>
    void InitSkill()
    {
        InitSkillActionCfg(PathDefine.SkillActionCfg);
        InitSkillMoveCfg(PathDefine.SkillMoveCfg);
        InitSkillCfg(PathDefine.SkillCfg);
    }


    /// <summary>
    /// 控制顺序
    /// </summary>
    void InitMap()
    {
        InitMonsterCfg(PathDefine.MonsterCfg);

        InitMapCfg(PathDefine.MapCfg);
    }

    #region 调试技能
    internal void ResetSkillCfgs()
    {
        skillDic.Clear();
        skillMoveDic.Clear();
        InitSkillCfg(PathDefine.SkillCfg);
        InitSkillMoveCfg(PathDefine.SkillMoveCfg);
    }
    #endregion


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


    #region 副本地图
    public List<string> mapLst = new List<string>();
    Dictionary<int, MapCfg> mapCfgDic=new Dictionary<int, MapCfg> ();
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
                                c.mainCamPos = XmlElement_ToVector3(e);
                            }
                            break;
                        case "mainCamRote":
                            {
                                c.mainCamRote = XmlElement_ToVector3(e);
                            }
                            break;
                        case "playerBornPos":
                            {
                                c.playerBornPos = XmlElement_ToVector3(e);
                            }
                            break;
                        case "playerBornRote":
                            {
                                c.playerBornRote = XmlElement_ToVector3(e);
                            }
                            break;
                        case "power":
                            {
                                c.power = int.Parse(e.InnerText);
                            }
                            break;
                        case "monsterLst":
                            {
                                c.monsterLst = String_ToMonsterDataLst(e.InnerText) ;
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
                        default:break;
                    }
                    
                }
                //
                mapCfgDic.Add(ID, c);

            }
        }
        //

    }

    public MapCfg GetMapCfg(int ID)
    {
        MapCfg c = null;
        if (mapCfgDic.TryGetValue(ID, out c))
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
                                c.taskName = e.InnerText;
                            }
                            break;
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


    #region 技能
    Dictionary<int, SkillCfg> skillDic = new Dictionary<int, SkillCfg>();
    
    private void InitSkillCfg(string path)
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
                SkillCfg c = new SkillCfg
                {
                    ID = ID
                };
                foreach (XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                        case "skillName":
                            {
                                c.skillName =e.InnerText;
                            }
                            break;
                        case "skillTime":
                            {
                                c.skillTime = int.Parse(e.InnerText);
                            }
                            break;
                        case "aniAction":
                            {
                                c.aniAction =  int.Parse(e.InnerText);
                            }
                            break;
                        case "fx":
                            {
                                c.fx = e.InnerText;
                            }
                            break;
                        case "skillMoveLst":
                            {
                                c.skillMoveLst=String_ToIntList( e.InnerText, '|');
                            }
                            break;
                        case "skillActionLst":
                            {
                                c.skillActionLst =String_ToIntList(e.InnerText, '|');
                            }
                            break;
                        case "skillDamageLst":
                            {
                                c.skillDamageLst = String_ToIntList(e.InnerText, '|');
                            }
                            break;
                        case "dmgType":
                            {
                                /* 作者采用写法
                                if (e.InnerText.Equals("1"))
                                {
                                    c.dmgType = DmgType.AD;
                                }
                                */
                                c.dmgType = (DmgType)int.Parse(e.InnerText);
                            }
                            break;
                        case "cdTime":
                            {
                                c.cdTime = float.Parse(e.InnerText);
                            }
                            break;
                        case "isCombo":
                            {
                                c.isCombo = int.Parse(e.InnerText)==1?true:false;
                            }
                            break;
                        case "isCollide":
                            {
                                c.isCollide = int.Parse(e.InnerText) == 1 ? true : false;
                            }
                            break;
                        case "isBreak":
                            {
                                c.isBreak = int.Parse(e.InnerText) == 1 ? true : false;
                            }
                            break;
                        default:break;
                    }

                }
                //
                skillDic.Add(ID, c);

            }

         
        }  
    }
    public SkillCfg GetSkillCfg(int ID)
    {
        SkillCfg c = null;
        if (skillDic.TryGetValue(ID, out c))
        {
            return c;
        }
        else
        {
            return null;
        }
    }



    #endregion


    #region 技能产生的移动
    Dictionary<int, SkillMoveCfg> skillMoveDic = new Dictionary<int, SkillMoveCfg>();

    private void InitSkillMoveCfg(string path)
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
                SkillMoveCfg c = new SkillMoveCfg
                {
                    ID = ID
                };
                foreach (XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                        case "moveTime":
                            {
                                c.moveTime = int.Parse(e.InnerText);
                            }
                            break;
                        case "moveDis":
                            {
                                c.moveDis = float.Parse(e.InnerText);
                            }
                            break;
                        case "delayTime":
                            {
                                c.delayTime = int.Parse(e.InnerText);
                            }
                            break;
                        default: break;
                    }

                }
                //
                skillMoveDic.Add(ID, c);

            }
        }
    }
    public SkillMoveCfg GetSkillMoveCfg(int ID)
    {
        SkillMoveCfg c = null;
        if (skillMoveDic.TryGetValue(ID, out c))
        {
            return c;
        }
        else
        {
            return null;
        }
    }
    #endregion


    #region 技能Action
    Dictionary<int, SkillActionCfg> skillActionDic = new Dictionary<int, SkillActionCfg>();

    private void InitSkillActionCfg(string path)
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
                SkillActionCfg c = new SkillActionCfg
                {
                    ID = ID
                };
                foreach (XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                        case "delayTime":
                            {
                                c.delayTime = int.Parse(e.InnerText);
                            }
                            break;
                        case "radius":
                            {
                                c.radius = float.Parse(e.InnerText);
                            }
                            break;
                        case "angle":
                            {
                                c.angle = int.Parse(e.InnerText);
                            }
                            break;
                        default: break;
                    }

                }
                //
                skillActionDic.Add(ID, c);
                
            }
        }
    }
    public SkillActionCfg GetSkillActionCfg(int ID)
    {
        SkillActionCfg c = null;
        if (skillActionDic.TryGetValue(ID, out c))
        {
            return c;
        }
        else
        {
            return null;
        }
    }
    #endregion


    #region monster
    Dictionary<int, MonsterCfg> monsterCfgDic = new Dictionary<int, MonsterCfg>();

    private void InitMonsterCfg(string path)
    {
        TextAsset xml = Resources.Load<TextAsset>(path);
        XmlNodeList nodLst = GetListFromTextAsset(xml);
        if (nodLst != null)
        {
            //public string mName;
            //public string resPath;
            for (int i = 0; i < nodLst.Count; i++)
            {
                XmlElement ele = nodLst[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)
                    continue;
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                //nodLst，ID
                MonsterCfg c = new MonsterCfg
                {
                    ID = ID,
                    props=new BattleProps()
                };
                foreach (XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                        case "mName":
                            {
                                c.mName = e.InnerText;
                            }
                            break;
                        case "resPath":
                            {
                                c.resPath = e.InnerText;
                            }
                            break;
                        case "hp":
                            {
                               c.props.hp =int.Parse( e.InnerText );
                            }
                            break;
                        case "ad":
                            {
                                c.props.ad = int.Parse(e.InnerText);
                            }
                            break;
                        case "ap":
                            {
                                c.props.ap = int.Parse(e.InnerText);
                            }
                            break;
                        case "addef":
                            {
                                c.props.addef = int.Parse(e.InnerText);
                            }
                            break;
                        case "apdef":
                            {
                                 c.props.apdef = int.Parse(e.InnerText);
                            }
                            break;
                        case "dodge":
                            {
                                c.props.dodge = int.Parse(e.InnerText);
                            }
                            break;
                        case "critical":
                            {
                                c.props.critical = int.Parse(e.InnerText);
                            }
                            break;
                        case "pierce":
                            {
                                c.props.pierce = int.Parse(e.InnerText);
                            }
                            break;
                        case "skillID":
                            {
                                c.skillID = int.Parse(e.InnerText);
                            }
                            break;
                        case "atkDis":
                            {
                                c.atkDis = float.Parse(e.InnerText);
                            }
                            break;
                        case "isStop":
                            {
                                c.isStop = (e.InnerText).Equals("1");
                            }
                            break;
                        case "mType":
                            {
                                c.mType = (MonsterType)(int.Parse(e.InnerText));
                            }
                            break;
                        default: break;
                    }

                }
                //
                monsterCfgDic.Add(ID, c);

            }
        }
    }
    public MonsterCfg GetMonsterCfg(int ID)
    {
        MonsterCfg c = null;
        if (monsterCfgDic.TryGetValue(ID, out c))
        {
            return c;
        }
        else
        {
            return null;
        }
    }




    /**
        < monsterLst >
            #|1001,-4.39,13.14,3.79,-50
            | 1001,-7.55,13.1, 3,0
            #|1001,18.86,13.6,3.7,-107.3
            | 1001,14.35,13.35,5.95,-117.4
            | 1001,15.11,13.35,1.63,-66.1
            #|1001,18.16,8.8,32,188
            | 1001,11.8,8.8,30.8,145.5
            | 1001,15.38,8.8,40.7,173.3
            | 1001,9,8.9,38.6,145.5
            | 2001,11.4,8.85,41,142
        </ monsterLst >
    **/

    List<MonsterData> String_ToMonsterDataLst(string text)
    {
        List<string> waveLst = String_SplitToStringLst(text,'#');
        List<MonsterData> lst=new List<MonsterData>();
        //
        for (int i = 0; i < waveLst.Count; i++)
        {

            List<string> indexLst = String_SplitToStringLst(waveLst[i], '|');

            for (int j = 0; j < indexLst.Count; j++)
            {
                List<string > dataLst = String_SplitToStringLst( indexLst[j], ',');
               
                int ID = int.Parse(dataLst[0]);
                float x = float.Parse(dataLst[1]);
                float y = float.Parse(dataLst[2]);
                float z = float.Parse(dataLst[3]);
                float r = float.Parse(dataLst[4]);
                //
                MonsterData data = new MonsterData
                {
                    ID = ID,
                    mWave = i,
                    mIndex = j,
                    mBornPos = new Vector3(x, y, z),
                    mBornRot = new Vector3(0f, r, 0f),
                    mCfg = GetMonsterCfg(ID),
                    lv= int.Parse(dataLst[5])

            };
                lst.Add(data);
            }
        }

        return lst;
    }


    #endregion


    #region Common
    /// <summary>
    /// 字符串数组转Vector3
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    Vector3 XmlElement_ToVector3(XmlElement e)
    {
        string[] valArr = e.InnerText.Split(',');
        return new Vector3(float.Parse(valArr[0]), float.Parse(valArr[1]), float.Parse(valArr[2]));
    }
    XmlNodeList GetListFromTextAsset(TextAsset xml)
    {
        if (!xml)
        {
            PECommon.Log("xml file:" + PathDefine.RDNameCfg + "not exist", LogType.Error);
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


    List<string> String_SplitToStringLst(string text, char split)
    {
        string[] arr = text.Split(split);
        List<string> lst = new List<string>();
        //
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == "")
            {
                continue;
            }
            lst.Add(arr[i]);
        }
        return lst;
    }


    /// <summary>
    /// 将字符串根据分隔符转为数组
    /// </summary>
    /// <param name="text"></param>
    /// <param name="split"></param>
    /// <returns></returns>
    List<int> String_ToIntList(string text, char split)
    {
        string[] arr = text.Split(split);
        List<int> lst = new List<int>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == "")
            {
                continue;

            }
            lst.Add( int.Parse(arr[i]) );
        }

        return lst;
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
