using System.Collections;
using UnityEngine;
/// <summary>
/// State for running away from enemy Beent.
/// Probably just pick a spot in the opposite direction and pathfind there.
/// </summary>
public class FleeState : State
{
    private Transform threatBeent; //transform of the current threat
    private Vector3 fleeLocation;
    [SerializeField] float MoveSpeed = 10;

    [Tooltip("Minimum distance to be considered at the flee location")]
    [SerializeField] float fleeLocationOffset;
    
    bool calculatedInitialPath;

    public override void EnterState()
    {
        //Set the move speed
        calculatedInitialPath = false;
        Debug.Log(gameObject.name + " is fleeing");
    }

    public override void ExitState()
    {
        StopAllCoroutines();
        Debug.Log(gameObject.name + " is no longer fleeing");
    }

    public override void UpdateState()
    {
        StartCoroutine(FleeThreat());
        return;  
    }

    protected override Vector3 ChooseDestination()
    {
        //find transform in opposite direction of enemy
        fleeLocation = -threatBeent.position; //not fullproof it might pick an invalid destination

        /*        if (*//*invalid pos*//*)
                {

                }*/
        return fleeLocation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // in range of enemy
        {
            //assign the threat beent
            threatBeent = other.gameObject.transform;
        }
    }

    IEnumerator FleeThreat()
    {
        yield return new WaitForFixedUpdate();

        //calculate a path if neccessary
        if(!calculatedInitialPath || Vector3.Distance(transform.position, fleeLocation) < fleeLocationOffset)
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
}