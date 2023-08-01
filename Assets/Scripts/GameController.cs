using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<HealthController> charactersInFight = new List<HealthController>();
    private UIController uiController;

    void Awake()
    {
        uiController = FindObjectOfType<UIController>();
    }

    public void StartFight(GameObject player1Character, GameObject player2Character)
    {
        charactersInFight.Add(player1Character.GetComponent<HealthController>());
        charactersInFight.Add(player2Character.GetComponent<HealthController>());
    }

    public void CharacterKnockedOut(HealthController character)
    {
        charactersInFight.Remove(character);
        DetermineWinner();
    }

    void DetermineWinner()
    {
        if (charactersInFight.Count == 1)
        {
            HealthController winner = charactersInFight[0];
            uiController.ShowWinner(winner);
        }
        else if (charactersInFight.Count == 0)
        {
            uiController.ShowGameOverScreen();
        }
    }
}