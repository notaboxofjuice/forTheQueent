using UnityEngine;
public class Beentbarian : Warrior
{
    Hive hive;
    private void Start()
    {
        hive = GameObject.FindGameObjectWithTag("Hive").GetComponent<Hive>();
    }
    protected override void DoSenses()
    {
        FindTarget();
        ChangeState(gameObject.GetComponent<Attack>());
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
        }
        else if(randChoice == 1)
        {
            if(hive.beents.Count > 0) 
            {
                int randIndex = Random.Range(0, hive.beents.Count);
                currentTarget = hive.beents[randIndex].gameObject;
            }
        }
        else
        {
            if(hive.defenses.Count > 0)
            {
                int randIndex = Random.Range(0, hive.defenses.Count);
                currentTarget = hive.defenses[randIndex];
            }
        }
    }
}