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
        inCombat = true;
    }
    private void OnDestroy()
    {
        UI.WarriorProductivity++; // Increment the warrior productivity for score calculation -Leeman
    }
    void FindTarget()
    {
        int randChoice = Random.Range(0, 3);
        if(randChoice == 0)
        {
            currentTarget = GameObject.FindGameObjectWithTag("Hive").gameObject;
            ChangeState(gameObject.GetComponent<Attack>());
        }
        else if(randChoice == 1)
        {
            if(hive.beents.Count > 0) 
            {
                int randIndex = Random.Range(0, hive.beents.Count);
                currentTarget = hive.beents[randIndex].gameObject;
                ChangeState(gameObject.GetComponent<Attack>());
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
        }
    }
}