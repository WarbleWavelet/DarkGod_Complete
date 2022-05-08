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
        PECommon.Log("Init Res",LogType.Log);
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
                adDict.Add(path,au);
            }
        }

  
      
        return au;
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
                MapCfg mc=new MapCfg 
                { 
                    ID = ID
                };
                foreach (XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                        case "mapName":
                            {
                                mc.mapName= e.InnerText;
                            }
                            break;
                        case "sceneName":
                            {
                                mc.sceneName = e.InnerText;
                            }
                            break;
                        case "mainCamPos":
                            {
                                mc.mainCamPos = ParseVector3ByXmlElement(e);
                            }
                            break;
                        case "mainCamRote":
                            {
                                mc.mainCamRote = ParseVector3ByXmlElement(e);
                            }
                            break;
                        case "playerBornPos":
                            {
                                mc.playerBornPos = ParseVector3ByXmlElement(e);
                            }
                            break;
                        case "playerBornRote":
                            {
                                mc.playerBornRote = ParseVector3ByXmlElement(e);
                            }
                            break;
                        default:
                            {

                            }
                            break;
                    }
                    
                }
                //
                mapCfgDataDic.Add(ID, mc);

            }
        }
        //

    }


    public MapCfg GetMapDataCfg(int ID)
    {
        MapCfg mc = null;
        if (mapCfgDataDic.TryGetValue(ID, out mc))
        {
            return mc;
        }
        else
        {
            return null;
        }


      
    }
    #endregion


    #region Common
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


}