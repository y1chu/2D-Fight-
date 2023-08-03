using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 50f;
    public string characterName;

    private PlayerInput playerInput;
    private Rigidbody2D rigidbody2D;
    private AttackController attackController;
    private AudioController audioController;
    private Animator animator;
    private bool isJumping;
    
    // Timer for the defensive maneuver
    private float defendTimer = 0f;
    // Duration for the defensive maneuver (can be adjusted)
    public float defendDuration = 3f;

    private int jumpCounter = 0;

    private void Start()
    {
        characterName = gameObject.name;
        animator = GetComponent<Animator>();
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
        
        if (defendTimer > 0)
        {
            defendTimer -= Time.deltaTime;
            if (defendTimer <= 0)
            {
                playerInput.isDefending = false; // Reset defending status
            }
        }
        
    }

    private void HandleMovement()
    {
        if (!isJumping) // Only allow movement if not jumping
        {
            Vector3 moveDirection = playerInput.GetMoveDirection();
            rigidbody2D.velocity = new Vector2(moveDirection.x * moveSpeed, rigidbody2D.velocity.y);
        }
        
    }

    private void HandleJump()
    {
        if (playerInput.GetJumpRequest() && jumpCounter == 0)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCounter++;
            IsJumping = true; // Player is jumping
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCounter = 0;
            IsJumping = false; // Player is not jumping
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
            animator.Play(characterName + "_Defense"); 
            PerformDefensiveManeuver();
            audioController.PlayDefendSound();
        }
    }

    private void PerformDefensiveManeuver()
    {
        playerInput.isDefending = true; // Set defending status to true
        defendTimer = defendDuration; // Set the defense timer
    }
    
    public bool IsJumping { get; private set; }
}