using UnityEngine;
using BeentEnums;

public abstract class Beent : MonoBehaviour
{
    #region Attributes
    #region Personal Attributes
<<<<<<< Updated upstream
    [SerializeField] protected int CurrentHealth;
    [SerializeField] protected float MoveSpeed;
    [SerializeField] public BeentType beentType;
=======
    [SerializeField] int CurrentHealth;
    [SerializeField] float MoveSpeed;
>>>>>>> Stashed changes
    #endregion
    #region State Machine Attributes
    protected State CurrentState { get; set; }
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
    protected float CheckPopulation(BeentType beent)
    { // get a count of all beents of desired type
        return FindObjectsOfType<Warrior>().Length;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) Destroy(gameObject);
    }
    #endregion
    #region State Machine Operations
    protected void ChangeState(State newState)
    {
        if (CurrentState != null) CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
    protected abstract void DoSenses(); // check events and trigger transitions
    #endregion
    #endregion
}