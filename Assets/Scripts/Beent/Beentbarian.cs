using UnityEngine;
public class Beentbarian : Warrior
{
    private void OnDestroy()
    {
        UI.WarriorProductivity++; // Increment the warrior productivity for score calculation -Leeman
    }
}