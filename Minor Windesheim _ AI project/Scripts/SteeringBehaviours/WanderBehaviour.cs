using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehaviour : SteeringBehaviour
{
    GameObject meGameObject;

    public float CircleRadius = 20;
    public float TurnChance = 0.05f;
    public float MaxRadius = 100;

    public float Mass = 15;
    public float MaxSpeed = 50;
    public float MaxForce = 15;

    private Vector3 velocity;
    private Vector3 wanderForce;
    private Vector3 target;

    private bool hadRandomWanderForce = false;

    public WanderBehaviour(GameObject me) : base(me)
    {
        meGameObject = me;

        velocity = Random.onUnitSphere;
        wanderForce = GetRandomWanderForce();
    }

    public override Vector3 Calculate(Vector3 _tarPos)
    {
        if (hadRandomWanderForce == false )
        {
            hadRandomWanderForce = true;
            return GetRandomWanderForce();
        }
        else {
            var desiredVelocity = GetWanderForce();
            desiredVelocity = desiredVelocity.normalized * MaxSpeed;

            var steeringForce = desiredVelocity - velocity;
            steeringForce = Vector3.ClampMagnitude(steeringForce, MaxForce);
            steeringForce /= Mass;

            velocity = Vector3.ClampMagnitude(velocity + steeringForce, MaxSpeed);

            Debug.DrawRay(meGameObject.transform.position, velocity.normalized * 2, Color.green);
            Debug.DrawRay(meGameObject.transform.position, desiredVelocity.normalized * 2, Color.magenta);


            return velocity;
        }
    }

    public override Vector3 Calculate()
    {
        if (hadRandomWanderForce == false)
        {
            hadRandomWanderForce = true;
            return GetRandomWanderForce();
        }
        else
        {
            var desiredVelocity = GetWanderForce();
            desiredVelocity = desiredVelocity.normalized * MaxSpeed;

            var steeringForce = desiredVelocity - velocity;
            steeringForce = Vector3.ClampMagnitude(steeringForce, MaxForce);
            steeringForce /= Mass;

            velocity = Vector3.ClampMagnitude(velocity + steeringForce, MaxSpeed);

            Debug.DrawRay(meGameObject.transform.position, velocity.normalized * 2, Color.green);
            Debug.DrawRay(meGameObject.transform.position, desiredVelocity.normalized * 2, Color.magenta);


            return velocity;
        }
    }

    private Vector3 GetWanderForce()
    {
        if (meGameObject.transform.position.magnitude > MaxRadius)
        {
            var directionToCenter = (target - meGameObject.transform.position).normalized;
            wanderForce = velocity.normalized + directionToCenter;
        }
        else if (Random.value < TurnChance)
        {
            wanderForce = GetRandomWanderForce();
        }

        return wanderForce;
    }

    private Vector3 GetRandomWanderForce()
    {
        var circleCenter = velocity.normalized;
        var randomPoint = Random.insideUnitCircle;

        var displacement = new Vector2(randomPoint.x, randomPoint.y) * CircleRadius;
        displacement = Quaternion.LookRotation(velocity) * displacement;

        var wanderForce = (Vector2)circleCenter + displacement;
        return new Vector3(wanderForce.x, 0, wanderForce.y);
    }
}
