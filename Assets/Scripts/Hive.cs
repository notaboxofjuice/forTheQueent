using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeentEnums;

//Working on this script: Ky'onna

public class Hive : MonoBehaviour
{
    public int beentPopulation; //making an int because beent is a whole number
    public float currentPollen;
    private float startPollen = 0;
    protected List<GameObject> defenses = new List<GameObject>();
    protected List<Beent> beents = new List<Beent>();



    // Start is called before the first frame update
    void Start()
    {
        currentPollen = startPollen;
    }

    // Update is called once per frame
    void Update()
    {
        CountBeentsByType(BeentType.Worker);
    }

    //Counts the number of defenses in the game
    public int CountDefenses()
    {
        int numDefenses = defenses.Count;

        return numDefenses;
    }

    //Adds a defense object to the defenses
    public void AddDefence(GameObject defenceObject)
    {
        defenses.Add(defenceObject);
    }

    //Count the number of beents in the game
    public int CountAllBeents()
    {
        int beentNum = beents.Count;

        return beents.Count;
    }

    protected int CountBeentsByType(BeentType _type)
    {
        int workerNum = 0;
        int gathererNum = 0;
        int warriorNum = 0;

        switch (_type)
        {
            case BeentType.Worker:
                foreach (Beent beent in beents)
                {
                    if (beent.beentType == _type) workerNum++;
                }
                return workerNum;
            
            case BeentType.Gatherer:
                foreach (Beent beent in beents)
                {
                    if (beent.beentType == _type) gathererNum++;
                }
                return gathererNum;
            
            case BeentType.Warrior:
                foreach (Beent beent in beents)
                {
                    if (beent.beentType == _type) warriorNum++;
                }
                return warriorNum;

            default:
                Debug.Log("Error: Invalid Beent Type");
                return -1;
                break;
        }
    }

    //Adds a beent to the total list of beents
    public void AddBeentToPopulation(Beent beent)
    {
        beents.Add(beent);
    }

}
