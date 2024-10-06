using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of different enemy prefabs
    public Transform[] spawnPoints; // Spawn points for enemies
    public Canvas canvas;
    public float spawnDelay = 5f; // Delay between spawns

    private float nextSpawnTime;

    void FixedUpdate()
    {
        // Check if it's time to spawn a new enemy
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnDelay; // Reset spawn delay
        }
    }

    void SpawnEnemy()
    {
        // Choose a random enemy prefab and a random spawn point
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length-1);
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length-1);

        // Instantiate the chosen enemy at the chosen spawn point
        var enemy = Instantiate(enemyPrefabs[randomEnemyIndex], spawnPoints[randomSpawnPointIndex].position, Quaternion.identity, canvas.transform);
        enemy.transform.Rotate(new Vector3(0,0,180));
    }
}
