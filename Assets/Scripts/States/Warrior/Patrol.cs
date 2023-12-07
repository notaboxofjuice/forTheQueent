using BeentEnums;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Patrol : State
{
    Warrior warrior;
    float spawnRange = 30f;
    float spawnRadius = 1f;
    List<Vector3> patrolPoints;
    int pointIndex;
    bool hasArrived;
    float timer;
    float tryTime = 10f;
    public override void EnterState()
    {
        warrior = Daddy as Warrior;
        myAgent.speed = warrior.GetMoveSpeed();
        SetUpPatrol();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void UpdateState()
    {
       FollowPatrolPath();
    }
    void SetUpPatrol()
    {
        patrolPoints = new List<Vector3>();
        pointIndex = 0;
        Vector3 patrolPoint;
        hasArrived = true;
        timer = 0;
        while(patrolPoints.Count < 5)
        {
            int _attempts = 5; // Number of attempts to find a spawn point
            do
            {
                _attempts--; // Decrement attempts
                             // Find a new spawn point
                patrolPoint = new(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
                //check is point on nav mesh
                if (NavMesh.SamplePosition(patrolPoint, out NavMeshHit hit, spawnRadius, NavMesh.AllAreas))
                {
                    patrolPoints.Add(hit.position);
                    Debug.Log(hit.position);
                }
                else continue;
            } while (Physics.CheckSphere(patrolPoint, spawnRadius, 8) && _attempts > 0); // While the spawn point is too close to other objects
        }  
    }
    void FollowPatrolPath()
    {
        if(hasArrived)
        {
            hasArrived = false;
            myAgent.ResetPath(); 
            timer = 0;
            if (pointIndex < patrolPoints.Count - 1)
            {
                pointIndex++;
            }
            else
            {
                pointIndex = 0;
            }
            myAgent.SetDestination(patrolPoints[pointIndex]);
        }
        else
        {
            Debug.Log("Attempting to move to index: " + pointIndex);
            Debug.Log(hasArrived);
            if (myAgent.remainingDistance <= myAgent.stoppingDistance || timer > tryTime)
            {
                hasArrived = true;
            }
            timer += Time.deltaTime;
            Debug.Log("Warrior has been trying to follow path for: " + Mathf.Round(timer) + " Seconds");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Collider>() is SphereCollider && other.gameObject.CompareTag("Enemy"))
        {
            warrior.SetTarget(other.gameObject);
            warrior.StartCombat();
            Debug.Log("New Enemy Found");
            ExitState();
            warrior.ChangeState(gameObject.GetComponent<Attack>());
        }
        else if (GetComponent<Collider>() is SphereCollider && other.gameObject.CompareTag("Beent") && !warrior.inCombat && other.gameObject.GetComponent<Beent>().beentType != BeentType.Warrior)
        {
            warrior.SetTarget(other.gameObject);
            Debug.Log("Moving to ecsort Friendly");
            ExitState();
            warrior.ChangeState(gameObject.GetComponent<Follow>());
        }
    }
}
