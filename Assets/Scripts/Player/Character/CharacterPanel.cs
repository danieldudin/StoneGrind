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

    public Player player { get; set; }

    // Stats
    private List<TextMeshProUGUI> playerStatTexts = new List<TextMeshProUGUI>();
    [SerializeField] private GameObject playerStatPrefab;
    [SerializeField] private GameObject playerStatPanel;

    void Awake() {
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnStatsChanged += UpdateStats;
        UIEventHandler.OnItemEquipped += EquipWeapon;
        UIEventHandler.OnPlayerLevelChange += UpdateLevel;

        health = transform.Find("Health_Number").GetComponent<TextMeshProUGUI>();
        level = transform.Find("Level_Number").GetComponent<TextMeshProUGUI>();
        healthFill = transform.Find("Health_Progress_Fill").GetComponent<Image>();
        levelFill = transform.Find("Level_Progress_Fill").GetComponent<Image>();

        // Stats

        playerStatPrefab = Resources.Load<GameObject>("UI/Character/Stat");
        playerStatPanel = transform.Find("Stats").gameObject;
    }

    void UpdateHealth(int currentHealth, int maxHealth) {
        this.health.text = currentHealth.ToString();
        this.healthFill.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void InitializeStats()
    {
        for(int i = 0; i < player.characterStats.stats.Count; i++)
        {
            playerStatTexts.Add(Instantiate(playerStatPrefab).GetComponent<TextMeshProUGUI>());
            playerStatTexts[i].transform.SetParent(playerStatPanel.transform);
        }
        UpdateStats(player.characterStats);
    }

    void UpdateStats(CharacterStats characterStats)
    {
        for (int i = 0; i < characterStats.stats.Count; i++)
        {
            playerStatTexts[i].text = characterStats.stats[i].StatName + ": " + characterStats.stats[i].GetCalculatedStatValue().ToString();
        }
    }

    void EquipWeapon(Item item) {
        Debug.Log("Weapon equipped from char panel");
    }

    void UpdateEquippedWeapon() {

    }

    void UpdateLevel() {

    }
}
