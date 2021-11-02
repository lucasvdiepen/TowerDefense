using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireTower : MonoBehaviour
{
    Tower towerScript = null;

    private void Start()
    {
        towerScript = GetComponent<Tower>();
    }

    private List<GameObject> GetTargets()
    {
        List<GameObject> l = new List<GameObject>();

        GameObject[] enemies = FindObjectOfType<EnemySpawner>().enemies.Values.ToArray();

        foreach(GameObject enemy in enemies)
        {
            if(Vector3.Distance(transform.position, enemy.transform.position) <= towerScript.range)
            {
                l.Add(enemy);
            }
        }

        return l;
    }

    public void OnTarget()
    {

    }

    public void Shoot()
    {
        List<GameObject> targets = GetTargets();

        foreach (GameObject target in targets)
        {
            target.GetComponent<Health>().TakeDamage(towerScript.damage);
        }
    }
}
