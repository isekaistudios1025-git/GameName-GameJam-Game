using UnityEngine;

public class HealthPickUp : BasePickup
{
    protected override void Collect(PlayerController player)
    {
        player.FullHeal();
        Destroy(gameObject);
    }
}