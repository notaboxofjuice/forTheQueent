using UnityEngine;
using BeentEnums;
/// <summary>
/// Abstract Beent script that the Gatherer, Worker, and Warior scripts inherit from
/// </summary>
public abstract class Beent : MonoBehaviour
{
    #region Attributes
    #region Personal Attributes
    [SerializeField] protected int CurrentHealth;
    [SerializeField] protected float MoveSpeed;
    public BeentType beentType;
    #endregion
    #region State Machine Attributes
    public State CurrentState;
    #endregion
    #endregion
    #region Operations
    #region Unity Operations
    private void Start()
    {
        Camera.main.GetComponent<CameraOffset>().targetTransforms.Add(transform); // add this transform to the camera offset script
    }

    private void OnDestroy()
    {
        Debug.Log("removing self from list");
        //Remove this beent from the list
        Hive.Instance.beents.Remove(this);

        //Update the beent count
        Hive.Instance.beentPopulation = Hive.Instance.CountAllBeents();
    }
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