using UnityEngine;

public class AIInput : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float detectionRange = 10f; // The range within which the AI can detect the player
    public float attackRange = 2f; // The range within which the AI can attack the player

    private PlayerInput playerInput;
    private CharacterControl characterControl;

    private float movementDirection = 1f; // Positive for right, negative for left

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterControl = GetComponent<CharacterControl>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Flip direction when hitting a wall or edge
        if (!characterControl.IsJumping && Physics2D.Raycast(transform.position, transform.right * movementDirection, 1f))
        {
            movementDirection *= -1f;
        }

        // Randomly jump sometimes
        if (Random.value < 0.005f) // Adjust this value to make the AI jump more or less frequently (0.01f = 1% chance)
        {
            playerInput.SetJumpRequest(true);
        }

        // If player is in detection range, move towards player
        if (distanceToPlayer < detectionRange)
        {
            movementDirection = Mathf.Sign(player.position.x - transform.position.x);
        }

        // Set the movement input based on the current direction
        playerInput.SetMoveDirection(new Vector3(movementDirection, 0f, 0f)); 

        // If player is in attack range, attack towards player
        if (distanceToPlayer < attackRange)
        {
            if (player.position.y > transform.position.y + 1f)
            {
                playerInput.SetAttackDirection("w");
            }
            else if (player.position.y < transform.position.y - 1f)
            {
                playerInput.SetAttackDirection("s");
            }
            else
            {
                playerInput.SetAttackDirection(player.position.x > transform.position.x ? "d" : "a");
            }
        }
    }
}
