using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// State in which Gatherer returns to Hive
/// </summary>
public class ReturnToHive : State
{
    public override void EnterState()
    {
        Debug.Log(gameObject.name + " is returning to Hive");
        ChooseDestination(); // Choose destination
    }

    public override void ExitState()
    {
        Debug.Log(gameObject.name + " is not returning to Hive");
    }

    public override void UpdateState() // Pathfind to Hive
    {
        return;
    }

    protected override void ChooseDestination() // Return Hive transform
    {
        myAgent.SetDestination(Hive.Instance.transform.position);
    }
}