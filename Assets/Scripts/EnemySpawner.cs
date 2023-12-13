using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Enemy Spawner singleton that will spawn new beentbarians at a set interval,
/// and maintains a List<Transform> of all beentbarians
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int maxEnemies;
    public static List<GameObject> enemyList = new();
    [Header("Spawn Settings")]
    [Tooltip("How long to wait between beentbarian spawns")][SerializeField] int SpawnCooldown;
    [Tooltip("Minimum distance between beentbarian and other objects")][SerializeField] int SpawnRadius;
    [Tooltip("Maximum range of the spawn area")][SerializeField] int SpawnRange;
    void Start() // Start coroutine to instantiate new beentbarians
    {
        StartCoroutine(SpawnEnemy());
    }
    private Vector3 FindSpawnPoint() // Find a random point in the game world that is not too close to other objects
    {
        Vector3 _spawnPoint;
        int _attempts = 5; // Number of attempts to find a spawn point
        do
        {
            _attempts--; // Decrement attempts
            // Find a new spawn point
            _spawnPoint = new(Random.Range(-SpawnRange, SpawnRange), 0, Random.Range(-SpawnRange, SpawnRange));
            //check is point on nav mesh
            if (NavMesh.SamplePosition(_spawnPoint, out NavMeshHit hit, SpawnRadius, NavMesh.AllAreas))
            {
                _spawnPoint = hit.position;
            }
            else _spawnPoint = Vector3.zero;
        } while (Physics.CheckSphere(_spawnPoint, SpawnRadius, 8) && _attempts > 0); // While the spawn point is too close to other objects
        return _spawnPoint; // Return the spawn point
    }
    IEnumerator SpawnEnemy() // Coroutine to spawn new beentbarians
    {
        while (enemyList.Count < maxEnemies) // While there are less than maxEnemy beentbarians in the game world
        {
            // Instantiate new beentbarian object in a random spot
            Vector3 _spawnHere = FindSpawnPoint();
            if (_spawnHere == Vector3.zero) continue;
            GameObject newbeentbarian = Instantiate(enemyPrefab, _spawnHere, Quaternion.identity);
            // Add new beentbarian object to beentbarianList
            enemyList.Add(newbeentbarian);
            //make the beentbarian object a child of this object, for organization
            newbeentbarian.transform.SetParent(this.gameObject.transform);
            // Wait for the SpawnCooldown before spawning another beentbarian object
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }
}