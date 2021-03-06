﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler : MonoBehaviour
{
    public delegate void ItemEventHandler(Item item);
    public static event ItemEventHandler OnItemAddedToInventory;
    public static event ItemEventHandler OnItemEquipped;

    public delegate void PlayerHealthEventHandler(int currentHealth, int maxHealth);
    public static event PlayerHealthEventHandler OnPlayerHealthChanged;

    public delegate void StatsEventHandler(CharacterStats characterStats);
    public static event StatsEventHandler OnStatsChanged;

    public delegate void PlayerLevelEventHandler(PlayerLevel playerLevel);
    public static event PlayerLevelEventHandler OnPlayerLevelChange;

    public static void ItemAddedToInventory(Item item) {
        OnItemAddedToInventory(item);
    }

    public static void ItemEquipped(Item item) {
        if (OnItemEquipped != null) {
            OnItemEquipped(item);
        }
    }

    public static void HealthChanged(int currentHealth, int maxHealth) {
        if (OnPlayerHealthChanged != null) {
            OnPlayerHealthChanged(currentHealth, maxHealth);
        }
    }

    public static void StatsChanged(CharacterStats characterStats) {
        if (OnStatsChanged != null) {
            OnStatsChanged(characterStats);
        }
    }

    public static void PlayerLevelChanged(PlayerLevel playerLevel) {
        if (OnPlayerLevelChange != null) {
            OnPlayerLevelChange(playerLevel);
        }
    }
}
