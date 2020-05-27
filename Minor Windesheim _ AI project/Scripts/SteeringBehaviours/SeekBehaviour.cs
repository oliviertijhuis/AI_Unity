using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : SteeringBehaviour
{
    private Vector3 targetPos;
    private GameObject meGameObject;
    private float maxSpeed = 100;

    public SeekBehaviour(GameObject me, Vector3 tarpos) : base(me)
    {
        targetPos = tarpos;
        meGameObject = me;
    }

    // should take a position
    public override Vector3 Calculate(Vector3 _tarPos)
    {

        Vector3 desiredVelocity = (_tarPos - meGameObject.transform.position);
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed; // multiply by maxforce

        Mathf.Clamp(desiredVelocity.x, 0, maxSpeed);
        Mathf.Clamp(desiredVelocity.z, 0, maxSpeed);

        return (desiredVelocity - meGameObject.GetComponent<Rigidbody>().velocity); ;
    }

    public override Vector3 Calculate()
    {
        Vector3 desiredVelocity = (targetPos - ME.transform.position);
        desiredVelocity.Normalize();
        desiredVelocity *= ME.GetComponent<MovingEntity>().maxSpeed; // should be ME.maxspeed
        return desiredVelocity - ME.GetComponent<Rigidbody>().velocity;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
