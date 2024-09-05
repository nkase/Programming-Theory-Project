using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    private Slider xpBarSlider;
    private TextMeshProUGUI levelText;

    private void OnEnable()
    {
        xpBarSlider = GetComponentInChildren<Slider>();
        levelText = GetComponentInChildren<TextMeshProUGUI>();
        Player.playerXPReport += UpdateBar;
    }

    private void UpdateBar(int xp, int maxXP, int level)
    {
        xpBarSlider.maxValue = maxXP;
        xpBarSlider.value = xp;
        levelText.text = "LVL: " + level;
    }


}
