using UnityEngine;
using UnityEngine.AI; // NavMeshAgent
//Working on this script: Ky'onna
public abstract class State : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent myAgent; // NavMeshAgent reference
    private void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>(); // Get NavMeshAgent
    }
    public abstract void EnterState(); // Called when a state is first entered, include logic for assigning variables and other initialization things
    public abstract void UpdateState(); // The states main functionality whether that be fleeing, processing, etc
    public abstract void ExitState(); // Any cleanup you want to do before exiting a state
}