using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        // Find a random point in the spawn range
        Vector3 spawnPoint = new(Random.Range(-SpawnRange, SpawnRange), 0, Random.Range(-SpawnRange, SpawnRange));
        while (Physics.CheckSphere(spawnPoint, SpawnRadius)) // While the spawn point is too close to other objects
        {
            spawnPoint = new(Random.Range(-SpawnRange, SpawnRange), 0, Random.Range(-SpawnRange, SpawnRange)); // Find a new spawn point
        }
        // Return the spawn point
        return spawnPoint;
    }
    IEnumerator SpawnPollen() // Coroutine to spawn new pollen objects
    {
        while (PollenList.Count < MaxPollen) // While there are less than MaxPollen pollen objects in the game world
        {
            // Instantiate new pollen object in a random spot
            GameObject newPollen = Instantiate(PollenPrefab, FindSpawnPoint(), Quaternion.identity);
            // Add new pollen object to PollenList
            PollenList.Add(newPollen);
            // Wait for the SpawnCooldown before spawning another pollen object
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }
}