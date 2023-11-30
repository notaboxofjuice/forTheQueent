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
        if (Random.Range(Random.Range(0, 1), 100) == 0) // 1% chance to change state
        {
            if (Random.Range(0, 2) == 0) daddy.ChangeState(GetComponent<FindPollen>());
            else daddy.ChangeState(GetComponent<ReturnToHive>());
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log(gameObject.name + " is not returning to Hive");
    }
}