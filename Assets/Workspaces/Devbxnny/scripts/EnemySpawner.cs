using UnityEngine;

[System.Serializable]
public class EnemySpawnEntry
{
    public GameObject enemyPrefab;
    public int enemyCount = 1;
    public float spawnInterval = 1f;
}

[System.Serializable]
public class EnemyWave
{
    public EnemySpawnEntry[] enemiesInWave;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private EnemyWave[] waves;

    [Header("Spawn Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float minimumDistanceFromPlayer = 2f;

    private Transform player;
    private float timer;

    private int currentWaveIndex = 0;
    private int currentEnemyTypeIndex = 0;
    private int spawnedCountForCurrentType = 0;

    private int totalEnemiesSpawnedThisWave = 0;
    private int totalEnemiesInWave = 0;
    private int aliveEnemiesInWave = 0;

    private bool finishedSpawningWave = false;

    private void Start()
    {
        GameObject foundPlayer = GameObject.Find("Player");

        if (foundPlayer != null)
        {
            player = foundPlayer.transform;
        }

        if (waves.Length > 0)
        {
            CalculateTotalEnemiesInWave();
        }
    }

    private void Update()
    {
        if (player == null) return;
        if (currentWaveIndex >= waves.Length) return;

        if (finishedSpawningWave)
        {
            if (aliveEnemiesInWave <= 0)
            {
                AdvanceToNextWave();
            }

            return;
        }

        EnemyWave currentWave = waves[currentWaveIndex];

        if (currentEnemyTypeIndex >= currentWave.enemiesInWave.Length)
        {
            finishedSpawningWave = true;
            return;
        }

        EnemySpawnEntry currentEntry = currentWave.enemiesInWave[currentEnemyTypeIndex];

        timer += Time.deltaTime;

        if (timer >= currentEntry.spawnInterval)
        {
            SpawnEnemy(currentEntry.enemyPrefab);
            spawnedCountForCurrentType++;
            totalEnemiesSpawnedThisWave++;
            aliveEnemiesInWave++;
            timer = 0f;
        }

        if (spawnedCountForCurrentType >= currentEntry.enemyCount)
        {
            currentEnemyTypeIndex++;
            spawnedCountForCurrentType = 0;
            timer = 0f;
        }

        if (totalEnemiesSpawnedThisWave >= totalEnemiesInWave)
        {
            finishedSpawningWave = true;
        }
    }

    private void SpawnEnemy(GameObject prefabToSpawn)
    {
        Vector3 spawnPosition = GetSpawnPosition();

        GameObject spawnedEnemy = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();

        if (enemyController != null)
        {
            enemyController.SetPlayer(player);
            enemyController.OnEnemyDied += HandleEnemyDied;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float spawnRadius = 4f;

        for (int i = 0; i < 10; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;

            Vector3 candidatePosition = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);
            candidatePosition.y = player.position.y;

            float distanceToPlayer = Vector3.Distance(candidatePosition, player.position);

            if (distanceToPlayer >= minimumDistanceFromPlayer)
            {
                return candidatePosition;
            }
        }

        Vector3 fallbackPosition = transform.position;
        fallbackPosition.y = player.position.y;
        return fallbackPosition;
    }

    private void HandleEnemyDied()
    {
        aliveEnemiesInWave--;

        if (aliveEnemiesInWave < 0)
        {
            aliveEnemiesInWave = 0;
        }
    }

    private void AdvanceToNextWave()
    {
        currentWaveIndex++;

        currentEnemyTypeIndex = 0;
        spawnedCountForCurrentType = 0;
        totalEnemiesSpawnedThisWave = 0;
        totalEnemiesInWave = 0;
        aliveEnemiesInWave = 0;
        finishedSpawningWave = false;
        timer = 0f;

        if (currentWaveIndex < waves.Length)
        {
            CalculateTotalEnemiesInWave();
        }
        else
        {
            Debug.Log("All waves completed.");
        }
    }

    private void CalculateTotalEnemiesInWave()
    {
        totalEnemiesInWave = 0;

        EnemyWave wave = waves[currentWaveIndex];

        foreach (EnemySpawnEntry entry in wave.enemiesInWave)
        {
            totalEnemiesInWave += entry.enemyCount;
        }
    }
}