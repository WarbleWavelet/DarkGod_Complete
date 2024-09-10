/****************************************************
    文件：SceneStart.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/1 22:9:40
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using static ExtendDesignPattern;

namespace Common.CharacterRelationship_Sinmple
{

    public class GameMgr : SingletonLazyNotMono<GameMgr>
    {

        public ImageNode FromNode;
        public ImageNode ToNode;
        public bool LineDrawing 
        {
            set { }
            get 
            { 
                if (FromNode == null) 
                    return false;
                return true;
            }
        }
    }
}



