using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 50f;

    private PlayerInput playerInput;
    private Rigidbody2D rigidbody2D;
    private AttackController attackController;
    private AudioController audioController;

    private int jumpCounter = 0;

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
        Vector3 moveDirection = playerInput.GetMoveDirection();
        rigidbody2D.velocity = new Vector2(moveDirection.x * moveSpeed, rigidbody2D.velocity.y);
    }

    private void HandleJump()
    {
        if (playerInput.GetJumpRequest() && jumpCounter == 0)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCounter++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCounter = 0;
        }
    }

    private void HandleAttack()
    {
        string attackDirection = playerInput.GetAttackDirection();
        if (attackDirection != null)
        {
            attackController.InitiateAttack(attackDirection);
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