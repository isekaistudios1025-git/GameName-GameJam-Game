using UnityEngine;

public class DMGBoostPickUp : BasePickup
{
    [SerializeField] private int bonusDamage = 1;

    protected override void Collect(PlayerController player)
    {
        player.ApplyDamageBoost(bonusDamage);
        Destroy(gameObject);
    }
}