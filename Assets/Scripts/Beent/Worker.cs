using UnityEngine;
using BeentEnums;
using System.Collections;
using System.Collections.Generic;

//Working on this script: Ky'onna

public class Worker : Beent
{
    [SerializeField] Transform defenseObjSpawn;
    public GameObject defenseObj;
    [SerializeField] float pollenProcessTime;
    private bool fleeing;


    private void Awake()
    {
        //initialization
        beentType = BeentType.Worker;
        fleeing = false;
    }

    protected override void DoSenses() // look for events and trigger transitions for state machine
    {
        //Since stayig alive is #1 priority, only do senses if not currently fleeing
        if (!fleeing)
        {
            //store beent populations
            int gathererNum = Hive.Instance.CountBeentsByType(BeentType.Gatherer);
            int warriorNum = Hive.Instance.CountBeentsByType(BeentType.Warrior);
            int beentCount = gathererNum + warriorNum; // total beents minus the workers and beentbarians

            if (warriorNum > gathererNum)
            {
                //More likely to produce nectar
                if ((Random.Range(0, beentCount) < warriorNum) && Hive.Instance.currentPollen > 0) ChangeState(GetComponent<ProduceNectar>());
                else if (Hive.Instance.HasOpenDefenseSockets()) ChangeState(GetComponent<BuildWalls>());
            }
            else
            {
                //more likely to build defensive walls
                if ((Random.Range(0, beentCount) < gathererNum) && Hive.Instance.HasOpenDefenseSockets()) ChangeState(GetComponent<BuildWalls>());
                else if (Hive.Instance.currentPollen > 0) ChangeState(GetComponent<ProduceNectar>());
            }

            //if all those statements return false we idle until they are true
            ChangeState(GetComponent<IdleRoam>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // in range of enemy
        {
            ChangeState(GetComponent<FleeState>()); // interrupt current state and flee
            fleeing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) // in range of enemy
        {
            fleeing = false;
        }
    }

}