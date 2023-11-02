using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Working on this script: Ky'onna

public class Hive : MonoBehaviour
{
    public int beentPopulation; //making an int because beent is a whole number
    public float currentPollen;
    private float startPollen = 0;
    protected List<GameObject> defenses = new List<GameObject>();
    protected List<GameObject> beents = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        currentPollen = startPollen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Counts the number of defenses in the scene
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

    public void AddBeentToPopulation()
    {

    }

}
