using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of different enemy prefabs
    public GameObject spawner;
    public float spawnDelay = 3f;     // Delay between spawns
    private float nextSpawnTime;      // Tracks when the next spawn should occur

    void Start()
    {
        // Set the initial spawn time
        nextSpawnTime = Time.time + spawnDelay;
    }

    void Update()
    {
        // Check if it's time to spawn a new enemy
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnDelay; // Reset the timer
        }
    }

    void SpawnEnemy()
    {
        // Randomly select an enemy type from the enemyPrefabs array
        GameObject selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];


        // Instantiate the enemy at the selected spawn point
        Instantiate(selectedEnemy, spawner.transform);
    }
}
