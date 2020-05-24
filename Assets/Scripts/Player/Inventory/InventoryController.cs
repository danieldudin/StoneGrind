using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; set; }
    public PlayerWeaponController playerWeaponController;
    public ConsumableController consumableController;
    public InventoryUIDetails inventoryDetailsPanel;
    public List<Item> playerItems = new List<Item>();
    public GameObject inventoryPanel;
    public GameObject inventoryUIController;
    public GameObject characterPanel;
    public GameObject userInterface;

    bool menuIsActive { get; set; }
    bool characterPanelIsActive { get; set; }

    void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        userInterface = this.transform.Find("UserInterface").gameObject;

        inventoryUIController = Instantiate(Resources.Load<GameObject>("UI/Inventory"));
        inventoryUIController.transform.SetParent(userInterface.transform);

        inventoryPanel = Instantiate(Resources.Load<GameObject>("UI/Inventory_Panel"));
        inventoryPanel.transform.SetParent(userInterface.transform, false);

        // TODO: Move this to its own file
        characterPanel = Instantiate(Resources.Load<GameObject>("UI/Character/Character_Panel"));
        characterPanel.transform.SetParent(userInterface.transform, false);
        characterPanel.SetActive(false);
        characterPanel.GetComponent<CharacterPanel>().player = GetComponent<Player>();
        characterPanel.GetComponent<CharacterPanel>().InitializeStats();
        // END TODO

        playerWeaponController = GetComponent<PlayerWeaponController>();
        consumableController = GetComponent<ConsumableController>();

        inventoryDetailsPanel = inventoryPanel.transform.GetChild(1).GetComponent<InventoryUIDetails>();

        GiveItem("sword");
        GiveItem("staff");
        GiveItem("potion_log");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            Debug.Log("Toggling inventory");
            menuIsActive = !menuIsActive;
            inventoryPanel.gameObject.SetActive(menuIsActive);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            Debug.Log("Toggling character panel");
            characterPanelIsActive = !characterPanelIsActive;
            characterPanel.gameObject.SetActive(characterPanelIsActive);
        }
    }

    public void GiveItem(string itemSlug) {
        Item item = ItemDatabase.Instance.GetItem(itemSlug);

        playerItems.Add(item);

        Debug.Log(playerItems.Count + "items in inventory. Added " + itemSlug);

        UIEventHandler.ItemAddedToInventory(item);
    }

    public void SetItemDetails(Item item, Button selectedButton) {
        inventoryDetailsPanel.SetItem(item, selectedButton);
    }

    public void EquipItem(Item itemToEquip) {
        playerWeaponController.EquipWeapon(itemToEquip);
    }

    public void ConsumeItem(Item itemToConsume) {
        consumableController.ConsumeItem(itemToConsume);
    }
}
