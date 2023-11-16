using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

//Working on this script: Ky'onna
public class BuildWalls : State
{
    [SerializeField] GameObject defenseObj;
    public override void EnterState()
    {
        Debug.Log("Building walls.");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting building walls");
    }

    public override void UpdateState()
    {
        //find a defense socket
        DefenseSocket socket = FindValidWallSocket();

        //if the socket is valid go to it and build a wall
        if (socket != null)
        {
            //path find to the location of the socket
            GetComponent<Pathfinding>().CalculatePath(transform.position, socket.gameObject.transform.position);

            if (Vector3.Distance(this.gameObject.transform.position, socket.gameObject.transform.position) < 1.0f)
            {
                //if close enough to the wall, build the wall
                BuildDefenses(defenseObj, socket);
                socket = null; //set socket to null so beent does not try to build another wall
            }    
        }
    }

    protected override Transform ChooseDestination()
    {
        Debug.Log("uneccessary for this state");
        return null;
    }

    private void BuildDefenses(GameObject _defensiveWall, DefenseSocket _socket)  //builds protective walls to keep Beentbarians at bay
    {
        //Vars
        GameObject wall;
        Quaternion wallRotation;
        Vector3 wallPos;

        wallPos = _socket.gameObject.transform.position; //Assign spawn location to the 

        wallRotation = _socket.gameObject.transform.rotation; //Set wall rotation to the direction the beent is facing

        //Use instantiate to make the wall at the right position and location, store in a variable
        wall = Instantiate(_defensiveWall, wallPos, wallRotation);
        wall.GetComponent<DefenseObj>().myDefenseSocket = _socket;

        //Assign as a child of the hive, this will make tracking the wallNum easier because it can count Children of type Wall
        wall.transform.SetParent(Hive.Instance.transform, false);

        //Update the defense obj list
        Hive.Instance.AddDefence(wall);
    }

    private DefenseSocket FindValidWallSocket() //finds a valid Socket to spawn a wall
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

            //Pick and assign one of those sockets and return it's value
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
}
