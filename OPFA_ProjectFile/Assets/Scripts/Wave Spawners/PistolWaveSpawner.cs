using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWaveSpawner : MonoBehaviour
{
    // Defining the data structure 
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable] // Allows us to change the values of the variables in the inspector 
    public class Wave
    {
        public string name;
        public Transform pistolEnemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountdown = timeBetweenWaves;

        // Error check for number of spawn points
        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points");
        }
    }

    void Update()
    {
        // Wave completion 
        if (state == SpawnState.WAITING)
        {
            // Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                // Begin a new wave
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        // If it is time to start spawning waves
        if (waveCountdown <= 0)
        {
            // Check if we've already started spawning waves
            if (state != SpawnState.SPAWNING)
            {
                // Start spawning wave at index nextWave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Complete");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        // Checks to see if the waves exceed the wave length 
        if (nextWave + 1 > waves.Length - 1)
        {
            // This is where we can add the finish game screen 
            Debug.Log("ALL WAVES COMPLETED!");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        // Search countdown is to make this code less taxing on the system, since without it, the code will go through each game object every frame
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;

            if (GameObject.FindGameObjectWithTag("Bat") == null && GameObject.FindGameObjectWithTag("Pistol_Enemy") == null) // If there are bat enemies than return false
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave:" + _wave.name);
        state = SpawnState.SPAWNING;

        // Spawning logic, for loop runs the number of enemies that we want to spawn 
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnPistolEnemy(_wave.pistolEnemy);

            // Every time an enemy is spawned, it will wait 1 second before spawning a new one, until the _wave.count has reached maximum 
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnPistolEnemy(Transform _pistolEnemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_pistolEnemy, _sp.position, _sp.rotation);
    }
}
