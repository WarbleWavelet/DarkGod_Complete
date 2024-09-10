/****************************************************
    文件：LoopDragonAnimation.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/27 19:4:31
	功能：龙一直飞
*****************************************************/

using UnityEngine;

public class LoopDragonAnimation : MonoBehaviour 
{
    public Animation ani;

    void Awake()
    {
        ani=GetComponent<Animation>();
        InvokeRepeating("PlayDragonANimation",0,20);
    }

    void PlayDragonANimation()
    {
        if (ani != null)
        { 
            ani.Play();
        }
    }
}