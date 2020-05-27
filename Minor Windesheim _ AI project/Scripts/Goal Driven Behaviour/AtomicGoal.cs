using System;
using UnityEngine;

public class AtomicGoal : Goal
{
    bool active;
    bool taskCompleted;

    public AtomicGoal()
    {
        active = false;
        taskCompleted = false;
    }

    public override void Activate()
    {
        active = true;
    }

    // atomicGoals does not have subgoals
    public override void AddSubGoal(Goal g)
    {
        throw new NotImplementedException();
    }

    public override void Completed()
    {
        taskCompleted = true;
    }

    public override void Deactivate()
    {
        active = false;
    }

    public override Goal GetCurrentSubGoal()
    {
        Goal goal = null;
        return goal;
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
        return taskCompleted;
    }

    public override Vector3 Process()
    {
        throw new NotImplementedException();
    }

    public override void Reset()
    {
        active = false;
        taskCompleted = false;
    }

    public override void Terminate()
    {
        throw new NotImplementedException();
    }
}
