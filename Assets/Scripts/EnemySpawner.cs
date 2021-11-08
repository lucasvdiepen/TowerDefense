using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyObjects;
    public int[] waves;
    public float spawnDelay = 0.1f;
    public float waveDelay = 7f;
    public int enemyIncrease = 4;
    public float yOffset = 0.623f;

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
    private int enemiesToSpawn = 0;
    private bool isStoppedSpawning = false;

    private void Start()
    {
        //Get spawnpoint
        spawnPoint = FindObjectOfType<Spawnpoint>().GetPosition();

        int wavesToWin = -1;

        if (waves == null || waves.Length == 0) wavesToWin = FindObjectOfType<GameInfo>().GetWavesToFinish();
        else wavesToWin = waves.Length;

        FindObjectOfType<PlayerWavesUI>().SetWavesToWinText(wavesToWin);
    }

    private void Update()
    {
        if(!isStoppedSpawning)
        {
            float time = Time.time;

            //Check if should start new wave
            if (!isSpawning && waitingForWave && time >= (lastWaveTime + waveDelay))
            {
                StartWave();
            }

            //Check if should spawn enemy
            if (isSpawning && time >= (lastEnemySpawnTime + spawnDelay))
            {
                lastEnemySpawnTime = time;
                SpawnEnemy();
                enemyWaveCount++;

                if ((waves == null || waves.Length == 0) && enemiesToSpawn > 0 && enemiesToSpawn <= enemyWaveCount) isSpawning = false;

                if (waves != null && waves.Length > 0 && enemyWaveCount >= waves[wave])
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
    }

    private void StartWave()
    {
        wave++;
        waitingForWave = false;

        if (waves != null && waves.Length > 0 && wave >= waves.Length)
        {
            WavesDone();
            return;
        }

        if((waves == null || waves.Length == 0) && FindObjectOfType<GameInfo>().GetWavesToFinish() <= wave)
        {
            WavesDone();
            return;
        }

        if(waves == null || waves.Length == 0)
        {
            enemiesToSpawn = enemyIncrease * (wave + 1);
        }

        FindObjectOfType<PlayerWavesUI>().SetWavesText(wave);

        enemyWaveCount = 0;
        isSpawning = true;
    }

    private void WavesDone()
    {
        if(!wavesDone)
        {
            wavesDone = true;
            FindObjectOfType<WinScreen>().ShowWinScreen();

            if(waves == null || waves.Length == 0) PlayerPrefs.SetInt("wavesToWin", FindObjectOfType<GameInfo>().GetWavesToFinish());

            Debug.Log("WavesDone");
        }
    }

    public void StopSpawning()
    {
        isStoppedSpawning = true;
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyObjects[0], spawnPoint + new Vector3(0, yOffset, 0), Quaternion.identity);
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
