using System.Collections;
using System.Collections.Generic;
using UnityEngine;










// IMPORTANT: MAKE SURE THAT ALL ENEMIES HAVE THE TAG "Enemy" !!!!!!!!!!!!!!!







public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {Spawning, Waiting, Counting}

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.Counting;

    void Start()
    {

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.Waiting)
        {
            // Checks if enemies are still alive
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return; // Waits till all enemies of wave are dead
            }
        }


        if (waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()  // Restarts wave process...
    {
        Debug.Log("Wave Completed.");

        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE. Looping...");
        } 
        else
        {
            nextWave++;
        }
    }


    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave (Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.Spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        // Spawn enemy
       Debug.Log("Spawning Enemy: " + _enemy.name);

       Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
       Instantiate(_enemy, _sp.position, _sp.rotation);
        
    }

}
