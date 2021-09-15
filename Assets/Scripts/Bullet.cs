using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 5f;

    public float bulletDestroyTime = 5f;

    private bool bulletStarted = false;

    private Vector3 target;

    private void Start()
    {
        Destroy(gameObject, bulletDestroyTime);
    }

    public void StartBullet(Vector3 _target)
    {
        target = _target;

        transform.LookAt(target);

        bulletStarted = true;
    }

    private void Update()
    {
        if(bulletStarted)
        {
            transform.Translate(new Vector3(0, 0, bulletSpeed * Time.deltaTime));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            //Deal damage
        }

        Destroy(gameObject);
    }
}
