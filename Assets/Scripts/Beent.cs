using UnityEngine;
public abstract class Beent : MonoBehaviour
{
    #region Attributes
    protected virtual int currentHealth { get; set; }
    protected virtual State currentState { get; set; }
    protected abstract State reactionState { get; set; }
    #endregion
    #region Operations
    protected virtual float CheckPopulation()
    { // get a count of all objects tagged with Beent
        return GameObject.FindGameObjectsWithTag("Beent").Length;
    }
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    protected abstract void DoStateStuff(State currentState);
    #endregion
}