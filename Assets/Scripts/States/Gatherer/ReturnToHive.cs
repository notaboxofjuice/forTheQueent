using UnityEngine;
/// <summary>
/// State in which Gatherer returns to Hive
/// </summary>
public class ReturnToHive : State
{
    public override void EnterState()
    {
        Debug.Log(gameObject.name + " is returning to Hive");
    }

    public override void ExitState()
    {
        return;
    }

    public override void UpdateState() // Pathfind to Hive
    {
        return;
    }

    protected override Vector3 ChooseDestination() // Return Hive transform
    {
        return Hive.Instance.transform.position; // Hive position
    }
}