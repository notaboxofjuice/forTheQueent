using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Follow : State
{
    Warrior warrior;
    GameObject target;
    bool hasArrived;
    float timer;
    public override void EnterState()
    {
        warrior = Daddy as Warrior;
        target = warrior.GetCurrentTarget();
        myAgent.speed = warrior.GetMoveSpeed();
        hasArrived = true;
        timer = 0;
    }
    public override void ExitState()
    {  
        base.ExitState();
    }
    public override void UpdateState()
    {
        myAgent.SetDestination(target.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            warrior.SetTarget(other.gameObject);
            warrior.StartCombat();
            warrior.ChangeState(GetComponent<Attack>());
        }
    }
}
