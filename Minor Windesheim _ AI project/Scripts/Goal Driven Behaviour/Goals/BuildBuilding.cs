using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBuilding : CompositeGoal
{
    GameObject meGameObject;

    List<GameObject> listOfLocations;
    Vector3 chosenLocation;

    // subgoals
    GoToLocation goToLocation;
    Build build;

    int buildingCost = 5;

    public BuildBuilding(GameObject me)
    {
        goalName = "CompositeGoal: Build building";

        meGameObject = me;
        ChooseLocation();

        goToLocation = new GoToLocation(meGameObject, chosenLocation);
        build = new Build(meGameObject, chosenLocation);

        AddSubGoal(goToLocation);
        AddSubGoal(build);

        // already take resources needed to build
        me.GetComponent<MovingEntity>().world.amountWood -= buildingCost;
        me.GetComponent<MovingEntity>().world.amountStone -= buildingCost;
    }

    private void ChooseLocation()
    {
        listOfLocations = meGameObject.GetComponent<MovingEntity>().world.GetListOfLocations();
        int randomNumber = (int)(World.RandomNumber(0, listOfLocations.Count));

        // get location
        chosenLocation = listOfLocations[randomNumber].transform.position;
    }

    public override Vector3 Process()
    {
        while (subGoals.Count != 0)
        {
            for (int i = 0; i < subGoals.Count; i++)
            {
                // activate subgoal
                subGoals[i].Activate();

                if (subGoals[i].IsCompleted() == true)
                {
                    // finish goal
                    subGoals[i].Deactivate();
                    subGoals.RemoveAt(i);

                    // check if it was the last goal otherwise move to the next goal
                    if (subGoals.Count <= 0)
                    {
                        this.Completed();
                    }
                }
                else
                {
                    return subGoals[i].Process();
                }
            }
        }

        return Vector3.zero;
    }
}
