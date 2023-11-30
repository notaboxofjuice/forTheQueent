using UnityEngine;
/// <summary>
/// State in which Gatherer looks for pollen
/// </summary>
public class FindPollen : State
{
    [SerializeField] private GameObject TargetPollen; // nearest pollen
    public override void EnterState()
    {
        Debug.Log(gameObject.name + " is looking for pollen");
        TargetPollen = LookForPollen(); // find nearest pollen
        if (TargetPollen != null) myAgent.SetDestination(TargetPollen.transform.position); // pathfind to nearest pollen
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log(gameObject.name + " is not looking for pollen");
    }

    public override void UpdateState()
    {
        if (PollenFactory.PollenList.Count == 0) ExitState(); // if no pollen, exit state
        if (TargetPollen != null && !PollenFactory.PollenList.Contains(TargetPollen))
        {
            TargetPollen = LookForPollen(); // if pollen is destroyed, find new pollen
        }
    }
    private GameObject LookForPollen() // loop through PollenFactory.PollenList and find closest one
    {
        if (PollenFactory.PollenList.Count == 0)
        {
            ExitState(); // if no pollen, exit state
            return null;
        }
        // Set closestPollen to first pollen in list
        GameObject closestPollen = PollenFactory.PollenList[0];
        // Loop through PollenFactory.PollenList
        foreach (GameObject pollen in PollenFactory.PollenList)
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