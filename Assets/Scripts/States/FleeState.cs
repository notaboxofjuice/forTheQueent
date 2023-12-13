using System.Collections;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// State for running away from enemy Beent.
/// Probably just pick a spot in the opposite direction and pathfind there.
/// </summary>
public class FleeState : State
{
    private Transform threatBeent; //transform of the current threat

    [SerializeField] float fleeSpeed;

    [Tooltip("Radius that a random flee point is generated")]
    [SerializeField] float fleeRadius;

    public override void EnterState()
    {
        //Set the move speed
        myAgent.speed = fleeSpeed;
        Debug.Log(gameObject.name + " is fleeing");
    }

    public override void ExitState()
    {
        StopAllCoroutines();
        Debug.Log(gameObject.name + " is no longer fleeing");
        base.ExitState();
    }

    public override void UpdateState()
    {
        if (threatBeent != null)
        {
            //get random point in the opposite direction of the threat, but within the flee radius
            Vector3 randomPoint = Random.insideUnitSphere * fleeRadius;
            Vector3 oppositeDirection = -threatBeent.position;
            randomPoint += oppositeDirection;

            // Ensure the point is on the NavMesh, if not exit the function and try again
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, fleeRadius, NavMesh.AllAreas))
            {
                randomPoint = hit.position;
            }
            else
            {
                return;
            }

            //Set the destination to the a flee point
            myAgent.destination = transform.position + randomPoint;
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

    private void OnTriggerExit(Collider other)
    {
        //if the threat beent is no longer there 
        if (other.gameObject.transform == threatBeent)
        {
            ExitState();
        }    
    }
}