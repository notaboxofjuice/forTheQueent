using BeentEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Warrior : Beent
{
    protected GameObject currentTarget;
    
    protected override void DoSenses() // look for events and trigger transitions
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(GetComponent<Collider>() is SphereCollider && other.gameObject.CompareTag("Enemy"))
        {
            currentTarget = other.gameObject;
            ChangeState(gameObject.GetComponent<Attack>());
            Debug.Log("New Enemy Found");
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
}