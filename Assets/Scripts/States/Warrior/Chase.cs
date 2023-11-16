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
    }
    public override void ExitState()
    {
        StopAllCoroutines();
    }
    public override void UpdateState()
    {
        StartCoroutine(ChaseTarget());
    }
    protected override Vector3 ChooseDestination()
    {
        Warrior warrior = gameObject.GetComponent<Warrior>();
        target = warrior.GetCurrentTarget();
        return target;
    }
    IEnumerator ChaseTarget()
    {
        yield return new WaitForFixedUpdate();
        MyPathfinder.CalculatePath(transform.position, ChooseDestination());
        if (MyPathfinder.lastFoundPath != null && MyPathfinder.lastFoundPath.Count > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, MyPathfinder.lastFoundPath[1].worldCoords, MoveSpeed * Time.deltaTime);
        }
        float distanceToTarget = Vector3.Distance(transform.position, target);
        if (distanceToTarget <= closeApproach)
        {
            StopCoroutine(ChaseTarget());
            //Exit chase state here
        }
    }
}
