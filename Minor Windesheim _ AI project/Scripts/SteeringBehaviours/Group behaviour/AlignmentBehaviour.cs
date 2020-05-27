using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentBehaviour : SteeringBehaviour
{
    List<GameObject> neighbours;
    GameObject meGameObject;
    float alignmentRadius = 100.0f;

    public AlignmentBehaviour(GameObject me) : base(me)
    {
        meGameObject = me;
    }

    public override Vector3 Calculate(Vector3 _tarPos)
    {
        // get neighbours
        neighbours = meGameObject.GetComponent<MovingEntity>().neighbours;

        // if no neighbours, maintain current alignment
        if (neighbours.Count == 0)
        {
            return  new WanderBehaviour(meGameObject).Calculate();
        }

        // add all points together and average
        Vector2 alignmentMove = Vector2.zero;
        foreach (GameObject item in neighbours)
        {
            alignmentMove += (Vector2)item.transform.forward;
        }
        // average out
        alignmentMove /= neighbours.Count;

        return new Vector3(alignmentMove.x, 0, alignmentMove.y);
    }

    public override Vector3 Calculate()
    {
        neighbours = meGameObject.GetComponent<MovingEntity>().neighbours;

        if (neighbours.Count == 0)
        {
            return new WanderBehaviour(meGameObject).Calculate();
        }

        Vector3 alignmentMove = new Vector3();

        foreach (GameObject item in neighbours)
        {
            Vector3 distanceBetween = item.transform.position - meGameObject.transform.position;

            if (distanceBetween.magnitude < alignmentRadius)
            {
                alignmentMove += item.GetComponent<Rigidbody>().velocity;
            }
        }
        return (alignmentMove /= neighbours.Count);
    }
}
