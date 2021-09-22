using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultTest : MonoBehaviour
{
    public GameObject catapultProjectile;
    public Transform catapultTarget;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject spawnedProjectile = Instantiate(catapultProjectile, transform.position, Quaternion.identity);
            spawnedProjectile.GetComponent<CatapultProjectile>().StartProjectile(catapultTarget.position);
        }
    }
}
