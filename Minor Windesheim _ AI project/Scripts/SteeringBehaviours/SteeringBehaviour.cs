using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class SteeringBehaviour
{
    public GameObject ME { get; set; }

    public abstract Vector3 Calculate(Vector3 _tarPos);

    public abstract Vector3 Calculate();

    public SteeringBehaviour(GameObject me)
    {
        ME = me;
    }
}
