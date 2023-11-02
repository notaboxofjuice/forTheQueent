using UnityEngine;
using BeentEnums;
public abstract class Beent : MonoBehaviour
{
    public BeentType beentType;

    #region Attributes
    #region Personal Attributes
    [SerializeField] int CurrentHealth;
    [SerializeField] int MoveSpeed;
    #endregion
    #region State Machine Attributes
    protected virtual State CurrentState { get; set; }
    #endregion
    #endregion
    #region Operations
    #region Unity Operations
    protected virtual void Update()
    {
        if (CurrentState != null) CurrentState.UpdateState();
        DoSenses();
    }
    #endregion
    #region Local Operations
    protected virtual float CheckPopulation()
    { // get a count of all objects tagged with Beent
        return GameObject.FindGameObjectsWithTag("Beent").Length;
    }
    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) Destroy(gameObject);
    }
    #endregion
    #region State Machine Operations
    protected virtual void ChangeState(State newState)
    {
        if (CurrentState != null) CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
    protected abstract void DoSenses(); // check events and trigger transitions
    #endregion
    #endregion
}