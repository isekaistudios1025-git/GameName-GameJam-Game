
// ----- ActorController.cs START -----
using UnityEngine;

public abstract class ActorController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 6f;

    protected Rigidbody rb;
    protected Vector3 moveDirection;
    //protected bool isInvincible;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.freezeRotation = true;
    }

    protected virtual void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public virtual void Move(Vector3 direction)
    {
        moveDirection = direction.normalized;

        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1f, 1f);
        }
    }

    public virtual void Attack()
    {
        Debug.Log($"{name} attacks");

        Vector3 attackOrigin = transform.position + transform.right * transform.localScale.x;
        float attackRadius = 1f;

        Collider[] hits = Physics.OverlapSphere(attackOrigin, attackRadius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject != gameObject)
            {
                Debug.Log($"{name} hit {hit.name}");
            }
        }
    }

    // I will pop this in next, but I want to get the basic movement and attack working first, then we can add the jump and interact functionality as needed
    public virtual void Jump()
    {
        Debug.Log($"{name} jumps");
    }

    public virtual void Interact()
    {
        Debug.Log($"{name} interacts");
    }


    //future hook for the invincable mode (we can use this in both enemies and players)

    //public virtual void SetInvincible(bool value)
    //{
    //    isInvincible = value;
    //}


    //--------------  HELPERS  --------------------------
    //---------------------------------------------------




    //this is strictly for debugging purposes, to visualize the attack range in the editor
    private void OnDrawGizmosSelected()
    {
        Vector3 attackOrigin = transform.position + transform.right * transform.localScale.x;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOrigin, 1f);
    }



}

// ----- ActorController.cs END -----