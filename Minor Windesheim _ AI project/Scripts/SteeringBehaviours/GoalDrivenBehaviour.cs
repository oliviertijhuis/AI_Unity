using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDrivenBehaviour : SteeringBehaviour
{
    GameObject meGameObject;
    World world;

    private bool hasEnoughWood = false;
    private bool hasEnoughStone = false;

    private int amountWoodNeeded = 5;
    private int amountStoneNeeded = 5;

    CompositeGoal currentGoal;

    BuildBuilding buildBuilding;
    GetWood getWood;
    GetStone getStone;

    bool firstTime = false;

    UIFunctionality uiFunctionality;

    public GoalDrivenBehaviour(GameObject me) : base(me)
    {
        meGameObject = me;
        world = me.GetComponent<MovingEntity>().world;
    }

    public override Vector3 Calculate(Vector3 _tarPos)
    {
        throw new System.NotImplementedException();
    }

    public override Vector3 Calculate()
    {
        // check for resources
        CheckResources();

        // choose what to do based on the amount of resources
        if (firstTime == false)
        {
            firstTime = true;
            ChooseAction();
        }
        else if (currentGoal.IsCompleted() == true)
        {
            ChooseAction();
        }

        // update goals in UI
        uiFunctionality = meGameObject.GetComponent<MovingEntity>().world.ui_Functionality;
        uiFunctionality.SetTextBasedOnId( meGameObject.GetComponent<MovingEntity>().id, currentGoal );
        
        return currentGoal.Process();
         
    }
    private void ChooseAction()
    {
        if (hasEnoughWood == true && hasEnoughStone == true)
        {
            // build a building
            currentGoal = new BuildBuilding(meGameObject);
        }
        else if(hasEnoughWood == false)
        {
            // GetWood
            currentGoal = new GetWood(meGameObject);
        }
        else if(hasEnoughStone == false)
        {
            // GetStone
            currentGoal = new GetStone(meGameObject);
        }
        else
        {
            // Giver error message
            meGameObject.GetComponent<Material>().color = Color.red;
        }

    }

    private void CheckResources()
    {
        CheckForWood();
        CheckForStone();
    }
    
    private void CheckForWood()
    {
        if (meGameObject.GetComponent<MovingEntity>().world.amountWood >= amountWoodNeeded)
        {
            hasEnoughWood = true;
        }
        else
        {
            hasEnoughWood = false;
        }
    }

    private void CheckForStone()
    {
        if (meGameObject.GetComponent<MovingEntity>().world.amountStone >= amountStoneNeeded)
        {
            hasEnoughStone = true;
        }
        else
        {
            hasEnoughStone = false;
        }
    }

} // class
