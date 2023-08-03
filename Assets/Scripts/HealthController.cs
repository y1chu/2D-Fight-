/*using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 100;
    public Image healthBar; // UI element for health bar

    private int currentHealth;
    private GameController gameController;

    void Start()
    {
        currentHealth = maxHealth;
        gameController = FindObjectOfType<GameController>();
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Update()
    {
        if (healthBar != null)
        {
            // Update the health bar's value to match the current health
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth); // Ensure health doesn't go negative

        if (currentHealth <= 0)
        {
            KnockoutCharacter();
        }
    }

    void KnockoutCharacter()
    {
        // Communicate with the GameController that the character has been knocked out.
        if (gameController != null)
        {
            gameController.CharacterKnockedOut(this);
        }
        else
        {
            Debug.LogError("No GameController found in the scene.");
        }
    }
}*/


using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Required for using Coroutines

public class HealthController : MonoBehaviour
{
    public int maxHealth = 100;
    public Image healthBar; // UI element for health bar

    private int currentHealth;
    private GameController gameController;

    void Start()
    {
        currentHealth = maxHealth;
        gameController = FindObjectOfType<GameController>();
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }

        // Start the test Coroutine
        StartCoroutine(TestHealthDecrease());
    }

    void Update()
    {
        if (healthBar != null)
        {
            // Update the health bar's value to match the current health
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth); // Ensure health doesn't go negative

        if (currentHealth <= 0)
        {
            KnockoutCharacter();
        }
    }

    void KnockoutCharacter()
    {
        // Communicate with the GameController that the character has been knocked out.
        if (gameController != null)
        {
            gameController.CharacterKnockedOut(this);
        }
        else
        {
            Debug.LogError("No GameController found in the scene.");
        }
    }

    // Test Coroutine
    IEnumerator TestHealthDecrease()
    {
        while (currentHealth > 0)
        {
            TakeDamage(1); // Decrease health by 1
            yield return new WaitForSeconds(1f); // Wait for 1 second
        }
    }
}
