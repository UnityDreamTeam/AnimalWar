using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFinder<NodeType>
{
    List<NodeType> GetPath(IGraph<NodeType> graph, NodeType startNode, NodeType endNode, int maxiterations = 1000);
}
