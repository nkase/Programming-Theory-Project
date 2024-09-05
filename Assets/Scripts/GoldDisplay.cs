using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldDisplay : MonoBehaviour
{
    private TextMeshProUGUI goldText;
    private void OnEnable()
    {
        goldText = GetComponentInChildren<TextMeshProUGUI>();
        Player.playerGoldReport += UpdateDisplay;
    }

    private void UpdateDisplay(int gold)
    {
        goldText.text = "Gold: " + gold;
    }
}
