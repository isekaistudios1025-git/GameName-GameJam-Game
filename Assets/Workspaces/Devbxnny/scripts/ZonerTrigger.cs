using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ZoneSpawnerEntry
{
    public EnemySpawner spawner;
    public float startDelay = 0f;
    public EnemySpawner waitForSpawner;
}

public class ZonerTrigger : MonoBehaviour
{
    public event Action OnZoneComplete;

    [Header("Spawner Setup")]
    [SerializeField] private ZoneSpawnerEntry[] spawnerEntries;
    [SerializeField] private bool triggerOnce = true;

    [Header("Zone Bounds")]
    [SerializeField] private Transform barrierEntry;
    [SerializeField] private Transform barrierExit;

    private bool hasTriggered = false;
    private PlayerController currentPlayer;

    private int totalSpawnersToComplete = 0;
    private int completedSpawnerCount = 0;
    private readonly HashSet<EnemySpawner> completedSpawners = new HashSet<EnemySpawner>();

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

        StartCoroutine(RunSpawnerEntries());
    }

    private IEnumerator RunSpawnerEntries()
    {
        if (spawnerEntries == null || spawnerEntries.Length == 0)
        {
            Debug.LogWarning("ZoneTrigger: No spawner entries assigned.");
            UnlockZoneAndComplete();
            yield break;
        }

        totalSpawnersToComplete = 0;
        completedSpawnerCount = 0;
        completedSpawners.Clear();

        for (int i = 0; i < spawnerEntries.Length; i++)
        {
            if (spawnerEntries[i] != null && spawnerEntries[i].spawner != null)
            {
                totalSpawnersToComplete++;
            }
        }

        if (totalSpawnersToComplete == 0)
        {
            Debug.LogWarning("ZoneTrigger: No valid spawners assigned.");
            UnlockZoneAndComplete();
            yield break;
        }

        for (int i = 0; i < spawnerEntries.Length; i++)
        {
            ZoneSpawnerEntry entry = spawnerEntries[i];

            if (entry == null || entry.spawner == null)
                continue;

            StartCoroutine(HandleSpawnerEntry(entry));
        }
    }

    private IEnumerator HandleSpawnerEntry(ZoneSpawnerEntry entry)
    {
        if (entry.waitForSpawner != null)
        {
            yield return new WaitUntil(() => completedSpawners.Contains(entry.waitForSpawner));
        }

        if (entry.startDelay > 0f)
        {
            yield return new WaitForSeconds(entry.startDelay);
        }

        entry.spawner.OnSpawnerComplete -= HandleSpawnerComplete;
        entry.spawner.OnSpawnerComplete += HandleSpawnerComplete;
        entry.spawner.BeginSpawn();
    }

    private void HandleSpawnerComplete(EnemySpawner completedSpawner)
    {
        completedSpawner.OnSpawnerComplete -= HandleSpawnerComplete;

        if (!completedSpawners.Contains(completedSpawner))
        {
            completedSpawners.Add(completedSpawner);
            completedSpawnerCount++;
        }

        if (completedSpawnerCount >= totalSpawnersToComplete)
        {
            UnlockZoneAndComplete();
        }
    }

    private void UnlockZoneAndComplete()
    {
        for (int i = 0; i < spawnerEntries.Length; i++)
        {
            if (spawnerEntries[i] != null && spawnerEntries[i].spawner != null)
            {
                spawnerEntries[i].spawner.OnSpawnerComplete -= HandleSpawnerComplete;
            }
        }

        if (currentPlayer != null)
        {
            currentPlayer.UnlockZone();
        }

        Debug.Log("ZoneTrigger: Zone complete.");
        OnZoneComplete?.Invoke();
    }
}