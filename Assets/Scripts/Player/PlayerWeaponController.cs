using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }
    public Animator playerAnimator;
    Transform spawnProjectile;

    IWeapon equippedWeapon;

    CharacterStats characterStats;
    void Start() {
        playerAnimator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        spawnProjectile = transform.Find("ProjectileSpawn");
    }

    public void EquipWeapon(Item itemToEquip) {

        if (EquippedWeapon != null)
        {
            characterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);
            Destroy(playerHand.transform.GetChild(0).gameObject);
        }

        EquippedWeapon = Instantiate((GameObject)Resources.Load("Prefabs/Items/Weapons/" + itemToEquip.ObjectSlug), playerHand.transform.position, playerHand.transform.rotation);

        equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();

        if (EquippedWeapon.GetComponent<IProjectileWeapon>() != null) {
            EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;
        }

        equippedWeapon.Stats = itemToEquip.Stats;
        EquippedWeapon.transform.SetParent(playerHand.transform);

        characterStats.AddStatBonus(itemToEquip.Stats);

        Debug.Log(equippedWeapon.Stats[0].GetCalculatedStatValue());

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.X)) {
            PerformWeaponAttack();
        }
    }
    public void PerformWeaponAttack() {
        EquippedWeapon.GetComponent<IWeapon>().PerformAttack();
    }
}
