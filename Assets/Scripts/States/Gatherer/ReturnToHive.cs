using UnityEngine;
/// <summary>
/// State in which Gatherer returns to Hive
/// </summary>
public class ReturnToHive : State
{
    Gatherer Father => Daddy as Gatherer;
    public override void EnterState()
    {
        Debug.Log(gameObject.name + " is returning to Hive");
        myAgent.SetDestination(Hive.Instance.transform.position); // pathfind to Hive
    }
    public override void UpdateState() // Pathfind to Hive
    {
        if (myAgent.remainingDistance > myAgent.stoppingDistance) return; // if not at Hive, do nothing
        if (Father.heldPollen > 0) DepositPollen(); // if has pollen, deposit
        Daddy.ChangeState(GetComponent<FindPollen>()); // return to finding pollen
    }
    private void DepositPollen()
    {
        Hive.Instance.CurrentPollen += Father.heldPollen; // add pollen to hive
        Father.heldPollen = 0; // empty pollen
        UI.GathererProductivity++; // Increment the gatherer productivity for score calculation
    }
}