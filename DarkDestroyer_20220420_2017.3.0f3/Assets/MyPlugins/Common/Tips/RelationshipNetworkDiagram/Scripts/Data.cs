using System.Collections.Generic;

namespace Common.RelationshipNetworkDiagram
{

    public class Node
    {
        public string Name;
        public List<Node> Children;
    }

    public class Line
    {
        public string Name;
        public Node Node_1;
        public Node Node_2;
    }
}
