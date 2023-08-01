using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public int maxHealth = 100; // Add max health for the player

    private int currentHealth; // Add current health for the player
    private Vector3 moveDirection;
    private bool jumpRequest;
    private bool attackRequest;
    private bool defensiveRequest;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentHealth = maxHealth; // Initialize current health to max health
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleAttackInput();
        HandleDefensiveInput();
    }

    private void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized * moveSpeed;
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequest = true;
        }
    }

    private void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Z)) // Z for attack
        {
            attackRequest = true;
        }
    }

    private void HandleDefensiveInput()
    {
        if (Input.GetKeyDown(KeyCode.X)) // X for defense
        {
            defensiveRequest = true;
        }
    }

    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    public bool GetJumpRequest()
    {
        bool jump = jumpRequest;
        jumpRequest = false; // reset jump request
        return jump;
    }

    public bool GetAttackRequest()
    {
        bool attack = attackRequest;
        attackRequest = false; // reset attack request
        return attack;
    }

    public bool GetDefensiveRequest()
    {
        bool defense = defensiveRequest;
        defensiveRequest = false; // reset defense request
        return defense;
    }

    // Added a method for taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Added a method for handling player death
    private void Die()
    {
        // Add logic here for what should happen when the player dies.
        // For example, you could load a game over screen or reset the level.
    }
}
