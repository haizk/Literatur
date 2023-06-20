using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    public Transform startPosition;
    public Transform targetPosition;
    public float period = 0.0f;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        if (period > 5f) {
            if (grid.finalPath != null) grid.finalPath.Clear();
            FindPath(startPosition.position, targetPosition.position);
            period = 0;
        }
        period += Time.deltaTime;
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Debug.Log("start");
        Node startNode = grid.NodeFromWorldPosition(startPos);
        Node targetNode = grid.NodeFromWorldPosition(targetPos);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);

        Debug.Log("loop");
        while (openList.Count > 0) {
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++) {
                if (openList[i].fCost < currentNode.fCost || (openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)) {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode) {
                Debug.Log("wtf? " + currentNode.gridX + " " + currentNode.gridY);
                GetFinalPath(startNode, targetNode);
                break;
            }

            foreach (Node neighbourNode in grid.GetNeighbouringNodes(currentNode)) {
                if (!neighbourNode.isWall || closedList.Contains(neighbourNode)) {
                    continue;
                }
                int moveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighbourNode);
                //Debug.Log("x:" + neighbourNode.gridX + " y:" + neighbourNode.gridY + (currentNode == targetNode ? "LOL" : "") + " cost:" + moveCost);
                if (moveCost < neighbourNode.gCost || !openList.Contains(neighbourNode)) {
                    neighbourNode.gCost = moveCost;
                    neighbourNode.hCost = GetManhattenDistance(neighbourNode, targetNode);
                    neighbourNode.parent = currentNode;

                    if (!openList.Contains(neighbourNode)) {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        openList.Clear();
        closedList.Clear();
        Debug.Log("KELAR");
    }

    void GetFinalPath(Node startNode, Node targetNode)
    {
        List<Node> finalPath = new List<Node>();
        Node currentNode = targetNode;

        while (currentNode != null) {
            finalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }

        finalPath.Reverse();
        grid.finalPath = finalPath;
        
        foreach (Node node in finalPath) {
            Debug.Log(node.gridX + " " + node.gridY);
        }
        Debug.Log("wtf");
    }

    int GetManhattenDistance(Node nodeA, Node nodeB)
    {
        int iX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int iY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        return iX + iY;
    }
}
