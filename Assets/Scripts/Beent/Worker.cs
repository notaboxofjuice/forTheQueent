using UnityEngine;
using BeentEnums;
using System.Collections;
using System.Collections.Generic;

//Working on this script: Ky'onna

public class Worker : Beent
{
    public float defaultSpeed;
    public float runSpeed;
    private void Awake()
    {
        //initialization
        beentType = BeentType.Worker;
    }

    protected override void DoSenses() // look for events and trigger transitions for state machine, called when current state == null
    {
        //store beent populations
        int gathererNum = Hive.Instance.CountBeentsByType(BeentType.Gatherer);
        int warriorNum = Hive.Instance.CountBeentsByType(BeentType.Warrior);
        int beentCount = gathererNum + warriorNum; // total beents minus the workers and beentbarians

        if (warriorNum > gathererNum)
        {
            //More likely to produce nectar
            if ((Random.Range(0, beentCount) < warriorNum) && Hive.Instance.CurrentPollen > 0) ChangeState(GetComponent<ProduceNectar>());
            else if (Hive.Instance.HasOpenDefenseSockets()) ChangeState(GetComponent<BuildWalls>());
        }
        else
        {
            //more likely to build defensive walls
            if ((Random.Range(0, beentCount) < gathererNum) && Hive.Instance.HasOpenDefenseSockets()) ChangeState(GetComponent<BuildWalls>());
            else if (Hive.Instance.CurrentPollen > 0) ChangeState(GetComponent<ProduceNectar>());
        }


        //if all those statements return false we idle for a bit and then check back to see if they are true
        //ChangeState(GetComponent<IdleRoam>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // in range of enemy
        {
            ChangeState(GetComponent<FleeState>()); // interrupt current state and flee
        }
    }

}