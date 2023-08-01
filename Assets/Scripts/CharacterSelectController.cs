using UnityEngine;

public class CharacterSelectController : MonoBehaviour
{
    public GameObject[] characterOptions;

    private GameObject player1Selection;
    private GameObject player2Selection;
    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public bool SelectCharacter(GameObject character)
    {
        if (player1Selection == null)
        {
            player1Selection = character;
            Debug.Log("Player 1 selected: " + character.name);
            player2Selection = ChooseAICharacter();
            Debug.Log("AI selected: " + player2Selection.name);
            CheckCharacterSelection();
            return true;
        }

        return false;
    }

    private GameObject ChooseAICharacter()
    {
        // Here you could add logic to choose the AI character.
        // For example, it could be random, or always the same,
        // or based on some other criteria.
        // For now, let's just choose the first available character
        // that isn't the same as player1Selection.

        foreach (GameObject option in characterOptions)
        {
            if (option != player1Selection)
            {
                return option;
            }
        }

        return null;
    }


    public void DeselectCharacter(int playerNumber)
    {
        if (playerNumber == 1 && player1Selection != null)
        {
            Debug.Log("Player 1 deselected: " + player1Selection.name);
            player1Selection = null;
        }
        else if (playerNumber == 2 && player2Selection != null)
        {
            Debug.Log("Player 2 deselected: " + player2Selection.name);
            player2Selection = null;
        }
    }

    void CheckCharacterSelection()
    {
        if (player1Selection != null && player2Selection != null)
        {
            // Both players have made their selections. Notify GameController to initiate the fight.
            gameController.StartFight(player1Selection, player2Selection);
        }
    }
}