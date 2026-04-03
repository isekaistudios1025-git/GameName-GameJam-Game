// ----- ActorController.cs START -----
using UnityEngine;
using System;

public abstract class ActorController : MonoBehaviour
{

    //events
    public event Action<float, float> OnHealthChanged;


    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 6f;

    [Header("Combat")]
    [SerializeField] protected float attackRadius = 1f;
    [SerializeField] protected float attackOffset = 1f;
    [SerializeField] protected int attackDamage = 1;


    [Header("Health")]
    [SerializeField] protected int maxHealth = 3;
    protected int currentHealth;

    [Header("Hit Stun")]
    [SerializeField] protected float hurtStunDuration = 0.1f;
    protected bool canMove = true;

    [Header("Audio")]
    [SerializeField] protected AudioClip attackSFX;
    [SerializeField] protected AudioClip hurtSFX;
    [SerializeField] protected AudioClip deathSFX;
    [SerializeField] protected AudioClip footstepSFX;
    [SerializeField] protected float footstepInterval = 0.35f;

    protected float footstepTimer;

    protected Rigidbody rb;
    protected Vector3 moveDirection;

    // shared facing direction for visuals + attack direction
    protected float facingDirection = 1f;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.freezeRotation = true;

        currentHealth = maxHealth;

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    protected virtual void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        HandleFootsteps();
    }

    public virtual void Move(Vector3 direction)
    {
        if (!canMove)
        {
            moveDirection = Vector3.zero;
            return;
        }

        moveDirection = direction.normalized;

        // update facing without flipping root collider
        if (direction.x != 0)
        {
            facingDirection = Mathf.Sign(direction.x);
        }
    }

    public virtual void Attack()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(attackSFX);
        }

        Vector3 attackOrigin = transform.position + Vector3.right * facingDirection * attackOffset;

        Collider[] hits = Physics.OverlapSphere(attackOrigin, attackRadius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject) continue;

            ActorController other = hit.GetComponent<ActorController>();

            if (other != null && other.GetType() != GetType())
            {
                other.TakeDamage(attackDamage);
                Debug.Log($"{name} hit {other.name} for {attackDamage}");
            }
        }
    }
    public virtual void TakeDamage(int damage)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(hurtSFX);
        }

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Hurt();
        ApplyHitStun();

        Debug.Log($"{name} took {damage} damage. HP: {currentHealth}");

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void InitializeHealth()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    public virtual void Hurt()
    {
        Debug.Log($"{name} hurt");
    }

    public virtual void Die()
    {

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(deathSFX);
        }

        Destroy(gameObject);
    }

    public virtual void Jump()
    {
        Debug.Log($"{name} jumps");
    }


    public virtual void Interact()
    {
        Debug.Log($"{name} interacts");
    }

    // future shared hook for invincible mode
    // protected bool isInvincible;
    // public virtual void SetInvincible(bool value) => isInvincible = value;

    //------------------HELPERS------------------



    protected virtual void ApplyHitStun()
    {
        canMove = false;
        Invoke(nameof(EndHitStun), hurtStunDuration);
    }

    protected virtual void EndHitStun()
    {
        canMove = true;
    }

    protected virtual void HandleFootsteps()
    {
        if (moveDirection == Vector3.zero || !canMove)
        {
            footstepTimer = 0f;
            return;
        }

        footstepTimer += Time.fixedDeltaTime;

        if (footstepTimer >= footstepInterval)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(footstepSFX);
            }

            footstepTimer = 0f;
        }
    }


    //------------------DEBUG------------------


    private void OnDrawGizmosSelected()
    {
        Vector3 attackOrigin = transform.position + Vector3.right * facingDirection * attackOffset;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOrigin, attackRadius);
    }
}
// ----- ActorController.cs END -----