using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    public Transform startPosition;
    public Transform targetPosition;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(startPosition.position, targetPosition.position);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPosition(startPos);
        Node targetNode = grid.NodeFromWorldPosition(targetPos);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);

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
                GetFinalPath(startNode, targetNode);
            }

            foreach (Node neighbourNode in grid.GetNeighbouringNodes(currentNode)) {

            }
        }
    }

    void GetFinalPath(Node startNode, Node targetNode)
    {
        List<Node> finalPath = new List<Node>();
        Node currentNode = targetNode;

        while (currentNode != startNode) {
            finalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }

        finalPath.Reverse();
        grid.finalPath = finalPath;
    }
}
