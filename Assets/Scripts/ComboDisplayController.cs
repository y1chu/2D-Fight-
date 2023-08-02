using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboDisplayController : MonoBehaviour
{
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI lastComboText;
    public TMP_FontAsset fontAsset;
    public AttackController attackController;

    private void Start()
    {
        comboText.font = fontAsset;
        lastComboText.font = fontAsset;
    }

    void Update()
    {
        comboText.text = "Combo: " + attackController.CurrentCombo;
        lastComboText.text = "Last Combo: " + attackController.LastSuccessfulCombo;
    }
}