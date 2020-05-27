using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBehaviour : SteeringBehaviour
{
    GameObject meGameObject;

    private SteeringBehaviour[] behaviours;
    private float[] weights;
    CohesionBehaviour cohesionBehaviour;
    AlignmentBehaviour alignmentBehaviour;
    AvoidanceBehaviour avoidanceBehaviour;

    float weightCohesion = 2.5f;
    float weighAlignment = 2;
    float weighAvoidance = 3;

    float extraSpeed = 150;

    public FlockBehaviour(GameObject me) : base(me)
    {
        meGameObject = me;

        behaviours = new SteeringBehaviour[3]
        {
            cohesionBehaviour = new CohesionBehaviour(me),
            alignmentBehaviour = new AlignmentBehaviour(me),
            avoidanceBehaviour = new AvoidanceBehaviour(me)
        };
        weights = new float[3] { weightCohesion, weighAlignment, weighAvoidance };
    }

    public override Vector3 Calculate(Vector3 _tarPos)
    {
        // handle data mismatch
        if (weights.Length != behaviours.Length)
        {
            //Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        // set up move
        Vector2 move = Vector2.zero;

        //iterate through behaviours
        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].Calculate(meGameObject.transform.position) * weights[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                move += partialMove;
            }
        }

        

        return new Vector3(move.x, 0, move.y) * extraSpeed;
    }

    public override Vector3 Calculate()
    {
        return Vector3.zero;
    }
}
