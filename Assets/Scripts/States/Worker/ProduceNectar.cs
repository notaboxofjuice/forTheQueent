using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Working on this script: Ky'onna
public class ProduceNectar : State
{
    [SerializeField] GameObject pollenStorage;
    [SerializeField] float processColldownTime;
    private bool canProcess;
    private bool calculatedInitialPath;
    private float MoveSpeed;
    private float pollenStorageOffset;
    
    public override void EnterState()
    {
        canProcess = true;
        calculatedInitialPath = false;
        Debug.Log("Processing pollen");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting process pollen");
        StopAllCoroutines();
    }

    public override void UpdateState()
    {
        //if within adequate distance, produce nectar
        if (Vector3.Distance(this.gameObject.transform.position, ChooseDestination()) < pollenStorageOffset )
        {
            StopCoroutine(GoToPollenStorage());
            
            if(canProcess) ProcessPollen();
        }
        else //path find to the pollen storage to process nectar
        {
            StartCoroutine(GoToPollenStorage());
        }
    }

    protected override Vector3 ChooseDestination()
    {
        //Get the transform of the pollen storage
        
        return pollenStorage.gameObject.transform.position;
    }

    private void ProcessPollen()
    {
        Hive.Instance.currentPollen--;
        Hive.Instance.currentNectar++;

        UI.WorkerProductivity++; //Increment the worker productivity for score calculation -Leeman

        //Start the cooldown
        canProcess = false;

        StartCoroutine(ProcessCooldown());
    }

    IEnumerator GoToPollenStorage()
    {
        yield return new WaitForFixedUpdate();

        //calculate a path if neccessary
        if (!calculatedInitialPath)
        {
            MyPathfinder.CalculatePath(transform.position, ChooseDestination()); //calculate path
            calculatedInitialPath = true;
        }

        //move to along the path
        if (MyPathfinder.lastFoundPath != null && MyPathfinder.lastFoundPath.Count > 1)
        {
            //mode towards the node in the path
            transform.position = Vector3.MoveTowards(transform.position, MyPathfinder.lastFoundPath[1].worldCoords, MoveSpeed * Time.deltaTime);
        }
    }

    IEnumerator ProcessCooldown()
    {
       yield return new WaitForSeconds(processColldownTime);
        canProcess = true;
    }

}
