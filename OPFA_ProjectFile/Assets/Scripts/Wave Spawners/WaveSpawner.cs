using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    // Defining the data structure 
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable] // Allows us to change the values of the variables in the inspector 
    public class Wave
    {
        public string name;
        public Transform batEnemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    public int nextWave = 0;

    public TextMeshProUGUI waveText;

    public Transform[] spawnPoints;
    public Transform[] itemSpawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    public Animator anim;
    public PistolWaveSpawner pistolWaveSpawner;
    public PlayerController player;

    public GameObject medkit;
    public GameObject ammoCrate;
    bool myMedkit = true;
    bool myAmmoCrate = true;

    public Transform[] projectileSpawnPoints;
    public GameObject rightProjectile;
    public GameObject leftProjectile;
    public GameObject upProjectile;
    public GameObject downProjectile;
    bool projectileSpawned = false;

    // Weapons
    public GameObject smgPickup;
    public GameObject hugePickup;

    // UI variables
    public GameObject dashText;
    public GameObject smgText;
    public GameObject hugeText;

    [SerializeField]int randomNum;

    void Start()
    {
        anim.SetTrigger("start");
        waveCountdown = timeBetweenWaves;

        // Error check for number of spawn points
        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points");
        }
    }

    void Update()
    {
        // Once wave is equal to 10, start spawning pistol enemies
        if (nextWave >= 10)
        {
            pistolWaveSpawner.GetComponent<PistolWaveSpawner>().enabled = true;
        }
        
        // Spawn items
        if (state == SpawnState.COUNTING && nextWave % 5 == 0 && nextWave > 0 && myMedkit == true) // Multiple of 5, every 5 waves
        {
            myMedkit = false;
            SpawnMedkit();
        }
        else if (state == SpawnState.COUNTING && nextWave % 3 == 0 && nextWave > 10 && myAmmoCrate == true)
        {
            myAmmoCrate = false;
            SpawnAmmoCrate();
        }

        // Unlock text
        if (state == SpawnState.COUNTING && nextWave >= 6)
        {
            player.dashUnlocked = true;
            dashText.SetActive(true);
        }
        
        if (player.smgFound == true && nextWave >= 10)
        {
            smgText.SetActive(true);
        }

        if (player.hugeFound == true && nextWave >= 16)
        {
            hugeText.SetActive(true);
        }

        // Spawn weapons
        if (nextWave == 10 && smgPickup != null)
        {
            smgPickup.SetActive(true);
        }
        else if (nextWave == 16 && hugePickup != null)
        {
            hugePickup.SetActive(true);
        }

        // Spawn Big Projectiles
        // This wave will always spawn a big projectile on wave 6 so player can test dash ability
        if (nextWave == 6 && state == SpawnState.SPAWNING && projectileSpawned == true)
        {
            if (randomNum <= 4)
            {
                SpawnProjectileRight();
                projectileSpawned = false;
            }
            else if (randomNum >= 5)
            {
                SpawnProjectileLeft();
                projectileSpawned = false;
            }

        }
        else if (nextWave > 6 && state == SpawnState.SPAWNING && projectileSpawned == true)
        {
            // Randomly spawn one of the four projectiles depending on what random number is generated
            if (randomNum == 1)
            {
                SpawnProjectileRight();
                projectileSpawned = false;
            }
            else if (randomNum == 2)
            {
                SpawnProjectileLeft();
                projectileSpawned = false;
            }
            else if (randomNum == 3)
            {
                SpawnProjectileUp();
                projectileSpawned = false;
            }
            else if (randomNum == 4)
            {
                SpawnProjectileDown();
                projectileSpawned = false;
            }
        }

        // Wave completion 
        if (state == SpawnState.WAITING)
        {
            // Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                // Begin a new wave
                WaveCompleted();
                randomNum = Random.Range(0, 9);
                waveText.text = "Wave: " + nextWave.ToString();
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

        // Turn boolean to true so that at the next appropiate wave, item will spawn only once
        if (myMedkit == false)
        {
            myMedkit = true;
        }

        if (myAmmoCrate == false)
        {
            myAmmoCrate = true;
        }

        if (projectileSpawned == false)
        {
            projectileSpawned = true;
        }

        // Play countdown animation
        anim.SetTrigger("start");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        // Checks to see if the waves exceed the wave length 
        if (nextWave + 1 > waves.Length - 1)
        {
            // ADD CUTSCENE BEFORE BOSS FIGHT
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
        Debug.Log("Spawning Wave (BAT):" + _wave.name);
        state = SpawnState.SPAWNING;

        // Spawning logic, for loop runs the number of enemies that we want to spawn 
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.batEnemy);

            // Every time an enemy is spawned, it will wait 1 second before spawning a new one, until the _wave.count has reached maximum 
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        // Spawn enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }

    void SpawnMedkit()
    {
        Transform _medSp = itemSpawnPoints[Random.Range(0, itemSpawnPoints.Length)];
        Instantiate(medkit, _medSp.position, _medSp.rotation);
    }

    void SpawnAmmoCrate()
    {
        Transform _ammoSp = itemSpawnPoints[Random.Range(0, itemSpawnPoints.Length)];
        Instantiate(ammoCrate, _ammoSp.position, _ammoSp.rotation);
    }

    void SpawnProjectileRight()
    {
        Transform _projectileSp = projectileSpawnPoints[0];
        Instantiate(rightProjectile, _projectileSp.position, _projectileSp.rotation);
    }

    void SpawnProjectileLeft()
    {
        Transform _projectileSp = projectileSpawnPoints[1];
        Instantiate(leftProjectile, _projectileSp.position, _projectileSp.rotation);
    }

    void SpawnProjectileUp()
    {
        Transform _projectileSp = projectileSpawnPoints[2];
        Instantiate(upProjectile, _projectileSp.position, _projectileSp.rotation);
    }

    void SpawnProjectileDown()
    {
        Transform _projectileSp = projectileSpawnPoints[3];
        Instantiate(downProjectile, _projectileSp.position, _projectileSp.rotation);
    }
}
