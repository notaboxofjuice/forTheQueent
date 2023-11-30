using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Follow : State
{
    Warrior warrior;
    Vector3 target;
    public override void EnterState()
    {
        warrior = daddy as Warrior;
        target = warrior.GetCurrentTarget().transform.position;
        myAgent.speed = warrior.GetMoveSpeed();
    }
    public override void ExitState()
    {  
        base.ExitState();
    }
    public override void UpdateState()
    {
        myAgent.SetDestination(target);
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
