using BeentEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    Warrior warrior;
    Vector3 target;
    [Tooltip("Time in seconds between each attack")]
    [SerializeField] protected float attackSpeed = 1f;
    [Tooltip("Damage done by each attack")]
    [SerializeField] public int attackDamage = 1;
    public override void EnterState()
    {
        warrior = Daddy as Warrior;
        if(warrior.GetCurrentTarget() != null)
        {
            target = warrior.GetCurrentTarget().transform.position;
        }
        myAgent.speed = warrior.GetMoveSpeed();
        Invoke(nameof(ExitState), 15f);
    }
    public override void ExitState()
    {
        base.ExitState();
        StopAllCoroutines();
        warrior.EndCombat();
    }
    public override void UpdateState()
    {
        if(warrior.GetCurrentTarget() != null)
        {
            float dTT = Vector3.Distance(target, transform.position);
            if(dTT < myAgent.stoppingDistance) 
            {
                StartCoroutine(AttackEnemy());  
            }
            else
            {
                myAgent.SetDestination(target);
            }
        }
        else
        {
            ExitState();
        }
    }
    IEnumerator AttackEnemy()
    {
        yield return new WaitForSeconds(attackSpeed);
        if (warrior.GetCurrentTarget().CompareTag("Enemy"))
        {
            warrior.GetCurrentTarget().GetComponent<Beentbarian>().TakeDamage(attackDamage);
        }
        else if(warrior.GetCurrentTarget().CompareTag("Beent"))
        {
            warrior.GetCurrentTarget().GetComponent<Beent>().TakeDamage(attackDamage);
        }
        else if(warrior.GetCurrentTarget().CompareTag("DefenseObj"))
        {
            warrior.GetCurrentTarget().GetComponent<DefenseObj>().TakeDamage(attackDamage);
        }
        else
        {
            Hive.Instance.Health -= attackDamage;
        }
    }
}
