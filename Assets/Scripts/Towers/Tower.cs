using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int damage = 5;
    public float range = 5f;
    public float fireRate = 0.2f;

    public float turnSpeed = 5f;

    public Transform weapon;
    public Transform shootPoint;

    public MonoBehaviour towerScript;

    [HideInInspector]
    public GameObject target;

    private float lastShootTime = 0f;

    private void Update()
    {
        UpdateTarget();

        RotateToTarget();

        Shoot();
    }

    private void UpdateTarget()
    {
        target = null;

        GameObject[] enemies = FindObjectOfType<EnemySpawner>().enemies.Values.ToArray();

        float shortestDistance = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < range)
            {
                if(distanceToEnemy < shortestDistance)
                {
                    target = enemy;
                    shortestDistance = distanceToEnemy;
                }
            }
        }
    }

    private void RotateToTarget()
    {
        if(target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(weapon.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            weapon.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    private void Shoot()
    {
        if (target != null && (Time.time > (lastShootTime + fireRate)))
        {
            lastShootTime = Time.time;

            towerScript.Invoke("Shoot", 0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
