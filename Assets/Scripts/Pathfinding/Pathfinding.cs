using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Transform target;
    PathGrid grid;
    private void Awake()
    {
        grid = GameObject.FindGameObjectWithTag("A*").GetComponent<PathGrid>();
    }
    private void Update()
    {
        CalculatePath(transform.position, target.position);
    }
    void CalculatePath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        startNode.gCost = 0;
        //use list for open nodes so that we can add and remove nodes from the list 
        List<Node> openNodes = new List<Node>();
        //use hashset for closed and already visited nodes
        HashSet<Node> visited = new HashSet<Node>();
        //add start node to open nodes
        openNodes.Add(startNode);
        //continue to loop as long as there are nodes remaining in the open set to check
        while(openNodes.Count > 0)
        {
            //initialize current node as the first in the list
            Node currentNode = openNodes[0];
            //loop through all nodes in the open set
            for(int i = 1; i < openNodes.Count; i++)
            {
                //look for a better node than the one we have currently by either searching for a lower fcost or in the case of a tie taking the node with the lower hcost as it is closer to the target
                if (openNodes[i].fCost() < currentNode.fCost() || openNodes[i].fCost() == currentNode.fCost() && openNodes[i].hCost < currentNode.hCost)
                {
                    //if a better node is found set current node to the new node
                    currentNode = openNodes[i];
                }
            }
            //after looping remove the current node from the active set and add it to the visited set
            openNodes.Remove(currentNode);
            visited.Add(currentNode);
            //If current node is the same as our target we have found our path and can return.
            if(currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                break;
            }
            //Run through each neighbor of the current node
            foreach(Node neighbor in grid.GetNeighborNodes(currentNode))
            {
                //if neighbor node is an obstacle or already visited skip it
                if(neighbor.isObstacle || visited.Contains(neighbor))
                {
                    continue;
                }
                //calculate new cost to neighbor from current node
                int newNeighborMoveCost = currentNode.gCost + CalculateDistance(currentNode, neighbor);
                //if the new cost is lower than the current known cost or the node is not in the list of open nodes
                if(newNeighborMoveCost < currentNode.gCost || !openNodes.Contains(neighbor))
                {
                    //update g and h cost of the node with the updated values
                    neighbor.gCost = newNeighborMoveCost;
                    neighbor.hCost = CalculateDistance(neighbor, targetNode);
                    //parent the neighbor to the current node
                    neighbor.parent = currentNode;
                    //if not in the open set of nodes to explore add to open set
                    if (!openNodes.Contains(neighbor))
                    {
                        openNodes.Add(neighbor);
                        Debug.Log(neighbor.gridX + "," + neighbor.gridY);
                    }
                }
            }
        }
    }

    void RetracePath(Node start, Node end)
    {
        //create a list of nodes that represents our path
        List<Node> path = new List<Node>();
        //traceback works backwards so we start at the end
        Node currentNode = end;
        while(currentNode != start)
        {
            path.Add(currentNode);
            //magic voodoo that goes back through the nodes by following a train of parented nodes
            currentNode = currentNode.parent;
        }
        //add the start node which is where we should be after the loop is finished
        path.Add(currentNode);
        //path is originally backwards so the order needs to flipped the right way around
        path.Reverse();
        Debug.Log("Path found with: " + path.Count + " nodes.");
        //for testing
        grid.path = path;
    }
    int CalculateDistance(Node A, Node B)
    {
        //get raw axis distance
        int distanceX = Mathf.Abs(A.gridX - B.gridX);
        int distanceY = Mathf.Abs(A.gridY - B.gridY);
        //If the distance along X is greater than the distance along Y
        if(distanceX > distanceY)
        {
            //14 represents the cost of a diagonal move which is the square root of 2 or about 1.4. Multiply by 10 to work with integer numbers
            //10 represents the cost of a normal move up or down which is 1 but multiplied by 10 to remain consistent with the earlier multiplication needed to work with an integer representation of root 2 
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else
        {
            //if the distance along Y is greater do this version instead
            //also default here if the distances are the same as it doesn't matter which goes first
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }
}
