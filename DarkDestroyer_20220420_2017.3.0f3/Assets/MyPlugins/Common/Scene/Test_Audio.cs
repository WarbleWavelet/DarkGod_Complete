/****************************************************
    文件：Test_Audio.cs
	作者：lenovo
    邮箱: 
    日期：2023/12/7 20:11:46
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Music = ExtendAudio.Music;
using Sound = ExtendAudio.Sound;
 

public class Test_Audio : MonoBehaviour
{

	void Start()
	{
		if (false)
		{
			//Music music = new Music(GetComponent<AudioSource>());
			//music.Play("Audio/BGM/Battle_BGM");		
		}
		else
		{ 
			Sound sound= new Sound(GetComponent<AudioSource>());
			sound.Play("Audio/Buttle/Power");		
		}


	}
}



