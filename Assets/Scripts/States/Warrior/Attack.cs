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
    [Tooltip("The range in which attacks will be attempted. If enemy leaves this range switch to new state")]
    [SerializeField] protected float attackRange = 1f;
    public override void EnterState()
    {
        warrior = Daddy as Warrior;
        target = warrior.GetCurrentTarget().transform.position;
        myAgent.speed = warrior.GetMoveSpeed();
    }
    public override void ExitState()
    {
        base.ExitState();
        StopAllCoroutines();
        warrior.EndCombat();
    }
    public override void UpdateState()
    {
        if(target != null)
        {
            float dTT = Vector3.Distance(target, transform.position);
            if(dTT < attackRange) 
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
        gameObject.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(attackSpeed);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(GetComponent<Collider>() is BoxCollider && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Beentbarian>().TakeDamage(attackDamage);
        }
    }
}
