using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ChopWood : AtomicGoal
{
    bool hasWood { get; set; }

    public ChopWood()
    {
        goalName = "AtomicGoal: chopping wood";
    }

    public override Vector3 Process()
    {
        Thread.Sleep(1000);
        hasWood = true;
        Completed();

        return Vector2.zero;
    }

}
