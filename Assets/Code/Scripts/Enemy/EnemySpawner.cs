using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs; // Array (GameObject[]) that stores enemies;
    
    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8; // Enemies to spawn at the first wave;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;

    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }
    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return; // !isSpawning => isSpawning == false; ! -> (Not Equal);

        timeSinceLastSpawn += Time.deltaTime; 
        
        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--; // => enemiesLeftToSpawn - 1;
            enemiesAlive++;       // => enemiesAlive + 1; 
            timeSinceLastSpawn = 0f; // Reset the timeSinceLastSpawn to spawn another enemy.
        }

        // If there are no more enemies alive and there are no more enemies to spawn.
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves); // Waiting for 5 seconds between waves.
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        // Debug.Log("Spawn Enemy");
        GameObject prefabToSpawn = enemyPrefabs[0]; // Choosing a prefab to Instantiate();
        Instantiate(prefabToSpawn, LevelManager.main.stratPoint.position, Quaternion.identity);
    }

    private void EnemyDestroyed()
    {
        // void means method does not return any data, and only executes commands.
        enemiesAlive--; // => enemiesAlive - 1;
    }

    // int(Integer), void -> return types
    // Primitive Data Types
    // Integer Numbers (int) : 1, 2, 3, 4, 5, 6, . . .
    // Floating Point Numbers (float) : 1.2, 1.5, 5.9 . . .
    // String (string) : "Hello World!", "Any sentence!" . . .
    // Boolean (bool) : true, false;
    private int EnemiesPerWave()
    {
        //Calculate the number of enemies to spawn;
        // The method returns int data;
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyFactor));
    }
}
