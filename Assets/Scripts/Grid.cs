using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform startPosition;
    public LayerMask wallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float distance;

    Node[,] grid;
    public List<Node> finalPath;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        for (int x=0; x<gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool wall = true;

                if (Physics2D.OverlapCircle(worldPoint, nodeRadius - 0.1f, wallMask)) {
                    wall = false;
                }

                Debug.Log(x + " " + y);
                grid[x, y] = new Node(wall, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 worldPos)
    {
        float xPoint = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float yPoint = (worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y;

        xPoint = Mathf.Clamp01(xPoint);
        yPoint = Mathf.Clamp01(yPoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xPoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPoint);

        return grid[x, y];
    }

    public List<Node> GetNeighbouringNodes(Node node)
    {
        List<Node> NeighbourList = new List<Node>();
        int xCheck;
        int yCheck;

        //Kanan
        xCheck = node.gridX + 1;
        yCheck = node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighbourList.Add(grid[xCheck, yCheck]);
            }
        }
        
        // Kiri
        xCheck = node.gridX - 1;
        yCheck = node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighbourList.Add(grid[xCheck, yCheck]);
            }
        }

        // Atas
        xCheck = node.gridX;
        yCheck = node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighbourList.Add(grid[xCheck, yCheck]);
            }
        }
        
        // Bawah
        xCheck = node.gridX;
        yCheck = node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighbourList.Add(grid[xCheck, yCheck]);
            }
        }

        return NeighbourList;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y));

        if (grid != null) {
            foreach (Node node in grid) {
                if (node.isWall) {
                    Gizmos.color = Color.black;
                } else {
                    Gizmos.color = Color.yellow;
                }

                if (finalPath != null) {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawCube(node.position, Vector3.one * (nodeDiameter - distance));
            }
        }
    }
}
