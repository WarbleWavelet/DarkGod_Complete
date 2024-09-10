/****************************************************
    文件：IControllerProcessor.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/8 23:21:19
	功能：
*****************************************************/

using UnityEngine;

public interface IControllerProcessor
{
    Controller Ctrl { get; set; }
    void SetAniBlend(float value);
    void SetAniAction(int value);

    AnimationClip[] GetAniClips();
}