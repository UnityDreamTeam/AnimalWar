using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<NodeType>
{
    private NodeType position;
    private int distnace;
    private NodeType father;//Father vertex, in order to reconstruct the path

    public Node(Node<NodeType> other) : this(other.Position, other.Distnace, other.Father) { }
    public Node(NodeType position, int distance, NodeType father)
    {
        this.Position = position;
        this.Distnace = distance;
        this.Father = father; //No father
    }

    public int Distnace { get => distnace; set => distnace = value; }
    public NodeType Position { get => position; set => position = value; }
    public NodeType Father { get => father; set => father = value; }
}
