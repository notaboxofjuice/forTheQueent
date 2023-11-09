using UnityEngine;
using BeentEnums;
using System.Collections;

public class Worker : Beent
{
    [SerializeField] Transform defenseObjSpawn;
    [SerializeField] float pollenProcessTime;


    private void Awake()
    {
        //assign the beent type
        beentType = BeentType.Worker;
    }

    protected override void DoSenses() // look for events and trigger transitions
    {

    }

    private void BuildWalls(GameObject _defensiveWall)  //builds protective walls to keep Beentbarians at bay
    {
        //Vars
        GameObject wall;
        Quaternion wallRotation;
        Vector3 wallPos;

        wallPos = defenseObjSpawn.position; //Assign spawn location to the 

        wallRotation = transform.rotation; //Set wall rotation to the direction the beent is facing

        //Use instantiate to make the wall at the right position and location, store in a variable
        wall = Instantiate(_defensiveWall, wallPos, wallRotation);

        //Assign as a child of the hive, this will make tracking the wallNum easier because it can count Children of type Wall
        wall.transform.SetParent(Hive.Instance.transform, false);

        //Update the defense obj list
        Hive.Instance.AddDefence(wall);
    }

    private Vector3 FindValidWallPos() //finds a valid position to spawn a wall
    {
        Vector3 wallPos;



        return wallPos;
    }

    private void ProcessPollen()
    {

    }

}