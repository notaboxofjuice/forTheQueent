using UnityEngine;
public class Beentbarian : Warrior
{
    Hive hive;
    private void Start()
    {
        hive = Hive.Instance;
    }
    protected override void DoSenses()
    {
        FindTarget();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Beent") && currentTarget == null)
        {
            currentTarget = other.gameObject;
            ChangeState(gameObject.GetComponent<Attack>());
        }
    }
    private void OnDestroy()
    {
        UI.WarriorProductivity++; // Increment the warrior productivity for score calculation -Leeman
    }
    void FindTarget()
    {
        int randChoice = Random.Range(0, 2);
        if(randChoice == 1)
        {
            if(hive.beents.Count > 0) 
            {
                int randIndex = Random.Range(0, hive.beents.Count);
                currentTarget = hive.beents[randIndex].gameObject;
                ChangeState(gameObject.GetComponent<Attack>());
            }
            else
            {
                ChargeHive();
            }
        }
        else
        {
            if(hive.defenses.Count > 0)
            {
                int randIndex = Random.Range(0, hive.defenses.Count);
                currentTarget = hive.defenses[randIndex].gameObject;
                ChangeState(gameObject.GetComponent<Attack>());
            }
            else
            {
                ChargeHive();
            }
        }
    }
    void ChargeHive()
    {
        currentTarget = Hive.Instance.gameObject;
        ChangeState(gameObject.GetComponent<Attack>());
    }
}