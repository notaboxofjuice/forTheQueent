using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Working on this script: Ky'onna
public class ProduceNectar : State
{
    [SerializeField] GameObject pollenStorage;
    [SerializeField] float processColldownTime;
    private bool canProcess;
    public override void EnterState()
    {
        canProcess = true;
        Debug.Log("Processing pollen");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting process pollen");
    }

    public override void UpdateState()
    {
        //pathfind to the pollen storage
        GetComponent<Pathfinding>().CalculatePath(transform.position, ChooseDestination().position);

        //if within adequate distance, produce nectar
        if (Vector3.Distance(this.gameObject.transform.position, pollenStorage.transform.position) > 1.0f)
        {
            ProcessPollen();
        }
    }

    protected override Transform ChooseDestination()
    {
        //Get the transform of the pollen storage
        Transform transform = pollenStorage.gameObject.transform;
        return transform;
    }

    private void ProcessPollen()
    {
        Hive.Instance.currentPollen--;
        Hive.Instance.currentNectar++;

        //Start the cooldown
        canProcess = false;

        StartCoroutine(ProcessCooldown());
    }

    IEnumerator ProcessCooldown()
    {
       yield return new WaitForSeconds(processColldownTime);
        canProcess = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
