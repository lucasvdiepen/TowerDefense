using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultTower : MonoBehaviour
{
    public Animator animator;
    public GameObject bullet;

    public float travelTime = 2f;

    //Animation delay
    public float shootDelay = 0.8f;

    private Tower towerScript;

    private bool shoot = false;
    private float shootStartTime = 0f;

    private void Start()
    {
        towerScript = GetComponent<Tower>();
    }

    public void OnTarget()
    {
        float timeBeforeShoot = towerScript.GetTimeBeforeShoot();
        Debug.Log("Time before shoot: " + timeBeforeShoot);
        if (towerScript.HasNoTimeToHit(timeBeforeShoot + shootDelay + travelTime))
        {
            Debug.Log("Ignore enemy before shoot");
            //Should ignore after shot???
            towerScript.AddToIgnoreList(towerScript.target.GetComponent<EnemyMovement>().enemyId, false);
        }
    }

    public void Shoot()
    {
        //Check if bullet has enough time
        if(towerScript.target != null)
        {
            Vector3? predictedPosition = towerScript.target.GetComponent<EnemyMovement>().GetPredictedPosition(shootDelay + travelTime);
            if (predictedPosition == null)
            {
                //Add enemy to ignore list
                towerScript.AddToIgnoreList(towerScript.target.GetComponent<EnemyMovement>().enemyId, true);
                return;
            }

            //Rotate to predicted position
            towerScript.SetPriorityTarget((Vector3)predictedPosition);

            animator.SetTrigger("Shoot");

            shootStartTime = Time.time;
            shoot = true;
        }
    }

    private void FireBullet()
    {
        if(towerScript.target != null)
        {
            Vector3? predictedPosition = towerScript.target.GetComponent<EnemyMovement>().GetPredictedPosition(travelTime);
            if (predictedPosition == null) return;

            GameObject newBullet = Instantiate(bullet, towerScript.shootPoint.position, towerScript.shootPoint.rotation);
            newBullet.GetComponent<CatapultProjectile>().StartProjectile((Vector3)predictedPosition, travelTime, 1f, towerScript.damage);
        }
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
