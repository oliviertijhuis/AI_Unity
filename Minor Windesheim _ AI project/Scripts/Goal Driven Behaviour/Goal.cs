using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal
{
    public string goalName;
    public abstract void Activate();
    public abstract void Completed();
    public abstract void Deactivate();
    public abstract Vector3 Process();
    public abstract void Terminate();
    public abstract void AddSubGoal(Goal g);

    public abstract Goal GetCurrentSubGoal();
    public abstract void Reset();

    public abstract bool IsActive();
    public abstract bool IsCompleted();
    public abstract bool HasFailed();
}
