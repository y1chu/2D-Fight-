using System.Collections.Generic;
using UnityEngine;

public class Garland_ComboManager : MonoBehaviour, IComboManager
{
    private List<string> combos;

    private void Start()
    {
        combos = new List<string>
        {
            /*"wwas",
            "wwww",
            "wsaw",
            "ssss"*/
            // Add more combos as needed
        };
    }

    public bool CheckCombo(string sequence)
    {
        // Check if the sequence matches any of the combos
        foreach (var combo in combos)
            if (sequence == combo)
                return true;
        return false;
    }
}