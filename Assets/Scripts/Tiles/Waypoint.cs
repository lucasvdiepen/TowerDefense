using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public int id = -1;

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
