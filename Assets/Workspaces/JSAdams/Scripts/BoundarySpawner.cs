//----- BoundarySpawner.cs START-----
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoundarySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemiesToSpawn = 5;
    [SerializeField] private float spawnInterval = 1f;

    private BoxCollider spawnBounds;
    private Transform player;

    private void Awake()
    {
        spawnBounds = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        GameObject foundPlayer = GameObject.Find("Player");

        if (foundPlayer != null)
        {
            player = foundPlayer.transform;
        }

        StartCoroutine(SpawnWaveRoutine());
    }

    private IEnumerator SpawnWaveRoutine()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 center = spawnBounds.bounds.center;
        Vector3 size = spawnBounds.bounds.size;

        float randomZ = Random.Range(center.z - size.z * 0.5f, center.z + size.z * 0.5f);

        Vector3 spawnPosition = new Vector3(
            center.x,
            player != null ? player.position.y : center.y,
            randomZ
        );

        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();

        if (enemyController != null && player != null)
        {
            enemyController.SetPlayer(player);
        }
    }

    private void OnDrawGizmosSelected()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(box.bounds.center, box.bounds.size);
    }
}
//----- BoundarySpawner.cs END-----