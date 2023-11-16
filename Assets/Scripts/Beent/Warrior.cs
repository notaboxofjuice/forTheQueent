using BeentEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Warrior : Beent
{
    Vector3 currentTarget;
    bool isChasing = false;
    public List<Node> currentPath;
    [Tooltip("Close Approach Distance that stops chase when reached.")]
    [SerializeField] float closeApproach = 1f;
    protected override void DoSenses() // look for events and trigger transitions
    {
        
    }
    IEnumerator ChaseTarget()
    {
        yield return new WaitForFixedUpdate();
        if (isChasing)
        {
            pathfinder.CalculatePath(transform.position, currentTarget);
            if(pathfinder.lastFoundPath != null )
            {
                transform.position = Vector3.MoveTowards(transform.position, pathfinder.lastFoundPath[1].worldCoords, MoveSpeed * Time.deltaTime);
            }  
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget);
            if(distanceToTarget <= closeApproach)
            {
                isChasing = false;
                StopCoroutine(ChaseTarget());
            }        
            StartCoroutine(ChaseTarget());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            currentTarget = other.transform.position;
            isChasing = true;
            Debug.Log("New Enemy Found");
            StartCoroutine(ChaseTarget());
        }
    }
}