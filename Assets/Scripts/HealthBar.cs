using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthBarSlider;

    private void OnEnable()
    {
        Player.playerHealthReport += UpdateBar;
        healthBarSlider = GetComponentInChildren<Slider>();
    }

    private void UpdateBar(int health, int maxHealth)
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = health;
    }
}
