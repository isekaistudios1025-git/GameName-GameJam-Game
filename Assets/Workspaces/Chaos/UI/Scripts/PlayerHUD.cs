using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WaveText;
    [SerializeField] private TextMeshProUGUI EnemiesNumberText;

    [SerializeField] private EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
    }

    void Update()
    {
        if (enemySpawner != null)
        {
            WaveText.text = $"Wave: {enemySpawner.CurrentWaveIndex + 1}";
            EnemiesNumberText.text = $"Enemies: {enemySpawner.AliveEnemiesInWave}";
        }
    }
}