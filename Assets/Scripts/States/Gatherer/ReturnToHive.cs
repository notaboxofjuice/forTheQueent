using UnityEngine;
/// <summary>
/// State in which Gatherer returns to Hive
/// </summary>
public class ReturnToHive : State
{
    public override void EnterState()
    {
        Debug.Log(gameObject.name + " is returning to Hive");
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        return;
    }

    public override void UpdateState()
    {
        return; // pathfind to ChooseDestination()
    }

    protected override Transform ChooseDestination() // 
    {
        return Hive.Instance.transform; // Hive transform
    }
}