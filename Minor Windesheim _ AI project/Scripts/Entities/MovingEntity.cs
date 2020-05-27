using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingEntity : BaseEntity
{
    public Vector3 velocity { get; set; }
    public float mass { get; set; }
    public float maxSpeed { get; set; }

    public bool hasWood { get; set; }
    public bool hasStone { get; set; }

    public List<Vector3> Path { get; set; }
    [SerializeField]
    public SteeringBehaviour SB { get; set; }

    public List<GameObject> neighbours = new List<GameObject>();

    private const string TAG_ENTITY = "entity";
    public World world;



    public MovingEntity(Vector3 pos, World w) : base(pos, w)
    {
        mass = GetComponent<Rigidbody>().mass;
        maxSpeed = 75;

        velocity = this.GetComponent<Rigidbody>().velocity;
        Path = new List<Vector3>();
        world = w;
    }

    public override void UpdateEntity()
    {

        Vector3 SteeringForce = SB.Calculate();

        Vector3 acceleration = SteeringForce / GetComponent<Rigidbody>().mass;

        this.GetComponent<Rigidbody>().velocity += (acceleration * Time.deltaTime);

        Mathf.Clamp(velocity.x, 0, maxSpeed);
        Mathf.Clamp(velocity.y, 0, maxSpeed);
        Mathf.Clamp(velocity.z, 0, maxSpeed);

        this.gameObject.transform.position += (this.GetComponent<Rigidbody>().velocity * Time.deltaTime * 10);
    }

    public void SetVariables(World w)
    {
        world = w;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TAG_ENTITY)
        {
            neighbours.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == TAG_ENTITY)
        {
            neighbours.Remove(other.gameObject);
        }
    }

    public void Update()
    {
    }

    private void Truncate(float max)
    {
        if (velocity.magnitude > max)
        {
            velocity.Normalize();
            velocity *= max;
        }
    }

    public override string ToString()
    {
        return String.Format("{0}", velocity);
    }

    public void SetPath(Vector3 dest)
    {
        //Path = World.Graph.ShortestPathFrom(Position, dest); //find the shortest path to dest
        Path.RemoveAt(0); //remove the firt target, this is usually just a node to get the entity on the grid
        Path.RemoveAt(Path.Count - 1); // remove the last node, this is the node closest to the dest
        Path.Add(dest);//go directly to the destination instead
    }

    private void PickBehaviour()
    {
        //if (Path.Count != 0)//check if path is not empty
        //{
        //    var crnttar = path.first();
        //    //see if near current target
        //    if (position.distancesq(crnttar) < 10 * 10)
        //    {
        //        //if near then change target
        //        path.remove(crnttar); //remove the first target of the path
        //        if (path.count != 0)
        //            crnttar = path.first(); //set new target
        //    }
        //    if (path.count == 1)
        //        sb = new arrivebehaviour(this, crnttar);
        //    else
        //        sb = new seekbehaviour(this, crnttar);

        //}
        //else
        //    sb = new arrivebehaviour(this, position);
    }
}
