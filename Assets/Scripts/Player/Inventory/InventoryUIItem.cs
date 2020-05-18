using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIItem : MonoBehaviour {
    public Item item;

    public void SetItem(Item item) {
        this.item = item;

        Debug.Log("Setting item:" + item.ItemName);
        SetupItemValues();
    }

    void SetupItemValues() {
        this.transform.Find("Item_Name").GetComponent<TextMeshProUGUI>().text = item.ItemName;
    }

    public void OnSelectItemButton() {
        InventoryController.Instance.SetItemDetails(item, GetComponent<Button>());
    }
}
