using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] Vector2 gridWorldSize;
    [SerializeField] float nodeRadius;
    Node[,] grid;
    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;
    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        //how many grid squares can fit in our grid based on physical size of each node
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        //find bottom left corner by starting in center and subtracting half the grid size vertically and half the grid horizontally
        Vector3 worldBottomLeftCorner = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;
        //loop through our two dimensional grid
        for(int i = 0; i < gridSizeX; i++)
        {
            for(int j = 0; j < gridSizeY; j++)
            {
                //find our world point based on how far along we are from the bottom left corner
                Vector3 worldPoint = worldBottomLeftCorner + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius);
                //checks for our layer mask to find nodes that should be marked as obstacles
                bool isObstacle = Physics.CheckSphere(worldPoint, nodeRadius, obstacleMask);
                //add node with parameter to grid array
                grid[i,j] = new Node(isObstacle, worldPoint);
            }
        }
    }
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        //Convert coordinate position into a percentage along the grid
        //addition of position to gridsize divided by two is to account for negative half of the grid
        //clamp value between 0 and 1
        float percentX = Mathf.Clamp01((worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x);
        float percentY = Mathf.Clamp01((worldPosition.z + gridWorldSize.y/2) / gridWorldSize.y);
        //convert percentage along grid to node address in our grid
        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
        // return node at address
        return grid[x,y];    
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if(grid != null)
        {
            foreach(Node n in grid)
            {
                Gizmos.color = n.isObstacle ? Color.red : Color.green;
                Gizmos.DrawCube(n.worldCoords, Vector3.one * (nodeDiameter-0.1f));
            }
        }
    }
}
