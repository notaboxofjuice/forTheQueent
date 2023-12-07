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
    [SerializeField] GameObject beentHolder; //holds the beents for organization
    public void ProduceGatherer() => ProduceBeent(BeentType.Gatherer); // Produce a gatherer
    public void ProduceWarrior() => ProduceBeent(BeentType.Warrior); // Produce a warrior
    public void ProduceWorker() => ProduceBeent(BeentType.Worker); // Produce a worker
    private void ProduceBeent(BeentType _type)
    {
        if (!CanProduce()) return; // if player can't produce, exit
        Debug.Log("Producing " + _type);
        GameObject _beentPrefab = ChooseBeent(_type);
        GameObject newBeent = Instantiate(_beentPrefab, spawnPoint.position, spawnPoint.rotation); // spawn beent
        newBeent.transform.SetParent(beentHolder.transform); //put beents into the holder
        Hive.Instance.AddBeentToPopulation(_beentPrefab.GetComponent<Beent>()); //update beent list

    }
    private GameObject ChooseBeent(BeentType type)
    {
        GameObject _beentPrefab = null;
        switch (type)
        {
            case BeentType.Gatherer:
                _beentPrefab = gathererPrefab;
                break;
            case BeentType.Warrior:
                _beentPrefab = warriorPrefab;
                break;
            case BeentType.Worker:
                _beentPrefab = workerPrefab;
                break;
        }
        return _beentPrefab;
    }
    private bool CanProduce()
    {
        int _nectar = Hive.Instance.CurrentNectar; // get current nectar
        Debug.Log("Current nektar: " + _nectar);
        if (_nectar < BeentCost) return false; // if player doesn't have enough nectar, exit
        Hive.Instance.CurrentNectar -= BeentCost; // subtract nectar
        return true;
    }
}