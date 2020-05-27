using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStone : CompositeGoal
{
    GameObject meGameObject;

    // subgoals
    ToTheMines toTheMines;
    MineStone mineStone;
    BackToCamp backToCamp;

    const string STONE_TYPE = "stone";

    public GetStone(GameObject me)
    {
        goalName = "CompositeGoal: Get stone";

        meGameObject = me;

        toTheMines = new ToTheMines(meGameObject);
        mineStone = new MineStone();
        backToCamp = new BackToCamp(meGameObject, STONE_TYPE);

        AddSubGoal(toTheMines);
        AddSubGoal(mineStone);
        AddSubGoal(backToCamp);
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
