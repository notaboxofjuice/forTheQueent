using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Working on this script: Ky'onna
public class DefenseSocket : MonoBehaviour
{
    //Note: These are valid positions for defense walls, the idea is that you place these where ever you want walls to be placed
    
    [HideInInspector] public bool isOccupied;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        //initialization
        isOccupied = false;

        //hide the socket
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.enabled = false;
    }
}
