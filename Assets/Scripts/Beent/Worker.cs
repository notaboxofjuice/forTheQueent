using UnityEngine;
using BeentEnums;
using System.Collections;
using System.Collections.Generic;

//Working on this script: Ky'onna

public class Worker : Beent
{
    private void Awake()
    {
        //initialization
        beentType = BeentType.Worker;
    }

    protected override void DoSenses() // look for events and trigger transitions for state machine, called when current state == null
    {
        #region Old Code using populations
        /*        //store beent populations
                int gathererNum = Hive.Instance.CountBeentsByType(BeentType.Gatherer);
                int warriorNum = Hive.Instance.CountBeentsByType(BeentType.Warrior);
                int beentCount = gathererNum + warriorNum; // total beents minus the workers and beentbarians

                if (warriorNum > gathererNum)
                {
                    //More likely to produce nectar
                    if ((Random.Range(0, beentCount) < warriorNum) && Hive.Instance.CurrentPollen > 0)
                    {
                        ChangeState(GetComponent<ProduceNectar>());
                        return;
                    }
                    else if (Hive.Instance.HasOpenDefenseSockets())
                    {
                        ChangeState(GetComponent<BuildWalls>());
                        return;
                    }
                }
                else if(warriorNum < gathererNum)
                {
                    //more likely to build defensive walls
                    if ((Random.Range(0, beentCount) < gathererNum) && Hive.Instance.HasOpenDefenseSockets())
                    {
                        ChangeState(GetComponent<BuildWalls>());
                        return;
                    }
                    else if (Hive.Instance.CurrentPollen > 0)
                    {
                        ChangeState(GetComponent<ProduceNectar>());
                        return;
                    }
                }*/
        #endregion

        int totalResources = Hive.Instance.CurrentPollen + Hive.Instance.CurrentNectar;
        int randomNumber = Random.Range(0, totalResources);

        if(randomNumber <= Hive.Instance.CurrentPollen && Hive.Instance.CurrentPollen > 0)
        {
            ChangeState(GetComponent<ProduceNectar>());
        }
        else if(randomNumber > Hive.Instance.CurrentPollen && Hive.Instance.HasOpenDefenseSockets()) 
        {
            ChangeState(GetComponent<BuildWalls>());
        }
        else //if no pollen and no defense sockets, idle
        {
            ChangeState(GetComponent<IdleRoam>());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // in range of enemy
        {
            ChangeState(GetComponent<FleeState>()); // interrupt current state and flee
        }
    }

}