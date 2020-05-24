using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public RectTransform inventoryPanel;
    public RectTransform scrollViewContent;
    InventoryUIItem itemContainer { get; set; }

    Item currentSelectedItem { get; set; }

    void Awake()
    {
        UIEventHandler.OnItemAddedToInventory += ItemAdded;

        inventoryPanel = (RectTransform)this.transform;
        scrollViewContent = (RectTransform)this.transform.GetChild(0).GetChild(0).GetChild(0);
        itemContainer = Resources.Load<InventoryUIItem>("UI/Item_Container");

        inventoryPanel.gameObject.SetActive(false);
    }

    public void ItemAdded(Item item) {
        InventoryUIItem emptyItem = Instantiate(itemContainer);
        emptyItem.SetItem(item);
        emptyItem.transform.SetParent(scrollViewContent);
    }
}
