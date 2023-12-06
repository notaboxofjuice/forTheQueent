using UnityEngine;
using UnityEngine.AI; // NavMeshAgent
//Working on this script: Ky'onna & Leeman
public abstract class State : MonoBehaviour
{
    protected Beent Daddy => GetComponent<Beent>();
    protected NavMeshAgent myAgent; // NavMeshAgent reference
    private void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }
    public abstract void EnterState(); // Called when a state is first entered, initialization things
    public abstract void UpdateState(); // The states main functionality whether that be fleeing, processing, etc
    public virtual void ExitState() // Any cleanup before exiting a state. Remember to do base.ExitState() if you override this method
    {
        Daddy.CurrentState = null;
    }
}