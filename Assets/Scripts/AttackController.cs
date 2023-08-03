using UnityEngine;
using System.Collections.Generic;

public class AttackController : MonoBehaviour
{
    private Animator animator;
    public int attackDamage = 40;
    public float attackRange = 1f;
    public float attackRate = 2f;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    private string characterName;
    private string inputSequence = "";
    private string lastSuccessfulCombo;
    private IComboManager comboManager;

    // New: Direction-to-Animation Mapping
    private Dictionary<string, string> directionToAnimation = new Dictionary<string, string> 
    {
        { "w", "UpSlash" },
        { "a", "SideSlash" },
        { "s", "DownSlash" }// Update as needed
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
        inputSequence += direction;

        /*if (inputSequence.Length == 4)
        {
            if (comboManager.CheckCombo(inputSequence)) 
            {
                Debug.Log(characterName + " Combo Detected " + inputSequence);
                LastSuccessfulCombo = inputSequence;
                inputSequence = ""; 
                // SpecialAttack(inputSequence);
                return;
            }
            else
            {
                inputSequence = inputSequence.Substring(inputSequence.Length - 3, 3);
            }
        }*/

        if (Time.time >= nextAttackTime)
        {
            Attack(direction);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void SpecialAttack(string combo)
    {
        string specialAttackAnimation = characterName + "_Special_" + combo;
        animator.Play(specialAttackAnimation);

        int specialAttackDamage = attackDamage * 2;

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(specialAttackDamage);
        }
    }

    void Attack(string direction)
    {
        string attackAnimation = characterName + "_" + directionToAnimation[direction];
        Debug.Log(attackAnimation);
        animator.Play(attackAnimation);

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

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
        private set { lastSuccessfulCombo = value; } 
    }
}
