using UnityEngine;
using BeentEnums;
/// <summary>
/// Abstract Beent script that the Gatherer, Worker, and Warior scripts inherit from
/// </summary>
public abstract class Beent : MonoBehaviour
{
    #region Attributes
    #region Personal Attributes
    protected Pathfinding pathfinder;
    [SerializeField] protected int CurrentHealth;
    [SerializeField] protected float MoveSpeed;
    public BeentType beentType;
    #endregion
    #region State Machine Attributes
    protected State CurrentState { get; set; }
    #endregion
    #endregion
    #region Operations
    #region Unity Operations
    protected virtual void FixedUpdate() // perform current state, or do senses
    {
        if (CurrentState != null) CurrentState.UpdateState();
        else DoSenses();
    }
    #endregion
    #region Local Operations
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) Destroy(gameObject);
    }
    #endregion
    #region State Machine Operations
    public void ChangeState(State newState)
    {
        if (CurrentState != null) CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
    protected abstract void DoSenses(); // check events and trigger transitions
    #endregion
    #endregion
}