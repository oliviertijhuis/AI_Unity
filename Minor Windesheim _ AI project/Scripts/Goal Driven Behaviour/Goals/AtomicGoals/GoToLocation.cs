using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLocation : AtomicGoal
{
    GameObject meGameObject;

    Vector3 location;
    float radius = 15;

    public GoToLocation(GameObject _me, Vector3 _location)
    {
        goalName = "AtomicGoal: Go to locaiton";

        meGameObject = _me;
        location = _location;
    }

    public override Vector3 Process()
    {
        // get difference in distance
        Vector3 distanceBetween = (meGameObject.transform.position - location);

        float distance = distanceBetween.magnitude;

        if (distance < radius)
        {
            Completed();
        }

        return new ArriveBehaviour(meGameObject, location).Calculate();
    }

}
