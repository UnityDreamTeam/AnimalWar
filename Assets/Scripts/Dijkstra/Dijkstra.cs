using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dijkstra<NodeType> : IPathFinder<NodeType>
{
	private void ConstructPath(List<Node<NodeType>> distances, NodeType startNode, Node<NodeType> end_node,  List<NodeType> outputPath)
    {
		//Construct the path at the end
		while (!end_node.Position.Equals(startNode))
		{
			outputPath.Add(end_node.Position);
			//Jump to the father
			NodeType father = end_node.Father;

			//find the father node and continue to the next node
			for (int j = 0; j < distances.Count; j++)
			{
				if (distances[j].Position.Equals(father))
				{
					end_node = distances[j];
					break;
				}
			}
		}

		outputPath.Add(startNode);//Add the source node of the path also
		outputPath.Reverse();//Reverse the path, since we constructed it from end to start
	}
	private void FindPath(
            IGraph<NodeType> graph,
			NodeType startNode, NodeType endNode,
            List<NodeType> outputPath, int maxiterations = 1000)
    {
		//Used to insert new nodes into the dynamic list of vertexes
		HashSet<NodeType> closedSet = new HashSet<NodeType>();

		//List of dynamicly generated nodes with their distances
		List<Node<NodeType>> nodes = new List<Node<NodeType>>();

		//Priority Queue to remove every iteration the neighbor with smallest weight
		List<Node<NodeType>> pq = new List<Node<NodeType>>();

		//Initialize the current node to be starting node. it has no father and distance to itself is zero
		NodeType father = (NodeType)Activator.CreateInstance(typeof(NodeType), int.MaxValue, int.MaxValue, int.MaxValue);
		Node<NodeType> currentNode = new Node<NodeType>(startNode, 0, father);

		//Add the first node to the list of existing nodes.
		nodes.Add(currentNode);

		//Limit amount of iterations
		for (int k = 0; k < maxiterations; ++k)
		{
			//Add new neighbors if we find new vertexes
			foreach (var neighbor in graph.Neighbors(currentNode.Position))
			{
				if (!closedSet.Contains(neighbor))
				{
					father = (NodeType)Activator.CreateInstance(typeof(NodeType), int.MaxValue, int.MaxValue, int.MaxValue);
					Node<NodeType> newNode = new Node<NodeType>(neighbor, int.MaxValue, father);
					nodes.Add(newNode);
					pq.Add(newNode);

					closedSet.Add(neighbor);
				}
			}

			//Now we can run the dijkstra algrithm
			foreach (var edge in graph.Edges(currentNode.Position))
			{
				//Find the other node
				Node<NodeType> otherNode = null;
				for (int j = 0; j < nodes.Count; j++)
				{
					if (edge.Other.Equals(nodes[j].Position))
					{
						otherNode = nodes[j];
						break;
					}
				}

				//Update father, if we found a new one
				if (currentNode.Distnace + edge.Weight < otherNode.Distnace)
				{
					otherNode.Father = currentNode.Position;
				}

				//Update lowest distnace
				otherNode.Distnace = Mathf.Min(currentNode.Distnace + edge.Weight, otherNode.Distnace);

				var item = pq.SingleOrDefault(node => node.Position.Equals(otherNode.Position));
				if(item != null)
                {
					pq.Remove(item);
					pq.Add(new Node<NodeType>(otherNode));
				}
			}

			//Find node with minimum distance and remove it, if list isn't empty
			if (pq.Any())
			{
				currentNode = pq.Aggregate((node1, node2) => node1.Distnace < node2.Distnace ? node1 : node2);
				pq.Remove(currentNode);
			}

            if (currentNode.Position.Equals(endNode))
            {
				//We found the target node, now construct the path
				ConstructPath(nodes, startNode, currentNode, outputPath);
				break;
            }
		}
	}

    public List<NodeType> GetPath(IGraph<NodeType> graph, NodeType startNode, NodeType endNode, int maxiterations = 1000)
    {
        List<NodeType> path = new List<NodeType>();
        FindPath(graph, startNode, endNode, path, maxiterations);

        return path;
    }
}