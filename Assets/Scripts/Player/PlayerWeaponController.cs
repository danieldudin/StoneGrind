using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }
    public Animator playerAnimator;

    Item currentlyEquippedItem;
    Transform spawnProjectile;
    IWeapon equippedWeapon;
    CharacterStats characterStats;

    void Awake() {
        playerAnimator = GetComponent<Animator>();
        characterStats = GetComponent<Player>().characterStats;

        spawnProjectile = transform.Find("ProjectileSpawn");
    }

    public void EquipWeapon(Item itemToEquip) {
        if (EquippedWeapon != null)
        {
            InventoryController.Instance.GiveItem(currentlyEquippedItem.ObjectSlug);

            characterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);
            Destroy(playerHand.transform.GetChild(0).gameObject);
        }

        EquippedWeapon = Instantiate((GameObject)Resources.Load("Prefabs/Items/Weapons/" + itemToEquip.ObjectSlug), playerHand.transform.position, playerHand.transform.rotation);

        equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();

        if (EquippedWeapon.GetComponent<IProjectileWeapon>() != null) {
            EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;
        }

        EquippedWeapon.transform.SetParent(playerHand.transform);

        currentlyEquippedItem = itemToEquip;

        equippedWeapon.Stats = itemToEquip.Stats;
        characterStats.AddStatBonus(itemToEquip.Stats);

        UIEventHandler.ItemEquipped(itemToEquip);

        Debug.Log("After weapon equip, stats now are" + characterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.X)) {
            PerformWeaponAttack();
        }
    }
    public void PerformWeaponAttack() {
        EquippedWeapon.GetComponent<IWeapon>().PerformAttack(CalculateDamage());
    }

    private int CalculateDamage() {
        int damageToDeal = characterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue();
        damageToDeal += CalculateCrit(damageToDeal);

        Debug.Log("Damage dealt" + damageToDeal);

        return damageToDeal;
    }

    private int CalculateCrit(int damage) {
        if (Random.value <= .10f) {
            int critDamage = (int)(damage * Random.Range(.5f, .75f));
            Debug.Log("Critical hit");
            return critDamage;
        }

        return 0;
    }
}
