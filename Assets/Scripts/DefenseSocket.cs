using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Working on this script: Ky'onna
public class DefenseSocket : MonoBehaviour
{
    //Note: These are valid positions for defense walls, the idea is that you place these whereever you want walls to be placed
    
    public bool isOccupied;

    private void Start()
    {
        //initialization
        isOccupied = false;
    }
}
