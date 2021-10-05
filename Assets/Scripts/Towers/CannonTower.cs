using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootExplosion;

    private Tower towerScript;

    private void Start()
    {
        towerScript = GetComponent<Tower>();
    }

    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, towerScript.shootPoint.position, towerScript.shootPoint.rotation);
        newBullet.GetComponent<Bullet>().StartBullet(towerScript.target.transform.position, towerScript.damage);

        GameObject newShootExplosion = Instantiate(shootExplosion, towerScript.shootPoint.position, Quaternion.identity);
        //Destroy(newShootExplosion, 0.50f);
    }
}
