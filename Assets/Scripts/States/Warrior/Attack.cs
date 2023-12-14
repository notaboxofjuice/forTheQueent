using BeentEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    Warrior warrior;
    GameObject target;
    [Tooltip("Time in seconds between each attack")]
    [SerializeField] protected float attackSpeed = 1f;
    [Tooltip("Damage done by each attack")]
    [SerializeField] public int attackDamage = 1;
    //[SerializeField] float staleTime = 45f;
    bool isAttacking = false;
    public override void EnterState()
    {
        warrior = Daddy as Warrior;
        if(warrior.GetCurrentTarget() != null)
        {
            target = warrior.GetCurrentTarget();
        }
        myAgent.speed = warrior.GetMoveSpeed();
        if (gameObject.CompareTag("Enemy"))
        {
            Invoke(nameof(ExitState), 10f);
        }
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
            if(myAgent.remainingDistance < myAgent.stoppingDistance && !isAttacking) 
            {
                StartCoroutine(AttackEnemy());  
            }
            else
            {
                myAgent.SetDestination(target.transform.position);
            }
        }
        else
        {
            ExitState();
        }
    }
    IEnumerator AttackEnemy()
    {
        isAttacking = true;
        switch(warrior.GetCurrentTarget().tag)
        {
            case "DefenseObj":
                warrior.GetCurrentTarget().GetComponent<DefenseObj>().TakeDamage(attackDamage);
                break;
            case "Hive":
                Hive.Instance.Health -= attackDamage;
                break;
            default:
                warrior.GetCurrentTarget().GetComponent<Beent>().TakeDamage(attackDamage);
                break;
        }
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }
}
