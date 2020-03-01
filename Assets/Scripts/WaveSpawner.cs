using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that spawns waves of random enemies at random locations.
/// 
/// As of creation it runs for as many waves are set
/// </summary>
public class WaveSpawner : MonoBehaviour
{

    [Header("Waves Vars")]
    public int[] waves; //This is an array with how many enemies are within the waves
    private int _currentWave;   //This is an index value for the current wave we are on
    public GameObject spawnLocationParent;  //This is a parent obj that has all the spawn points as it's children
    private Transform[] SpawnPoints
    {
        get
        {
            return spawnLocationParent.GetComponentsInChildren<Transform>();
        }
    }
    public int timeBetweenWaves;

    [Header("Enemies Vars")]
    public GameObject[] enemyPrefabs;    //Array of prefabs that can be spawned
    public List<GameObject> spawnedEnemies;   //List of currently spawned enemies
    public int spawnRate;

    [Header("Misc")]
    public GameObject playerObj;
    public float playerTargetChance;    //chance the enemy will target the player
    public GameObject baseObj;
    public float baseTargetChance;  //Chance the enemy will target the base

    private void Start()
    {
        StartWave();
    }

    /// <summary>
    /// Simple method that starts a wave
    /// </summary>
    public void StartWave()
    {
        //Replaces this with proper UI later
        print("Starting Wave " + (_currentWave + 1));

        StartCoroutine(WaveRoutine());
    }

    /// <summary>
    /// Coroutine that spawns all enemies and keeps track of when
    /// they are all dead
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaveRoutine()
    {
        GameObject.Find("PlayerUI").GetComponent<UIControl>().round = _currentWave + 1;
        int enemySpawnedCount = 0;
        spawnedEnemies = new List<GameObject>(waves[_currentWave]);

        //Spawns how many enemies are specified for this wave
        while(enemySpawnedCount < waves[_currentWave])
        {
            int randSpawn = Random.Range(1, SpawnPoints.Length);    //Note: 0 is the parent obj and should not be chosen
            int randEnemy = Random.Range(0, enemyPrefabs.Length);

            GameObject newEnemy = Instantiate(enemyPrefabs[randEnemy], SpawnPoints[randSpawn].position, SpawnPoints[randSpawn].rotation);

            //Sets references for the new enemy
            spawnedEnemies.Add(newEnemy);
            enemySpawnedCount++;
            newEnemy.GetComponent<EnemyHealth>().spawner = this;

            //chooses a random target for the new enemy
            float randChance = Random.Range(0, playerTargetChance + baseTargetChance);
            if (randChance < playerTargetChance)
                newEnemy.GetComponent<SimpleAI>().currentTarget = playerObj.transform;
            else
                newEnemy.GetComponent<SimpleAI>().currentTarget = baseObj.transform;

            yield return new WaitForSeconds(spawnRate);
        }

        //This waits unti all enemies are dead (end of wave)
        while(spawnedEnemies.Count > 0)
        {
            yield return new WaitForEndOfFrame();
        }

        OnWaveEnd();

        yield return null;
    }

    /// <summary>
    /// Method that handles when the wave ends. It can either
    /// start the countdown for the next wave or tell the player
    /// they have won
    /// </summary>
    private void OnWaveEnd()
    {
        //Replaces this with proper UI later
        print("Wave " + (_currentWave + 1) + " has ended");

        _currentWave++;

        if (_currentWave < waves.Length)
            StartCoroutine(WaitNextRound());
        else
            //Replaces this with proper UI later
            print("YOU WIN");
    }

    /// <summary>
    /// Coroutine that waits for the next rounds.
    /// 
    /// As of creation this only outputs a print statement but later
    /// we should add in UI elements to display the count down
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitNextRound()
    {
        int count = 0;
        while(count < timeBetweenWaves)
        {
            //Replaces this with proper UI later
            print("Time till next wave: " + (timeBetweenWaves - count));

            count++;

            yield return new WaitForSeconds(1f);
        }

        StartWave();
    }
}
