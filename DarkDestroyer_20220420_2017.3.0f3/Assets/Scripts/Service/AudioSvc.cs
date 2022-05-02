/****************************************************
    文件：AudioSvc.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/27 17:25:1
	功能：声音播放服务
*****************************************************/

using UnityEngine;

public class AudioSvc : MonoBehaviour
{

    public static AudioSvc Instance;
    public AudioSource bgAudio;
    public AudioSource uiAudio;


    public void InitSvc()
    {
        Debug.Log("AudioSvc");
        Instance = this;
        JustForSee();

    }

    /// <summary>
    /// 播放BGM
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isLoop"></param>
    public void PlayBg(string name, bool isLoop = true)
    {
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        if (bgAudio.clip == null || bgAudio.clip.name != name)
        {
            bgAudio.clip = audio;
            bgAudio.loop = isLoop;
            bgAudio.Play();
        }
    }

    public void PlayUIAudio(string name)
    {
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        if (uiAudio.clip == null || uiAudio.clip.name != name)
        {
            uiAudio.clip = audio;
            uiAudio.Play();
        }
        else//重复点击
        {
            uiAudio.Play();
        }
    }

    void JustForSee()
    {
        bgAudio=transform.Find("BGAudio").GetComponent<AudioSource>();
        uiAudio = transform.Find("UIAudio").GetComponent<AudioSource>();

    }
}