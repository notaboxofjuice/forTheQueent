using UnityEngine;
/// <summary>
/// State in which Gatherer looks for pollen
/// </summary>
public class FindPollen : State
{
    public override void EnterState()
    {
        Debug.Log(gameObject.name + " is looking for pollen");
    }

    public override void ExitState()
    {
        Debug.Log(gameObject.name + " is not looking for pollen");
    }

    public override void UpdateState()
    {
        return; // pathfind to ChooseDestination()
    }

    protected override Transform ChooseDestination()
    {
        return null; // find nearest pollen
    }
}