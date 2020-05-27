using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeGoal : Goal
{
    public List<Goal> subGoals = new List<Goal>();

    private bool active;
    private bool completed;

    public CompositeGoal()
    {
        active = false;
        completed = false;
    }

    public override void Activate()
    {
        completed = false;
        active = true;
    }

    public override void AddSubGoal(Goal g)
    {
        subGoals.Add(g);
    }

    public override void Completed()
    {
        completed = true;
    }

    public override void Deactivate()
    {
        active = false;
    }

    public override Goal GetCurrentSubGoal()
    {
        Goal activeGoal = null;

        for (int i = 0; i < subGoals.Count; i++)
        {
            if (subGoals[i].IsActive())
            {
                activeGoal = subGoals[i];
            }
        }

        return activeGoal;
    }

    public override bool HasFailed()
    {
        throw new NotImplementedException();
    }

    public override bool IsActive()
    {
        return active;
    }

    public override bool IsCompleted()
    {
        return completed;
    }

    public override Vector3 Process()
    {
        throw new NotImplementedException();
    }

    public override void Reset()
    {
        active = false;
        completed = false;
    }

    public override void Terminate()
    {
        throw new NotImplementedException();
    }
}
