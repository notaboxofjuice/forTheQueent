using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isObstacle;
    public Vector3 worldCoords;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public Node parent;
    public Node(bool _Obstacle, Vector3 _worldCoords, int _gridX, int _gridY)
    {
        isObstacle = _Obstacle;
        worldCoords = _worldCoords;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost()
    {
        return gCost + hCost;
    }
}
