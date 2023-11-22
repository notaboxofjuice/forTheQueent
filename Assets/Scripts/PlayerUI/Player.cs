using BeentEnums;
using UnityEngine;
/// <summary>
/// Assigned to player object, allows them to produce beents and pause the game
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] int BeentCost; // Cost of producing a beent
    public void ProduceGatherer() => ProduceBeent(BeentType.Gatherer); // Produce a gatherer
    public void ProduceWarrior() => ProduceBeent(BeentType.Warrior); // Produce a warrior
    public void ProduceWorker() => ProduceBeent(BeentType.Worker); // Produce a worker
    private void ProduceBeent(BeentType type)
    {
        if (!CanProduce()) return; // if player can't produce, exit
        // switch statement to pick the beent to instantiate
        switch (type)
        {
            case BeentType.Gatherer:
                Instantiate(Resources.Load("Prefabs/Gatherer"), transform.position, Quaternion.identity);
                break;
            case BeentType.Warrior:
                Instantiate(Resources.Load("Prefabs/Warrior"), transform.position, Quaternion.identity);
                break;
            case BeentType.Worker:
                Instantiate(Resources.Load("Prefabs/Worker"), transform.position, Quaternion.identity);
                break;
        }
    }
    private bool CanProduce()
    {
        if (Hive.Instance.currentNectar < BeentCost) return false; // if player doesn't have enough nectar, exit
        Hive.Instance.currentNectar -= BeentCost; // subtract nectar
        return true;
    }
}