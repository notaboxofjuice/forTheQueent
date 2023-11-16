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
        Debug.Log(gameObject.name + " is not returning to Hive");
    }

    public override void UpdateState() // Pathfind to Hive
    {
        MyPathfinder.CalculatePath(transform.position, ChooseDestination());
    }

    protected override Vector3 ChooseDestination() // Return Hive transform
    {
        return Hive.Instance.transform.position; // Hive position
    }
}