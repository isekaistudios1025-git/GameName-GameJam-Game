// ----- ActorController.cs START -----
using UnityEngine;

public abstract class ActorController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 6f;

    [Header("Combat")]
    [SerializeField] protected float attackRadius = 1f;
    [SerializeField] protected float attackOffset = 1f;
    [SerializeField] protected int attackDamage = 1;


    [Header("Health")]
    [SerializeField] protected int maxHealth = 3;

    protected int currentHealth;

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
    }

    protected virtual void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public virtual void Move(Vector3 direction)
    {
        moveDirection = direction.normalized;

        // update facing without flipping root collider
        if (direction.x != 0)
        {
            facingDirection = Mathf.Sign(direction.x);
        }
    }

    public virtual void Attack()
    {
        Debug.Log($"{name} attacks");

        Vector3 attackOrigin = transform.position + Vector3.right * facingDirection * attackOffset;

        Collider[] hits = Physics.OverlapSphere(attackOrigin, attackRadius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject) continue;

            ActorController other = hit.GetComponent<ActorController>();

            if (other != null)
            {
                other.TakeDamage(attackDamage);
                Debug.Log($"{name} hit {other.name} for {attackDamage}");
            }
        }
    }
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log($"{name} took {damage} damage. HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        Debug.Log($"{name} died");

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

    private void OnDrawGizmosSelected()
    {
        Vector3 attackOrigin = transform.position + Vector3.right * facingDirection * attackOffset;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOrigin, attackRadius);
    }
}
// ----- ActorController.cs END -----