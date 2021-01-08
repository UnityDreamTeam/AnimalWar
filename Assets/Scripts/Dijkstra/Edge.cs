using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    Vector3Int other;//The node in the other side of the edge
    int weight;

    public Edge(Vector3Int other, int weight)
    {
        this.Other = other;
        this.Weight = weight;
    }

    public int Weight { get => weight; set => weight = value; }
    public Vector3Int Other { get => other; set => other = value; }
}
