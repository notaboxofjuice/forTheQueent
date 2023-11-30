using BeentEnums;
using UnityEngine;
/// <summary>
/// Assigned to player object, allows them to produce beents and pause the game
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] int BeentCost; // Cost of producing a beent
    [SerializeField] Transform spawnPoint; // Where beents will spawn
    [Header("Prefabs")]
    [SerializeField] GameObject gathererPrefab; // Gatherer prefab
    [SerializeField] GameObject warriorPrefab; // Warrior prefab
    [SerializeField] GameObject workerPrefab; // Worker prefab
    public void ProduceGatherer() => ProduceBeent(BeentType.Gatherer); // Produce a gatherer
    public void ProduceWarrior() => ProduceBeent(BeentType.Warrior); // Produce a warrior
    public void ProduceWorker() => ProduceBeent(BeentType.Worker); // Produce a worker
    private void ProduceBeent(BeentType type)
    {
        if (!CanProduce()) return; // if player can't produce, exit
        Debug.Log("producing " + type);
        // switch statement to pick the beent to instantiate
        switch (type)
        {
            case BeentType.Gatherer:
                Instantiate(gathererPrefab, spawnPoint.position, Quaternion.identity);
                break;
            case BeentType.Warrior:
                Instantiate(warriorPrefab, spawnPoint.position, Quaternion.identity);
                break;
            case BeentType.Worker:
                Instantiate(workerPrefab, spawnPoint.position, Quaternion.identity);
                break;
        }
    }
    private bool CanProduce()
    {
        int _nectar = Hive.Instance.currentNectar; // get current nectar
        Debug.Log("Current nektar: " + _nectar);
        if (_nectar < BeentCost) return false; // if player doesn't have enough nectar, exit
        Hive.Instance.currentNectar -= BeentCost; // subtract nectar
        return true;
    }
}