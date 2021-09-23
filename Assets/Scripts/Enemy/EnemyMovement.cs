using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;

    private int waypointsCount = 0;

    private Vector3 target = Vector3.zero;

    private int enemyId = -1;

    private void Start()
    {
        target = (Vector3)GetNextWaypoint(waypointsCount);
    }

    public void StartEnemy(int _enemyId)
    {
        enemyId = _enemyId;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //Check if enemy reached waypoint
        if(Vector3.Distance(transform.position, target) < 0.001f)
        {
            //Get new waypoint
            waypointsCount++;
            Vector3? newWaypoint = GetNextWaypoint(waypointsCount);
            if (newWaypoint == null)
            {
                //Reached end
                Debug.Log("Enemy reached end");

                //For now just destroy itself
                FindObjectOfType<EnemySpawner>().DestroyEnemy(enemyId);
            }
            else target = (Vector3)newWaypoint;
        }
    }

    private Vector3? GetNextWaypoint(int waypointId)
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();

        foreach(Waypoint waypoint in waypoints)
        {
            if (waypoint.id == waypointId) return waypoint.GetPosition();
        }

        return null;
    }
}
