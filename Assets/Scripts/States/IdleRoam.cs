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
        myAgent.speed = patrolSpeed;
        setInitialPoint = false;
        //makes sure we don't roam forever
        StartCoroutine(RoamTimer());
        Debug.Log("Idle roaming");
    }

    public override void ExitState()
    {
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

        //set y pos to be achievable
        randomPoint.y = transform.position.y;

        //go to point
        //Debug.Log("Distance to point: " + Vector3.Distance(transform.position, randomPoint));
        myAgent.SetDestination(randomPoint);
    }

    private IEnumerator RoamTimer()
    {
        yield return new WaitForSeconds(roamDuration);
        ExitState();
    }
}
