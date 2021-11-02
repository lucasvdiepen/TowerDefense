using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireTower : MonoBehaviour
{
    public float damage = 5f;
    public float range = 5f;
    public float fireRate = 0.1f;

    private float lastAttackTime = 0;

    TowerRange towerRangeScript = null;

    private void Start()
    {
        towerRangeScript = GetComponent<TowerRange>();
    }

    private List<GameObject> GetTargets()
    {
        List<GameObject> l = new List<GameObject>();

        GameObject[] enemies = FindObjectOfType<EnemySpawner>().enemies.Values.ToArray();

        foreach(GameObject enemy in enemies)
        {
            if(Vector3.Distance(transform.position, enemy.transform.position) <= range)
            {
                l.Add(enemy);
            }
        }

        return l;
    }

    public void Attack()
    {
        List<GameObject> targets = GetTargets();

        foreach(GameObject target in targets)
        {
            target.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void Update()
    {
        float time = Time.time;

        if(time >= (lastAttackTime + fireRate))
        {
            Attack();

            lastAttackTime = time;
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
}
