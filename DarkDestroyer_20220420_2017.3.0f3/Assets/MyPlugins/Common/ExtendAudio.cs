/****************************************************
    文件：ExtendAudio.cs
	作者：lenovo
    邮箱: 
    日期：2023/12/7 19:42:20
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
                                                     
public static partial class ExtendAudio
{

    public class Src
    {
       protected AudioSource _src;

        public Src(AudioSource src)
        {
            _src = src;
            _src.playOnAwake = false;

            Debug.Log("Src");
        }
        public void Off()
        {
            _src.Pause();
            _src.mute = true;
        }

        public void On()
        {
            _src.UnPause();
            _src.mute = false;
        }

        /// <summary>
        /// 后缀.ogg跑不了，所以是这个这个后缀，也不要加.ogg在形参上
        /// </summary>
        public void Play(string path)
        {
            AudioClip clip = Resources.Load<AudioClip>(path);
            _src.clip = clip;
            _src.Play();
        }

        public void Play()
        {
            _src.Play();
        }

        public void Stop()
        {
            _src.Stop();
        }
        public void Pause()
        {
            _src.Pause();
        }
        public void Resume()
        {
            _src.UnPause();
        }
    }


    public class Music:Src
    { 

        public  Music(AudioSource src):base(src)
        {
            _src.loop = true;
        }
    }
    public  class Sound: Src 
    {

        public Sound(AudioSource src) :base(src)
        {
            _src.loop = false;
        }

    }

    public class AudioMgr
    {
        Music _music;
        Sound _sound;

        public AudioMgr(AudioSource src1, AudioSource src2)
        {
            _music = new Music(src1);
            _sound = new Sound(src2);
        }
    }

}




