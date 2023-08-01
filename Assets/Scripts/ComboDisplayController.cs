using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboDisplayController : MonoBehaviour
{
    public TextMeshProUGUI comboText;
    public TMP_FontAsset fontAsset;
    public AttackController attackController;

    private void Start()
    {
        comboText.font = fontAsset;
    }

    void Update()
    {
        comboText.text = "Combo: " + attackController.CurrentCombo;
    }
}