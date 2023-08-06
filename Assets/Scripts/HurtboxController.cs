using UnityEngine;

public class HurtboxController : MonoBehaviour
{
    public HealthController linkedHealthController;

    private void OnTriggerEnter(Collider other)
    {
        // Detect if this hurtbox is hit by an active attack hitbox
        if (other.CompareTag("AttackHitbox"))
        {
            // Call the TakeDamage method on the linked HealthController
            linkedHealthController.TakeDamage(other.GetComponent<AttackController>().attackDamage);
        }
    }
}