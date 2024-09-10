using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Common.RelationshipNetworkDiagram
{
    public class ImageNode : MonoBehaviour
    {
        public Node ImgNode;

        public void Init(string name, UnityAction action)
        {
            ImgNode = new Node();
            ImgNode.Name = name;
            ImgNode.Children = new List<Node>();
            transform.Find("Text").GetComponent<Text>().text = name;
            transform.GetComponent<Button>().onClick.AddListener(action);
        }

        public void AddNodeIntoChildren(Node node)
        {
            if (ImgNode.Children.Contains(node))
            {
                return;
            }
            ImgNode.Children.Add(node);
        }
    }
}
