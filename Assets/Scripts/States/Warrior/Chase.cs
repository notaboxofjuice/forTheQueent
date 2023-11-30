using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    float MoveSpeed;
    Vector3 target;
    [Tooltip("Close Approach Distance that stops chase when reached.")]
    [SerializeField] float closeApproach = 1f;
    public override void EnterState()
    {
        MoveSpeed = gameObject.GetComponent<Warrior>().GetMoveSpeed();
        myAgent.SetDestination(gameObject.GetComponent<Warrior>().GetCurrentTarget());
    }
    public override void ExitState()
    {
        StopAllCoroutines();
    }
    public override void UpdateState()
    {
        StartCoroutine(ChaseTarget());
    }
    IEnumerator ChaseTarget()
    {
        yield return new WaitForFixedUpdate();
        
    }
}
