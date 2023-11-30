using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

//Working on this script: Ky'onna

public class IdleRoam : State
{
    [SerializeField] float roamPointRadius;
    [SerializeField] float roamPointOffset;
    [SerializeField] float roamDuration;
    private Vector3 randomPoint;
    private bool setInitialPoint;
    public override void EnterState()
    {
        setInitialPoint = false;
        Debug.Log("Idle roaming");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting idle roam");
    }

    public override void UpdateState()
    {
        //makes sure we don't roam forever
        StartCoroutine(RoamTimer());

        //if location == point --> return, this will exit the function and find a new point
        if (Vector3.Distance(transform.position, randomPoint) <= roamPointOffset || !setInitialPoint)
        {
            //Select random valid point on the map -->
            randomPoint = transform.position + Random.insideUnitSphere * roamPointRadius;

            //check is point on nav mesh
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, roamPointRadius, NavMesh.AllAreas))
            {
                randomPoint = hit.position;
                setInitialPoint = true;
            }
            else
            {
                //exit the function
                return;
            }
        }
        
        //go to point
        myAgent.SetDestination(randomPoint);
    }

    private IEnumerator RoamTimer()
    {
        yield return new WaitForSeconds(roamDuration);
        ExitState();
    }
}
