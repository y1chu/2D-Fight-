using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 50f;

    private PlayerInput playerInput;
    private Rigidbody2D rigidbody2D;
    private AttackController attackController;
    private AudioController audioController;

    private int jumpCounter = 0; // New counter for tracking jumps

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        attackController = GetComponent<AttackController>();
        audioController = FindObjectOfType<AudioController>();
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
        Vector2 moveDirection = playerInput.GetMoveDirection();
        rigidbody2D.velocity = new Vector2(moveDirection.x * moveSpeed, rigidbody2D.velocity.y);
    }

    private void HandleJump()
    {
        // Only allow jump if jumpCounter is 0
        if (playerInput.GetJumpRequest() && jumpCounter == 0)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCounter++; // Increment jumpCounter
        }
    }

    // Reset jumpCounter when character touches ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCounter = 0;
        }
    }

    private void HandleAttack()
    {
        if (playerInput.GetAttackRequest())
        {
            attackController.InitiateAttack();
            audioController.PlayAttackSound();
        }
    }

    private void HandleDefensiveManeuver()
    {
        if (playerInput.GetDefensiveRequest())
        {
            PerformDefensiveManeuver();
            audioController.PlayDefendSound();
        }
    }

    private void PerformDefensiveManeuver()
    {
        // Implement defensive maneuvers here
    }
}
