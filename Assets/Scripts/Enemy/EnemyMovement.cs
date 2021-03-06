using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float startingSpeed = 5f;
    public float speed = 5f;
    public float yOffset = 0.623f;

    private int waypointsCount = 0;

    private Vector3 target = Vector3.zero;
    private float slowForSeconds = 0;
    private float lastSlowTime = 0;
    private bool isSlow = false;

    private void Start()
    {
        target = (Vector3)GetNextWaypoint(waypointsCount);
    }

    private void Update()
    {
        //CheckSlowDone();

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
                //Debug.Log("Enemy reached end");
                GetComponent<EnemyAttack>().DealDamage();

                //For now just destroy itself
                FindObjectOfType<EnemySpawner>().DestroyEnemy(GetComponent<EnemyID>().GetID());
            }
            else target = (Vector3)newWaypoint;
        }
    }

    public Vector3? GetPredictedPosition(float timeInSeconds)
    {
        Vector3 previousWaypoint = transform.position;
        int waypointId = waypointsCount;

        while(true)
        {
            Vector3? nextWaypoint = GetNextWaypoint(waypointId);
            if (nextWaypoint == null) return null;

            float distanceToWaypoint = Vector3.Distance(previousWaypoint, (Vector3)nextWaypoint);
            float timeToWaypoint = distanceToWaypoint / speed;

            if (timeInSeconds < timeToWaypoint) return Vector3.MoveTowards(previousWaypoint, (Vector3)nextWaypoint, speed * timeInSeconds);

            timeInSeconds -= timeToWaypoint;
            waypointId++;
            previousWaypoint = (Vector3)nextWaypoint;
        }
    }

    private Vector3? GetNextWaypoint(int waypointId)
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();

        foreach(Waypoint waypoint in waypoints)
        {
            if (waypoint.id == waypointId) return waypoint.GetPosition() + new Vector3(0, yOffset, 0);
        }

        return null;
    }

    private void CheckSlowDone()
    {
        float time = Time.time;
        if(isSlow && time >= (lastSlowTime + slowForSeconds))
        {
            UnSlow();
        }
    }

    public void UnSlow()
    {
        speed = startingSpeed;
        isSlow = false;
    }

    public void Slow(float speed, float seconds)
    {
        if(!isSlow)
        {
            lastSlowTime = Time.time;
            isSlow = true;
            slowForSeconds = seconds;

            this.speed -= speed;
        }
    }
}
