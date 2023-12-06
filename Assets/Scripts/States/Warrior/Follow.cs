using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Follow : State
{
    Warrior warrior;
    Vector3 target;
    float followDistance = 2f;
    public override void EnterState()
    {
        warrior = Daddy as Warrior;
        target = warrior.GetCurrentTarget().transform.position;
        myAgent.speed = warrior.GetMoveSpeed();
    }
    public override void ExitState()
    {  
        base.ExitState();
    }
    public override void UpdateState()
    {
        if(Vector3.Distance(target, transform.position) < followDistance)
        {
            myAgent.SetDestination(target);
            myAgent.isStopped = false;
        }
        else
        {
            myAgent.isStopped = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Collider>() is SphereCollider && other.gameObject.CompareTag("Enemy"))
        {
            warrior.SetTarget(other.gameObject);
            warrior.ChangeState(GetComponent<Attack>());
        }
    }
}
