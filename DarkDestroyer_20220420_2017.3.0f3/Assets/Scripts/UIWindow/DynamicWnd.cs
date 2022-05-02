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
    public Animation tipsAni;
    public Text txtTips;
    /// <summary>每个人都有份，慢慢演；不初始化报空指针</summary>
    public Queue<string> tipsQuene = new Queue<string>();
    /// <summary>有没有人</summary>
    public  bool isTipsShow=false;

   public void Init()
    {
        InitWnd();
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
        SetActive(txtTips,false);
    }

     void SetTips(string tips)
    {
        SetActive(txtTips);
        SetText(txtTips,tips);

       
        AnimationClip clip = tipsAni.GetClip("TipsShowAni");
        tipsAni.Play();
        StartCoroutine(AniPlayDone(clip.length, () =>{ 
            SetActive(txtTips,false); 
            isTipsShow = false; 
        }));
        
    }


  
    IEnumerator AniPlayDone(float sec,Action cb)
    {
        yield return new WaitForSeconds(sec);
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
}