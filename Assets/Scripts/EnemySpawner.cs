using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyObjects;

    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();

    private Vector3 spawnPoint = Vector3.zero;

    private void Start()
    {
        //Get spawnpoint
        spawnPoint = FindObjectOfType<Spawnpoint>().GetPosition();

        enemies.Add(Instantiate(enemyObjects[0], spawnPoint, Quaternion.identity));
        enemies.Add(Instantiate(enemyObjects[0], spawnPoint, Quaternion.identity));
        enemies.Add(Instantiate(enemyObjects[0], spawnPoint, Quaternion.identity));
        enemies.Add(Instantiate(enemyObjects[0], spawnPoint, Quaternion.identity));
        enemies.Add(Instantiate(enemyObjects[0], spawnPoint, Quaternion.identity));
    }
}
