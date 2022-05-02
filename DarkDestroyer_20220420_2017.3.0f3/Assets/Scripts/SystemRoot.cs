/****************************************************
    文件：SystemRoot.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/27 18:26:15
	功能：业务系统基类
*****************************************************/

using UnityEngine;

public class SystemRoot : MonoBehaviour 
{
    public ResSvc resSvc = null;
    public AudioSvc audioSvc = null;

    public virtual void InitSys()
    {
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }
}