/****************************************************
    文件：CharacterRelationship.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/1 21:45:19
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Common.CharacterRelationship_Sinmple
{
    //挂载在节点上的脚本，用于注册节点和交互事件
    public class ImageNode : MonoBehaviour
    {
        public Node ImgNode;

        public void Init(string name, UnityAction action)
        {
            ImgNode = new Node();
            ImgNode.Name = name;
            ImgNode.Children = new List<Node>();
            transform.Find(GameObjectName.Text).GetComponent<Text>().text = name;
        }


    }

}


