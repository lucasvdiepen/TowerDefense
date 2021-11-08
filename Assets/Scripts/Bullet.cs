using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 5f;

    public float bulletDestroyTime = 5f;

    private float damage = 0;

    private bool bulletStarted = false;

    private Transform target;

    private void Start()
    {
        Destroy(gameObject, bulletDestroyTime);
    }

    public void StartBullet(Transform _target, float _damage)
    {
        target = _target;
        damage = _damage;

        //transform.LookAt(target);

        bulletStarted = true;
    }

    private void Update()
    {
        if(bulletStarted)
        {
            //transform.Translate(new Vector3(0, 0, bulletSpeed * Time.deltaTime));

            transform.position = Vector3.MoveTowards(transform.position, target.position, bulletSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            //Deal damage
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
