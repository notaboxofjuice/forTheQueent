
using UnityEngine;
public class Warrior : Beent
{
    protected GameObject currentTarget;
    public bool inCombat = false;
    protected Hive hive;
    private void Start()
    {
        hive = Hive.Instance;
    }
    protected override void DoSenses() // look for events and trigger transitions
    {
        ChangeState(gameObject.GetComponent<Patrol>());
    }
    public GameObject GetCurrentTarget()
    { 
        return currentTarget;
    }
    public float GetMoveSpeed()
    {
        return MoveSpeed;
    }
    public void EndCombat()
    {
        inCombat = false;
        currentTarget = null;
    }
    public void StartCombat(GameObject target)
    {
        inCombat = true;
        SetTarget(target);
    }
    public void SetTarget(GameObject target) 
    {
        currentTarget = target;
    }
}