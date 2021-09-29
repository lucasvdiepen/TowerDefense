using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyObjects;

    [HideInInspector]
    public Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();

    private Vector3 spawnPoint = Vector3.zero;

    private int enemyCounter = 0;

    private void Start()
    {
        //Get spawnpoint
        spawnPoint = FindObjectOfType<Spawnpoint>().GetPosition();

        for(int i = 0; i < 5; i++)
        {
            SpawnEnemy();

        }
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyObjects[0], spawnPoint, Quaternion.identity);
        newEnemy.GetComponent<EnemyMovement>().StartEnemy(enemyCounter);

        enemies.Add(enemyCounter, newEnemy);

        enemyCounter++;
    }

    public void DestroyEnemy(int id)
    {
        if (enemies.TryGetValue(id, out GameObject enemy)) Destroy(enemy);

        enemies.Remove(id);
    }
}
