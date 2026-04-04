using UnityEngine;

public abstract class BasePickup : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        Collect(player);
    }

    protected abstract void Collect(PlayerController player);
}