using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CohesionBehaviour : SteeringBehaviour
{
    List<GameObject> neighbours;
    GameObject meGameObject;
    float cohesionRadius = 50;

    public CohesionBehaviour(GameObject me) : base(me)
    {
        meGameObject = me;
    }

    public override Vector3 Calculate(Vector3 _tarPos)
    {
        neighbours = meGameObject.GetComponent<MovingEntity>().neighbours;

        if (neighbours.Count == 0)
        {
            return Vector3.zero;
        }

        // add all points together and average
        Vector2 cohesionMove = Vector2.zero;
        foreach (GameObject item in neighbours)
        {
            cohesionMove += (Vector2)item.transform.position;
        }
        // average out
        cohesionMove /= neighbours.Count;

        // create offset from agent position
        cohesionMove -= (Vector2)meGameObject.transform.position;
        return new Vector3(cohesionMove.x, 0, cohesionMove.y);
    }

    public override Vector3 Calculate()
    {
        neighbours = meGameObject.GetComponent<MovingEntity>().neighbours;

        if (neighbours.Count == 0)
        {
            return meGameObject.GetComponent<Rigidbody>().velocity;
        }

        Vector3 cohesionMove = new Vector3(200, 0, 200);

        foreach (GameObject item in neighbours)
        {
            Vector3 distanceBetween = item.transform.position - meGameObject.transform.position;

            if (distanceBetween.magnitude > 0 && distanceBetween.magnitude < cohesionRadius)
            {
                cohesionMove += item.transform.position;
            }
        }

        // average out
        cohesionMove /= neighbours.Count;
        // Create offset from own position
        cohesionMove -= meGameObject.transform.position;

        return cohesionMove;
    }

}
