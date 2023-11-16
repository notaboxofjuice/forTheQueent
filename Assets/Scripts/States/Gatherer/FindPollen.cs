using UnityEngine;
/// <summary>
/// State in which Gatherer looks for pollen
/// </summary>
public class FindPollen : State
{
    private GameObject TargetPollen; // nearest pollen
    public override void EnterState()
    {
        Debug.Log(gameObject.name + " is looking for pollen");
        TargetPollen = LookForPollen(); // find nearest pollen
    }

    public override void ExitState()
    {
        Debug.Log(gameObject.name + " is not looking for pollen");
    }

    public override void UpdateState()
    {
        MyPathfinder.CalculatePath(transform.position, ChooseDestination()); // pathfind to pollen
    }

    protected override Vector3 ChooseDestination()
    {
        return TargetPollen.transform.position; // return nearest pollen
    }

    private GameObject LookForPollen() // loop through PollenFactory.PollenList and find closest one
    {
        // Set closestPollen to first pollen in list
        GameObject closestPollen = PollenFactory.Instance.PollenList[0]; 
        // Loop through PollenFactory.PollenList
        foreach (GameObject pollen in PollenFactory.Instance.PollenList)
        {
            // If the pollen is closer than the current closest pollen
            if (Vector3.Distance(pollen.transform.position, transform.position) < Vector3.Distance(closestPollen.transform.position, transform.position))
            {
                closestPollen = pollen; // Set closestPollen to this pollen
            }
        }
        return closestPollen; // Return closest pollen
    }
}