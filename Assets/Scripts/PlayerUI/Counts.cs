using TMPro;
using UnityEngine;
using BeentEnums;
public class Counts : MonoBehaviour
{
    private TextMeshProUGUI PopulationText => GetComponent<TextMeshProUGUI>();
    private void Start()
    {
        Hive.Instance.OnPopulationChange.AddListener(UpdateText);
    }
    private void UpdateText()
    {
        string gatherers = Hive.Instance.CountBeentsByType(BeentType.Gatherer).ToString();
        string warriors = Hive.Instance.CountBeentsByType(BeentType.Warrior).ToString();
        string workers = Hive.Instance.CountBeentsByType(BeentType.Worker).ToString();
        PopulationText.text = gatherers + "\t" + warriors + "\t" + workers;
    }
}