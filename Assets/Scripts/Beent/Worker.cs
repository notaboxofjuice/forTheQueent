using UnityEngine;
using BeentEnums;
using System.Collections;
using System.Collections.Generic;

public class Worker : Beent
{
    [SerializeField] Transform defenseObjSpawn;
    [SerializeField] float pollenProcessTime;


    private void Awake()
    {
        //assign the beent type
        beentType = BeentType.Worker;
    }

    protected override void DoSenses() // look for events and trigger transitions for state machine
    {

    }

    private void BuildWalls(GameObject _defensiveWall, DefenseSocket _socket)  //builds protective walls to keep Beentbarians at bay
    {
        //Vars
        GameObject wall;
        Quaternion wallRotation;
        Vector3 wallPos;

        wallPos = _socket.gameObject.transform.position; //Assign spawn location to the 

        wallRotation = _socket.gameObject.transform.rotation; //Set wall rotation to the direction the beent is facing

        //Use instantiate to make the wall at the right position and location, store in a variable
        wall = Instantiate(_defensiveWall, wallPos, wallRotation);

        //Assign as a child of the hive, this will make tracking the wallNum easier because it can count Children of type Wall
        wall.transform.SetParent(Hive.Instance.transform, false);

        //Update the defense obj list
        Hive.Instance.AddDefence(wall);
    }

    private DefenseSocket FindValidWallPos() //finds a valid position to spawn a wall
    {
        DefenseSocket defenseSocket; //socket to be returned
        List<DefenseSocket> sockets = new List<DefenseSocket>(); //list of valid sockets

        //Check for valid defenses
        if (Hive.Instance.CountDefenses() > 0)
        {
            //create a list of valid sockets
            foreach (DefenseSocket socket in Hive.Instance.defenseSockets)
            {
                if (socket.isOccupied == false) sockets.Add(socket);
            }

            //Pick and assign one of those sockets and return
            int randInt = Random.Range(0, sockets.Count);
            sockets[randInt].isOccupied = true;
            defenseSocket = sockets[randInt];
            return defenseSocket;
        }
        else
        {
            //Tell user that there are no valid sockets
            Debug.Log("No valid defense positions, defense socket null");
            defenseSocket = null;
            return defenseSocket;
        }
    }

    private void ProcessPollen()
    {

    }

}