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
        CheckforEnemies();
    }
    void SetUpPatrol()
    {
        patrolPoints = new List<Vector3>();
        pointIndex = 0;
        Vector3 patrolPoint;
        hasArrived = true;
        timer = 0;
        while(patrolPoints.Count < 3)
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
            patrolPoints.Add(Hive.Instance.gameObject.transform.position);
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
                FindEnemy();
            }
            myAgent.SetDestination(patrolPoints[pointIndex]);
        }
        else
        {
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
        if (other.gameObject.CompareTag("Beent") && !warrior.inCombat && other.gameObject.GetComponent<Beent>().beentType != BeentType.Warrior)
        {
            warrior.SetTarget(other.gameObject);
            Debug.Log("Moving to ecsort Friendly");
            ExitState();
            warrior.ChangeState(gameObject.GetComponent<Follow>());
        }
    }
    void FindEnemy()
    {
        if(EnemySpawner.enemyList.Count > 0)
        {
            int randBarb = Random.Range(0, EnemySpawner.enemyList.Count);
            warrior.StartCombat(EnemySpawner.enemyList[randBarb].gameObject);
            Debug.Log("New Enemy Found");
            ExitState();
            warrior.ChangeState(gameObject.GetComponent<Attack>());
        }
    }
    void CheckforEnemies()
    {
        foreach (GameObject Beentbarian in EnemySpawner.enemyList)
        {
            if (Vector3.Distance(transform.position, Beentbarian.transform.position) < myAgent.stoppingDistance)
            {
                warrior.StartCombat(Beentbarian);
                Debug.Log("New Enemy Found");
                ExitState();
                warrior.ChangeState(gameObject.GetComponent<Attack>());
                break;
            }
        }
    }
}
