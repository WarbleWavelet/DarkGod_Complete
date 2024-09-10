using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Common.RelationshipNetworkDiagram
{ 
public class Controller : MonoBehaviour
{
    public GameObject LinePrefab;
    public GameObject NodePrefab;
    public Transform Canvas;
    public Text ShowText;

    public ImageNode CurrentSelNode;

    bool isDrawLine = false;

    int num = 0;

    List<UILineRenderer> lines;
    Dictionary<Node, ImageNode> nodes = new Dictionary<Node, ImageNode>();

    void Start()
    {
        lines = new List<UILineRenderer>();
        nodes = new Dictionary<Node, ImageNode>();
        InitEvent();
    }


    void InitEvent()
    {
        Canvas.Find("OtherUGUI/CrtButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            ImageNode node = Instantiate(NodePrefab, Canvas.Find("Nodes"), false).GetComponent<ImageNode>();
            node.Init($"Node{num + 1}", () => NodeSelectEvent(node));
            node.gameObject.name = $"Node{num + 1}";
            nodes.Add(node.ImgNode, node);
            num++;
        });
        Canvas.Find("OtherUGUI/DrawButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            isDrawLine = !isDrawLine;
            Canvas.Find("OtherUGUI/DrawButton/Text").GetComponent<Text>().text = isDrawLine ? "DrawLine:ON" : "DrawLine:OFF";
        });
        Canvas.Find("OtherUGUI/DeleteButton").GetComponent<Button>().onClick.AddListener(DeleteNode);
    }


    // Update is called once per frame
    void Update()
    {
        UpdateLine();
        HighColorNodes();
        HighColorLines();
    }


    private void NodeSelectEvent(ImageNode imageNode)
    {
        if (imageNode == CurrentSelNode)
            return;
        if (isDrawLine&&CurrentSelNode!=null)
        {
            AddALine(imageNode.ImgNode, CurrentSelNode.ImgNode);
        }
        if (CurrentSelNode != null)
        {
            CurrentSelNode.GetComponent<Image>().color = Color.white;
            SetNormalNodes();
        }
        imageNode.GetComponent<Image>().color = Color.green;
        CurrentSelNode = imageNode;
        ShowText.text = imageNode.ImgNode.Name;
    }

    private void DeleteNode()
    {

#if NET_4_7_OR_NEWER
            if (CurrentSelNode is null)
            return;
#endif

        for(int i = 0; i < lines.Count;)
        {
            if (lines[i].line.Node_1 == CurrentSelNode.ImgNode || lines[i].line.Node_2 == CurrentSelNode.ImgNode)
            {
                Destroy(lines[i].gameObject);
                lines.RemoveAt(i);
                
                               
            }
            else
            {
                i++;
            }
        }
        foreach (var item in CurrentSelNode.ImgNode.Children)
        {
            item.Children.Remove(CurrentSelNode.ImgNode);
        }
        nodes.Remove(CurrentSelNode.ImgNode);
        Destroy(CurrentSelNode.gameObject);
        CurrentSelNode = null;
        ShowText.text = "NULL";
    }

    private void HighColorNodes()
    {

        if (CurrentSelNode == null)
        {
            foreach (var item in nodes)
            {
                item.Value.GetComponent<Image>().color = Color.white;
            }
            return;
        }
        foreach (var item in CurrentSelNode.ImgNode.Children)
        {
            nodes[item].GetComponent<Image>().color = Color.yellow;
        }
    }

    private void HighColorLines()
    {
        if (CurrentSelNode == null)
        {
            foreach (var item in lines)
            {
                item.color = Color.white;
            }
            return;
        }
        foreach (var item in lines)
        {
            if (item.line.Node_1 == CurrentSelNode.ImgNode || item.line.Node_2 == CurrentSelNode.ImgNode)
                item.color = Color.yellow;
            else
                item.color = Color.white;
        }
    }


    private void SetNormalNodes()
    {
        if (CurrentSelNode == null)
            return;
        foreach (var item in CurrentSelNode.ImgNode.Children)
        {
            nodes[item].GetComponent<Image>().color = Color.white;
        }
    }

    /// <summary>
    /// 重新绘制线条
    /// </summary>
    private void UpdateLine()
    {
#if NET_4_7_OR_NEWER
            if (lines is null || lines.Count == 0)
            return;
#endif

            Line line;
        foreach (var i in lines)
        {
            line = i.line;
            i.SetPositions(nodes[line.Node_1].transform.localPosition, nodes[line.Node_2].transform.localPosition);
        }
    }

    private bool isHaveLine(Node node1, Node node2)
    {
        if (nodes[node1].ImgNode.Children.Contains(node2))
            return true;
        return false;
    }

    /// <summary>
    /// 注册一条线
    /// </summary>
    private void AddALine(Node node1, Node node2)
    {
        if (isHaveLine(node1, node2))
            return;
        Line line = new Line()
        {
            Node_1 = node1,
            Node_2 = node2,
        };
        UILineRenderer uILineRenderer = Instantiate(LinePrefab, Canvas.Find("Lines"), false).GetComponent<UILineRenderer>();
        uILineRenderer.line = line;
        uILineRenderer.gameObject.name = $"Node({node1.Name} with {node2.Name})";
        lines.Add(uILineRenderer);
        nodes[node1].AddNodeIntoChildren(node2);
        nodes[node2].AddNodeIntoChildren(node1);
    }
}
      }