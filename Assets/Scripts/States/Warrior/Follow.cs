using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


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
        if(Vector3.Distance(target, transform.position) <= myAgent.stoppingDistance)
        {
            myAgent.SetDestination(target);
        }
        else
        {
            myAgent.ResetPath();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Collider>() is SphereCollider && other.gameObject.CompareTag("Enemy"))
        {
            warrior.SetTarget(other.gameObject);
            warrior.StartCombat();
            warrior.ChangeState(GetComponent<Attack>());
        }
    }
}
