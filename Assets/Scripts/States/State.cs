using UnityEngine;
using UnityEngine.AI; // NavMeshAgent
//Working on this script: Ky'onna
public abstract class State : MonoBehaviour
{
    private Beent daddy; // Beent reference
    [SerializeField] protected NavMeshAgent myAgent; // NavMeshAgent reference
    private void Awake()
    {
        daddy = GetComponent<Beent>(); // Get Beent
        myAgent = GetComponent<NavMeshAgent>(); // Get NavMeshAgent
    }
    public abstract void EnterState(); // Called when a state is first entered, initialization things
    public abstract void UpdateState(); // The states main functionality whether that be fleeing, processing, etc
    public virtual void ExitState() // Any cleanup before exiting a state. Remember to do base.ExitState() if you override this method
    {
        daddy.CurrentState = null;
    }
}