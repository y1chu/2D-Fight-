using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int attackDamage = 40;
    public float attackRange = 1f;
    public float attackRate = 2f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private float nextAttackTime = 0f;

    public void InitiateAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack()
    {
        // Play an attack animation here if any

        // Detect enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach(Collider enemy in hitEnemies)
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
}