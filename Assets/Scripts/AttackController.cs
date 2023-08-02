using UnityEngine;

public class AttackController : MonoBehaviour
{
    private Animator animator;
    public int attackDamage = 40;
    public float attackRange = 1f;
    public float attackRate = 2f;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    private Scarlett_ComboManager scarlettComboManager;
    private string inputSequence = "";
    private string lastSuccessfulCombo; // This line was missing

    private float nextAttackTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        scarlettComboManager = GetComponent<Scarlett_ComboManager>();
    }

    public void InitiateAttack(string direction)
    {
        inputSequence += direction;

        if (inputSequence.Length == 4)
        {
            if (scarlettComboManager.CheckCombo(inputSequence)) // Check combo with the next input
            {
                Debug.Log("Scarlett Combo Detected " + inputSequence);
                LastSuccessfulCombo = inputSequence;
                inputSequence = ""; // Reset the input sequence after performing a combo
                return; // Return here to skip the normal attack
            }
            else
            {
                // If no combo was detected, keep the latest 3 inputs
                inputSequence = inputSequence.Substring(inputSequence.Length - 3, 3);
            }
        }

        // If no combo was detected, perform the normal attack
        if (Time.time >= nextAttackTime)
        {
            Attack(direction);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }


    void SpecialAttack(string combo)
    {
        Vector3 attackOffset = Vector3.zero;
        string specialAttackAnimation = "";

        switch (combo)
        {
            case "wwww":
                Debug.Log("Scarlett Special Attack: 4x Up Slash");
                attackOffset = new Vector3(0, 1, 0); // Update as needed
                specialAttackAnimation = "Scarlett_Special_UpSlash";
                break;
            case "wwas":
                attackOffset = new Vector3(0, -1, 0); // Update as needed
                specialAttackAnimation = "Scarlett_Special_DownSlash";
                break;
            case "wsaw":
                attackOffset = new Vector3(-1, 0, 0); // Update as needed
                specialAttackAnimation = "Scarlett_Special_SideSlash";
                break;
        }

        animator.Play(specialAttackAnimation);

        // Use a stronger attack for special attacks
        int specialAttackDamage = attackDamage * 2;

        // Detect enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position + attackOffset, attackRange, enemyLayers);

        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(specialAttackDamage);
        }
    }

    void Attack(string direction)
    {
        // Play an attack animation here if any

        // Modify the attack point based on direction
        Vector3 attackOffset = Vector3.zero;

        switch (direction)
        {
            case "w":
                attackOffset = new Vector3(0, 1, 0);
                animator.Play("Scarlett_UpSlash");
                break;
            case "s":
                attackOffset = new Vector3(0, -1, 0);
                animator.Play("Scarlett_DownSlash");
                break;
            case "a":
                attackOffset = new Vector3(-1, 0, 0);
                animator.Play("Scarlett_SideSlash");
                break;
            case "d":
                attackOffset = new Vector3(1, 0, 0);
                // animator.Play("Scarlett_Defense");
                break;
        }

        // Detect enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position + attackOffset, attackRange, enemyLayers);

        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    // Draw the attack range in editor for easy tweaking
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public string CurrentCombo
    {
        get { return inputSequence; }
    }

    public string LastSuccessfulCombo
    {
        get { return lastSuccessfulCombo ?? ""; }
        private set { lastSuccessfulCombo = value; } // This setter should be private to control changes to it
    }
}