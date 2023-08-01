using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int attackDamage = 20;
    public float attackRate = 1f;
    public float attackRange = 2f;
    public Transform attackPoint;
    public LayerMask playerLayers;

    private int currentHealth;
    private float nextAttackTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Defeat();
        }
    }

    void Defeat()
    {
        // Implement defeat logic here, such as playing a death animation or disabling the enemy GameObject.
    }

    void Attack()
    {
        // Detect players in range
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayers);

        // Damage them
        foreach(Collider player in hitPlayers)
        {
            player.GetComponent<PlayerInput>().TakeDamage(attackDamage);
        }
    }


    // Draw the attack range in editor for easy tweaking
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}