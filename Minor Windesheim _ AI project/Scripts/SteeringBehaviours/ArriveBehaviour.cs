using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveBehaviour : SteeringBehaviour
{
    Vector3 targetPos;
    float radius = 100;
    float maxSpeed = 100;
    GameObject meGameobject;

    public ArriveBehaviour(GameObject me, Vector3 tarPos) : base(me)
    {
        targetPos = tarPos;
        meGameobject = me;
    }


    public override Vector3 Calculate(Vector3 _tarPos)
    {
        Vector3 toTarget = _tarPos - ME.transform.position;
        float distance = toTarget.magnitude;

        if (distance > 3)
        {
            const float DECELERATION_TWEAKER = 0.3f;
            const int DECELARATION = 1;

            float speed = distance / (DECELARATION * DECELERATION_TWEAKER);
            
            // make sure it doesn't exceed maxSpeed
            speed = Mathf.Clamp(speed, 0, maxSpeed);

            Vector3 speedVector = new Vector3(speed, 0, speed);

            Vector3 desiredVelocity = toTarget * speed / distance;

            return desiredVelocity - ME.GetComponent<Rigidbody>().velocity;
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }

    public override Vector3 Calculate()
    {

        float distance = (targetPos - ME.transform.position).magnitude;

        if (distance > 0)
        {
            float decelerationTweaker = 0.3f;
            int deceleration = 5;
            float speed = distance / (deceleration * decelerationTweaker);


            //speed = Math.Min(speed, 150);

            Vector3 speedVector = new Vector3(speed, speed, speed);

            Vector3 desiredVelocity = (targetPos - ME.transform.position) * speed / distance;


            return desiredVelocity - ME.GetComponent<Rigidbody>().velocity;
        }
        else { 
            return new Vector3(0, 0, 0);
        }
    }
}
