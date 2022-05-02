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
    /// <summary>不停查询进度</summary>
    Action prgCB;

    Dictionary<string, AudioClip> adDict = new Dictionary<string, AudioClip>();


    public List<string> surnameLst= new List<string>();
    public List<string> manLst= new List<string>();
    public List<string> womanLst= new List<string>();
    void Update()
    {
        if (prgCB != null)
        {

            prgCB();
        }

    }


    /// <summary>
    /// 初始化服务
    /// </summary>
    public void InitSvc()
    {
        Instance = this;
        InitRDNameCfgs();
        Debug.Log("Init Res");
    }




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

    /// <summary>
    /// 初始化随机名字的配置文件
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void InitRDNameCfgs()
    {
        TextAsset xml = Resources.Load<TextAsset>(PathDefine.RDNameCfg);
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



    XmlNodeList GetListFromTextAsset(TextAsset xml)
    {
        if (!xml)
        {
            Debug.Log("xml file:" + PathDefine.RDNameCfg + "not exist");
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


    public string GetRDName(bool man=true)
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
}