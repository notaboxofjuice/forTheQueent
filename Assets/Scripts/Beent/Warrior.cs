using BeentEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Warrior : Beent
{
    protected GameObject currentTarget;
    public bool inCombat = false;

    [SerializeField] GameObject DEBUGTARGET;

    protected override void DoSenses() // look for events and trigger transitions
    {
        ChangeState(gameObject.GetComponent<Patrol>());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Collider>() is SphereCollider && other.gameObject.CompareTag("Enemy"))
        {
            currentTarget = other.gameObject;
            ChangeState(gameObject.GetComponent<Attack>());
            inCombat = true;
            Debug.Log("New Enemy Found");
        }
        else if (GetComponent<Collider>() is SphereCollider && other.gameObject.CompareTag("Beent") && !inCombat && other.gameObject.GetComponent<Beent>().beentType != BeentType.Warrior)
        {
            currentTarget = other.gameObject;
            ChangeState(gameObject.GetComponent<Follow>());
            Debug.Log("Moving to ecsort Friendly");  
        }
    }
    public GameObject GetCurrentTarget()
    { 
        return currentTarget;
    }
    public float GetMoveSpeed()
    {
        return MoveSpeed;
    }
    public void EndCombat()
    {
        inCombat = false;
        currentTarget = null;
    }
    public void StartCombat()
    {
        inCombat = true;
    }
    public void SetTarget(GameObject target) 
    {
        currentTarget = target;
    }
}