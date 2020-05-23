using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIDetails : MonoBehaviour
{
    Item item;
    Button selectedItemButton, itemInteractButton;
    TextMeshProUGUI itemNameText, itemDescriptionText, itemInteractButtonText, statText;

    void Start() {
        itemNameText = transform.Find("Item_Name").GetComponent<TextMeshProUGUI>();
        itemDescriptionText = transform.Find("Item_Description").GetComponent<TextMeshProUGUI>();
        itemInteractButton = transform.Find("Item_Interact").GetComponent<Button>();
        itemInteractButtonText = itemInteractButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        statText = transform.Find("Item_Stats").GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    public void SetItem(Item item, Button selectedButton) {
        gameObject.SetActive(true);
        statText.text = "";

        if (item.Stats != null) {
            foreach(BaseStat stat in item.Stats) {
                statText.text += stat.StatName + ": " + stat.BaseValue + "\n";
            }
        }

        itemInteractButton.onClick.RemoveAllListeners();

        this.item = item;
        selectedItemButton = selectedButton;
        itemNameText.text = item.ItemName;
        itemDescriptionText.text = item.Description;
        itemInteractButtonText.text = item.ActionName;
        itemInteractButton.onClick.AddListener(OnItemInteract);
    }

    public void OnItemInteract() {
        if (item.ItemType == Item.ItemTypes.Consumable) {
            InventoryController.Instance.ConsumeItem(item);
            Destroy(selectedItemButton.gameObject);
        } else if (item.ItemType == Item.ItemTypes.Weapon) {
            InventoryController.Instance.EquipItem(item);
            Destroy(selectedItemButton.gameObject);
        }
        item = null;
        gameObject.SetActive(false);
    }
}
