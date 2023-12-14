using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Follow : State
{
    Warrior warrior;
    GameObject target;
    public override void EnterState()
    {
        warrior = Daddy as Warrior;
        target = warrior.GetCurrentTarget();
        myAgent.speed = warrior.GetMoveSpeed();
    }
    public override void ExitState()
    {  
        base.ExitState();
    }
    public override void UpdateState()
    {
        if(warrior.GetCurrentTarget() != null)
        {
            myAgent.SetDestination(target.transform.position);
        }
        else
        {
            ExitState();
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject != null)
        {
            warrior.SetTarget(other.gameObject);
            warrior.StartCombat();
            ExitState();
            warrior.ChangeState(GetComponent<Attack>());
        }
    }
}
