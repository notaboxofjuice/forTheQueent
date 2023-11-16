using BeentEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Warrior : Beent
{
    protected Vector3 currentTarget;
    protected override void DoSenses() // look for events and trigger transitions
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            currentTarget = other.transform.position;
            ChangeState(gameObject.GetComponent<Chase>());
            Debug.Log("New Enemy Found");
        }
    }
    public Vector3 GetCurrentTarget()
    { 
        return currentTarget;
    }
    public float GetMoveSpeed()
    {
        return MoveSpeed;
    }
}