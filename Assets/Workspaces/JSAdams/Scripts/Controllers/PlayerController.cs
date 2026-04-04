// ----- PlayerController.cs START -----
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : ActorController
{
    private PlayerControls controls;
    private Vector2 moveInput;

    private PlayerState playerState;
    private Vector3 spawnPosition;

    private Transform visuals;
    private Animator animator;

    private bool zoneLocked = false;
    private float leftLimit;
    private float rightLimit;

    [SerializeField] private float damageBoostDuration = 5f;
    private int baseAttackDamage;
    private bool damageBoostActive = false;
    private Coroutine damageBoostCoroutine;

    protected override void Awake()
    {
        base.Awake();

        baseAttackDamage = attackDamage;

        controls = new PlayerControls();

        visuals = transform.Find("Visuals");
        animator = visuals.GetComponent<Animator>();


        playerState = GetComponent<PlayerState>();
        spawnPosition = transform.position;
    }

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new PlayerControls();
        }

        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        moveInput = controls.Player.Move.ReadValue<Vector2>();

        if (!isAttacking)
        {
            Move(new Vector3(moveInput.x, 0f, moveInput.y));
        }
        else
        {
            Move(Vector3.zero);
        }

        // animate walk / idle
        animator.SetBool("IsMoving", moveDirection != Vector3.zero);

        // flip only visuals
        if (moveInput.x != 0 && !isAttacking)
        {
            visuals.localScale = new Vector3(Mathf.Sign(moveInput.x), 1f, 1f);
        }

        // zone locking to keep player within certain bounds during cutscenes or events
        if (zoneLocked)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, leftLimit, rightLimit);
            transform.position = pos;
        }

        if (controls.Player.Attack.triggered && canAttack && !isAttacking)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(AttackRoutine());
        }
    }

    public override void Hurt()
    {
        base.Hurt();
        animator.SetTrigger("Hurt");
    }

    public override void Die()
    {
        canMove = false;
        animator.SetTrigger("Die");

        Debug.Log($"{name} player death");

        Invoke(nameof(HandleDeathComplete), 0.4f);
    }
    private void HandleDeathComplete()
    {
        if (playerState != null)
        {
            playerState.LoseLife();
        }

        if (playerState != null && playerState.CurrentLives > 0)
        {
            Respawn();
        }
        else
        {
            GameOver();
        }
    }
    private void Respawn()
    {
        currentHealth = maxHealth;
        InitializeHealth();

        rb.position = spawnPosition;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        canMove = true;
        moveDirection = Vector3.zero;

        animator.ResetTrigger("Die");
        animator.Play("Player_Idle");
    }
    private void GameOver()
    {
        Debug.Log("GAME OVER");
        FindFirstObjectByType<GameOverMenu>()?.Show();
    }

    //road block methods for locking player in certain zones during cutscenes or events
    public void LockZone(float leftX, float rightX)
    {
        zoneLocked = true;
        leftLimit = Mathf.Min(leftX, rightX);
        rightLimit = Mathf.Max(leftX, rightX);
    }

    public void UnlockZone()
    {
        zoneLocked = false;
    }

    //pickup mechanics
    public void FullHeal()
    {
        currentHealth = maxHealth;
        InitializeHealth();

        Debug.Log("Player healed to full health.");
    }

    public void ApplyDamageBoost(int bonusDamage)
    {
        if (damageBoostCoroutine != null)
        {
            StopCoroutine(damageBoostCoroutine);
        }

        damageBoostCoroutine = StartCoroutine(DamageBoostRoutine(bonusDamage));
    }

    private System.Collections.IEnumerator DamageBoostRoutine(int bonusDamage)
    {
        damageBoostActive = true;
        attackDamage = baseAttackDamage + bonusDamage;

        Debug.Log("Damage boost applied.");

        yield return new WaitForSeconds(damageBoostDuration);

        attackDamage = baseAttackDamage;
        damageBoostActive = false;
        damageBoostCoroutine = null;

        Debug.Log("Damage boost ended.");
    }
}
// ----- PlayerController.cs END -----