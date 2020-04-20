using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }

    IWeapon equippedWeapon;
    private PhotonView PV;

    CharacterStats characterStats;
    void Start() 
    {
        PV = GetComponent<PhotonView>();
        characterStats = GetComponent<CharacterStats>();
    }

    public void EquipWeapon(Item itemToEquip) 
    {
        if (PV.IsMine)
        {
            if (EquippedWeapon != null)
            {
                characterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);
                Destroy(playerHand.transform.GetChild(0).gameObject);
            }

            EquippedWeapon = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Items/Weapons", itemToEquip.ObjectSlug), playerHand.transform.position, playerHand.transform.rotation, 0);

            equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
            equippedWeapon.Stats = itemToEquip.Stats;

            EquippedWeapon.transform.SetParent(playerHand.transform);

            characterStats.AddStatBonus(itemToEquip.Stats);

            Debug.Log(equippedWeapon.Stats[0].GetCalculatedStatValue());
        }
    }

    public void PerformWeaponAttack() 
    {
        EquippedWeapon.GetComponent<IWeapon>().PerformAttack();
    }
}
