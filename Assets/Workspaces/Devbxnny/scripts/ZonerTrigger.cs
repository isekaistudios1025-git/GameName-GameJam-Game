using System;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public event Action OnZoneComplete;

    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private bool triggerOnce = true;

    [SerializeField] private Transform barrierEntry;
    [SerializeField] private Transform barrierExit;

    private bool hasTriggered = false;
    private PlayerController currentPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && triggerOnce) return;
        if (!other.CompareTag("Player")) return;

        hasTriggered = true;

        Debug.Log("ZoneTrigger: Player entered zone.");

        currentPlayer = other.GetComponent<PlayerController>();

        if (currentPlayer != null && barrierEntry != null && barrierExit != null)
        {
            currentPlayer.LockZone(barrierEntry.position.x, barrierExit.position.x);
        }

        if (spawner != null)
        {
            spawner.OnSpawnerComplete += HandleSpawnerComplete;
            spawner.BeginSpawn();
        }
        else
        {
            Debug.LogWarning("ZoneTrigger: No spawner assigned.");
        }
    }

    private void HandleSpawnerComplete()
    {
        spawner.OnSpawnerComplete -= HandleSpawnerComplete;

        if (currentPlayer != null)
        {
            currentPlayer.UnlockZone();
        }

        Debug.Log("ZoneTrigger: Zone complete.");
        OnZoneComplete?.Invoke();
    }
}