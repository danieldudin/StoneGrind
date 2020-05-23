using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI health, level;
    [SerializeField]
    private Image healthFill, levelFill;
    [SerializeField]
    private Player player;

    void Awake() {
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnStatsChanged += UpdateStats;
        UIEventHandler.OnPlayerLevelChange += UpdateLevel;

        health = transform.Find("Health_Number").GetComponent<TextMeshProUGUI>();
        level = transform.Find("Level_Number").GetComponent<TextMeshProUGUI>();
        healthFill = transform.Find("Health_Progress_Fill").GetComponent<Image>();
        levelFill = transform.Find("Level_Progress_Fill").GetComponent<Image>();
    }

    void UpdateHealth(int currentHealth, int maxHealth) {
        this.health.text = currentHealth.ToString();
        this.healthFill.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    void UpdateStats() {

    }

    void UpdateEquippedWeapon() {

    }

    void UpdateLevel() {

    }
}
