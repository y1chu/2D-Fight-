using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Animator animator;
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    public string characterName;
    public bool isAI;

    public bool isDefending = false;
    private int currentHealth;
    private Vector3 moveDirection;
    private bool jumpRequest;
    private string attackDirection;
    private bool defensiveRequest;
    private CharacterController characterController;
    private CharacterControl characterControl;

    private void Start()
    {
        characterName = gameObject.name;
        characterController = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        characterControl = GetComponent<CharacterControl>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleAttackInput();
        HandleDefensiveInput();
    }

    /*private void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized * moveSpeed;
    }*/

    private void HandleMovementInput()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow) && !characterControl.IsJumping)
        {
            horizontalInput = -1f; // Move Left

            if (isAI)
            {
                animator.Play(characterName + "_Run");
            }

            else
            {
                animator.Play(characterName + "_Idle");
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !characterControl.IsJumping)
        {
            horizontalInput = 1f; // Move Right

            if (isAI)
            {
                animator.Play(characterName + "_Idle");
            }

            else
            {
                animator.Play(characterName + "_Run");
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            verticalInput = 1f; // Move Up
            animator.Play(characterName + "_Jump");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            verticalInput = -1f; // Move Down
            animator.Play(characterName + "_Crouch");
        }

        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized * moveSpeed;
    }


    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.Play(characterName + "_Jump");
            jumpRequest = true;
        }
    }

    private void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) // W for up attack
        {
            attackDirection = "w";
        }
        else if (Input.GetKeyDown(KeyCode.A)) // A for left attack
        {
            attackDirection = "a";
        }
        else if (Input.GetKeyDown(KeyCode.S)) // S for down attack
        {
            attackDirection = "s";
        }
    }


    private void HandleDefensiveInput()
    {
        if (Input.GetKeyDown(KeyCode.D)) // D for defense
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
        if (jumpRequest)
        {
            // Debug.Log("Jump requested");
            jumpRequest = false; // reset jump request
            return true;
        }

        return false;
    }

    public string GetAttackDirection()
    {
        string direction = attackDirection;
        attackDirection = null; // reset attack direction
        return direction;
    }

    public bool GetDefensiveRequest()
    {
        bool defense = defensiveRequest;
        defensiveRequest = false; // reset defense request
        return defense;
    }


    public void TakeDamage(int damage)
    {
        if (isDefending)
        {
            // Decrease damage intake by 90%
            currentHealth -= damage / 10;
        }
        else
        {
            currentHealth -= damage;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetJumpRequest(bool jumpRequest)
    {
        this.jumpRequest = jumpRequest;
    }

    public void SetMoveDirection(Vector3 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    public void SetAttackDirection(string attackDirection)
    {
        this.attackDirection = attackDirection;
    }

    private void Die()
    {
        // Add logic here for what should happen when the player dies.
        // For example, you could load a game over screen or reset the level.
    }
}