using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Build : AtomicGoal
{
    Vector3 locationToBuild;
    GameObject meGameObject;
    bool hasBuild { get; set; }

    public Build(GameObject me, Vector3 location)
    {
        goalName = "AtomicGoal: building a building";

        meGameObject = me;
        locationToBuild = location;
    }

    public override Vector3 Process()
    {
        Thread.Sleep(1000);
        hasBuild = true;

        meGameObject.GetComponent<MovingEntity>().world.CreateBuilding(locationToBuild);

        Completed();

        return Vector2.zero;
    }
}
