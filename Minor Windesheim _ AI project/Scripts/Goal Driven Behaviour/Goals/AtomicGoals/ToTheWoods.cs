using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTheWoods : AtomicGoal
{
    List<GameObject> listOfTrees;
    GameObject meGameObject;
    float radius = 15;

    Vector3 chosenTreeLoc;

    public ToTheWoods(GameObject _me)
    {
        goalName = "AtomicGoal: To the woods";

        meGameObject = _me;
        ChooseLocation();
    }
    
    private void ChooseLocation()
    {
        listOfTrees = meGameObject.GetComponent<MovingEntity>().world.GetListOfTrees();
        int randomNumber = (int)(World.RandomNumber(0, listOfTrees.Count));

        // get location
        chosenTreeLoc = listOfTrees[randomNumber].transform.position;
    }

    // function process
    public override Vector3 Process()
    {
        // get difference in distance
        Vector3 distanceBetween = (meGameObject.transform.position - chosenTreeLoc);

        float distance = distanceBetween.magnitude;

        if (distance < radius)
        {
            Completed();
        }

        Vector3 steeringForce = new ArriveBehaviour(meGameObject, chosenTreeLoc).Calculate();

        return steeringForce;
    }

}
