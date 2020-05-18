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
        inventoryPanel = (RectTransform)this.transform;
        scrollViewContent = (RectTransform)this.transform.GetChild(0).GetChild(0).GetChild(0);

        itemContainer = Resources.Load<InventoryUIItem>("UI/Item_Container");
        Debug.Log("Loaded" + itemContainer);
        UIEventHandler.OnItemAddedToInventory += ItemAdded;
        inventoryPanel.gameObject.SetActive(false);
    }

    public void ItemAdded(Item item) {
        Debug.Log("Item to instantiate is" + item.ItemName);
        InventoryUIItem emptyItem = Instantiate(itemContainer);
        emptyItem.SetItem(item);
        emptyItem.transform.SetParent(scrollViewContent);
    }
}
