using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float damage = 5;
    public float range = 5f;
    public float fireRate = 0.2f;

    public float turnSpeed = 5f;

    public bool canRotate = true;

    public Transform weapon;
    public Transform shootPoint;

    public MonoBehaviour towerScript;

    private int previousTargetId = -1;

    [HideInInspector]
    public GameObject target;

    private Vector3? priorityTarget;

    private float lastShootTime = 0f;

    private List<int> ignoreList = new List<int>();

    private TowerRange towerRangeScript;

    private void Start()
    {
        towerRangeScript = GetComponent<TowerRange>();

        UpdateRange();
    }

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
            if(!ignoreList.Contains(enemy.GetComponent<EnemyID>().GetID()))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < range)
                {
                    if (distanceToEnemy < shortestDistance)
                    {
                        target = enemy;
                        shortestDistance = distanceToEnemy;
                    }
                }
            }
        }

        if (target != null)
        {
            int targetId = target.GetComponent<EnemyID>().GetID();
            if(targetId != previousTargetId)
            {
                towerScript.Invoke("OnTarget", 0f);
                previousTargetId = targetId;
            }
        }
        else previousTargetId = -1;
    }

    private void RotateToTarget()
    {
        if(canRotate)
        {
            Vector3 rotateTarget = Vector3.zero;

            if (priorityTarget != null)
            {
                rotateTarget = (Vector3)priorityTarget;
            }
            else
            {
                if (target != null)
                {
                    rotateTarget = target.transform.position;
                }
                else return;
            }

            Vector3 dir = rotateTarget - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(weapon.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            weapon.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    public void UpgradeRangeAmount(float amount)
    {
        //Upgrade range here
        range += amount;

        UpdateRange();
    }

    public void UpgradeDamageAmount(float amount)
    {
        //Upgrade damage here
        damage += amount;
    }

    private void UpdateRange()
    {
        towerRangeScript.UpdateRangeImage(range);
    }

    public void SetPriorityTarget(Vector3 target)
    {
        priorityTarget = target;
    }

    public void RemovePriorityTarget()
    {
        priorityTarget = null;
    }

    public float GetTimeBeforeShoot()
    {
        float time = fireRate - (Time.time - lastShootTime);
        if (time > 0) return time;
        else return 0;
    }

    public bool HasNoTimeToHit(float travelTime)
    {
        Vector3? predictedPosition = target.GetComponent<EnemyMovement>().GetPredictedPosition(travelTime);
        if (predictedPosition == null) return true;

        return false;
    }

    private void Shoot()
    {
        if (target != null && (Time.time > (lastShootTime + fireRate)))
        {
            lastShootTime = Time.time;

            RemovePriorityTarget();

            previousTargetId = -1;

            towerScript.Invoke("Shoot", 0f);
        }
    }

    public void AddToIgnoreList(int enemyId, bool resetDelay)
    {
        if (resetDelay) lastShootTime = 0;
        if (!ignoreList.Contains(enemyId)) ignoreList.Add(enemyId);

        UpdateTarget();
    }
    
    public void RemvoveFromIgnoreList(int enemyId)
    {
        if (ignoreList.Contains(enemyId)) ignoreList.Remove(enemyId);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
