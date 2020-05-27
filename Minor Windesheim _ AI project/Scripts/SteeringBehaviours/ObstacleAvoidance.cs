using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : SteeringBehaviour
{
    private const float SPHERECAST_MAXLENGTH = 15;

    GameObject meGameObject;

    // direction for the raycasts
    private Vector3 forward;
    private Vector3 left;
    private Vector3 right;

    public ObstacleAvoidance(GameObject me) : base(me)
    {
        meGameObject = me;

        forward = me.transform.TransformDirection(Vector3.forward);
        left = me.transform.TransformDirection(Vector3.left);
        right = me.transform.TransformDirection(Vector3.right);
    }

    public override Vector3 Calculate(Vector3 _tarPos)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Calculates the forces for breaking and avoiding
    /// NOTE: use worldspace instead of localspace position
    /// </summary>
    /// <returns></returns>
    public override Vector3 Calculate()
    {
        Vector3 steeringForce = new Vector3();
                       
        RaycastHit hit;
                
        // create spherecast
        if (Physics.SphereCast(meGameObject.transform.position, 5, meGameObject.transform.forward, out hit, SPHERECAST_MAXLENGTH))
        {
            // the closer the agent is to an object, the stronger the steering force should be
            var multiplier = 1.0f + ((SPHERECAST_MAXLENGTH - hit.transform.position.x) / SPHERECAST_MAXLENGTH);

            // calculate the lateral force
            steeringForce.y = (1.0f - hit.transform.position.y) * multiplier;

            // apply a breaking force proportional to the obstacle's distance from the vehicle
            const float BREAKING_WEIGHT = 0.2f;

            steeringForce.x = (1.0f - hit.transform.position.x ) * BREAKING_WEIGHT;
        }
         
        
        return new Vector3(steeringForce.x, steeringForce.y);
    }

    /// <summary>
    /// Creates 3 raycast: one at the front and two at the side of the gameobject 
    /// These raycast are used to check if there are walls nearby
    /// </summary>
    private void CreateFeelers()
    {
        RaycastHit hit;

        // forward
        if (Physics.Raycast(meGameObject.transform.position, forward, out hit, SPHERECAST_MAXLENGTH))
        {

        }

        // left
        if (Physics.Raycast(meGameObject.transform.position, left, out hit, SPHERECAST_MAXLENGTH))
        {

        }

        // right
        if (Physics.Raycast(meGameObject.transform.position, right, out hit, SPHERECAST_MAXLENGTH))
        {

        }
    }
}
