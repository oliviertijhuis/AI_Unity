using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTheMines : AtomicGoal
{
    List<GameObject> listOfStone;
    GameObject meGameObject;
    float radius = 15;

    Vector3 chosenRockLoc;

    public ToTheMines(GameObject _me)
    {

        goalName = "AtomicGoal: To the mines";

        meGameObject = _me;
        ChooseLocation();
    }

    private void ChooseLocation()
    {
        listOfStone = meGameObject.GetComponent<MovingEntity>().world.GetListOfStones();
        int randomNumber = (int)(World.RandomNumber(0, listOfStone.Count));

        // get location
        chosenRockLoc = listOfStone[randomNumber].transform.position;
    }

    // function process
    public override Vector3 Process()
    {
        // get difference in distance
        Vector3 distanceBetween = (meGameObject.transform.position - chosenRockLoc);

        float distance = distanceBetween.magnitude;

        if (distance < radius)
        {
            Completed();
        }

        Vector3 steeringForce = new ArriveBehaviour(meGameObject, chosenRockLoc).Calculate();

        return steeringForce;
    }
}
