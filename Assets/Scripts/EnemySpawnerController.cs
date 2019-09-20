using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField]
    GameObject enemySpawnerPrefab;

    [Range(0, 10)]
    public float spawnDelay = 3f;
    [Range(0, 10)]
    public float changeSpeedManufactureClones = 2f;
    [Range(0, 10)]
    public float randomSpawning = 1f;

    private List<GameObject> enemies = new List<GameObject>();

    private float lastSpawnTime;
    private float randomSpawnDelay;
    private bool stop = false;

    private void Start()
    {
        if (enemySpawnerPrefab == null)
        {
            return;
        }
        randomSpawnDelay = spawnDelay;
        SpawnEnemy();
    }

    private void Update()
    {
        if (!stop && Time.time > lastSpawnTime + randomSpawnDelay)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        lastSpawnTime = Time.time;

        // Öka eller sänk hastigheten av tillverkingen av kloner
        lastSpawnTime *= changeSpeedManufactureClones;
        randomSpawnDelay = Random.Range(spawnDelay - randomSpawning, spawnDelay + randomSpawning);

        // Skapar enemy klon
        GameObject enemy = Instantiate(enemySpawnerPrefab);

        // Lägger till klonen i listan
        enemies.Add(enemy);
        EnemyFollowController enemyFollowController = enemy.GetComponentInChildren<EnemyFollowController>();

        enemyFollowController.enemySpawnerController = this;
    }

    void DestroyEnemyClone(GameObject enemy)
    {
        // Ta bort enemy från listan
        enemies.Remove(enemy);

        // Förstör enemy
        Destroy(enemy);
    }

    void Stop()
    {
        stop = true;

        // Gå igenom listan nerifrån och förstör enemies
        for (int i  = enemies.Count - 1; i >= 0; i--)
        {
            DestroyEnemyClone(enemies[i]);
        }
    }

}
