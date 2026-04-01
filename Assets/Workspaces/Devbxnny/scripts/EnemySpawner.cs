using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;

    private float timer;
    private Transform player;

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

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y = player.position.y;

        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();

        if (enemyController != null)
        {
            enemyController.SetPlayer(player);
        }
    }
}