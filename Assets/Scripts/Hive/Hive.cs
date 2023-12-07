using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeentEnums;
using UnityEngine.Events;
//Working on this script: Ky'onna

public class Hive : MonoBehaviour
{
    public static Hive Instance { get; private set; } // singelton instantiation
    [SerializeField] int startingNectar = 100; // starting nectar
    
    [Header("READONLY")]
    public int beentPopulation;
    private int pollen;
    public int CurrentPollen
    {
        get { return pollen; }
        set
        {
            pollen = value;
            OnPollenChange.Invoke();
        }
    }
    private int nectar;
    public int CurrentNectar
    {
        get { return nectar; }
        set
        {
            nectar = value;
            OnNectarChange.Invoke();
        }
    }
    public int maxDefenses;
    public List<GameObject> defenses = new();
    public List<Beent> beents = new();

    [Header("References")]
    [Tooltip("Place all the sockets in this scene here")]public List<DefenseSocket> defenseSockets;
    public GameObject hivebounds;

    [Header("Events")]
    public UnityEvent OnPollenChange;
    public UnityEvent OnNectarChange;

    private void Awake()
    {
        #region Singleton instantiation
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
        #endregion

        //Initialization
        CurrentPollen = 0;
        CurrentNectar = startingNectar;
        beentPopulation = 0;
        maxDefenses = defenseSockets.Count; //max defenses = the number of sockets, be sure to add all existing socket to the list
    }

    // Start is called before the first frame update
    void Start()
    {
        return; // todo
    }

    // Update is called once per frame
    void Update()
    {
        
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
        return beentNum;
    }

    public bool HasOpenDefenseSockets()
    {
        int validDefenseNum = 0;

        foreach(DefenseSocket socket in defenseSockets)
        {
            if (!socket.isOccupied)
            {
                validDefenseNum++;
            }
        }

        if (validDefenseNum > 0) return true;
        else return false;
    }

    public int CountBeentsByType(BeentType _type)
    {
        int workerNum = 0;
        int gathererNum = 0;
        int warriorNum = 0;
        int beentbarianNum = 0;

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
            
            case BeentType.Beentbarian:
                foreach (Beent beent in beents)
                {
                    if (beent.beentType == _type) beentbarianNum++;
                }
                return beentbarianNum;

            default:
                Debug.Log("Error: Invalid Beent Type");
                return -1;
        }
    }

    //Adds a beent to the total list of beents
    public void AddBeentToPopulation(Beent beent)
    {
        beents.Add(beent);
    }

    private void OnDestroy()
    {
        // Trigger game over -Leeman
        GameObject.FindGameObjectWithTag("Player").GetComponent<UI>().EndGame();
    }
}
