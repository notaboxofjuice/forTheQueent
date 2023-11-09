using BeentEnums;
using UnityEngine;
public class Warrior : Beent
{
    Vector3 currentTarget;
    bool isChasing = false;
    protected override void DoSenses() // look for events and trigger transitions
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            currentTarget = other.transform.position;
            isChasing = true;
            Debug.Log("New Enemy Found");
        }
        if (isChasing)
        {
            pathfinder.CalculatePath(transform.position, currentTarget);
        }
    }
}