using UnityEngine;
/// <summary>
/// Script for the Gatherer Beent, which collects pollen and returns it to the hive
/// </summary>
public class Gatherer : Beent
{
    #region Attributes
    [SerializeField] int maxPollen;
    [SerializeField] int heldPollen;
    #endregion
    #region Operations
    protected override void DoSenses() // called in Beent.FixedUpdate() if current state is null
    {
        if (heldPollen == maxPollen) ChangeState(GetComponent<ReturnToHive>()); // if full, return to hive
        else // decide to Find Pollen or Return To Hive
        {
            // check populations
            int WarriorCount = Hive.Instance.CountBeentsByType(BeentEnums.BeentType.Warrior);
            int WorkerCount = Hive.Instance.CountBeentsByType(BeentEnums.BeentType.Worker);
            int BeentCount = WarriorCount + WorkerCount; // total beents, not counting gatherers

            if (WarriorCount > WorkerCount) // more warriors, more likely to switch to FindPollen state
            {
                if (Random.Range(0, BeentCount) < WarriorCount) ChangeState(GetComponent<FindPollen>());
                else ChangeState(GetComponent<ReturnToHive>());
            }
            else if (WorkerCount > WarriorCount) // more workers, more likely to switch to ReturnToHive state
            {
                if (Random.Range(0, BeentCount) < WorkerCount) ChangeState(GetComponent<ReturnToHive>());
                else ChangeState(GetComponent<FindPollen>());
            }
            else // equal populations, 50/50 chance to switch to either state
            {
                if (Random.Range(0, 2) == 0) ChangeState(GetComponent<FindPollen>());
                else ChangeState(GetComponent<ReturnToHive>());
            }
            {

            }
        }
    }
    private void OnTriggerEnter(Collider other) // object has a big sphere collider with isTrigger = true
    {
        if (other.CompareTag("Pollen") && heldPollen < maxPollen) // in range of pollen and has room
        {
            heldPollen++; // collect pollen
            Destroy(other.gameObject); // destroy pollen
            CurrentState = null; // exit current state (has a chance to find more or return to hive)
        }
        else if (other.CompareTag("Hive")) // in range of hive
        {
            if (heldPollen > 0) DepositPollen(); // has pollen, deposit pollen
        }
        else if (other.CompareTag("Enemy")) // in range of enemy
        {
            ChangeState(GetComponent<FleeState>()); // interrupt current state and flee
        }
    }
    private void DepositPollen()
    {
        Hive.Instance.currentPollen += heldPollen; // add pollen to hive
        heldPollen = 0; // empty pollen
        ChangeState(GetComponent<FindPollen>()); // return to finding pollen
    }
    #endregion
}