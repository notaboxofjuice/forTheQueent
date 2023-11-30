using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Patrol : State
{
    Warrior warrior;
    public override void EnterState()
    {
        warrior = daddy as Warrior;
        myAgent.speed = warrior.GetMoveSpeed();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void UpdateState()
    {
       
    }
}
