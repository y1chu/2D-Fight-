using UnityEngine;
using System.Collections.Generic;

public class AttackController : MonoBehaviour
{
    private Animator animator;
    public int attackDamage = 40;
    public float attackRange = 1f;
    public float attackRate = 2f;
    public List<Transform> attackHitboxes; // Multiple hitboxes
    public LayerMask enemyLayers;
    private string characterName;
    private string inputSequence = "";
    private string lastSuccessfulCombo;
    private IComboManager comboManager;
    private bool isSpecialAttack = false; // flag to differentiate between normal and special attacks
    private List<HealthController> hitDuringThisAttack = new List<HealthController>();

    // Direction-to-Animation Mapping
    private Dictionary<string, string> directionToAnimation = new Dictionary<string, string>
    {
        { "w", "UpSlash" },
        { "a", "SideSlash" },
        { "s", "DownSlash" }
    };

    private float nextAttackTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterName = gameObject.name;
        comboManager = GetComponent<IComboManager>();
    }

    public void InitiateAttack(string direction)
    {
        Debug.Log($"Initiating attack in direction {direction}");

        inputSequence += direction;
        isSpecialAttack = false; // reset

        if (inputSequence.Length == 4)
        {
            if (comboManager.CheckCombo(inputSequence))
            {
                Debug.Log(characterName + " Combo Detected " + inputSequence);
                LastSuccessfulCombo = inputSequence;
                SpecialAttack(inputSequence);
                inputSequence = "";
                return;
            }
            else
            {
                inputSequence = inputSequence.Substring(inputSequence.Length - 3, 3);
            }
        }

        if (Time.time >= nextAttackTime)
        {
            Attack(direction);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void SpecialAttack(string combo)
    {
        string specialAttackAnimation = characterName + "_Special_" + combo;
        Debug.Log(specialAttackAnimation);
        animator.Play(specialAttackAnimation);
        isSpecialAttack = true; // set the flag
    }

    void Attack(string direction)
    {
        string attackAnimation = characterName + "_" + directionToAnimation[direction];
        Debug.Log(attackAnimation);
        animator.Play(attackAnimation);
    }

    public void ApplyDamage()
    {
        Debug.Log("Applying Damage");

        int damageToApply = isSpecialAttack ? attackDamage * 2 : attackDamage;

        foreach (Transform hitbox in attackHitboxes)
        {
            Collider2D[] hitHurtboxes = Physics2D.OverlapCircleAll(hitbox.position, attackRange, enemyLayers);
            Debug.Log($"Checking hitbox at {hitbox.position} found {hitHurtboxes.Length} potential hurtboxes.");

            foreach (Collider2D hurtbox in hitHurtboxes)
            {
                HurtboxController hurtboxController = hurtbox.GetComponent<HurtboxController>();
            
                if (hurtboxController != null)
                {
                    HealthController enemyHealth = hurtboxController.linkedHealthController;

                    // Check if we've already hit this enemy during this attack
                    if (!hitDuringThisAttack.Contains(enemyHealth))
                    {
                        hitDuringThisAttack.Add(enemyHealth); // Add to the list to ensure we don't double-hit
                        Debug.Log("Hurtbox found! Applying damage.");
                        enemyHealth.TakeDamage(damageToApply);
                    }
                }
                else
                {
                    Debug.Log("Hurtbox object detected but no HurtboxController component attached.");
                }
            }
        }
    
        hitDuringThisAttack.Clear(); // Clear the list after applying damage
    }
    void OnDrawGizmosSelected()
    {
        if (attackHitboxes.Count == 0)
            return;

        foreach (Transform hitbox in attackHitboxes)
        {
            Gizmos.DrawWireSphere(hitbox.position, attackRange);
        }
    }

    public string CurrentCombo
    {
        get { return inputSequence; }
    }

    public string LastSuccessfulCombo
    {
        get { return lastSuccessfulCombo ?? ""; }
        private set { lastSuccessfulCombo = value; }
    }

    // Enable all hitboxes
    public void EnableHitboxes()
    {
        Debug.Log("Enabling Hitboxes");
        foreach (Transform hitbox in attackHitboxes)
        {
            hitbox.gameObject.SetActive(true);
        }
    }

    public void DisableHitboxes()
    {
        Debug.Log("Disabling Hitboxes");
        foreach (Transform hitbox in attackHitboxes)
        {
            hitbox.gameObject.SetActive(false);
        }

        hitDuringThisAttack.Clear();
    }

    public void ActivateHurtbox()
    {
        Debug.Log("Activating Hurtbox");
        GetComponent<Collider>().enabled = true;
    }

    public void DeactivateHurtbox()
    {
        Debug.Log("Deactivating Hurtbox");
        GetComponent<Collider>().enabled = false;
    }
}