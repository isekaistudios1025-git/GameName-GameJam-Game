using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Transform player;

    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0f, 1f, 0f);

        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();

        if (enemyController != null)
        {
            enemyController.SetPlayer(player);
        }
    }
}
