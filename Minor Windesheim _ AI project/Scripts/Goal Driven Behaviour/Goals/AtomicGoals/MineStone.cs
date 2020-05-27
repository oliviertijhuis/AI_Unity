using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class MineStone : AtomicGoal
{
    bool hasStone { get; set; }

    public MineStone()
    {
        goalName = "AtomicGoal: Mining stone";
    }

    public override Vector3 Process()
    {
        Thread.Sleep(1000);
        hasStone = true;
        Completed();

        return Vector2.zero;
    }
}
