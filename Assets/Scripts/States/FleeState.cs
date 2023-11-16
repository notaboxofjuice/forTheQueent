using UnityEngine;
/// <summary>
/// State for running away from enemy Beent.
/// Probably just pick a spot in the opposite direction and pathfind there.
/// </summary>
public class FleeState : State
{
    public override void EnterState()
    {
        Debug.Log(gameObject.name + " is fleeing");
    }

    public override void ExitState()
    {
        Debug.Log(gameObject.name + " is no longer fleeing");
    }

    public override void UpdateState()
    {
        return; // pathfind to ChooseDestination()
    }

    protected override Transform ChooseDestination()
    {
        return null; // find transform in opposite direction of enemy
    }
}