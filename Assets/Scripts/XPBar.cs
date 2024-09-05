using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    private Slider xpBarSlider;

    private void OnEnable()
    {
        xpBarSlider = GetComponentInChildren<Slider>();
        Player.playerXPReport += UpdateBar;
    }

    private void UpdateBar(int xp, int maxXP)
    {
        xpBarSlider.maxValue = maxXP;
        xpBarSlider.value = xp;
    }


}
