using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public List<BaseStat> Stats { get; set; }
    public Animator swordAnimator;
    public int CurrentDamage { get; set; }

    void Start() {
        swordAnimator = GetComponent<Animator>();
    }

    public void PerformAttack(int damage)
    {
        CurrentDamage = damage;

        swordAnimator.SetTrigger("Base_Attack");
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Enemy") {
            Debug.Log("Hit: " + collider.name);

            collider.GetComponent<IEnemy>().TakeDamage(CurrentDamage);
        }
    }
}
