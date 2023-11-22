using UnityEngine;
[CreateAssetMenu(fileName = "CounterSO", menuName = "ScriptableObjects/CounterSO", order = 1)]
public class CounterSO : ScriptableObject
{
    public int MaxCount { get; private set; }
    public int CurrentCount { get; set; }
    public void CheckScore(int score)
    {
        if (score > MaxCount)
        {
            CurrentCount = 0; // Reset current count
            MaxCount = score; // Update max count
        }
    }
}