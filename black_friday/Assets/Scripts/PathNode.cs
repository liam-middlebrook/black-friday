using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathNode : MonoBehaviour, IComparable<PathNode>
{
    //Total distance
    public float F { get; set; }

    //Actual distance from start
    public float G { get; set; }

    //Hueristic distance to end
    public float H { get; set; }

    //Parent vertex on the path per Pathfinder ID
    public Dictionary<int, PathNode> Parent = new Dictionary<int,PathNode>();

    //Node Placement in Lists per Pathfinder ID
    public Dictionary<int, bool> InOpenList = new Dictionary<int,bool>();
    public Dictionary<int, bool> InClosedList = new Dictionary<int,bool>();

    // List of Adjacent Vertices
    public List<PathNode> AdjacentVertices;

    // Compares the distance that nodes take up on the path.
    public int CompareTo(PathNode o)
    {
        return (int)(F - o.F);
    }

    // Allows for PathNodes to be implicitly casted as Vector3s that represent position
    public static implicit operator Vector3(PathNode n)
    {
        return n.transform.position;
    }

    #region GraphBuilder

    // A static list of all pathnodes that exist
    public static List<PathNode> pathNodes = new List<PathNode>();
    
    // On Awake Add PathNode to static list
    public void Awake()
    {
        pathNodes.Add(this);
    }

    // Add a pathfinder to all nodes and set default values
    // inside all dictionaries for that Pathfinder ID
    public static void AddPathfinder(int pathfinder)
    {
        for (int i = 0; i < pathNodes.Count; i++)
        {
            pathNodes[i].InClosedList[pathfinder] = false;
            pathNodes[i].InOpenList[pathfinder] = false;
            pathNodes[i].Parent[pathfinder] = null;
        }
    }

    #endregion
}