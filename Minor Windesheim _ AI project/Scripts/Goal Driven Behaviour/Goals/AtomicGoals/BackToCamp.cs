using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToCamp : AtomicGoal
{
    GameObject meGameObject;
    float baseRadius = 15;
    string resourceType;

    // resource types
    const string WOOD_TYPE = "wood";
    const string STONE_TYPE = "stone";

    public BackToCamp(GameObject _me, string _resourceType)
    {
        goalName = "AtomicGoal: Back to camp";

        meGameObject = _me;
        resourceType = _resourceType;
    }

    public override Vector3 Process()
    {
        World world = meGameObject.GetComponent<MovingEntity>().world;

        // get location
        Vector3 locationBase = world.GetBaseLocation();

        // get difference in distance
        Vector3 distanceBetween = (meGameObject.transform.position - locationBase);

        float distance = distanceBetween.magnitude;

        if (distance < baseRadius)
        {
            // add one wood to the total amount of wood
            AddResource();
            Completed();
        }

        Vector3 steeringForce = new ArriveBehaviour(meGameObject, locationBase).Calculate();

        return steeringForce;

    }

    private void AddResource()
    {
        if (resourceType == WOOD_TYPE)
        {
            meGameObject.GetComponent<MovingEntity>().world.amountWood += 1;
        }
        else if(resourceType == STONE_TYPE)
        {
            meGameObject.GetComponent<MovingEntity>().world.amountStone += 1;
        }
    }
}
