using UnityEngine;
//Working on this script: Ky'onna
public abstract class State : MonoBehaviour
{
    /*protected Beent beent; //reference to the beent that the state machine will manipulate, context object
    public State(Beent beent)
    {
        this.beent = beent;
    }*/
    // unsure if the above stuff is necessary, we'll revisit later -Leeman
    protected abstract Transform ChooseDestination(); // Abstract because destination choosing is different for each class
    public abstract void EnterState(); // Called when a state is first entered, include logic for assigning variables and other initialization things
    public abstract void UpdateState(); // The states main functionality whether that be fleeing, processing, etc
    public abstract void ExitState(); // Any cleanup you want to do before exiting a state
}