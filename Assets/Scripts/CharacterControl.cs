using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = 9.81f;
    public float jumpForce = 5f;

    private PlayerInput playerInput;
    private CharacterController characterController;
    private AttackController attackController;
    private Vector3 velocity;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        attackController = GetComponent<AttackController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
        HandleDefensiveManeuver();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = playerInput.GetMoveDirection();
        moveDirection.y += velocity.y;
        characterController.Move(moveDirection * Time.deltaTime);

        // Apply gravity
        if (characterController.isGrounded)
        {
            velocity.y = -2f; // small value to ground the player
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }
    }


    private void HandleJump()
    {
        if (playerInput.GetJumpRequest() && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }
    }

    private void HandleAttack()
    {
        if (playerInput.GetAttackRequest())
        {
            attackController.InitiateAttack();
        }
    }

    private void HandleDefensiveManeuver()
    {
        if (playerInput.GetDefensiveRequest())
        {
            PerformDefensiveManeuver();
        }
    }

    private void PerformDefensiveManeuver()
    {
        // Implement defensive maneuvers here
    }
}