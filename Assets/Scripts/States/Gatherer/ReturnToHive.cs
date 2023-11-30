using UnityEngine;
/// <summary>
/// State in which Gatherer returns to Hive
/// </summary>
public class ReturnToHive : State
{
    public override void EnterState()
    {
        Debug.Log(gameObject.name + " is returning to Hive");
        myAgent.SetDestination(Hive.Instance.transform.position); // pathfind to Hive
    }
    public override void UpdateState() // Pathfind to Hive
    {
        myAgent.SetDestination(Hive.Instance.transform.position); // pathfind to Hive
    }
    public override void ExitState()
    {
        Debug.Log(gameObject.name + " is not returning to Hive");
    }
}