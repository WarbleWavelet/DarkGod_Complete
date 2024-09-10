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
using Random = UnityEngine.Random;

namespace Common.CharacterRelationship_Sinmple
{
    //基本的数据结构
    public class Node
    {
        public string Name;
        public List<Node> Children;
    }

    public class Line
    {
        public string Name;
        public Node FromNode;
        public Node ToNode;
    }
}


