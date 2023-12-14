using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

//Working on this script: Ky'onna

public class IdleRoam : State
{
    [SerializeField] float patrolSpeed;
    [SerializeField] float roamPointRadius;
    [SerializeField] float roamDuration;
    private Vector3 randomPoint;
    private bool setInitialPoint;
    public override void EnterState()
    {
        setInitialPoint = false;
        randomPoint = Vector3.zero; //reset the random point
        myAgent.speed = patrolSpeed;
        //makes sure we don't roam forever
        StartCoroutine(RoamTimer());
        Debug.Log("Idle roaming");
    }

    public override void ExitState()
    {
        setInitialPoint = false;
        Debug.Log("Exiting roam");
        base.ExitState();
    }

    public override void UpdateState()
    {
        //if location == point --> return, this will exit the function and find a new point
        if (Vector3.Distance(transform.position, randomPoint) <= myAgent.stoppingDistance || !setInitialPoint)
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

        if (setInitialPoint)
        {
            //set y pos to be achievable
            randomPoint.y = transform.position.y;

            //go to point
            myAgent.SetDestination(randomPoint);
        }
    }

    private IEnumerator RoamTimer()
    {
        yield return new WaitForSeconds(roamDuration);
        ExitState();
    }
}
