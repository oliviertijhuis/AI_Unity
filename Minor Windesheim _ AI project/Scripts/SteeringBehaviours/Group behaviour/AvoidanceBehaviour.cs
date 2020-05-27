using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceBehaviour : SteeringBehaviour
{
    List<GameObject> neighbours;
    GameObject meGameObject;

    float avoidanceRadius = 100;
    int nAvoid = 0;

    public AvoidanceBehaviour(GameObject me) : base(me)
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
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;

        foreach (GameObject item in neighbours)
        {
            if (Vector2.SqrMagnitude(item.transform.position - meGameObject.transform.position) < avoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(meGameObject.transform.position - item.transform.position);
            }
        }
        if (nAvoid > 0)
        {
            // average out
            avoidanceMove /= nAvoid;
        }

        return new Vector3(avoidanceMove.x, 0, avoidanceMove.y);
    }

    public override Vector3 Calculate()
    {
        neighbours = meGameObject.GetComponent<MovingEntity>().neighbours;

        if (neighbours.Count == 0)
        {
            return new Vector3();
        }

        Vector3 avoidanceMove = new Vector3();

        foreach (GameObject item in neighbours)
        {
            Vector3 distanceBetween = item.transform.position - meGameObject.transform.position;

            // item position - agent position
            if (distanceBetween.magnitude > 0 && distanceBetween.magnitude < avoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += meGameObject.transform.position - item.transform.position;
            }
        }

        if (nAvoid > 0)
        {
            // average out
            avoidanceMove /= nAvoid;
        }

        return avoidanceMove;
    }

}
