// ----- PlayerController.cs START -----
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : ActorController
{
    private PlayerControls controls;
    private Vector2 moveInput;

    private Transform visuals;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();

        controls = new PlayerControls();

        visuals = transform.Find("Visuals");
        animator = visuals.GetComponent<Animator>();
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

        Move(new Vector3(moveInput.x, 0f, moveInput.y));

        // animate walk / idle
        animator.SetBool("IsMoving", moveInput != Vector2.zero);

        // flip only visuals
        if (moveInput.x != 0)
        {
            visuals.localScale = new Vector3(Mathf.Sign(moveInput.x), 1f, 1f);
        }

        if (controls.Player.Attack.triggered)
        {
            Attack();

            animator.SetTrigger("Attack");
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

        Invoke(nameof(OnDeathAnimationFinished), 0.4f);
    }
    public void OnDeathAnimationFinished()
    {
        Debug.Log("GAME OVER");
        gameObject.SetActive(false);
    }


}
// ----- PlayerController.cs END -----