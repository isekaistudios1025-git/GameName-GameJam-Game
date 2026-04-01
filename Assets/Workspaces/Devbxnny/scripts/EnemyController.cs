using UnityEngine;

public class EnemyController : ActorController
{
    [Header("EnemyTracking")]
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Move(Vector3.zero);
            return;
        }
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f; // Ignore vertical difference for movement

        Move(directionToPlayer);
    }

    public void SetPlayer(Transform target)
    {
        player = target;
    }
}
