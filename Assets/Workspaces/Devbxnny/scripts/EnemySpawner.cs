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

    private Transform player;
    private float timer;

    private int currentWaveIndex = 0;
    private int currentEnemyTypeIndex = 0;
    private int spawnedCountForCurrentType = 0;

    private void Start()
    {
        GameObject foundPlayer = GameObject.Find("Player");

        if (foundPlayer != null)
        {
            player = foundPlayer.transform;
        }
    }

    void Update()
    {
        if (player == null) return;
        if (currentWaveIndex >= waves.Length) return;

        EnemyWave currentWave = waves[currentWaveIndex];

        if (currentEnemyTypeIndex >= currentWave.enemiesInWave.Length)
        {
            currentWaveIndex++;
            currentEnemyTypeIndex = 0;
            spawnedCountForCurrentType = 0;
            timer = 0f;
            return;
        }

        EnemySpawnEntry currentEntry = currentWave.enemiesInWave[currentEnemyTypeIndex];

        timer += Time.deltaTime;

        if (timer >= currentEntry.spawnInterval)
        {
            SpawnEnemy(currentEntry.enemyPrefab);
            spawnedCountForCurrentType++;
            timer = 0f;
        }

        if (spawnedCountForCurrentType >= currentEntry.enemyCount)
        {
            currentEnemyTypeIndex++;
            spawnedCountForCurrentType = 0;
            timer = 0f;
        }
    }

    private void SpawnEnemy(GameObject prefabToSpawn)
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y = player.position.y;

        GameObject spawnedEnemy = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();

        if (enemyController != null)
        {
            enemyController.SetPlayer(player);
        }
    }
}