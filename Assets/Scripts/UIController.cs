using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI gameOverText;
    public Dictionary<HealthController, Slider> healthBars = new Dictionary<HealthController, Slider>();

    public void UpdateHealthBar(HealthController character, float currentHealth)
    {
        if (healthBars.TryGetValue(character, out Slider healthBar))
        {
            healthBar.value = currentHealth;
        }
    }

    public void ShowWinner(HealthController winner)
    {
        winnerText.text = "Winner: " + winner.gameObject.name;
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        gameOverText.text = "Game Over: All characters knocked out";
    }
}