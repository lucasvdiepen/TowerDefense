using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;

    private int waypointsCount = 0;

    private Vector3 target = Vector3.zero;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(transform.position == target)
        {
            GetNextWaypoint();
        }

        
    }

    private void GetNextWaypoint()
    {

    }
}
