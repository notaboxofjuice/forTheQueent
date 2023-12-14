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
        if (other.gameObject.CompareTag("Enemy"))
        {
            warrior.SetTarget(other.gameObject);
            warrior.StartCombat();
            Debug.Log("New Enemy Found");
            ExitState();
            warrior.ChangeState(gameObject.GetComponent<Attack>());
        }
    }
}
