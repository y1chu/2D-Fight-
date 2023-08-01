using UnityEngine;

public class AttackController : MonoBehaviour
{
    private Animator animator;
    public int attackDamage = 40;
    public float attackRange = 1f;
    public float attackRate = 2f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private float nextAttackTime = 0f;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void InitiateAttack(string direction)
    {
        if (Time.time >= nextAttackTime)
        {
            Attack(direction);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack(string direction)
    {
        // Play an attack animation here if any

        // Modify the attack point based on direction
        Vector3 attackOffset = Vector3.zero;

        switch (direction)
        {
            case "up":
                attackOffset = new Vector3(0, 1, 0);
                animator.Play("Scarlett_UpSlash");  
                break;
            case "down":
                attackOffset = new Vector3(0, -1, 0);
                animator.Play("Scarlett_DownSlash");
                break;
            case "left":
                attackOffset = new Vector3(-1, 0, 0);
                animator.Play("Scarlett_SideSlash");
                break;
            case "right":
                attackOffset = new Vector3(1, 0, 0);
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
}