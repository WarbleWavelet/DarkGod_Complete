/****************************************************
    文件：Dynamic.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/27 20:30:51
	功能：动态UI界面
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicWnd : WindowRoot
{

    [Header("提示信息")]
    public Animation tipsAni;
    public Text txtTips;
    /// <summary>每个人都有份，慢慢演；不初始化报空指针</summary>
    public Queue<string> tipsQuene = new Queue<string>();
    /// <summary>有没有人</summary>
    public  bool isTipsShow=false;

    [Header("血条相关")]
   
    /// <summary>挂靴调的父节点</summary> 
    public Transform hpItmRoot;
    public AnimationClip playerDodgeClip;  
    public Animation playerDodgeAni;  
    public Dictionary<string, ItemEntityHp> hpItemDic=new Dictionary<string, ItemEntityHp>();
    public void Init()
    {
        InitWnd();
        SetActive(txtTips, false);
    }


    void Update()
    {
        if (tipsQuene.Count > 0 && isTipsShow == false)
        {
            
            lock (tipsQuene )
            {
                isTipsShow = true;
                string tips = tipsQuene.Dequeue();
                SetTips(tips);
            }

        }
    }


    protected override void InitWnd()
    { 
        base.InitWnd();
    
    }




  
    IEnumerator AniPlayDone(float sec,Action cb)
    {

        yield return   new WaitForSeconds(sec);
       // yield return new WaitForSeconds(sec);
        if (cb != null)
        {
            cb();
        }


    }

    public void AddTips(string tips)
    {
        lock (tipsQuene)
        {
            tipsQuene.Enqueue(tips);

        }
    }



    #region 血条
    public void AddHpItemInfo(Transform t, Transform hpRoot, string mName, int hp)
    {
        ItemEntityHp item = null;
        if (hpItemDic.TryGetValue(mName, out item))
        {
            return;
        }
        else
        {
            GameObject go = resSvc.LoadPrefab(PathDefine.ItemEntityHp, true);
            go.transform.SetParent(hpItmRoot);
            go.transform.localPosition = new Vector3(-1000, 0, 0);//看不到

            
            item = go.GetComponent<ItemEntityHp>();
            item.InitItemHpInfo(t, hpRoot, hp);
            hpItemDic.Add(mName, item);

        }
    }

    public void RemoveHpItemInfo(string mName)
    {
        ItemEntityHp item = null;
        if (hpItemDic.TryGetValue(mName, out item))
        {
            Destroy(item.gameObject);
            hpItemDic.Remove(mName);
        }
    
    }


    /// <summary>
    /// 清空数据，销毁go
    /// </summary>
    public void ClearHpItemInfo()
    {
        foreach (var item in hpItemDic)
        {
            Destroy(item.Value.gameObject);
        }
        hpItemDic.Clear();
    }

    public void SetHurt(string mName, int hp)
    {
        ItemEntityHp item = null;
        if (hpItemDic.TryGetValue(mName, out item))
        {
            item.SetHurt(hp);
        }
    }

    public void SetCritical(string mName, int hp)
    {
        ItemEntityHp item = null;
        if (hpItemDic.TryGetValue(mName, out item))
        {
            item.SetCritical(hp);
        }
    }

    public void SetDodge(string mName)
    {
        ItemEntityHp item = null;
        if (hpItemDic.TryGetValue(mName, out item))
        {
            item.SetDodge();
        }
    }



    internal void SetHpVal(string mName, int oldVal, int newVal)
    {
        ItemEntityHp item = null;
        if (hpItemDic.TryGetValue(mName, out item))
        {
            item.SetHPVal(oldVal, newVal);
        }
    }

    public void SetPlayerDodge()
    {
        playerDodgeAni.Stop();
        playerDodgeAni.Play();
    }
    #endregion


    #region 动画
    public void SetTips(string tips)
    {
        SetActive(txtTips);
        SetText(txtTips, tips);


        AnimationClip clip = tipsAni.GetClip("TipsShowAni");
        tipsAni.Play();
        StartCoroutine(AniPlayDone(clip.length, () =>
        {

            SetActive(txtTips, false);
            isTipsShow = false;
        }));


    }


    #endregion

}