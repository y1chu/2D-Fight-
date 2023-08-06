/*
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

        float chancePerFrame = 0.005f / (5f * 60f);
        // Randomly jump sometimes
        if (Random.value < chancePerFrame)
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
*/

using UnityEngine;

public class AIInput : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float patrolSpeed = 0.5f; // Speed at which AI patrols
    public float reactionDelay = 0.5f; // Delay before AI takes a decision

    private PlayerInput playerInput;
    private CharacterControl characterControl;

    private float movementDirection = 1f;
    private float lastDecisionTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterControl = GetComponent<CharacterControl>();
        lastDecisionTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - lastDecisionTime < reactionDelay)
        {
            return; // Wait for reaction delay
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Flip direction when hitting a wall or edge
        if (!characterControl.IsJumping && Physics2D.Raycast(transform.position, transform.right * movementDirection, 1f))
        {
            movementDirection *= -1f;
        }

        float chancePerFrame = 0.005f / (5f * 60f);
        // Randomly jump sometimes
        if (Random.value < chancePerFrame)
        {
            playerInput.SetJumpRequest(true);
        }

        // If player is in detection range, pursue player aggressively
        if (distanceToPlayer < detectionRange)
        {
            movementDirection = Mathf.Sign(player.position.x - transform.position.x);
        }
        else // Patrol when the player is not detected
        {
            movementDirection *= patrolSpeed;
        }

        playerInput.SetMoveDirection(new Vector3(movementDirection, 0f, 0f)); 

        // If player is in attack range, attack or feint attack
        if (distanceToPlayer < attackRange)
        {
            float chanceToFeint = 0.2f;
            if (Random.value < chanceToFeint)
            {
                // Feint attack: Do not really attack, just simulate it to confuse player
                // This can be a simple animation or any other visual cue without causing damage
            }
            else
            {
                DecideAttackDirection();
            }
        }

        lastDecisionTime = Time.time; // Reset last decision time
    }

    private void DecideAttackDirection()
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
