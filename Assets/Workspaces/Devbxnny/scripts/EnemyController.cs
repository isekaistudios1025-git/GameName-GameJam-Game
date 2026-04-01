using UnityEngine;

public class EnemyController : ActorController
{
    [Header("Enemy Tracking")]
    [SerializeField] private Transform player;
    [SerializeField] private float stopDistance = 1.5f;

    [Header("Enemy Attack")]
    [SerializeField] private float attackCooldown = 1f;

    private float attackTimer;

    void Update()
    {
        if (player == null)
        {
            Move(Vector3.zero);
            return;
        }

        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f;

        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > stopDistance)
        {
            Move(directionToPlayer);
            attackTimer = 0f;
        }
        else
        {
            Move(Vector3.zero);

            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                Attack();
                attackTimer = 0f;
            }
        }
    }

    public void SetPlayer(Transform target)
    {
        player = target;
    }
}