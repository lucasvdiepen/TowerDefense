using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AOETower : MonoBehaviour
{
    public float range = 5f;
    public float fireRate = 0.2f;

    private float lastAttackTime = 0;

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
            target.GetComponent<EnemyMovement>().
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
}
