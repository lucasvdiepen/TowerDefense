using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyObjects;
    public int[] waves;
    public float spawnDelay = 0.1f;
    public float waveDelay = 7f;

    [HideInInspector]
    public Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();

    private Vector3 spawnPoint = Vector3.zero;
    private int enemyCounter = 0;
    private bool isSpawning = false;
    private float lastEnemySpawnTime = 0;
    private float lastWaveTime = Mathf.Infinity;
    private int wave = -1;
    private int enemyWaveCount = 0;
    private bool waitingForWave = false;
    private bool wavesDone = false;

    private void Start()
    {
        //Get spawnpoint
        spawnPoint = FindObjectOfType<Spawnpoint>().GetPosition();
    }

    private void Update()
    {
        float time = Time.time;

        //Check if should start new wave
        if (!isSpawning && waitingForWave && time >= (lastWaveTime + waveDelay))
        {
            StartWave();
        }

        //Check if should spawn enemy
        if(isSpawning && time >= (lastEnemySpawnTime + spawnDelay))
        {
            lastEnemySpawnTime = time;
            SpawnEnemy();
            enemyWaveCount++;

            if(enemyWaveCount >= waves[wave])
            {
                isSpawning = false;
            }
        }

        if (!waitingForWave && enemies.Count <= 0)
        {
            //Start wave timer
            lastWaveTime = time;
            waitingForWave = true;
        }
    }

    private void StartWave()
    {
        wave++;
        waitingForWave = false;

        if (wave >= waves.Length)
        {
            WavesDone();
            return;
        }

        enemyWaveCount = 0;
        isSpawning = true;
    }

    private void WavesDone()
    {
        if(!wavesDone)
        {
            wavesDone = true;
            Debug.Log("WavesDone");
        }
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyObjects[0], spawnPoint, Quaternion.identity);
        newEnemy.GetComponent<EnemyID>().Setup(enemyCounter);

        enemies.Add(enemyCounter, newEnemy);

        enemyCounter++;
    }

    public void DestroyEnemy(int id)
    {
        if (enemies.TryGetValue(id, out GameObject enemy)) Destroy(enemy);

        enemies.Remove(id);
    }
}
