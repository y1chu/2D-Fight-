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

    public void SelectCharacterForPlayer(GameObject character)
    {
        if (player1Selection == null)
        {
            player1Selection = character;
            Debug.Log("Player 1 selected: " + character.name);
            player2Selection = ChooseAICharacter();
            Debug.Log("AI selected: " + player2Selection.name);
            CheckCharacterSelection();
        }
    }

    private GameObject ChooseAICharacter()
    {
        foreach (GameObject option in characterOptions)
        {
            if (option != player1Selection)
            {
                return option;
            }
        }

        return null;
    }

    void CheckCharacterSelection()
    {
        if (player1Selection != null && player2Selection != null)
        {
            gameController.StartFight(player1Selection, player2Selection);
        }
    }
}