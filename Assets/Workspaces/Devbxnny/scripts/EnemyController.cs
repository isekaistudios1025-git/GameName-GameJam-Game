//----- EnemyController.cs START-----

using System;
using System.Collections;
using UnityEngine;

public class EnemyController : ActorController
{
    [Header("Enemy Tracking")]
    [SerializeField] private Transform player;
    [SerializeField] private float stopDistance = 1.5f;

    [Header("Enemy Attack")]
    [SerializeField] private float attackCooldown = 1f;

    [Header("Enemy Death")]
    [SerializeField] private float deathDelay = 1f;

    [Header("Enemy Rewards")]
    [SerializeField] private int scoreValue = 100;

    private float attackTimer;
    private Transform visuals;
    private Animator animator;
    private bool isDead = false;

    private PlayerState playerState;

    public Action OnEnemyDied;

    protected override void Awake()
    {
        base.Awake();

  
        visuals = transform.Find("Visuals");

        if (visuals != null)
        {
            animator = visuals.GetComponent<Animator>();
        }
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            playerState = playerObject.GetComponent<PlayerState>();
        }

    }

    void Update()
    {
        if (isDead)
        {
            Move(Vector3.zero);

            if (animator != null)
            {
                animator.SetBool("IsMoving", false);
            }

            return;
        }

        if (player == null)
        {
            Move(Vector3.zero);

            if (animator != null)
            {
                animator.SetBool("IsMoving", false);
            }

            return;
        }

        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f;

        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > stopDistance)
        {
            Move(directionToPlayer);

            if (animator != null)
            {
                animator.SetBool("IsMoving", true);
            }

            if (directionToPlayer.x != 0 && visuals != null)
            {
                visuals.localScale = new Vector3(Mathf.Sign(directionToPlayer.x), 1f, 1f);
            }

            attackTimer = 0f;
        }
        else
        {
            Move(Vector3.zero);

            if (animator != null)
            {
                animator.SetBool("IsMoving", false);
            }

            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                Attack();

                if (animator != null)
                {
                    animator.SetTrigger("Attack");
                }

                attackTimer = 0f;
            }
        }
    }

    public void SetPlayer(Transform target)
    {
        player = target;
    }

    public override void Hurt()
    {
        base.Hurt();
        PlayHurtAnimation();
    }

    public void PlayHurtAnimation()
    {
        if (isDead) return;

        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }
    }

    public override void Die()
    {
        if (isDead) return;

        isDead = true;

        Move(Vector3.zero);

        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
            animator.SetTrigger("Die");
        }

        if (playerState != null)
        {
            playerState.AddScore(scoreValue);
        }

        OnEnemyDied?.Invoke();
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(deathDelay);
        base.Die();
    }
}

//----- EnemyController.cs END-----