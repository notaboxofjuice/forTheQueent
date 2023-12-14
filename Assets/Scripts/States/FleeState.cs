using System.Collections;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// State for running away from enemy Beent.
/// Probably just pick a spot in the opposite direction and pathfind there.
/// </summary>
public class FleeState : State
{
    [SerializeField] Transform threatBeent; //transform of the current threat

    [Tooltip("Min distance for the beent to consider itself safe and exit the flee state")]
    [SerializeField] float safeDistance;
    private bool setInitialPoint;
    Vector3 fleePoint;

    [SerializeField] float fleeSpeed;

    [Tooltip("Radius that a random flee point is generated")]
    [SerializeField] float fleeRadius;

    public override void EnterState()
    {
        //reset flee point
        setInitialPoint = false;
        fleePoint = Vector3.zero;

        //Set the move speed
        myAgent.speed = fleeSpeed;
        Debug.Log(gameObject.name + " is fleeing");
    }

    public override void ExitState()
    {
        StopAllCoroutines();
        setInitialPoint = false;
        threatBeent = null;
        Debug.Log(gameObject.name + " is no longer fleeing");
        base.ExitState();
    }

    public override void UpdateState()
    {
        //flee logic
        if (threatBeent != null)
        {
            //if not point set or at the current point
            if(Vector3.Distance(transform.position, fleePoint) <= myAgent.stoppingDistance || !setInitialPoint)
            {
                //get random point in the opposite direction of the threat, but within the flee radius
                 fleePoint = Random.insideUnitSphere * fleeRadius;
                Vector3 oppositeDirection = -threatBeent.position;
                fleePoint += oppositeDirection;

                // Ensure the point is on the NavMesh, if not exit the function and try again
                NavMeshHit hit;

                if (NavMesh.SamplePosition(fleePoint, out hit, myAgent.stoppingDistance, NavMesh.AllAreas))
                {
                    fleePoint = hit.position;
                    setInitialPoint = true;
                }
                else
                {
                    return;
                }
            }

            if (setInitialPoint)
            {
                //Set the destination to the a flee point
                myAgent.destination = fleePoint;
            }

            //check if safe
            if (Vector3.Distance(transform.position, threatBeent.transform.position) > safeDistance)
            {
                //no need to flee the beent is safe
                ExitState();
            }

        }
        else
        {
            Debug.Log("No threat beent");
            ExitState();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // in range of enemy
        {
            //assign the threat beent
            threatBeent = other.gameObject.transform;
        }
    }
}