using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseEntity : MonoBehaviour
{
    public Vector3 position;
    public float scale;

    public int id { get; set; }

    public BaseEntity(Vector3 pos, World w)
    {
        position = pos;
    }

    public abstract void UpdateEntity();


}
