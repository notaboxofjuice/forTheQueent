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
        patrolPoints.Clear();
        pointIndex = 0;
        Vector3 patrolPoint;
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
                }
                else continue;
            } while (Physics.CheckSphere(patrolPoint, spawnRadius, 8) && _attempts > 0); // While the spawn point is too close to other objects
        }  
    }
    void FollowPatrolPath()
    {
        myAgent.SetDestination(patrolPoints[pointIndex]);
        if(myAgent.remainingDistance <= 1f)
        {
            if(pointIndex < patrolPoints.Count - 1)
            {
                pointIndex++;
            }
            else
            {
                pointIndex = 0;
            }
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
