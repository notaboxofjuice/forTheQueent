using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isObstacle;
    public Vector3 worldCoords;

    public Node(bool _Obstacle, Vector3 _worldCoords)
    {
        isObstacle = _Obstacle;
        worldCoords = _worldCoords;
    }
}
