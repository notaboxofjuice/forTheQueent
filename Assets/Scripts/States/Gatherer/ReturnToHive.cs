using UnityEngine;
/// <summary>
/// State in which Gatherer returns to Hive
/// </summary>
public class ReturnToHive : State
{
    private float maxTime = 15f;
    private float timer = 0f;
    public override void EnterState()
    {
        Debug.Log(gameObject.name + " is returning to Hive");
        myAgent.SetDestination(Hive.Instance.transform.position); // pathfind to Hive
        timer = maxTime;
    }
    public override void UpdateState() // Pathfind to Hive
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            ExitState();
            return;
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log(gameObject.name + " is not returning to Hive");
    }
}