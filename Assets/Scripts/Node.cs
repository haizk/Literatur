using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;
    public int gridY;
    
    public bool isWall;
    public Vector3 position;

    public Node parent;

    public int gCost;
    public int hCost;

    public int fCost { get { return gCost; } }

    public Node(bool isWall, Vector3 position, int gridX, int gridY)
    {
        this.gridX = gridX;
        this.gridY = gridY;
        this.isWall = isWall;
        this.position = position;    
    }
}
