using System.Collections.Generic;
using UnityEngine;

public class Scarlett_ComboManager : MonoBehaviour
{
    private List<string> combos;

    private void Start()
    {
        combos = new List<string>()
        {
            "wwas",
            "ddsw",
            "wads",
            "wadd",
            "wwww"
            // Add more combos as needed
        };
    }

    public bool CheckCombo(string sequence)
    {
        // Check if the sequence matches any of the combos
        foreach (string combo in combos)
        {
            if (sequence == combo)
            {
                return true;
            }
        }
        return false;
    }
}