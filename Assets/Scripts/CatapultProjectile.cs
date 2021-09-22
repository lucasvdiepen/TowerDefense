using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultProjectile : MonoBehaviour
{
    public float travelTime = 2f;
    public float height = 5f;

    private Vector3 target;
    private Vector3 startPosition;

    private float timeElapsed = 0f;

    private float explosionRange = 1f;
    private int damage = 5;

    private void OnEnable()
    {
        startPosition = transform.position;
    }

    public void StartProjectile(Vector3 _target, float _explosionRange, int _damage)
    {
        target = _target;
        damage = _damage;
    }

    private void Update()
    {
        if (timeElapsed <= travelTime)
        {
            transform.position = Parabola(startPosition, target, height, timeElapsed / travelTime);
            timeElapsed += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject[] enemies = FindObjectOfType<EnemySpawner>().enemies.ToArray();
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(explosionRange <= distanceToEnemy)
            {
                enemy.GetComponent<Health>().TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

}
