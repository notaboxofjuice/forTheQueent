using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Working on this script: Ky'onna
public class ProduceNectar : State
{
    [SerializeField] GameObject pollenStorage;
    [SerializeField] float processColldownTime;
    private bool canProcess;
    private float pollenStorageOffset;
    
    public override void EnterState()
    {
        canProcess = true;
        Debug.Log("Processing pollen");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting process pollen");
        StopAllCoroutines();
        base.ExitState();
    }

    public override void UpdateState()
    {
        //if within adequate distance, produce nectar
        if (Vector3.Distance(this.gameObject.transform.position, Hive.Instance.transform.position) < myAgent.stoppingDistance)
        {
            if (canProcess && Hive.Instance.CurrentPollen > 0)
            {
                ProcessPollen();
            }
            else if(Hive.Instance.CurrentPollen <= 0)
            {
                ExitState();
            }
        }
        else //path find to the pollen storage to process nectar
        {
            myAgent.destination = Hive.Instance.transform.position;
        }
    }

    private void ProcessPollen()
    {
        Hive.Instance.CurrentPollen--;
        Hive.Instance.CurrentNectar++;

        UI.WorkerProductivity++; //Increment the worker productivity for score calculation -Leeman

        //Start the cooldown
        canProcess = false;

        StartCoroutine(ProcessCooldown());
    }

    private IEnumerator ProcessCooldown()
    {
       yield return new WaitForSeconds(processColldownTime);
        canProcess = true;
    }

}
