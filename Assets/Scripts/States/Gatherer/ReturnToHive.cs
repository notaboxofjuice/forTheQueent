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
        return;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hive")) // in range of hive
        {
            if (Father.heldPollen > 0) DepositPollen(); // has pollen, deposit pollen
            Daddy.ChangeState(GetComponent<FindPollen>()); // return to finding pollen
        }
        else if (other.CompareTag("Enemy")) // in range of enemy
        {
            Daddy.ChangeState(GetComponent<FleeState>()); // interrupt current state and flee
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log(gameObject.name + " is not returning to Hive");
    }
    private void DepositPollen()
    {
        Hive.Instance.currentPollen += Father.heldPollen; // add pollen to hive
        Father.heldPollen = 0; // empty pollen
        Daddy.ChangeState(GetComponent<FindPollen>()); // return to finding pollen
        UI.GathererProductivity++; // Increment the gatherer productivity for score calculation
    }

}