using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultTower : MonoBehaviour
{
    public Animator animator;
    public GameObject bullet;

    public float shootDelay = 0.8f;

    private Tower towerScript;

    private bool shoot = false;
    private float shootStartTime = 0f;

    private void Start()
    {
        towerScript = GetComponent<Tower>();
    }

    public void Shoot()
    {
        animator.SetTrigger("Shoot");

        shootStartTime = Time.time;
        shoot = true;
    }

    private void FireBullet()
    {
        GameObject newBullet = Instantiate(bullet, towerScript.shootPoint.position, towerScript.shootPoint.rotation);
        newBullet.GetComponent<CatapultProjectile>().StartProjectile(towerScript.target);
    }

    private void Update()
    {
        if(shoot && Time.time > (shootStartTime + shootDelay))
        {
            FireBullet();
            shoot = false;
        }
    }
}
