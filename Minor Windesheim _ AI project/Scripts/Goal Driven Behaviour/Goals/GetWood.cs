using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWood : CompositeGoal
{

    GameObject meGameObject;

    // subgoals
    ToTheWoods toTheWoods;
    ChopWood chopWood;
    BackToCamp backToCamp;

    const string WOOD_TYPE = "wood";

    public GetWood(GameObject me)
    {
        goalName = "CompositeGoal: Get wood";

        meGameObject = me;

        toTheWoods = new ToTheWoods(meGameObject);
        chopWood = new ChopWood();
        backToCamp = new BackToCamp(meGameObject, WOOD_TYPE);

        AddSubGoal(toTheWoods);
        AddSubGoal(chopWood);
        AddSubGoal(backToCamp);
    }

    public override Vector3 Process()
    {
        while(subGoals.Count != 0)
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


}// class
