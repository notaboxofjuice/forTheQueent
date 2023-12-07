using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Pollen Factory singleton that will spawn new pollen objects at a set interval,
/// and maintains a List<Transform> of all pollen objects for Gatherers to reference
/// </summary>
public class PollenFactory : MonoBehaviour
{
    [Header("Pollen Settings")]
    [SerializeField] GameObject PollenPrefab;
    [SerializeField] int MaxPollen;
    public static List<GameObject> PollenList = new();
    [Header("Spawn Settings")]
    [Tooltip("How long to wait between pollen spawns")][SerializeField] int SpawnCooldown;
    [Tooltip("Minimum distance between pollen and other objects")] [SerializeField] int SpawnRadius;
    [Tooltip("Maximum range of the spawn area")] [SerializeField] int SpawnRange;
    void Start() // Start coroutine to instantiate new pollen objects
    {
        StartCoroutine(SpawnPollen());
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
    IEnumerator SpawnPollen() // Coroutine to spawn new pollen objects
    {
        while (PollenList.Count < MaxPollen) // While there are less than MaxPollen pollen objects in the game world
        {
            // Instantiate new pollen object in a random spot
            Vector3 _spawnHere = FindSpawnPoint();
            if (_spawnHere == Vector3.zero) continue;
            GameObject newPollen = Instantiate(PollenPrefab, _spawnHere, Quaternion.identity);
            // Add new pollen object to PollenList
            PollenList.Add(newPollen);
            // Wait for the SpawnCooldown before spawning another pollen object
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }
}