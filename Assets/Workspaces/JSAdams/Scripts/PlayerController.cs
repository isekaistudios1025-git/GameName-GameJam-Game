// ----- PlayerController.cs  START -----
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : ActorController
{
    private PlayerControls controls;
    private Vector2 moveInput;

    protected override void Awake()
    {
        base.Awake();
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
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

        if (controls.Player.Attack.triggered)
        {
            Attack();
        }
    }
}


// --- PlayerController.cs  END -----