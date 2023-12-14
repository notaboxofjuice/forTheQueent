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
        if (warrior.GetCurrentTarget() != null)
        {
            myAgent.SetDestination(target.transform.position);
        }
        else
        {
            ExitState();
        }
        CheckforEnemies();
    }
    void CheckforEnemies()
    {
        foreach(GameObject Beentbarian in EnemySpawner.enemyList)
        {
            if(Vector3.Distance(transform.position, Beentbarian.transform.position) < myAgent.stoppingDistance)
            {
                warrior.StartCombat(Beentbarian);
                Debug.Log("New Enemy Found");
                ExitState();
                warrior.ChangeState(gameObject.GetComponent<Attack>());
                break;
            }
        }
    }
}
