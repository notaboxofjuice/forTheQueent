using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Working on this script: Ky'onna

public class IdleRoam : State
{
    public override void EnterState()
    {
        Debug.Log("Idle roaming");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting idle roam");
    }

    public override void UpdateState()
    {
        //Select random valid point on the map --> ChooseDestination();

        //pathfind

        //go to point

        //if location == point --> return, this will exit the function and find a new point

    }

    protected override Transform ChooseDestination()
    {
        //find a new point
        return null;
    }
}
