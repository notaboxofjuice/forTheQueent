using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

//Working on this script: Ky'onna
public class BuildWalls : State
{
    [SerializeField] GameObject defenseObj;
    [SerializeField] float wallBuildCooldown;
    [SerializeField] NavMeshSurface[] navMeshes;
    private bool canBuild = true;
    private DefenseSocket socket;
    public override void EnterState()
    {
        socket = null;
        Debug.Log("Building walls.");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting building walls");
        base.ExitState();
    }

    public override void UpdateState()
    {

        if(socket == null && canBuild)
        {
            socket = FindValidWallSocket();
        }
        else if(canBuild)
        {
            //path find to the location of the socket
            myAgent.SetDestination(socket.transform.position);

            if (Vector3.Distance(this.gameObject.transform.position, socket.transform.position) < myAgent.stoppingDistance)
            {
                //if close enough to the wall, build the wall
                BuildDefenses(defenseObj, socket);
                socket = null; //set socket to null so beent does not try to build another wall
                ExitState(); //exist the state to trigger do senses, checking the condition of the nest
            }
        }
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
        wall.transform.SetParent(Hive.Instance.transform, true);

        //Update the defense obj list
        Hive.Instance.AddDefence(wall);

        //update the nav mesh
        UpdateNavMeshes();

        //start cooldown
        canBuild = false;
        StartCoroutine(WallCooldown());

        UI.WorkerProductivity++; //Increment the worker productivity for score calculation -Leeman
    }

    private DefenseSocket FindValidWallSocket() //finds a valid Socket to spawn a wall
    {
        DefenseSocket defenseSocket; //socket to be returned
        List<DefenseSocket> sockets = new List<DefenseSocket>(); //list of valid sockets

        //create a list of valid sockets
        foreach (DefenseSocket socket in Hive.Instance.defenseSockets)
        {
            if (socket.isOccupied == false) sockets.Add(socket);
        }

        //check for valid sockets
        if(sockets.Count <= 0)
        {
            Debug.Log("no valid sockets: " + sockets.Count);
            Debug.Log("Hive sockets: " + Hive.Instance.defenseSockets.Count);
            ExitState();
            return null;
        }
        else
        {
            //Pick and assign one of those sockets and return it's value
            int randInt = Random.Range(0, sockets.Count);
            sockets[randInt].isOccupied = true;
            defenseSocket = sockets[randInt];
            Debug.Log("Found a socket: " + defenseSocket);
            return defenseSocket;
        }
    }

    IEnumerator WallCooldown()
    {
        yield return new WaitForSeconds(wallBuildCooldown);
        canBuild = true;
    }

    private void UpdateNavMeshes()
    {
        Debug.Log("Updating Navmesh");

        NavMeshData data;
        foreach(NavMeshSurface surface in navMeshes)
        {
            data = surface.navMeshData;
            surface.UpdateNavMesh(data);
        }
    }
}
